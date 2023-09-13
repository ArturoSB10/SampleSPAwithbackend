using System;
using System.Collections.Generic;

namespace SampleSPAwithbackend.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsAdmin { get; set; } = false;

    public DateTime RegistrationDate { get; set; }
}
