using ETicaret.Entity.Entities;

namespace ETicaret.WebUI.Models;

public class ProductListViewModel
{
    public List<Product> Products { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public List<Brand> Brands { get; set; } = new();

    // Seçili filtreler
    public int? CategoryId { get; set; }
    public int? BrandId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Search { get; set; }

    // Sayfalama
    public int Page { get; set; } = 1;
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
}