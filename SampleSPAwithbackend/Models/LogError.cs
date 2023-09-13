using System;
using System.Collections.Generic;

namespace SampleSPAwithbackend.Models;

public partial class LogError
{
    public int Id { get; set; }

    public string? Error { get; set; }

    public DateTime RegistrationDate { get; set; }
}
