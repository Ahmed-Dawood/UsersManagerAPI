using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet("Authenticate")]
        async public Task<IActionResult> Authenticate([FromQuery] string userName, [FromQuery] string pwd)
        {
            //Test Mail Sender
            MailService mailService = new MailService();

            MailClass mail = new MailClass()
            {
                Body = mailService.GetMailBody("Ahmed Dawood"),
                IsBodyHtml = true,
                Subject = "Hi From Far",
                ToMailIds = new List<string>() { new string("akdawood97@gmail.com") }
            };

            string msg = await mailService.SendMail(mail);

            //------------------------------------------

            bool Confirmation = await ValidateUsers.AuthenticateAsync(userName, pwd);
            if (Confirmation)
            {
                return Ok(new
                {
                    Token = await TokensGenerator.NewTokenAsync(userName),
                    msg = msg
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
                Token = await TokensGenerator.NewTokenAsync("Ahmed"),
                UserName = username
            });
        }
    }
}
