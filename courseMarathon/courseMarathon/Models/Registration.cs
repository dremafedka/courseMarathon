using System;
using System.Collections.Generic;

namespace courseMarathon.Models;

public partial class Registration
{
    public int RegistrationId { get; set; }

    public TimeSpan? StartTime { get; set; }

    public string? IntermediatePoint { get; set; }

    public TimeSpan? FinishTime { get; set; }

    public int? MemberId { get; set; }

    public int? DistanceTypeId { get; set; }

    public virtual DistanceType? DistanceType { get; set; }

    public virtual Member? Member { get; set; }
}
