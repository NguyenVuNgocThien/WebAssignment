using API.Models;
using CustomerSite;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestAssignment
{
    public class CartTest
    {
        public interface ICartRepository
        {
            bool AddToCart(GioHang gioHang, LoginModel loginModel);
            bool PutProduct(string MaSP, int txtSL, LoginModel loginModel);
        }
        public class CartController : Controller
        {
            private readonly ICartRepository _cart;
            public CartController(ICartRepository cart)
            {
                _cart = cart;
            }
            public bool AddToCart(GioHang gioHang, LoginModel loginModel)
            {
                var httpService = new HttpService(new HttpClient());
                var listgh = httpService.GetAsync<List<GioHang>>(url: "https://localhost:44348/api/GioHangs");
                if (listgh.Result.Where(g => g.MaKh == gioHang.MaKh).FirstOrDefault(m => m.MaSp == gioHang.MaSp) == null)
                {
                    var tokenResponse = httpService.GetToken("https://localhost:44348/api/Login/Authenticate", loginModel);

                    var ghs = httpService.PostAsync<GioHang>(url: "https://localhost:44348/api/GioHangs", data: gioHang, accessToken: tokenResponse.Result.AccessToken);
                    if (ghs.Result != null)
                    {
                        return true;
                    }
                    return false;
                }
                else
                    return false;
            }
            public bool PutProduct(string MaSP, int txtSL, LoginModel loginModel)
            {
                var httpService = new HttpService(new HttpClient());
                GioHang gioHang = new GioHang();
                var gs = httpService.GetAsync<List<GioHang>>(url: "https://localhost:44348/api/GioHangs");
                gioHang = gs.Result.Where(g => g.MaKh == "2").FirstOrDefault(g => g.MaSp == MaSP);
                if (gioHang != null)
                {
                    gioHang.SoLuong = txtSL;
                    gioHang.ThanhTien = txtSL * gioHang.ThanhTien;
                    gioHang.MaKhNavigation = null;
                    gioHang.MaSpNavigation = null;
                    var tokenResponse = httpService.GetToken("https://localhost:44348/api/Login/Authenticate", loginModel);
                    var ghs = httpService.PutAsync<GioHang>(url: "https://localhost:44348/api/GioHangs", data: gioHang, accessToken: tokenResponse.Result.AccessToken);
                    if (ghs.Result != null)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
        }
        [Fact]
        public void TestAddToCart()
        {
            var mock = new Mock<ICartRepository>();
            LoginModel loginModel_true = new LoginModel();
            loginModel_true.TaiKhoan = "KhachHang01";
            loginModel_true.MatKhau = "123";
            GioHang gioHang = new GioHang();
            gioHang.MaSp = "BH01";
            gioHang.MaKh = "2";
            gioHang.IsDatHang = false;
            gioHang.SoLuong = 1;
            gioHang.ThanhTien = 29000;
            mock.Setup(s => s.AddToCart(gioHang, loginModel_true)).Returns(true);
            CartController cartController = new CartController(mock.Object);
            Assert.Equal(true, cartController.AddToCart(gioHang, loginModel_true));
        }
        [Fact]
        public void TestPutProduct()
        {
            var mock =new Mock<ICartRepository>();
            LoginModel loginModel_true = new LoginModel();
            loginModel_true.TaiKhoan = "KhachHang01";
            loginModel_true.MatKhau = "123";
            mock.Setup(m => m.PutProduct("BH01", 10, loginModel_true));
            CartController cartController = new CartController(mock.Object);
            Assert.Equal(true, cartController.PutProduct("BH01", 10, loginModel_true));
        }
    }
}
