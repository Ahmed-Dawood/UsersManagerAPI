using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;

namespace UsersManagerAPI.DataAccess.IDataAccess
{
    public interface IUsersCRUD
    {
        Task<IUserInfo> GetUserAsync(IUserInfo userInfo);

        Task<IUserInfo> AddUserAsync(IUserInfo userInfo);

        Task<IUserInfo> DeleteUserAsync(IUserInfo userInfo);

        Task<IUserInfo> UpdateUserAsync(IUserInfo userInfo);
    }
}
