using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;

namespace UsersManagerAPI.IServices
{
    public interface IUserInfoHandler
    {
        UserInfo AuthenticateAsync(string UserName, string Password);
    }
}
