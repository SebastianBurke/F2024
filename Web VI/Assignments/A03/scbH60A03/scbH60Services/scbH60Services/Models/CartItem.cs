﻿namespace scbH60Services.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation properties
        public ShoppingCart Cart { get; set; }
        public Product Product { get; set; }
    }
}
