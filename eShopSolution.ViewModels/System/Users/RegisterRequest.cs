using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.System.Users
{
    public class RegisterRequest
    {
        public string UserName { set; get; }
        public string Email { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string PhoneNumber { set; get; }
        public DateTime Dob { set; get; }
        public string Password { set; get; }
        public string ConfirmPassword { set; get; }
    }
}
