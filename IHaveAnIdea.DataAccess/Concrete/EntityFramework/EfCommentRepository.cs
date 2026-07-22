using IHaveAnIdea.DataAccess.Abstract;
using IHaveAnIdea.DataAccess.Context;
using IHaveAnIdea.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace IHaveAnIdea.DataAccess.Concrete.EntityFramework;

public class EfCommentRepository : EfRepositoryBase<Comment>, ICommentRepository
{
    public EfCommentRepository(AppDbContext context) : base(context)
    {
    }

    public IList<Comment> GetByPostWithDetails(int postId)
    {
        return _context.Comments
            .Include(c => c.AppUser)
            .Where(c => c.PostId == postId)
            .OrderBy(c => c.CreatedAt)
            .ToList();
    }
}
