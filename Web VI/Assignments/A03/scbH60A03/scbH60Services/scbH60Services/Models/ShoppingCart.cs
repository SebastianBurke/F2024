namespace scbH60Services.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public string CustomerId { get; set; }
        public DateTime DateCreated { get; set; }

        // Navigation properties
        public User Customer { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }

}
