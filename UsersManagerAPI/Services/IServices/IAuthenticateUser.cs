using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models.IModels;

namespace UsersManagerAPI.IServices
{
    public interface IAuthenticateUser
    {
        Task<IUserInfo> AuthenticateAsync(IUserInfo userInfo);
    }
}
