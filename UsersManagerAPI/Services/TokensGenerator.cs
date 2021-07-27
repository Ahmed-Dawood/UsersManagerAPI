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
        private string key = "your wanted 32 character key"; //A345rbde&3yd(@%$Nckeoc-e9vjv97c9
        private readonly IUserInfoHandler UserInfoHandler;

        public TokensGenerator(IUserInfoHandler UserInfoHandler)
        {
            tokenHandler = new JwtSecurityTokenHandler();
            secretKey = Encoding.ASCII.GetBytes(key);
            this.UserInfoHandler = UserInfoHandler;
        }

        async public Task<string> NewTokenAsync(string userName)
        {
            //get user token data from users DB
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

        
        async public Task<bool> AuthenticateAsync(string UserName, string Password)
        {
            if (!string.IsNullOrWhiteSpace(UserName) &&
                !string.IsNullOrWhiteSpace(Password) &&
                await UserInfoHandler.ValidateAsync(UserName, Password))
                return true;
            else
                return false;
        }
        public bool VerifyToken(string token)
        {
            return false;
        }

    }
}
