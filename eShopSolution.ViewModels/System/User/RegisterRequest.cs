using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.System.User
{
    public class RegisterRequest
    {
        [DisplayName("Tên")]
        public string FirstName { get; set; }

        [DisplayName("Họ")]

        public string LastName { get; set; }

        [DisplayName("Ngày sinh")]
        [DataType(DataType.Date)]

        public DateTime Dob { get; set; }


        public string Email { get; set; }
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Tài khoản")]
        public string UserName { get; set; }
        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Xác nhận lại mật khẩu")]
        [DataType(DataType.Password)]
        public string ComfirmPassword { get; set; }

    }
}
