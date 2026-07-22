using IHaveAnIdea.Business.Abstract;
using IHaveAnIdea.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IHaveAnIdea.WebUI.Controllers;

public class PostController : Controller
{
    private readonly IPostService _postService;
    private readonly ICategoryService _categoryService;

    public PostController(IPostService postService, ICategoryService categoryService)
    {
        _postService = postService;
        _categoryService = categoryService;
    }

    // GET: /Post/Detail/5
    public IActionResult Detail(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var post = _postService.GetById(id, userId);
        if (post == null) return NotFound();

        ViewBag.CommentDto = new CommentCreateDto { PostId = id };
        return View(post);
    }

    // GET: /Post/Create
    [Authorize]
    public IActionResult Create()
    {
        ViewBag.Categories = _categoryService.GetAll();
        return View();
    }

    // POST: /Post/Create
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PostCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _categoryService.GetAll();
            return View(dto);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        _postService.Add(dto, userId);
        TempData["Success"] = "Gönderiniz başarıyla paylaşıldı!";
        return RedirectToAction("Index", "Home");
    }

    // GET: /Post/Edit/5
    [Authorize]
    public IActionResult Edit(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var post = _postService.GetById(id, userId);
        if (post == null) return NotFound();
        if (post.AppUserId != userId)
        {
            TempData["Error"] = "Bu gönderiyi düzenleme yetkiniz yok.";
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Categories = _categoryService.GetAll();
        var dto = new PostEditDto { Id = post.Id, Title = post.Title, Content = post.Content, CategoryId = post.CategoryId };
        return View(dto);
    }

    // POST: /Post/Edit/5
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PostEditDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _categoryService.GetAll();
            return View(dto);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var post = _postService.GetById(dto.Id, userId);
        if (post == null || post.AppUserId != userId)
        {
            TempData["Error"] = "Bu gönderiyi düzenleme yetkiniz yok.";
            return RedirectToAction("Index", "Home");
        }

        _postService.Update(dto);
        TempData["Success"] = "Gönderi güncellendi.";
        return RedirectToAction("Detail", new { id = dto.Id });
    }

    // POST: /Post/Delete/5
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var post = _postService.GetById(id, userId);
        if (post == null || post.AppUserId != userId)
        {
            TempData["Error"] = "Bu gönderiyi silme yetkiniz yok.";
            return RedirectToAction("Index", "Home");
        }

        _postService.Delete(id);
        TempData["Success"] = "Gönderi silindi.";
        return RedirectToAction("Index", "Home");
    }
}
