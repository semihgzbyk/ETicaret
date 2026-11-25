using ETicaret.Entity.Entities;

namespace ETicaret.WebUI.Models;

public class ProductDetailViewModel
{
    public Product Product { get; set; } = null!;
    public List<Product> RelatedProducts { get; set; } = new();
}