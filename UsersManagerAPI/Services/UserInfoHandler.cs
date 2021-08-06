using System.Threading.Tasks;
using UsersManagerAPI.IServices;

namespace UsersManagerAPI.Services
{
    public class UserInfoHandler : IUserInfoHandler
    {
        async public Task<bool> AuthenticateAsync(string UserName, string Password)
        {
            if (!string.IsNullOrWhiteSpace(UserName) &&
                !string.IsNullOrWhiteSpace(Password) &&
                await //check DB(UserName, Password))
                return true;
            else
                return false;
        }
    }
}
