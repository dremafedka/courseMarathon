using System;
using System.Collections.Generic;

namespace courseMarathon.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string? Fullname { get; set; }

    public bool? Sex { get; set; }

    public int? Discount { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool? SpecialStatus { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
