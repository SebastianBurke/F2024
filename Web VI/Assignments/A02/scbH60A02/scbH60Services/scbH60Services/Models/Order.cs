namespace scbH60Services.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateFufilled { get; set; }
        public decimal Total { get; set; }
        public decimal Taxes { get; set; }
    }
}
