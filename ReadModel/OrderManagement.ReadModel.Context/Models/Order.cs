using System;
using System.Collections.Generic;

namespace OrderManagement.ReadModel.Context.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Items { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
