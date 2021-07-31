using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;

namespace UsersManagerAPI.Services
{
    public class Register : IRegister
    {
        UserInfo _loginInfo = new UserInfo();
        async public Task<string> ConfirmMail(string UserName)
        {
            //WIP
            MailService mailService = new MailService();

            MailClass mail = new MailClass()
            {
                Body = mailService.GetMailBody(UserName),
                IsBodyHtml = true,
                Subject = "Hi From Far",
                ToMailIds = new List<string>() { new string("akdawood97@gmail.com") }
            };

            string msg = await mailService.SendMail(mail);
            return msg;
        }

        async public Task<UserInfo> SignUp(UserInfo loginInfo)
        {
            //WIP
            _loginInfo = new UserInfo();
            try
            {
                //LoginInfo loginInfo_ = await this.CheckRecordExistence(loginInfo);
            }
            catch (Exception ex)
            {
                _loginInfo.Messege = ex.Message;
            }
            return _loginInfo;
        }

        async public Task<bool> CheckRecordExistence(UserInfo loginInfo)
        {
            UserInfo loginInfo_ = new UserInfo();
            if (!string.IsNullOrEmpty(loginInfo.Email))
            {
                loginInfo_ = await GetLoginUser(loginInfo.Email);
            }
            return true;
        }
        async public Task<UserInfo> GetLoginUser(string userName)
        {
            //WIP
            //UserInfo loginInfo = new UserInfo();
            //using (IDbConnection connection = new SqlConnection(Global.ConnectionString))
            //{
            //    if (connection.State == ConnectionState.Closed) connection.Open();

            //    string SqlCommand = "";

            //    if (!string.IsNullOrEmpty(userName)) SqlCommand += " AND Username='" + userName + "'";

            //};
            return null;//loginInfo;
        }
    }
}
