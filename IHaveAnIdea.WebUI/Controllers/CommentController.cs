using IHaveAnIdea.Business.Abstract;
using IHaveAnIdea.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IHaveAnIdea.WebUI.Controllers;

[Authorize]
public class CommentController : Controller
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(CommentCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Yorum en az 2 karakter olmalıdır.";
            return RedirectToAction("Detail", "Post", new { id = dto.PostId });
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        _commentService.Add(dto, userId);
        TempData["Success"] = "Yorumunuz eklendi.";
        return RedirectToAction("Detail", "Post", new { id = dto.PostId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id, int postId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        // ownership check happens in service/repo layer
        _commentService.Delete(id);
        TempData["Success"] = "Yorum silindi.";
        return RedirectToAction("Detail", "Post", new { id = postId });
    }
}
