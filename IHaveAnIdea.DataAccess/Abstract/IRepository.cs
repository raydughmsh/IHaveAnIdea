using IHaveAnIdea.Entity.Abstract;
using System.Linq.Expressions;

namespace IHaveAnIdea.DataAccess.Abstract;

public interface IRepository<T> where T : class, IEntity
{
    T? GetById(int id);
    T? Get(Expression<Func<T, bool>> filter);
    IList<T> GetAll(Expression<Func<T, bool>>? filter = null);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    bool Any(Expression<Func<T, bool>> filter);
}
