﻿using System;
using System.Collections.Generic;

namespace NWUTechTrends_WebAPI.Models;

public partial class Client
{
    public Guid ClientId { get; set; }

    public string? ClientName { get; set; }

    public string? PrimaryContactEmail { get; set; }

    public DateTime? DateOnboarded { get; set; }

    public ICollection<JobTelemetry> JobTelemetries { get; set; }
}
