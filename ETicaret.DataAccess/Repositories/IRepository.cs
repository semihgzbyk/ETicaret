using System.Linq.Expressions;
using ETicaret.Entity.Common;

namespace ETicaret.DataAccess.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> Query();
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> SaveChangesAsync();
}