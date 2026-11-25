using ETicaret.Business.Services;
using ETicaret.DataAccess.Repositories;
using ETicaret.Entity.Entities;
using ETicaret.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Controllers;

public class ProductsController : Controller
{
    private const int PageSize = 12;

    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IRepository<Brand> _brandRepository;

    public ProductsController(
        IProductService productService,
        ICategoryService categoryService,
        IRepository<Brand> brandRepository)
    {
        _productService = productService;
        _categoryService = categoryService;
        _brandRepository = brandRepository;
    }

    public async Task<IActionResult> Index(
        int? categoryId, int? brandId, decimal? minPrice, decimal? maxPrice,
        string? search, int page = 1)
    {
        var filtered = (await _productService
            .GetFilteredAsync(categoryId, brandId, minPrice, maxPrice, search)).ToList();

        var totalPages = (int)Math.Ceiling(filtered.Count / (double)PageSize);
        page = Math.Clamp(page, 1, Math.Max(totalPages, 1));

        var model = new ProductListViewModel
        {
            Products = filtered.Skip((page - 1) * PageSize).Take(PageSize).ToList(),
            Categories = (await _categoryService.GetAllAsync()).ToList(),
            Brands = (await _brandRepository.GetAllAsync()).OrderBy(b => b.Name).ToList(),
            CategoryId = categoryId,
            BrandId = brandId,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Search = search,
            Page = page,
            TotalPages = totalPages,
            TotalCount = filtered.Count
        };

        return View(model);
    }
}