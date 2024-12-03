using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace scbH60Services.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Category is required.")]
        public int ProdCatId { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Manufacturer name cannot exceed 100 characters.")]
        public string? Manufacturer { get; set; }

        [Required(ErrorMessage = "Stock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a positive number.")]
        public int Stock { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Buy price must be greater than zero.")]
        public decimal? BuyPrice { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Sell price must be greater than zero.")]
        public decimal? SellPrice { get; set; }

        [StringLength(2000, ErrorMessage = "Employee notes cannot exceed 1000 characters.")]
        public string? EmployeeNotes { get; set; }

        public string? ImageUrl { get; set; }

        // Navigation properties

        [BindNever]
        public virtual ProductCategory? ProdCat { get; set; } = null!;
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

}
