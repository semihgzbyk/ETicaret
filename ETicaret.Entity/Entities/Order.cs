using ETicaret.Entity.Common;
using ETicaret.Entity.Enums;

namespace ETicaret.Entity.Entities;

public class Order : BaseEntity
{
    public string AppUserId { get; set; } = null!;
    public AppUser? AppUser { get; set; }

    public string OrderNumber { get; set; } = null!;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string City { get; set; } = null!;
    public string District { get; set; } = null!;
    public string AddressLine { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}