using System;
using System.Threading.Tasks;
using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;
using UsersManagerAPI.IServices;
using UsersManagerAPI.SecurityServices;
using UsersManagerAPI.Services.IServices;

namespace UsersManagerAPI.Services
{
    public class RegisterUser : IRegisterUser
    {
        private IUsersCRUD UsersCURD { get; set; }
        private IConfirmMail ConfirmMail { get; set; }
        Hashing Hashingservices;
        SaltKey SaltKey;

        public RegisterUser(
            IUsersCRUD usersCURD,
            IConfirmMail ConfirmMail)
        {          
            SaltKey = new SaltKey();
            Hashingservices = new Hashing();
            UsersCURD = usersCURD;
            this.ConfirmMail = ConfirmMail;
        }

        async public Task<IUserInfo> SignUp(IUserInfo userInfo)
        {
            try
            {
                if (!string.IsNullOrEmpty(userInfo.Email))
                {
                    String rndSaltKey = SaltKey.GetSalt(24);
                    userInfo.CreatedDate = DateTime.Now;
                    userInfo.UpdatedDate = DateTime.Now;
                    userInfo.AccountPricingPlan = "Free";
                    userInfo.AccountType = "Lone_Wolf";
                    userInfo.SaltKey = rndSaltKey;
                    userInfo.IsMailConfirmed = false;
                    userInfo.HashPassword = Hashingservices.ComputeSha256Hash(rndSaltKey + userInfo.Password);
                    userInfo = await UsersCURD.AddUserAsync(userInfo);
                    if (userInfo.Message == Message.Success)
                    {
                        userInfo = await ConfirmMail.ConfirmEmail(userInfo);
                    }
                }
            }
            catch
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = userInfo.DetailedMessage + " - Error in SignUp method in RegisterUser Class";
            }
            return userInfo;
        }
    }
}
