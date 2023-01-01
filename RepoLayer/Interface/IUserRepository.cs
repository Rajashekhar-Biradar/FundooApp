using CommonLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface IUserRepository
    {
        public UserEntity RegisterUser(UserPostModel user);
        public string user_Login(User_LoginModel loginModel);
        public bool Reset_PassWord(string Email, string Password, string ConfirmPassword);

        public string Forgot_Password(string email);

    }
}
