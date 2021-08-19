using System;
using System.Threading.Tasks;
using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;
using UsersManagerAPI.IServices;
using UsersManagerAPI.SecurityServices;

namespace UsersManagerAPI.Services
{
    public class RegisterUser : IRegisterUser
    {
        private IUsersCRUD UsersCURD { get; set; }
        Hashing Hashingservices;
        SaltKey SaltKey;

        public RegisterUser(
            IUsersCRUD usersCURD)
        {          
            SaltKey = new SaltKey();
            Hashingservices = new Hashing();
            UsersCURD = usersCURD;
        }

        async public Task<IUserInfo> SignUp(IUserInfo RegisterInfo)
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
                    RegisterInfo = await UsersCURD.AddUserAsync(RegisterInfo);  
                }
            }
            catch
            {
                RegisterInfo.Message = Message.ErrorFound;
                RegisterInfo.DetailedMessage = "Error in SignUp method in RegisterUser Class";
            }
            return RegisterInfo;
        }
    }
}
