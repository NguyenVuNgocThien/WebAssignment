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
using System.Security.Cryptography;

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
        public IActionResult Validate([FromBody] LoginModel model)
        {
            var user = context.TaiKhoanDangNhaps.SingleOrDefault(t => t.TaiKhoan == model.TaiKhoan && model.MatKhau==t.MatKhau);
            TaiKhoanDangNhap.currentUser = user;
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
                AccessToken=GenerateToken(user).AccessToken,
                RefreshToken=GenerateToken(user).RefreshToken
            });
        }
        [HttpPost("Refresh")]
        public IActionResult RefreshTokenAsync([FromBody] TokenModel tokenModel)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(app.SecretKey);
            var tokenParam = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero,

                ValidateLifetime=false
            };
            try
            {
                var tokenVerification = jwtTokenHandler.ValidateToken(tokenModel.AccessToken,tokenParam,out var validateToken);

                if(validateToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return Ok( new LoginResponse
                        {
                            Success = false,
                            Message = "Invalid Token",
                        });
                    }
                }
                var utcExpireDate = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return Ok(new LoginResponse
                    {
                        Success = false,
                        Message = "Access Token has not yet expired",
                    });
                }
                return Ok(new LoginResponse
                {
                    Success = true,
                    Message="Refresh Token Success",
                    AccessToken =  GenerateToken(TaiKhoanDangNhap.currentUser).AccessToken,
                    RefreshToken=GenerateToken(TaiKhoanDangNhap.currentUser).RefreshToken  
                });
            }
            catch(Exception ex)
            {
                return BadRequest( new LoginResponse
                {
                    Success = false,
                    Message = "Something went wrong",
                });
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            return dateTimeInterval;
        }

        private TokenModel GenerateToken(TaiKhoanDangNhap taiKhoanDangNhap)
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
                Expires=DateTime.UtcNow.AddSeconds(20),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),SecurityAlgorithms.HmacSha512Signature)
                
            };
            var token = jwtToken.CreateToken(tokenDescription);
            var accessToken= jwtToken.WriteToken(token);
            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken=GenerateRefreshToken()
            };
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            } 
        }
    }
}
