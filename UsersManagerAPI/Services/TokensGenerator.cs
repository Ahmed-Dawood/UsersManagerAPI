using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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

        async public Task<string> NewTokenAsync(string userName)
        {
            //get user token data from users DB Async
            //-----------
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, "Ahmed Dawood"),
                    new Claim(ClaimTypes.Role, "Manager"),
                    new Claim("UserPlan", "Premium")
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
