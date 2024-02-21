using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }

        public Book Books { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
    