using IHaveAnIdea.Entity.Concrete;

namespace IHaveAnIdea.DataAccess.Abstract;

public interface IPostRepository : IRepository<Post>
{
    IList<Post> GetAllWithDetails();
    Post? GetByIdWithDetails(int id);
    IList<Post> GetByCategoryWithDetails(int categoryId);
    IList<Post> GetByUserWithDetails(string userId);
}
