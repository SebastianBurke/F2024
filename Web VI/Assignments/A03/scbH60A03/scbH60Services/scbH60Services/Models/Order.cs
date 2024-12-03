namespace scbH60Services.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateFulfilled { get; set; }
        public decimal Total { get; set; }
        public decimal Taxes { get; set; }

        // Navigation properties
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public User Customer { get; set; }
    }
}
