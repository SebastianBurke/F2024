using Microsoft.AspNetCore.Identity;

namespace scbH60Services.Models
{
    public class User : IdentityUser
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Province { get; set; }
        public string CreditCard { get; set; }

        // Navigation properties
        public List<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
