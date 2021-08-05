using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;
using UsersManagerAPI.Services.IServices;

namespace UsersManagerAPI.Services
{
    public class MailService : IMailService
    {
        public string GetMailBody(string username)//UserInfo loginInfo)
        {
            string url = Global.DomainName + "api/Authentication/ConfirmMail?username=\"" + username + "\"";

            return string.Format(@"<div style='text-align:center;'>
                                    <h1>Welcome to our Web Site</h1>
                                    <h3>Click below button for verify your Email Id</h3>
                                    <form method='post' action='{0}' style='display: inline;'>
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
                                </div>", url);
        }

        async public Task<string> SendMail(MailClass mailClass)
        {
            //WIP
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(mailClass.FromMailId);
                    //mailClass.ToMailIds.ForEach(x =>
                    //{
                    //    mail.To.Add(x);
                    //});
                    mail.To.Add("akdawood97@gmail.com");
                    mail.Subject = mailClass.Subject;
                    mail.Body = mailClass.Body;
                    mail.IsBodyHtml = mailClass.IsBodyHtml;
                    if (mailClass.Attachments != null)
                    {
                        mailClass.Attachments.ForEach(x =>
                        {
                            mail.Attachments.Add(new Attachment(x));
                        });
                    }
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                    {
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(mailClass.FromMailId, mailClass.FromMailPassword);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                        return Message.MailSent;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message; ;
            }
        }
    }
}
