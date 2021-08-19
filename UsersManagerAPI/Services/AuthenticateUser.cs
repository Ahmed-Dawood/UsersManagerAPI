using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;
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

        public IUserInfo AuthenticateAsync(IUserInfo userInfo)
        {
            
            if (!string.IsNullOrWhiteSpace(userInfo.UserName) &&
               !string.IsNullOrWhiteSpace(userInfo.Password))
            {
                string Password = userInfo.Password;
                userInfo = UsersCRUD.GetUser(userInfo);
                string HashedPassword = HashingService.ComputeSha256Hash(userInfo.SaltKey + Password);
                if (HashedPassword == userInfo.HashPassword)
                    userInfo.Message = Message.Success;
                else
                    userInfo.Message = Message.InvalidUser;
                    userInfo.DetailedMessage = userInfo.DetailedMessage + "\nError in AuthenticateAsync in AuthenticateUser Class";
                return userInfo;
            }
            else
            {
                userInfo.Message = Message.InvalidUser;
                userInfo.DetailedMessage = userInfo.DetailedMessage + "\nError in AuthenticateAsync in AuthenticateUser Class";
                return userInfo;
            }
        }
    }
}
