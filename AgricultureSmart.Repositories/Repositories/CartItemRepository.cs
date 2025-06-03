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
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(AgricultureSmartDbContext context) : base(context)
        {
        }

        public async Task<CartItem?> GetCartItemAsync(int cartId, int productId)
        {
            return await _dbSet
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            return await _dbSet
                .Include(ci => ci.Product)
                .Where(ci => ci.CartId == cartId)
                .ToListAsync();
        }

        public async Task<int> GetCartItemCountAsync(int cartId)
        {
            return await _dbSet
                .Where(ci => ci.CartId == cartId)
                .SumAsync(ci => ci.Quantity);
        }

        public async Task<bool> RemoveAllCartItemsAsync(int cartId)
        {
            var cartItems = await _dbSet
                .Where(ci => ci.CartId == cartId)
                .ToListAsync();

            if (!cartItems.Any())
                return false;

            _dbSet.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return true;
        }

        // Override GetByIdAsync to include Product
        public async new Task<CartItem?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        // Override GetAllAsync to include Product
        public async new Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await _dbSet
                .Include(ci => ci.Product)
                .ToListAsync();
        }
    }
} 