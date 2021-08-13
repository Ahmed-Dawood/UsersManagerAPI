using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;

namespace UsersManagerAPI.Services
{
    public class RegisterUser : IRegisterUser
    {
        private IUsersCRUD UsersCURD { get; set; }
        UserInfo userInfo;

        public RegisterUser(IUsersCRUD usersCURD)
        {
            userInfo = new UserInfo();
            UsersCURD = usersCURD;
        }

        async public Task<UserInfo> SignUp(UserInfo RegisterInfo)
        {
            try
            {
                if (!string.IsNullOrEmpty(RegisterInfo.Email))
                {
                    userInfo = await UsersCURD.AddUserAsync(RegisterInfo);                    
                }
            }
            catch (Exception ex)
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = ex.InnerException.Message;
            }
            return userInfo;
        }
    }
}
