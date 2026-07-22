using IHaveAnIdea.Entity.Concrete;

namespace IHaveAnIdea.DataAccess.Abstract;

public interface ICommentRepository : IRepository<Comment>
{
    IList<Comment> GetByPostWithDetails(int postId);
}
