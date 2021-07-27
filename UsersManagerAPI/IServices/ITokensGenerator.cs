﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersManagerAPI.IServices
{
    public interface ITokensGenerator
    {
        Task<bool> AuthenticateAsync(string UserName, string Password);
        Task<string> NewTokenAsync(string userName);
        bool VerifyToken(string token);
    }
}
