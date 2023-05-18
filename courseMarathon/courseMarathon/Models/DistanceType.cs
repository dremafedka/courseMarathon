using System;
using System.Collections.Generic;

namespace courseMarathon.Models;

public partial class DistanceType
{
    public int DistanceTypeId { get; set; }

    public string? Name { get; set; }

    public int? Distance { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
