using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;
using UsersManagerAPI.SecurityServices;

namespace UsersManagerAPI.Services
{
    public class RegisterUser : IRegisterUser
    {
        private IUsersCRUD UsersCURD { get; set; }
        Hashing Hashingservices = new Hashing();
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
                    RegisterInfo.CreatedDate = DateTime.Now;
                    RegisterInfo.UpdatedDate = DateTime.Now;
                    RegisterInfo.AccountPricingPlan = "Free";
                    RegisterInfo.AccountType = "Lone Wolf";
                    RegisterInfo.SaltKey = "Hamada";
                    RegisterInfo.IsMailConfirmed = false;
                    RegisterInfo.Role = "Admin";
                    RegisterInfo.HashPassword = Hashingservices.ComputeSha256Hash("Hamada" + RegisterInfo.Password);
                    userInfo = await UsersCURD.AddUserAsync(RegisterInfo);                    
                }
            }
            catch
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = "Error in SignUp method in RegisterUser Class";
            }
            return userInfo;
        }
    }
}
