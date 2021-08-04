using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;

namespace UsersManagerAPI.DataAccess.IDataAccess
{
    public interface IUsersCRUD
    {
        Task<UserInfo> GetUser(string userName);
    }
}
