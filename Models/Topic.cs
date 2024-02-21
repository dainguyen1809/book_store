using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models
{
	public class Topic
	{
		[Key]
        public int Id { get; set; }

		[Required, MaxLength(100, ErrorMessage = "Hãy nhập tên chủ đề")]
        public string TopicName { get; set; }
	}
}
