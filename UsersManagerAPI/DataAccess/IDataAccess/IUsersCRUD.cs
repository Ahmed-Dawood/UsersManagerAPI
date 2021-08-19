using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;

namespace UsersManagerAPI.DataAccess.IDataAccess
{
    public interface IUsersCRUD
    {
        IUserInfo GetUser(IUserInfo userInfo);

        Task<IUserInfo> AddUserAsync(IUserInfo userInfo);

        Task<IUserInfo> DeleteUser(IUserInfo userInfo);

        Task<IUserInfo> UpdateUser(IUserInfo userInfo);
    }
}
