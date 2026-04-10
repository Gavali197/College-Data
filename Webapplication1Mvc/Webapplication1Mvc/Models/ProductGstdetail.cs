using System;
using System.Collections.Generic;

namespace Webapplication1Mvc.models;

public partial class ProductGstdetail
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public decimal? Sgst { get; set; }

    public decimal? Cgst { get; set; }

    public decimal? TotalGst { get; set; }

    public virtual Product? Product { get; set; }
}
