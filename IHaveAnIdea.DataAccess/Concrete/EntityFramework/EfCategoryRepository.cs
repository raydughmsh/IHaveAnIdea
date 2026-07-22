using IHaveAnIdea.DataAccess.Abstract;
using IHaveAnIdea.DataAccess.Context;
using IHaveAnIdea.Entity.Concrete;

namespace IHaveAnIdea.DataAccess.Concrete.EntityFramework;

public class EfCategoryRepository : EfRepositoryBase<Category>, ICategoryRepository
{
    public EfCategoryRepository(AppDbContext context) : base(context)
    {
    }
}
