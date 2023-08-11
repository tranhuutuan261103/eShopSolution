using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eShopSolution.ViewModels.System.Users
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
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
    }
}
