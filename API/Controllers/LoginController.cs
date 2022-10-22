using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedModel.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CryptographyHelper.HashAlgorithms;
using System.Security.Cryptography;
using System;
using API.Models;
using Microsoft.Extensions.Options;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly qlbanhangContext context;
        private readonly AppSettings app;
        public LoginController(qlbanhangContext context,IOptionsMonitor<AppSettings> options)
        {
            this.context = context;
            app=options.CurrentValue;
        }
        [HttpPost("Authenticate")]
        public IActionResult Validate(LoginModel model)
        {
            var user = context.TaiKhoanDangNhaps.SingleOrDefault(t => t.TaiKhoan == model.TaiKhoan && model.MatKhau==t.MatKhau);
            if (user == null)
            {
                return Ok(new LoginResponse
                {
                    Success=false,
                    Message="Invalid username or password"

                });
            }


            return Ok(new LoginResponse
            {
                Success=true,
                Message="Authenticate success",
                Data=GenerateToken(user)
            });
        }
        private string GenerateToken(TaiKhoanDangNhap taiKhoanDangNhap)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(app.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(new[]
                {
                    new Claim("TaiKhoan",taiKhoanDangNhap.TaiKhoan),

                    new Claim("MatKhau",taiKhoanDangNhap.MatKhau),


                    new Claim("TokenID",Guid.NewGuid().ToString())
                }),
                Expires=DateTime.UtcNow.AddMinutes(1),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),SecurityAlgorithms.HmacSha256)
                
            };
            var token = jwtToken.CreateToken(tokenDescription);
            return jwtToken.WriteToken(token);
        }
    }
}
