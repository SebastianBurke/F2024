using System;
using System.Collections.Generic;

namespace PartA.Models;

public partial class Person
{
    public decimal PersonId { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public decimal CityId { get; set; }

    public virtual City City { get; set; } = null!;
}
