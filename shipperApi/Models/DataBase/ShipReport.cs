using System;
using System.Collections.Generic;

namespace shipperApi.Models.DataBase;

public partial class ShipReport
{
    public int IdUnico { get; set; }

    public string? ReportName { get; set; }

    public string? ReportPath { get; set; }
}
