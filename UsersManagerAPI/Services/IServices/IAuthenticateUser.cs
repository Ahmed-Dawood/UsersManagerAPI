using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;

namespace UsersManagerAPI.IServices
{
    public interface IAuthenticateUser
    {
        UserInfo AuthenticateAsync(string UserName, string Password);
    }
}
