using ETicaret.DataAccess.Repositories;
using ETicaret.Entity.Entities;

namespace ETicaret.Business.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepo;

    public ProductService(IProductRepository productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task<List<Product>> GetAllAsync()
        => await _productRepo.GetAllWithDetailsAsync();

    public async Task<List<Product>> GetFilteredAsync(
        int? categoryId, int? brandId, decimal? minPrice, decimal? maxPrice, string? search)
        => await _productRepo.GetFilteredAsync(categoryId, brandId, minPrice, maxPrice, search);

    public async Task<Product?> GetByIdAsync(int id)
        => await _productRepo.GetByIdWithDetailsAsync(id);

    public async Task<bool> CreateAsync(Product product)
    {
        if (product.Price <= 0 || product.Stock < 0)
            return false;
        if (product.DiscountPrice.HasValue && product.DiscountPrice >= product.Price)
            return false;

        await _productRepo.AddAsync(product);
        await _productRepo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        if (product.Price <= 0 || product.Stock < 0)
            return false;
        if (product.DiscountPrice.HasValue && product.DiscountPrice >= product.Price)
            return false;

        _productRepo.Update(product);
        await _productRepo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product is null)
            return false;

        _productRepo.Delete(product);
        await _productRepo.SaveChangesAsync();
        return true;
    }
}