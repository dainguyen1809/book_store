using Microsoft.AspNetCore.Identity;

namespace Book_Store.Models
{
    public class AppCustomer : IdentityUser
    {
        public string CustomerName { get; set; }
        public DateTime Birth { get; set; }
        public string Gender { get; set; }
        public string? Account { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }


    }
}
