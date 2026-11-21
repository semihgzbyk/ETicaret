using ETicaret.Entity.Entities;

namespace ETicaret.Business.Services;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetFilteredAsync(int? categoryId, int? brandId, decimal? minPrice, decimal? maxPrice, string? search);
    Task<Product?> GetByIdAsync(int id);
    Task<bool> CreateAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}