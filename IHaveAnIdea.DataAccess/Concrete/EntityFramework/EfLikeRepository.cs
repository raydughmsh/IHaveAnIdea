using IHaveAnIdea.DataAccess.Abstract;
using IHaveAnIdea.DataAccess.Context;
using IHaveAnIdea.Entity.Concrete;

namespace IHaveAnIdea.DataAccess.Concrete.EntityFramework;

public class EfLikeRepository : EfRepositoryBase<Like>, ILikeRepository
{
    public EfLikeRepository(AppDbContext context) : base(context)
    {
    }

    public Like? GetByPostAndUser(int postId, string userId)
    {
        return _context.Likes
            .FirstOrDefault(l => l.PostId == postId && l.AppUserId == userId);
    }

    public int GetLikeCount(int postId)
    {
        return _context.Likes.Count(l => l.PostId == postId);
    }
}
