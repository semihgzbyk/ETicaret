using ETicaret.Business.Services;
using ETicaret.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public HomeController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();

        var model = new HomeViewModel
        {
            FeaturedProducts = products.Where(p => p.IsFeatured).ToList(),
            Categories = await _categoryService.GetAllAsync()
        };

        return View(model);
    }
}