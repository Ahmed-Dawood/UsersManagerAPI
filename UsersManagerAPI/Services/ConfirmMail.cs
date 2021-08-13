using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.Services.IServices;

namespace UsersManagerAPI.Services
{
    public class ConfirmMail : IConfirmMail
    {
        async public Task<string> ConfirmEmail(string UserName)
        {
            MailService mailService = new MailService();

            MailClass mail = new MailClass()
            {
                Body = mailService.GetMailBody(UserName),
                IsBodyHtml = true,
                Subject = "Hi From Far",
                ToMailIds = new List<string>() { "akdawood97@gmail.com" }
            };

            string msg = await mailService.SendMail(mail);
            return msg;
        }
    }
}
