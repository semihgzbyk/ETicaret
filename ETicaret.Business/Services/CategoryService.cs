using ETicaret.DataAccess.Repositories;
using ETicaret.Entity.Entities;

namespace ETicaret.Business.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepo;
    private readonly IRepository<Product> _productRepo;

    public CategoryService(IRepository<Category> categoryRepo, IRepository<Product> productRepo)
    {
        _categoryRepo = categoryRepo;
        _productRepo = productRepo;
    }

    public async Task<List<Category>> GetAllAsync()
        => await _categoryRepo.GetAllAsync();

    public async Task<Category?> GetByIdAsync(int id)
        => await _categoryRepo.GetByIdAsync(id); // expression-bodied member :Süslü parantez ve return yerine => yazıyorsunuz. Derleyici için hiçbir fark yok, sadece daha kısa.

    public async Task<bool> CreateAsync(Category category)
    {
        var sameName = await _categoryRepo.FindAsync(c => c.Name == category.Name);
        if (sameName.Any())
            return false;

        await _categoryRepo.AddAsync(category);
        await _categoryRepo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Category category)
    {
        var sameName = await _categoryRepo.FindAsync(c => c.Name == category.Name && c.Id != category.Id);
        if (sameName.Any())
            return false;

        _categoryRepo.Update(category);
        await _categoryRepo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _categoryRepo.GetByIdAsync(id);
        if (category is null)
            return false;

        var hasProducts = await _productRepo.FindAsync(p => p.CategoryId == id);
        if (hasProducts.Any())
            return false;

        _categoryRepo.Delete(category);
        await _categoryRepo.SaveChangesAsync();
        return true;
    }
}