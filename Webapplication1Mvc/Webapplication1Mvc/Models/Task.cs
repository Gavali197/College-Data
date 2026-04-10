using System;
using System.Collections.Generic;

namespace Webapplication1Mvc.models;

public partial class Task
{
    public int TaskId { get; set; }

    public string TaskName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime Timeline { get; set; }
}
