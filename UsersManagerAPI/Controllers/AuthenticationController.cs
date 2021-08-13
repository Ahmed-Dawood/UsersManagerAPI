using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;
using UsersManagerAPI.Services;

namespace UsersManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokensGenerator TokensGenerator;
        private readonly IAuthenticateUser UserInfoHandler;

        public AuthenticationController(
            ITokensGenerator TokensGenerator,
            IAuthenticateUser UserInfoHandler)
        {
            this.TokensGenerator = TokensGenerator;
            this.UserInfoHandler = UserInfoHandler;
        }
        [HttpPost("SignUp")]
        async public Task<IActionResult> Register([FromBody]RegisterInfo userRegisterInfo)
        {
            return Ok();
        }
        [HttpGet("Authenticate")]
        async public Task<IActionResult> Authenticate([FromBody]LoginInfo loginInfo)
        {          
            UserInfo UserInfo = UserInfoHandler.AuthenticateAsync(loginInfo.UserName, loginInfo.Password);
            if (UserInfo.Message == Message.Success)
            {
                return Ok(new
                {
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
