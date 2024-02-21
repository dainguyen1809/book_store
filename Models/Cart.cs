using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string UrlImgCover { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total
        {
            get { return Quantity * Price; }
        }

        public Cart()
        {
            
        }

        public Cart(Book prod)
        {
            BookId = prod.Id;
            BookName = prod.BookName;
            UrlImgCover = prod.UrlImgCover;
            Price = prod.Price;
            Quantity = 1;
        }
    }
}
