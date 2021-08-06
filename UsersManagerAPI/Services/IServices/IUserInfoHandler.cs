using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersManagerAPI.IServices
{
    public interface IUserInfoHandler
    {
        Task<bool> AuthenticateAsync(string UserName, string Password);
    }
}
