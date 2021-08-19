using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;
using UsersManagerAPI.Services.IServices;

namespace UsersManagerAPI.Services
{
    public class MailService : IMailService
    {
        public string GetMailBody(IUserInfo userInfo)
        {
            string url = Global.DomainName + "api/Authentication/ConfirmMail?username=" + userInfo.UserName + "&password=" + userInfo.HashPassword;

            return string.Format(@"<div style='text-align:center;'>
                                    <h1>Hello {0} {1}.</h1>
                                    <h2>Welcome to our Website</h2>
                                    <h3>Click below button for verify your Email Address</h3>
                                    <form method='post' action='{2}' style='display: inline;'>
                                      <button type = 'submit' style=' display: block;
                                                                    text-align: center;
                                                                    font-weight: bold;
                                                                    background-color: #008CBA;
                                                                    font-size: 16px;
                                                                    border-radius: 10px;
                                                                    color:#ffffff;
                                                                    cursor:pointer;
                                                                    width:100%;
                                                                    padding:10px;'>
                                        Confirm Mail
                                      </button>
                                    </form>
                                </div>", userInfo.LastName, userInfo.FirstName[0] ,url);
        }

        async public Task<string> SendMailAsync(MailClass mailClass)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(mailClass.FromMailId);
                    mail.To.Add(mailClass.ToMailIds);
                    mail.Subject = mailClass.Subject;
                    mail.Body = mailClass.Body;
                    mail.IsBodyHtml = mailClass.IsBodyHtml;
                    //if (mailClass.Attachments != null)
                    //{
                    //    mailClass.Attachments.ForEach(x =>
                    //    {
                    //        mail.Attachments.Add(new Attachment(x));
                    //    });
                    //}
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                    {
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(mailClass.FromMailId, mailClass.FromMailPassword);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                        return Message.Success;
                    }
                }
            }
            catch
            {
                return Message.ErrorFound;
            }
        }
    }
}
