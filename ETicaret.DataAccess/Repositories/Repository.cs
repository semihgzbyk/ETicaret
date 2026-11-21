using System.Linq.Expressions;
using ETicaret.DataAccess.Context;
using ETicaret.Entity.Common;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IQueryable<T> Query() => _dbSet.AsQueryable();

    public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.Where(predicate).ToListAsync();

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        _dbSet.Update(entity);
    }

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}