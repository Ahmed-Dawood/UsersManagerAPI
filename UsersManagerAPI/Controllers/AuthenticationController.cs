using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;
using UsersManagerAPI.Services;
using System.Text.Json;

namespace UsersManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region DI attributes
        private readonly ITokensGenerator TokensGenerator;
        private readonly IAuthenticateUser AuthenticateUser;
        private readonly IRegisterUser RegisterUser;
        #endregion

        public AuthenticationController(
            ITokensGenerator TokensGenerator,
            IAuthenticateUser AuthenticateUser,
            IRegisterUser RegisterUser)
        {
            this.TokensGenerator = TokensGenerator;
            this.AuthenticateUser = AuthenticateUser;
            this.RegisterUser = RegisterUser;
        }
        [HttpPost("SignUp")]
        async public Task<IActionResult> Register([FromBody] RegisterInfo userRegisterInfo)
        {
            var serializedParent = JsonSerializer.Serialize(userRegisterInfo);
            UserInfo userInfo = JsonSerializer.Deserialize<UserInfo>(serializedParent);
            userInfo = await RegisterUser.SignUp(userInfo);
            if(userInfo.Message == Message.Success)
            {
                return Ok(Message.Success);
            }
            else
            {
                ModelState.AddModelError("Failed", "Failed to sign up");
                return Ok(ModelState); 
            }
        }
        [HttpGet("Authenticate")]
        async public Task<IActionResult> Authenticate([FromBody]LoginInfo loginInfo)
        {          
            UserInfo UserInfo = AuthenticateUser.AuthenticateAsync(loginInfo.UserName, loginInfo.Password);
            if (UserInfo.Message == Message.Success)
            {
                return Ok(new
                {
                    UserName = UserInfo.UserName,
                    AccountType = UserInfo.AccountType,
                    Token = await TokensGenerator.NewTokenAsync(UserInfo)
                });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "Unauthorized access");
                return Unauthorized(ModelState);
            }
        }
        [HttpGet("ConfirmMail")]
        async public Task<IActionResult> ConfirmMail([FromQuery] string username)
        {
            return Ok(new
            {
                //Token = await TokensGenerator.NewTokenAsync(UserInfo),
                UserName = username
            });
        }
    }
}
