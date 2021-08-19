using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;

namespace UsersManagerAPI.IServices
{
    public interface ITokensGenerator
    {
        Task<string> NewTokenAsync(IUserInfo UserInfo);
    }
}
