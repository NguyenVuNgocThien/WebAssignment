using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class LoginModel
    {
        public static LoginModel loginModel;
        public static void setLogin(LoginModel loginModel)
        {
            LoginModel.loginModel = loginModel;
        }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
    }
}
