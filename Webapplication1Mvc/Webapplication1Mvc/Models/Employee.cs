using System;
using System.Collections.Generic;

namespace Webapplication1Mvc.models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Post { get; set; } = null!;

    public int Salary { get; set; }
}
