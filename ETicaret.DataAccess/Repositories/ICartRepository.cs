using ETicaret.Entity.Entities;

namespace ETicaret.DataAccess.Repositories;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> GetByUserIdAsync(string userId);
}