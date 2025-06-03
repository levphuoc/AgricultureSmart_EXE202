using AgricultureSmart.Services.Models.CartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(int userId);
        Task<CartItemDto> AddToCartAsync(int userId, AddToCartDto addToCartDto);
        Task<CartItemDto?> UpdateCartItemAsync(int userId, int cartItemId, UpdateCartItemDto updateCartItemDto);
        Task<bool> RemoveCartItemAsync(int userId, int cartItemId);
        Task<bool> ClearCartAsync(int userId);
        Task<int> GetCartItemCountAsync(int userId);
    }
} 