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
    public class ProductTest
    {
        public interface IProductRepository
        {
            bool GetProduct(int maloaisp = 0, string Tim = "");
            bool GetProductById(string id);
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
                    var httpService = new HttpService(new HttpClient());
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
                    var httpService = new HttpService(new HttpClient());
                    var sps = httpService.GetAsync<List<SanPham>>(url: "https://localhost:44348/api/Sanphams/loaiSP/" + maloaisp);
                    if (sps.Result != null)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            public bool GetProductById(string id)
            {
                var httpService = new HttpService(new HttpClient());
                var sp = httpService.GetAsync<SanPham>(url:"https://localhost:44348/api/Sanphams/" + id);
                if (sp.Result != null)
                {
                    return true;
                }
                else
                    return false;
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
        [Fact]
        public void TestGetProductDetails()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(s => s.GetProductById("BH01")).Returns(true);
            ProductController product = new ProductController(mock.Object);
            Assert.Equal(true, product.GetProductById("BH01"));
        }
    }
}
