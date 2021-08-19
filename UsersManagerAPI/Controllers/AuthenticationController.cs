using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;
using System.Text.Json;
using UsersManagerAPI.DomainClasses.Models.IModels;

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
        private IUserInfo UserInfo;
        #endregion

        public AuthenticationController(
            ITokensGenerator TokensGenerator,
            IAuthenticateUser AuthenticateUser,
            IRegisterUser RegisterUser,
            IUserInfo UserInfo)
        {
            this.TokensGenerator = TokensGenerator;
            this.AuthenticateUser = AuthenticateUser;
            this.RegisterUser = RegisterUser;
            this.UserInfo = UserInfo;
        }
        [HttpPost("SignUp")]
        async public Task<IActionResult> Register([FromBody] RegisterInfo userRegisterInfo)
        {
            var serializedParent = JsonSerializer.Serialize(userRegisterInfo);
            UserInfo = JsonSerializer.Deserialize<UserInfo>(serializedParent);
            UserInfo = await RegisterUser.SignUp(UserInfo);
            if(UserInfo.Message == Message.Success)
            {
                return Ok(Message.Success);
            }
            else
            {
                ModelState.AddModelError("Failed", UserInfo.Message + " - " + UserInfo.DetailedMessage);
                return Ok(ModelState); 
            }
        }
        [HttpGet("Authenticate")]
        async public Task<IActionResult> Authenticate([FromBody]LoginInfo loginInfo)
        {
            var serializedParent = JsonSerializer.Serialize(loginInfo);
            UserInfo = JsonSerializer.Deserialize<UserInfo>(serializedParent);
            UserInfo = AuthenticateUser.AuthenticateAsync(UserInfo);
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
                ModelState.AddModelError("Unauthorized", UserInfo.Message + " - " + UserInfo.DetailedMessage);
                return Unauthorized(ModelState);
            }
        }
        [HttpGet("ConfirmMail")]
        async public Task<IActionResult> ConfirmMail([FromQuery] string username)
        {
            return Ok(new
            {                
                UserName = username
            });
        }
    }
}
