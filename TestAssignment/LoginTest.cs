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
        //    [Fact]
        //    public void TestLogintrue()
        //    {
        //        LoginModel loginModel_true = new LoginModel();
        //        loginModel_true.TaiKhoan = "KhachHang01";
        //        loginModel_true.MatKhau = "123";
        //        Assert.Equal(true,DangNhap(loginModel_true));
        //    }
        //    [Fact]
        //    public void TestLoginfalse()
        //    {
        //        LoginModel loginModel_false = new LoginModel();
        //        loginModel_false.TaiKhoan = "KhachHang01";
        //        loginModel_false.MatKhau = "13";
        //        Assert.Equal(false, DangNhap(loginModel_false));
        //    }
        //    [Fact]
        //    public void TestAddToCart()
        //    {
        //        LoginModel loginModel_true = new LoginModel();
        //        loginModel_true.TaiKhoan = "KhachHang01";
        //        loginModel_true.MatKhau = "123";
        //        GioHang gioHang = new GioHang();
        //        gioHang.MaSp = "BH01";
        //        gioHang.MaKh = "KH01";
        //        gioHang.IsDatHang = false;
        //        gioHang.SoLuong = 1;
        //        gioHang.ThanhTien = 29000;
        //        Assert.Equal(false, AddToCart(gioHang,loginModel_true));
        //    }
        //    [Fact]
        //    public void TestGetProductByName()
        //    {
        //        Assert.Equal(true, GetProduct(0,"bia"));
        //    }
        //    [Fact]
        //    public void TestGetNewProduct()
        //    {
        //        Assert.Equal(true, GetProduct(10));
        //    }
        //    [Fact]
        //    public void TestGetAllProduct()
        //    {
        //        Assert.Equal(true, GetProduct(0));
        //    }
        //    [Fact]
        //    public void TestPutProduct()
        //    {
        //        LoginModel loginModel_true = new LoginModel();
        //        loginModel_true.TaiKhoan = "KhachHang01";
        //        loginModel_true.MatKhau = "123";
        //        Assert.Equal(true, PuProduct("BH01", 10, loginModel_true));
        //    }
        //    bool  DangNhap(LoginModel loginModel)
        //    {
        //        var httpService = new HttpService(new HttpClient());
        //        var tokenResponse = httpService.GetToken("https://localhost:44348/api/Login/Authenticate", loginModel);
        //        return tokenResponse.Result.Success;
        //    }
        //    bool AddToCart(GioHang gioHang,LoginModel loginModel)
        //    {
        //        if (context.GioHangs.Where(g => g.MaKh == g.MaKh).FirstOrDefault(m => m.MaSp == gioHang.MaSp) == null)
        //        {
        //            var httpService = new HttpService(new HttpClient());
        //            var tokenResponse = httpService.GetToken("https://localhost:44348/api/Login/Authenticate", loginModel);

        //            var ghs = httpService.PostAsync<GioHang>(url: "https://localhost:44348/api/GioHangs", data: gioHang, accessToken: tokenResponse.Result.AccessToken);

        //            if (ghs.Result != null)
        //            {
        //                return true;
        //            }
        //            return false;
        //        }
        //        else
        //            return false;
        //    }
        //    bool PuProduct(string MaSP, int txtSL,LoginModel loginModel)
        //    {
        //        GioHang gioHang = new GioHang();
        //        gioHang = context.GioHangs.Where(m => m.MaSp == MaSP).FirstOrDefault(m => m.MaKh == "KH01");
        //        if (gioHang != null)
        //        {
        //            gioHang.SoLuong = txtSL;
        //            var httpService = new HttpService(new HttpClient());
        //            var tokenResponse = httpService.GetToken("https://localhost:44348/api/Login/Authenticate", loginModel);
        //            var ghs = httpService.PutAsync<GioHang>(url: "https://localhost:44348/api/GioHangs", data: gioHang, accessToken: tokenResponse.Result.AccessToken);
        //            if (ghs.Result != null)
        //            {
        //                return true;
        //            }
        //            else
        //                return false;
        //        }
        //        else
        //            return false;
        //    }
        //    
        //}
    }
}
