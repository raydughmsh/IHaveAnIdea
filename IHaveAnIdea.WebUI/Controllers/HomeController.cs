using IHaveAnIdea.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IHaveAnIdea.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IPostService _postService;
    private readonly ICategoryService _categoryService;

    public HomeController(IPostService postService, ICategoryService categoryService)
    {
        _postService = postService;
        _categoryService = categoryService;
    }

    public IActionResult Index(int? categoryId, string? search)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        ViewBag.Categories = _categoryService.GetAll();
        ViewBag.SelectedCategory = categoryId;
        ViewBag.Search = search;

        var posts = !string.IsNullOrWhiteSpace(search)
            ? _postService.Search(search, userId)
            : categoryId.HasValue
                ? _postService.GetByCategory(categoryId.Value, userId)
                : _postService.GetAll(userId);

        return View(posts);
    }
}
