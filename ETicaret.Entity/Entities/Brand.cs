using ETicaret.Entity.Common;

namespace ETicaret.Entity.Entities;

public class Brand : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? LogoUrl { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}