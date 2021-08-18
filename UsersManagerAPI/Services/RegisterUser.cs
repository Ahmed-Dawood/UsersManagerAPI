using System;
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
        Hashing Hashingservices;
        SaltKey SaltKey;
        UserInfo userInfo;

        public RegisterUser(IUsersCRUD usersCURD)
        {
            userInfo = new UserInfo();
            SaltKey = new SaltKey();
            Hashingservices = new Hashing();
            UsersCURD = usersCURD;
        }

        async public Task<UserInfo> SignUp(UserInfo RegisterInfo)
        {
            try
            {
                if (!string.IsNullOrEmpty(RegisterInfo.Email))
                {
                    String rndSaltKey = SaltKey.GetSalt(24);
                    RegisterInfo.CreatedDate = DateTime.Now;
                    RegisterInfo.UpdatedDate = DateTime.Now;
                    RegisterInfo.AccountPricingPlan = "Free";
                    RegisterInfo.AccountType = "Lone_Wolf";
                    RegisterInfo.SaltKey = rndSaltKey;
                    RegisterInfo.IsMailConfirmed = false;
                    RegisterInfo.HashPassword = Hashingservices.ComputeSha256Hash(rndSaltKey + RegisterInfo.Password);
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
