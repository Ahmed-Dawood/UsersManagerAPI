using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;

namespace UsersManagerAPI.Services
{
    public class Register : IRegister
    {
        private IUsersCRUD UsersCURD { get; set; }
        UserInfo userInfo;

        public Register(IUsersCRUD usersCURD)
        {
            userInfo = new UserInfo();
            UsersCURD = usersCURD;
        }

        async public Task<string> ConfirmMail(string UserName)
        {
            MailService mailService = new MailService();

            MailClass mail = new MailClass()
            {
                Body = mailService.GetMailBody(UserName),
                IsBodyHtml = true,
                Subject = "Hi From Far",
                ToMailIds = new List<string>() {"akdawood97@gmail.com"}
            };

            string msg = await mailService.SendMail(mail);
            return msg;
        }
        async public Task<UserInfo> SignUp(RegisterInfo RegisterInfo)
        {
            try
            {
                if (!string.IsNullOrEmpty(RegisterInfo.Email))
                {
                    userInfo = UsersCURD.GetUser(RegisterInfo.Email);
                    if (userInfo.Message == Message.UserAlreadyExist)
                    {

                    }
                }
                bool UserExistFlag = await CheckRecordExistence(RegisterInfo.Email); 
                if(!UserExistFlag)
                {

                }
            }
            catch (Exception ex)
            {
                userInfo.Message = ex.Message;
            }
            return userInfo;
        }
        async Task<bool> CheckRecordExistence(string UserEmail)
        {
            return true;
        }
    }
}
