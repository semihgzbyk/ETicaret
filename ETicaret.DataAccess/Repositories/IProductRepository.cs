using ETicaret.Entity.Entities;

namespace ETicaret.DataAccess.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetAllWithDetailsAsync();
    Task<Product?> GetByIdWithDetailsAsync(int id);
    Task<List<Product>> GetFilteredAsync(int? categoryId, int? brandId, decimal? minPrice, decimal? maxPrice, string? search);
}