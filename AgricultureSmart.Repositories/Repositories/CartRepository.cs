using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AgricultureSmartDbContext context) : base(context)
        {
        }

        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Cart?> GetCartWithItemsByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<decimal> UpdateCartTotalAsync(int cartId)
        {
            var cart = await _dbSet
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
                throw new ArgumentException($"Cart with ID {cartId} not found");

            cart.TotalAmount = cart.CartItems.Sum(ci => ci.TotalPrice);
            cart.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return cart.TotalAmount;
        }

        // Override GetByIdAsync to include CartItems
        public async new Task<Cart?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
} 