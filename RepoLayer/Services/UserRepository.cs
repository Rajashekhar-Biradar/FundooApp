using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class UserRepository : IUserRepository
    {
        DBContext dBContext;
        private readonly IConfiguration config;

        public UserRepository(DBContext dBContext, IConfiguration config)
        {
            this.dBContext = dBContext;
            this.config = config;
        }

        public UserEntity RegisterUser(UserPostModel user)  //adding user details 
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.UserID = new UserEntity().UserID;
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                userEntity.EmailID = user.EmailID;
                userEntity.Password = Encrypt(user.Password);              
                dBContext.userTable.Add(userEntity);
                dBContext.SaveChanges();
                return userEntity;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string user_Login(User_LoginModel loginModel)  // login using credentials
        {
            try
            {
                
               var user_details = this.dBContext.userTable.FirstOrDefault(x => x.EmailID == loginModel.EmailID && x.Password == Encrypt(loginModel.Password));
                if (user_details != null)
                {
                    var token = this.GenerateToken(user_details.EmailID, user_details.UserID);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GenerateToken(string EmailID, long userID)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config[("Jwt:Key")]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, EmailID),
                        new Claim("userID", userID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Encrypt(string passWord)
        {
            byte[] b = Encoding.UTF8.GetBytes(passWord);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
        public string Decrypt(string passWord)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(passWord);
                decrypted = Encoding.UTF8.GetString(b);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return decrypted;
        }
        public bool Reset_PassWord(string Email, string Password,string ConfirmPassword)
        {
            try
            {
                if (Password.Equals(ConfirmPassword))
                {
                    var user = this.dBContext.userTable.Where(x => x.EmailID == Email).FirstOrDefault();
                    user.Password = Encrypt(ConfirmPassword);
                    dBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Forgot_Password(string email)
        {
            try
            {
                var result = this.dBContext.userTable.FirstOrDefault(x => x.EmailID == email);
                if (result != null)
                {

                    var token = this.GenerateToken(result.EmailID, result.UserID);
                    MSMQ mssq= new MSMQ();
                    mssq.SendMessage(token, result.EmailID, result.FirstName);
                    return token.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
