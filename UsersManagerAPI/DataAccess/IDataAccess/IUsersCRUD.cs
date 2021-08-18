using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;

namespace UsersManagerAPI.DataAccess.IDataAccess
{
    public interface IUsersCRUD
    {
        UserInfo GetUser(string userEmail);

        Task<UserInfo> AddUserAsync(UserInfo userInfo);

        Task<UserInfo> DeleteUser(string userEmail);

        Task<UserInfo> UpdateUser(UserInfo userInfo);
    }
}
