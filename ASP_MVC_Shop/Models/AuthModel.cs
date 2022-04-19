using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP_MVC_Shop.Models
{
    public class AuthLoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        public string email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        public string password { get; set; }
    }
    public class AuthRegisterModel : AuthLoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập xác nhận mật khẩu!")]
        [Compare("password", ErrorMessage ="Xác thực mật khẩu không chính xác!")]
        public string rePassword { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên!")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ!")]
        public string lastName { get; set; }
    }
}