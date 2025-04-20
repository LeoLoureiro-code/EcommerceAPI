using System;
using System.Collections.Generic;

namespace ECommerceAPI.DataAccess.EF.Models;

public partial class User
{
    public required int UserId { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
