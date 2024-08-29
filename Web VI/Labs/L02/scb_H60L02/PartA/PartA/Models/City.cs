using System;
using System.Collections.Generic;

namespace PartA.Models;

public partial class City
{
    public decimal CityId { get; set; }

    public string? City1 { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
