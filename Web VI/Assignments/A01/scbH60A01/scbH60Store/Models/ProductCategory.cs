﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace scbH60Store.Models
{
    public partial class ProductCategory
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Product category name is required.")]
        [StringLength(100, ErrorMessage = "Product category name cannot exceed 100 characters.")]
        public string ProdCat { get; set; } = null!;

        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string? ImageUrl { get; set; }

        // Navigation property
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
