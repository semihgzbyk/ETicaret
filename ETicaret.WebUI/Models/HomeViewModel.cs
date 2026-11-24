
using ETicaret.Entity.Entities;

namespace ETicaret.WebUI.Models;

public class HomeViewModel
{
    public List<Product> FeaturedProducts { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
}