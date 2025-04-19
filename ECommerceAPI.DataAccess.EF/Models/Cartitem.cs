using System;
using System.Collections.Generic;

namespace ECommerceAPI.DataAccess.EF.Models;

public partial class Cartitem
{
    public int CartItemId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
