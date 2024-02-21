using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int Checkout { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public string UserName { get; set; }

        public string AspNetUserId { get; set; }

        
    }
}
