using System;
using System.Collections.Generic;

namespace Webapplication1Mvc.models;

public partial class Vehicle
{
    public int Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public string Color { get; set; } = null!;

    public decimal Price { get; set; }
}
