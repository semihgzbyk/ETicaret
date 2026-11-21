using ETicaret.DataAccess.Context;
using ETicaret.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.DataAccess.Repositories;

public class CartRepository : Repository<Cart>, ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Cart?> GetByUserIdAsync(string userId)
    {
        return await _context.Carts
            .Where(c => c.AppUserId == userId)
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync();
    }
}