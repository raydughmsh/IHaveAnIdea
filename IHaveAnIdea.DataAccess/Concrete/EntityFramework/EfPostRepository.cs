using IHaveAnIdea.DataAccess.Abstract;
using IHaveAnIdea.DataAccess.Context;
using IHaveAnIdea.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace IHaveAnIdea.DataAccess.Concrete.EntityFramework;

public class EfPostRepository : EfRepositoryBase<Post>, IPostRepository
{
    public EfPostRepository(AppDbContext context) : base(context)
    {
    }

    public IList<Post> GetAllWithDetails()
    {
        return _context.Posts
            .Include(p => p.AppUser)
            .Include(p => p.Category)
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.CreatedAt)
            .ToList();
    }

    public Post? GetByIdWithDetails(int id)
    {
        return _context.Posts
            .Include(p => p.AppUser)
            .Include(p => p.Category)
            .Include(p => p.Comments)
                .ThenInclude(c => c.AppUser)
            .Include(p => p.Likes)
            .FirstOrDefault(p => p.Id == id && p.IsActive);
    }

    public IList<Post> GetByCategoryWithDetails(int categoryId)
    {
        return _context.Posts
            .Include(p => p.AppUser)
            .Include(p => p.Category)
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .Where(p => p.CategoryId == categoryId && p.IsActive)
            .OrderByDescending(p => p.CreatedAt)
            .ToList();
    }

    public IList<Post> GetByUserWithDetails(string userId)
    {
        return _context.Posts
            .Include(p => p.Category)
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .Where(p => p.AppUserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToList();
    }
}
