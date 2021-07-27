using System.Threading.Tasks;
using UsersManagerAPI.IServices;

namespace UsersManagerAPI.Services
{
    public class UserInfoHandler : IUserInfoHandler
    {
        async public Task<bool> ValidateAsync(string UserName, string Password)
        {
            return true;
        }
    }
}
