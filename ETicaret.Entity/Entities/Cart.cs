using System;
using ETicaret.Entity.Common;

namespace ETicaret.Entity.Entities;

public class Cart : BaseEntity
{
    public string AppUserId { get; set; } = null!;
    public AppUser? AppUser { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = []; //(collection expression)
}