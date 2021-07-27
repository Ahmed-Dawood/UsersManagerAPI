using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersManagerAPI.IServices;

namespace UsersManagerAPI.Services
{
    public class ValidateUsers : IValidateUsers
    {
        private readonly IUserInfoHandler UserInfoHandler;

        public ValidateUsers(IUserInfoHandler UserInfoHandler)
        {
            this.UserInfoHandler = UserInfoHandler;
        }
        async public Task<bool> AuthenticateAsync(string UserName, string Password)
        {
            if (!string.IsNullOrWhiteSpace(UserName) &&
                !string.IsNullOrWhiteSpace(Password) &&
                await UserInfoHandler.ValidateAsync(UserName, Password))
                return true;
            else
                return false;
        }
    }
}
