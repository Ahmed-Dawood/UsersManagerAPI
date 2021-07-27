using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersManagerAPI.IServices
{
    interface IValidateUsers
    {
        Task<bool> AuthenticateAsync(string UserName, string Password);
    }
}
