using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models
{
	public class Book
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(255, ErrorMessage = "Hãy nhập tên sách")]
		public string BookName { get; set; }

		[Required(ErrorMessage = "Hãy nhập giá sách")]
		public double Price { get; set; }

		[Required, MaxLength(255, ErrorMessage = "Hãy nhập mô tả của sách")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Hãy nhập thời gian cập nhật sách")]
		public DateTime DateUpdate { get; set; }

		[Required, MaxLength(255, ErrorMessage = "Hãy chọn ảnh bìa sách")]
		public string UrlImgCover { get; set; }

		[Required(ErrorMessage = "Hãy nhập số lượng sách có trong kho")]
		public int InventoryNumber { get; set; }

        //foreign key

        public int TopicId { get; set; }

		public int PublisherId { get; set; }

		public Topic Topic { get; set; }
		public Publisher Publisher { get; set; }
	}
}
