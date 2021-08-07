using System.Threading.Tasks;
using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;

namespace UsersManagerAPI.Services
{
    public class UserInfoHandler : IUserInfoHandler
    {
        public IUsersCRUD UsersCRUD { get; }

        public UserInfoHandler(IUsersCRUD usersCRUD)
        {
            UsersCRUD = usersCRUD;
        }

        async public Task<UserInfo> AuthenticateAsync(string UserName, string Password)
        {
            UserInfo userInfo = await ValidateCredentials(UserName, Password);
            if (!string.IsNullOrWhiteSpace(UserName) &&
                !string.IsNullOrWhiteSpace(Password) &&
                userInfo.Message == Message.Success)               
                return userInfo;
            else
            {
                userInfo.Message = Message.InvalidUser;
                return userInfo;
            }
        }
        async private Task<UserInfo> ValidateCredentials(string UserName, string Password)
        {
            UserInfo userInfo = UsersCRUD.GetUser(UserName);
            await check DB(UserName, Password);
            return userInfo;
        }
    }
}
