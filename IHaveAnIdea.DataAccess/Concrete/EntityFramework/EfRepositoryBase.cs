using IHaveAnIdea.DataAccess.Abstract;
using IHaveAnIdea.DataAccess.Context;
using IHaveAnIdea.Entity.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IHaveAnIdea.DataAccess.Concrete.EntityFramework;

public class EfRepositoryBase<T> : IRepository<T> where T : class, IEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public EfRepositoryBase(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public T? Get(Expression<Func<T, bool>> filter)
    {
        return _dbSet.FirstOrDefault(filter);
    }

    public IList<T> GetAll(Expression<Func<T, bool>>? filter = null)
    {
        return filter == null
            ? _dbSet.ToList()
            : _dbSet.Where(filter).ToList();
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }

    public bool Any(Expression<Func<T, bool>> filter)
    {
        return _dbSet.Any(filter);
    }
}
