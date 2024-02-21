using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models
{
	public class Publisher
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(255, ErrorMessage = "Hãy nhập tên NXB")]
		public string NamePublisher { get; set; }

		[Required, MaxLength(255, ErrorMessage = "Hãy nhập địa chỉ")]
		public string Address { get; set; }

		[Required, MaxLength(255, ErrorMessage = "Hãy nhập SĐT")]
        public string Phone { get; set; }
	}
}
