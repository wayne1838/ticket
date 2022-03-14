using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ticket.Db;
using Ticket.Enum;
using Ticket.Extensions;
using Ticket.Models.Ticket;
using Ticket.Models.User;
using Ticket.Service.Login.Interface;
using Ticket.ViewModels;

namespace Ticket.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService _loginService;
        public LoginController( ILoginService loginService)
        {

            this._loginService = loginService;
        }

        /// <summary>
        /// 登入 
        /// 如果要在swagger使用token 要加上Bearer+"空白"+token
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Token</returns>
        //[HttpGet()]
        [HttpPost("login")]
        public IActionResult LogIn([FromBody] LoginModel user)
        {
            var userData = _loginService.Get(user.UserName);
            if (userData != null)
            {
                return Ok(new
                {
                    token = _loginService.GetToken(userData)
                });
                //return GetToken(userData);
            }
            else
            {
                return BadRequest();
            }
        }



    }
}
