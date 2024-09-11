using System;
using System.Collections.Generic;

namespace scbH60Store.Models;

public partial class ProductCategory
{
    public int CategoryId { get; set; }
    public string ProdCat { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
