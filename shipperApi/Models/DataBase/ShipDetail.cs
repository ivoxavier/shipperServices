using System;
using System.Collections.Generic;

namespace shipperApi.Models.DataBase;

public partial class ShipDetail
{
    public int Id { get; set; }

    public long HeaderId { get; set; }

    public string RefNumber { get; set; } = null!;
}
