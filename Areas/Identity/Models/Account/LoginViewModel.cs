using System.ComponentModel.DataAnnotations;

namespace f7.Areas.Identity.Models
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Display(Name = "Thư điện tử")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phải nhập {0}")]
        [Display(Name = "Địa chỉ email hoặc tên tài khoản")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhớ thông tin đăng nhập?")]
        public bool RememberMe { get; set; }
    }
}
