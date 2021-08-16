using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;

namespace UsersManagerAPI.Services
{
    public class TokensGenerator : ITokensGenerator
    {
        private JwtSecurityTokenHandler tokenHandler;
        private byte[] secretKey;
        private string key = "A345rbde&3yd(@%$Nckeoc-e9vjv97c9"; //your 32 char secret key like what is written

        public TokensGenerator()
        {
            tokenHandler = new JwtSecurityTokenHandler();
            secretKey = Encoding.ASCII.GetBytes(key);
        }

        async public Task<string> NewTokenAsync(UserInfo UserInfo)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, UserInfo.UserName),
                    new Claim(ClaimTypes.Role, UserInfo.Role),
                    new Claim("UserPlan", UserInfo.AccountPricingPlan)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var JwtString = tokenHandler.WriteToken(token);
            return JwtString;
        }
    }
}
