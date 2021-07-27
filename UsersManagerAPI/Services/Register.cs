using System;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;

namespace UsersManagerAPI.Services
{
    public class Register : IRegister
    {
        UserInfo _loginInfo = new UserInfo();
        public Task<string> ConfirmMail(string UserName)
        {
            throw new NotImplementedException();
        }

        async public Task<UserInfo> SignUp(UserInfo loginInfo)
        {
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
            if (!string.IsNullOrEmpty(loginInfo.UserName))
            {
                loginInfo_ = await this.GetLoginUser(loginInfo.UserName);
            }
            return true;
        }
        async public Task<UserInfo> GetLoginUser(string userName)
        {
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
