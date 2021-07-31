using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IValidateUsers ValidateUsers;
        public AuthenticationController(
            ITokensGenerator TokensGenerator, 
            IValidateUsers ValidateUsers)
        {
            this.TokensGenerator = TokensGenerator;
            this.ValidateUsers = ValidateUsers;
        }
        [HttpPost("RegisterUser")]
        async public Task<IActionResult> Register([FromBody]RegisterInfo userRegisterInfo)
        {
            return Ok();
        }
        [HttpGet("Authenticate")]
        async public Task<IActionResult> Authenticate([FromBody]LoginInfo loginInfo)
        {          
            bool Confirmation = await ValidateUsers.AuthenticateAsync(loginInfo.Email, loginInfo.Password);
            if (Confirmation)
            {
                return Ok(new
                {
                    Token = await TokensGenerator.NewTokenAsync(loginInfo.Email)
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
                Token = await TokensGenerator.NewTokenAsync(username),
                UserName = username
            });
        }
    }
}
