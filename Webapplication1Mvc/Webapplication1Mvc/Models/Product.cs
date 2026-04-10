using System;
using System.Collections.Generic;

namespace Webapplication1Mvc.models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Rate { get; set; }

    public int? Category { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ProductGstdetail> ProductGstdetails { get; set; } = new List<ProductGstdetail>();
}
