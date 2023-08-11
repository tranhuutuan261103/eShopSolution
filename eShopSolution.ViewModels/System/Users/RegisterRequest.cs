using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.System.Users
{
    public class RegisterRequest
    {
        [Display(Name = "Tên đăng nhập")]
        public string UserName { set; get; }
        [Display(Name = "Email")]
        public string Email { set; get; }
        [Display(Name = "Tên")]
        public string FirstName { set; get; }
        [Display(Name = "Họ")]
        public string LastName { set; get; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { set; get; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { set; get; }
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { set; get; }
    }
}
