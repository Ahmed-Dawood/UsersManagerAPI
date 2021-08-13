using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;
using UsersManagerAPI.SecurityServices;

namespace UsersManagerAPI.Services
{
    public class AuthenticateUser : IAuthenticateUser
    {
        public IUsersCRUD UsersCRUD { get; }
        Hashing HashingService = new Hashing();

        public AuthenticateUser(IUsersCRUD usersCRUD)
        {
            UsersCRUD = usersCRUD;
        }

        public UserInfo AuthenticateAsync(string UserName, string Password)
        {
            UserInfo userInfo = ValidateCredentials(UserName, Password);
            if (userInfo.Message == Message.Success)               
                return userInfo;
            else
            {
                userInfo.Message = Message.InvalidUser;
                return userInfo;
            }
        }

        private UserInfo ValidateCredentials(string UserName, string Password)
        {
            UserInfo userInfo = new UserInfo();
            if (!string.IsNullOrWhiteSpace(UserName) &&
               !string.IsNullOrWhiteSpace(Password))
            {
                userInfo = UsersCRUD.GetUser(UserName);
                string HashedPassword = HashingService.ComputeSha256Hash(userInfo.SaltKey + Password);
                if (HashedPassword == userInfo.HashPassword)
                    userInfo.Message = Message.Success;
                else
                    userInfo.Message = Message.ErrorFound;
                return userInfo;
            }
            else
            {
                userInfo.Message = Message.InvalidUser;
                return userInfo;
            } 
        }
    }
}
