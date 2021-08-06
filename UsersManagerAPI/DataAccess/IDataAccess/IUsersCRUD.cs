using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;

namespace UsersManagerAPI.DataAccess.IDataAccess
{
    public interface IUsersCRUD
    {
        UserInfo GetUserCredentials(string userEmail);

        Task<UserInfo> AddUserAsync(UserInfo userInfo);

        Task<UserInfo> DeleteUser(string userEmail);

        Task<UserInfo> UpdateUser(UserInfo userInfo, IEnumerable<UpdatableInfoEnum> UpdatedItems);
    }
}
