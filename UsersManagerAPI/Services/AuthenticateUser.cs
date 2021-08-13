using System.Threading.Tasks;
using System.Security.Cryptography;
using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;
using System.Text;

namespace UsersManagerAPI.Services
{
    public class AuthenticateUser : IAuthenticateUser
    {
        public IUsersCRUD UsersCRUD { get; }

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
                string HashedPassword = ComputeSha256Hash(userInfo.SaltKey + Password);
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
        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
