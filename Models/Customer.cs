using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nhập tên của bạn")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Nhập ngày sinh của bạn")]
        public DateTime Birth { get; set; }

        [Required(ErrorMessage = "Nhập giới tính của bạn")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Nhập SĐT của bạn")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Nhập tên tài khoản của bạn")]
        public string Account { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Nhập mật khẩu của bạn")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Nhập Email của bạn"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nhập địa chỉ của bạn")]
        public string Address { get; set; }


    }
}