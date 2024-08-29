using System;
using System.Collections.Generic;

namespace PartA.Models;

public partial class PersonPet
{
    public decimal PersonId { get; set; }

    public decimal PetId { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual Pet Pet { get; set; } = null!;
}
