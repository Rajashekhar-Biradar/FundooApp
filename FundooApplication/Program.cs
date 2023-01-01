using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using RepoLayer.Context;
using RepoLayer.Interface;
using RepoLayer.Services;
using System.Text;
using StackExchange.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    ////Host.CreateDefaultBuilder(args)
    ////                .ConfigureWebHostDefaults(webBuilder =>
    ////                {
    ////                    //                 webBuilder.UseStartup<Startup>();
    ////                }).ConfigureLogging(logging =>
    ////                {
    ////                    logging.ClearProviders();
    ////                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
    ////                }).UseNLog();

    
    builder.Services.AddControllers();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
        name: "AllowOrigin",
      builder => {
          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
      });
    });
    builder.Services.AddMemoryCache();  
    builder.Services.AddStackExchangeRedisCache(option =>
    {
        option.Configuration = "localhost:6379";
     });
    builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FundooDB")));
    //Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


    builder.Services.AddTransient<IUserRepository, UserRepository>();
    builder.Services.AddTransient<IUserBusiness, UserBusiness>();
    builder.Services.AddTransient<INoteRepository, NoteRepository>();
    builder.Services.AddTransient<INoteBusiness, NoteBusiness>();
    builder.Services.AddTransient<ILabelRepository, LabelRepository>();
    builder.Services.AddTransient<ILabelBusiness, LabelBusiness>();
    builder.Services.AddTransient<ICollabRepository, CollabRepository>();
    builder.Services.AddTransient<ICollabBusiness, CollabBusiness>();
    
  

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "FundooNotes", Version = "V1", Description = "WelCome to FundooNotes" });
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            Scheme = "bearer",
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "enter JWT Bearer token on textbox below!",

            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
        c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                 { jwtSecurityScheme, Array.Empty<string>() }
                    });
    });
    var tokenKey = builder.Configuration.GetValue<string>("Jwt:Key");
    var key = Encoding.ASCII.GetBytes(tokenKey);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


    //builder.Services.AddSwaggerGen(
    //    c =>
    //    {
    //        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API WSVAP (WebSmartVeiw)", Version = "v1" });
    //        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    //    });

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }



    app.UseAuthentication();

    app.UseHttpsRedirection();

    app.UseCors("AllowOrigin");

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    throw ex;
}
finally
{
    NLog.LogManager.Shutdown();
}
