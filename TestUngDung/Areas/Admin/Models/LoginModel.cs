using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestUngDung.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string Username { set; get; }

        [Required(ErrorMessage = "Mời nhập password!")]
        public string Password { set; get; }

        public bool RememberMe { set; get; }
    }
}