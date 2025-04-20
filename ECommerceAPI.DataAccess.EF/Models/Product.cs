using System;
using System.Collections.Generic;

namespace ECommerceAPI.DataAccess.EF.Models;

public partial class Product
{
    public required int ProductId { get; set; }

    public required int CategoryId { get; set; }

    public required string ProductName { get; set; } = null!;

    public required string Description { get; set; }

    public required decimal Price { get; set; }

    public required int StockQuantity { get; set; }

    public required string ImageUrl { get; set; }

    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}
