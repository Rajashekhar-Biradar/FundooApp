using BusinessLayer.Interface;
using CommonLayer;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository userRepository;

        public UserBusiness(IUserRepository userRepository)
        { 
            this.userRepository = userRepository;
        }

        public UserEntity RegisterUser(UserPostModel user)
        {
            try
            {
                return userRepository.RegisterUser(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public string user_Login(User_LoginModel loginModel)
        {
            try
            {
                return userRepository.user_Login(loginModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Reset_PassWord(string Email, string Password, string ConfirmPassword)
        {
            try
            {
                return userRepository.Reset_PassWord(Email, Password,ConfirmPassword);
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
                return userRepository.Forgot_Password(email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
