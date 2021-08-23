using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.IServices;
using System.Text.Json;
using UsersManagerAPI.DomainClasses.Models.IModels;
using Microsoft.AspNetCore.Authorization;
using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.Services.IServices;
using UsersManagerAPI.SecurityServices;

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
        private IUsersCRUD UsersCURD;
        private readonly IConfirmMail confirmMail;
        #endregion

        #region constructor
        public AuthenticationController(
            ITokensGenerator TokensGenerator,
            IAuthenticateUser AuthenticateUser,
            IRegisterUser RegisterUser,
            IConfirmMail confirmMail,
            IUserInfo UserInfo,
            IUsersCRUD UsersCURD)
        {
            this.TokensGenerator = TokensGenerator;
            this.AuthenticateUser = AuthenticateUser;
            this.RegisterUser = RegisterUser;
            this.UserInfo = UserInfo;
            this.confirmMail = confirmMail;
            this.UsersCURD = UsersCURD;
        }
        #endregion

        #region EndPoints
        [AllowAnonymous]
        [HttpPost("SignUp")]
        async public Task<IActionResult> Register([FromBody]RegisterInfo userRegisterInfo)
        {
            var serializedParent = JsonSerializer.Serialize(userRegisterInfo);
            UserInfo = JsonSerializer.Deserialize<UserInfo>(serializedParent);
            UserInfo = await RegisterUser.SignUpAsync(UserInfo);
            if (UserInfo.Message == Message.Success)
            {
                return Ok(new
                {
                    Token = await TokensGenerator.NewTokenAsync(UserInfo)
                });
            }
            else
            {
                return BadRequest(UserInfo.Message + " - " + UserInfo.DetailedMessage);
            }
        }

        [AllowAnonymous]
        [HttpGet("Authenticate")]
        async public Task<IActionResult> Authenticate([FromBody]LoginInfo loginInfo)
        {
            var serializedParent = JsonSerializer.Serialize(loginInfo);
            UserInfo = JsonSerializer.Deserialize<UserInfo>(serializedParent);
            UserInfo = await AuthenticateUser.AuthenticateAsync(UserInfo);
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
                return BadRequest(UserInfo.Message + " - " + UserInfo.DetailedMessage);
            }
        }

        [HttpPost("ConfirmMail")]
        async public Task<IActionResult> ConfirmMail([FromQuery]string username)
        {
            UserInfo.UserName = username;
            UserInfo = await confirmMail.UpdateConfirmMailAsync(UserInfo);
            if (UserInfo.Message == Message.Success)
            {
                return Ok(Message.Success);
            }
            else
            {
                return BadRequest(UserInfo.Message);
            }
        }

        [HttpPost("ChangePassword")]
        async public Task<IActionResult> ChangePassword([FromBody]ResetPasswordInfo ResetInfo)
        {
            UserInfo.UserName = ResetInfo.UserName;
            UserInfo.Password = ResetInfo.OldPassword;
            UserInfo = await AuthenticateUser.AuthenticateAsync(UserInfo);
            if (UserInfo.Message == Message.Success)
            {
                Hashing Hashingservices = new Hashing();
                UserInfo.HashPassword = Hashingservices.ComputeSha256Hash(UserInfo.SaltKey + ResetInfo.NewPassword);
                UserInfo = await UsersCURD.UpdateUserAsync(UserInfo);
                if (UserInfo.Message == Message.Success)
                {
                    return Ok(Message.Success);
                }
                else
                {
                    return BadRequest(UserInfo.Message);
                }
            }            
            else
            {
                return BadRequest(UserInfo.Message);
            }
        }
        #endregion
    }
}
