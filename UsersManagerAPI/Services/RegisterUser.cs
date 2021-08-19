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

        async public Task<IUserInfo> SignUp(IUserInfo RegisterInfo)
        {
            int length;
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
                    length = rndSaltKey.Length;
                    RegisterInfo.IsMailConfirmed = false;
                    RegisterInfo.HashPassword = Hashingservices.ComputeSha256Hash(rndSaltKey + RegisterInfo.Password);
                    length = RegisterInfo.HashPassword.Length;
                    RegisterInfo = await UsersCURD.AddUserAsync(RegisterInfo);
                    await ConfirmMail.ConfirmEmail(RegisterInfo);
                    RegisterInfo.Message = Message.Success;
                }
            }
            catch
            {
                RegisterInfo.Message = Message.ErrorFound;
                RegisterInfo.DetailedMessage = RegisterInfo.DetailedMessage + "\nError in SignUp method in RegisterUser Class";
            }
            return RegisterInfo;
        }
    }
}
