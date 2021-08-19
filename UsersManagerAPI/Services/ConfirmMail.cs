using System.Threading.Tasks;
using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;
using UsersManagerAPI.Services.IServices;

namespace UsersManagerAPI.Services
{
    public class ConfirmMail : IConfirmMail
    {
        private IUsersCRUD UsersCURD;
        private IMailService mailService;
        public ConfirmMail(
            IUsersCRUD UsersCURD,
            IMailService mailService)
        {
            this.UsersCURD = UsersCURD;
            this.mailService = mailService;
        }

        async public Task<IUserInfo> SendConfirmEmailAsync(IUserInfo userInfo)
        {
            MailClass mail = new MailClass()
            {
                Body = mailService.GetMailBody(userInfo),
                IsBodyHtml = true,
                Subject = "Email Address Confirmation",
                ToMailIds = userInfo.Email
            };
            userInfo.Message = await mailService.SendMailAsync(mail);
            if (userInfo.Message==Message.ErrorFound)
            {
                userInfo.DetailedMessage = userInfo.DetailedMessage + " - Error in ConfirmEmail method in ConfirmMail Class";
            }
            return userInfo;
        }

        async public Task<IUserInfo> UpdateConfirmMailAsync(IUserInfo userInfo)
        {
            userInfo = await UsersCURD.GetUserAsync(userInfo);
            if (userInfo.Message == Message.Success)
            {
                userInfo.IsMailConfirmed = true;
                userInfo = await UsersCURD.UpdateUserAsync(userInfo);                                
            }
            return userInfo;
        }
    }
}
