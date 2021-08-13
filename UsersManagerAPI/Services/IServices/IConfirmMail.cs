using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersManagerAPI.Services.IServices
{
    public interface IConfirmMail
    {
        Task<string> ConfirmEmail(string UserName);
    }
}
