using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ticket.Adapter.Login.Interface;
using Ticket.Adapter.Ticket.Interface;
using Ticket.Db;
using Ticket.Enum;
using Ticket.Extensions;
using Ticket.Models.Ticket;
using Ticket.Models.User;
using Ticket.Service.Login.Interface;
using Ticket.Service.Ticket.Interface;
using Ticket.ViewModels.Ticket;

namespace Ticket.Service.Login
{
    public class LoginService : ILoginService
    {

        private readonly IConfiguration _configuration;
        private readonly ILoginAdapter _loginAdapter;

        public LoginService(IConfiguration configuration, ILoginAdapter loginAdapter)
        {

            this._configuration = configuration;
            this._loginAdapter = loginAdapter;
        }


        public UserModel Get(string userName)
        {
            

            return _loginAdapter.Get(userName);
        }


        public string GetToken(UserModel user, int expireMinutes = 30)
        {   //https://blog.miniasp.com/post/2019/12/16/How-to-use-JWT-token-based-auth-in-aspnet-core-31


            var issuer = _configuration.GetValue<string>("JwtSettings:Issuer");
            var signKey = _configuration.GetValue<string>("JwtSettings:SignKey");
            // 設定要加入到 JWT Token 中的聲明資訊(Claims)
            var claims = new List<Claim>();

            // 在 RFC 7519 規格中(Section#4)，總共定義了 7 個預設的 Claims，我們應該只用的到兩種！
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // JWT ID

            // 登入者角色 與使用者ID
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


            return serializeToken;

        }

    }
}
