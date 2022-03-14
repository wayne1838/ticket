﻿using Microsoft.AspNetCore.Authorization;
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
using Ticket.ViewModels;

namespace Ticket.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {

        private readonly MyContext _context;
        private readonly IConfiguration Configuration;
        public LoginController( MyContext context, IConfiguration configuration) {

            this._context = context;
            this.Configuration = configuration;
        }

        /// <summary>
        /// 登入 
        /// 如果要在swagger使用token 要加上Bearer+"空白"+token
        /// </summary>
        /// <param name="User"></param>
        /// <returns>Token</returns>
        //[HttpGet()]
        [HttpPost("login")]
        public IActionResult LogIn([FromBody] LoginModel User)
        {
            var userData = _context.User.FirstOrDefault(f => f.UserName == User.UserName);
            if (userData != null)
            {
                return GetToken(userData);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("~/claims")]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(p => new { p.Type, p.Value }));
        }

        [HttpGet("~/username")]
        public IActionResult GetUserName()
        {
            return Ok(User.Identity.Name);
        }

        [HttpGet("~/jwtid")]
        public IActionResult GetUniqueId()
        {
            var jti = User.Claims.FirstOrDefault(p => p.Type == "jti");
            return Ok(jti.Value);
        }


        private IActionResult GetToken(UserModel user, int expireMinutes = 30)
        {   //https://blog.miniasp.com/post/2019/12/16/How-to-use-JWT-token-based-auth-in-aspnet-core-31
            
            if (!string.IsNullOrEmpty(user.UserName))
            {
                var issuer = Configuration.GetValue<string>("JwtSettings:Issuer");
                var signKey = Configuration.GetValue<string>("JwtSettings:SignKey");
                // 設定要加入到 JWT Token 中的聲明資訊(Claims)
                var claims = new List<Claim>();

                // 在 RFC 7519 規格中(Section#4)，總共定義了 7 個預設的 Claims，我們應該只用的到兩種！
                //claims.Add(new Claim(JwtRegisteredClaimNames.Iss, issuer));
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName)); // User.Identity.Name
                                                                              //claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "The Audience"));
                                                                              //claims.Add(new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds().ToString()));
                                                                              //claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())); // 必須為數字
                                                                              //claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())); // 必須為數字
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // JWT ID

                // 網路上常看到的這個 NameId 設定是多餘的
                //claims.Add(new Claim(JwtRegisteredClaimNames.NameId, userName));

                // 這個 Claim 也以直接被 JwtRegisteredClaimNames.Sub 取代，所以也是多餘的
                //claims.Add(new Claim(ClaimTypes.Name, userName));

                // 你可以自行擴充 "roles" 加入登入者該有的角色
                //claims.Add(new Claim("roles", "Admin"));
                //claims.Add(new Claim("roles", "Users"));
                claims.Add(new Claim("roles", EnumExtension.GetEnumDescription<RoleEnum>(user.Role)));
                claims.Add(new Claim("userId", user.Id.ToString()));

                var userClaimsIdentity = new ClaimsIdentity(claims);

                // 建立一組對稱式加密的金鑰，主要用於 JWT 簽章之用
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));

                // HmacSha256 有要求必須要大於 128 bits，所以 key 不能太短，至少要 16 字元以上
                // https://stackoverflow.com/questions/47279947/idx10603-the-algorithm-hs256-requires-the-securitykey-keysize-to-be-greater
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                // 建立 SecurityTokenDescriptor
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = issuer,
                    //Audience = issuer, // 由於你的 API 受眾通常沒有區分特別對象，因此通常不太需要設定，也不太需要驗證
                    //NotBefore = DateTime.Now, // 預設值就是 DateTime.Now
                    //IssuedAt = DateTime.Now, // 預設值就是 DateTime.Now
                    Subject = userClaimsIdentity,
                    Expires = DateTime.Now.AddMinutes(expireMinutes),
                    SigningCredentials = signingCredentials
                };

                // 產出所需要的 JWT securityToken 物件，並取得序列化後的 Token 結果(字串格式)
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var serializeToken = tokenHandler.WriteToken(securityToken);


                return Ok(new
                {
                    token = serializeToken
                });
            }
            else
            {
                return BadRequest(new { message = "username or password is incorrect." });
            }
        }

    }
}
