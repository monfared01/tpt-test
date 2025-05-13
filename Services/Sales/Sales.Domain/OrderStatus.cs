using System.ComponentModel;

namespace Sales.Domain;

public enum OrderStatus
{
    [Description("Pending")]
    Pending = 1,
    [Description("Shipped")]
    Shipped = 2,
    [Description("Delivered")]
    Delivered = 3,
    [Description("Cancelled")]
    Cancelled = 4
}
