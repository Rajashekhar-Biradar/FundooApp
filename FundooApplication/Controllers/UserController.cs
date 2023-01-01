using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        private readonly ILogger<UserController> logger;

        public UserController(IUserBusiness userBusiness, ILogger<UserController> logger)
        {
            this.userBusiness = userBusiness;
            this.logger = logger;
            logger.LogDebug(1, "NLog injected into HomeController");
        }

        [HttpPost("UserRegistration")]
        //[Route("UserRegistration")]   //use HttpPost or Route
        public IActionResult UserRegister(UserPostModel userPostModel)
        {
            this.logger.LogInformation("Registration of new user initialized");
            try
            {
                var response = this.userBusiness.RegisterUser(userPostModel);
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "registration successful", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "registration failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("User_login")]
        public IActionResult User_login(User_LoginModel login_model)
        {
            this.logger.LogInformation("Logged In");
            try
            {
                var response = this.userBusiness.user_Login(login_model);
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "Login successful",token      =response});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Login failed" });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("Forgot_Password")]
        public IActionResult Forgot_Password(string Email)
        {
            try
            {
                var response = this.userBusiness.Forgot_Password(Email);
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "Message sent to your MailID" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Email not registered" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("Reset_Password")]
        public IActionResult Reset_Password(string Password, string ConfirmPassword)
        {
            try
            {
                string Email = (User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email).Value);
                var response = this.userBusiness.Reset_PassWord(Email, Password, ConfirmPassword);
                if (response == true)
                {
                    return this.Ok(new { success = true, message = "Password has been reset successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Reset password failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
