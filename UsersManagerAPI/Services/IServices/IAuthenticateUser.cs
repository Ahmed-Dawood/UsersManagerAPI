using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;

namespace UsersManagerAPI.IServices
{
    public interface IAuthenticateUser
    {
        IUserInfo AuthenticateAsync(IUserInfo userInfo);
    }
}
