using UsersManagerAPI.DomainClasses.Models;

namespace UsersManagerAPI.IServices
{
    public interface IAuthenticateUser
    {
        UserInfo AuthenticateAsync(string UserName, string Password);
    }
}
