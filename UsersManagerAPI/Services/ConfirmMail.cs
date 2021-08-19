using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;
using UsersManagerAPI.Services.IServices;

namespace UsersManagerAPI.Services
{
    public class ConfirmMail : IConfirmMail
    {
        async public Task<IUserInfo> ConfirmEmail(IUserInfo userInfo)
        {
            MailService mailService = new MailService();

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
                userInfo.DetailedMessage = userInfo.DetailedMessage + "\nError in ConfirmEmail method in ConfirmMail Class";
            }

            return userInfo;
        }
    }
}
