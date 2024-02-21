using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Nhập tên tài khoản của bạn")]
        public string Account { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Nhập mật khẩu của bạn")]
        public string Password { get; set; }

        public string returnURL { get; set; }
    }
}
