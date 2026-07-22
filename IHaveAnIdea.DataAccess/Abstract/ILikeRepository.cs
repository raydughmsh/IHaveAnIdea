using IHaveAnIdea.Entity.Concrete;

namespace IHaveAnIdea.DataAccess.Abstract;

public interface ILikeRepository : IRepository<Like>
{
    Like? GetByPostAndUser(int postId, string userId);
    int GetLikeCount(int postId);
}
