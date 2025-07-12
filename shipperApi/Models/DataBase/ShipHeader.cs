using System;
using System.Collections.Generic;

namespace shipperApi.Models.DataBase;

public partial class ShipHeader
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Dest { get; set; } = null!;

    public int Torders { get; set; }
}
