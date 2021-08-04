using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersManagerAPI.IServices
{
    public interface ITokensGenerator
    {
        Task<string> NewTokenAsync(string userName);
    }
}
