using CustomerSite;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestAssignment
{
    public class ProductTest
    {
        public interface IProductRepository
        {
            bool GetProduct(int maloaisp = 0, string Tim = "");
        }
        public class ProductController : Controller
        {
            private readonly IProductRepository _product;
            public ProductController(IProductRepository product)
            {
                _product = product;
            }
            public bool GetProduct(int maloaisp = 0, string Tim = "")
            {
                if (Tim != "")
                {
                    var httpService = new HttpService(new System.Net.Http.HttpClient());
                    var sps = httpService.GetAsync<List<SanPham>>(url: "https://localhost:44348/api/Sanphams/SP/" + Tim);
                    if (sps.Result != null)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    var httpService = new HttpService(new System.Net.Http.HttpClient());
                    var sps = httpService.GetAsync<List<SanPham>>(url: "https://localhost:44348/api/Sanphams/loaiSP/" + maloaisp);
                    if (sps.Result != null)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
        }
        [Fact]
        public void TestGetProduct()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(s => s.GetProduct(0,"")).Returns(true);
            ProductController product = new ProductController(mock.Object);
            Assert.Equal(true,product.GetProduct(10));
        }
    }
}
