using API.Models;
using API.Controllers;
using System;
using Xunit;
using System.Net.Http;
using CustomerSite;
using System.Threading.Tasks;
using SharedModel.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestAssignment
{
    public class LoginTest
    {
        public interface ILoginRepository
        {
            bool Login(LoginModel loginModel);
        }
        public class LoginController : Controller
        {
            private readonly ILoginRepository _login;
            public LoginController(ILoginRepository login)
            {
                _login = login;
            }
            public bool Login(LoginModel loginModel)
            {
                var httpService = new HttpService(new HttpClient());
                var tokenResponse = httpService.GetToken("https://localhost:44348/api/Login/Authenticate", loginModel);
                return tokenResponse.Result.Success;
            }
        }
        [Fact]
        public void LoginSuccess()
        {
            var mock = new Mock<ILoginRepository>();
            LoginModel loginModel_true = new LoginModel();
            loginModel_true.TaiKhoan = "KhachHang01";
            loginModel_true.MatKhau = "123";
            mock.Setup(p => p.Login(loginModel_true)).Returns(true);
            LoginController login = new LoginController(mock.Object);
            bool res = login.Login(loginModel_true);
            Assert.Equal(true, res);
        }
        [Fact]
        public void LoginFailed()
        {
            var mock = new Mock<ILoginRepository>();
            LoginModel loginModel_failed = new LoginModel();
            loginModel_failed.TaiKhoan = "KhachHang01";
            loginModel_failed.MatKhau = "13";
            mock.Setup(p => p.Login(loginModel_failed)).Returns(false);
            LoginController login = new LoginController(mock.Object);
            bool res = login.Login(loginModel_failed);
            Assert.Equal(false, res);
        }
    }
}
