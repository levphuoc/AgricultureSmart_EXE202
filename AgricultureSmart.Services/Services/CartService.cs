using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.CartModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CartService> _logger;

        public CartService(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IProductRepository productRepository,
            IMapper mapper,
            ILogger<CartService> logger)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CartDto> GetCartAsync(int userId)
        {
            try
            {
                // Get the user's cart or create a new one if it doesn't exist
                var cart = await _cartRepository.GetCartWithItemsByUserIdAsync(userId);
                if (cart == null)
                {
                    // Create a new cart for the user
                    cart = new Cart
                    {
                        UserId = userId,
                        TotalAmount = 0,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        CartItems = new List<CartItem>()
                    };

                    await _cartRepository.AddAsync(cart);
                }

                return _mapper.Map<CartDto>(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cart for user ID {UserId}", userId);
                throw;
            }
        }

        public async Task<CartItemDto> AddToCartAsync(int userId, AddToCartDto addToCartDto)
        {
            try
            {
                // Get the product to validate it exists and get its price
                var product = await _productRepository.GetByIdAsync(addToCartDto.ProductId);
                if (product == null)
                {
                    throw new ArgumentException($"Product with ID {addToCartDto.ProductId} not found");
                }

                // Check if product is active
                if (!product.IsActive)
                {
                    throw new InvalidOperationException($"Product with ID {addToCartDto.ProductId} is not available");
                }

                // Check if product has enough stock
                if (product.Stock < addToCartDto.Quantity)
                {
                    throw new InvalidOperationException($"Not enough stock available. Available: {product.Stock}");
                }

                // Get the user's cart or create a new one
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = userId,
                        TotalAmount = 0,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _cartRepository.AddAsync(cart);
                }

                // Check if the product is already in the cart
                var cartItem = await _cartItemRepository.GetCartItemAsync(cart.Id, addToCartDto.ProductId);
                
                if (cartItem != null)
                {
                    // Update existing cart item
                    cartItem.Quantity += addToCartDto.Quantity;
                    cartItem.UpdatedAt = DateTime.UtcNow;
                    
                    // Use discounted price if available, otherwise use regular price
                    decimal unitPrice = product.DiscountPrice.HasValue && product.DiscountPrice.Value > 0 
                        ? product.DiscountPrice.Value 
                        : product.Price;
                        
                    cartItem.UnitPrice = unitPrice;
                    cartItem.TotalPrice = unitPrice * cartItem.Quantity;

                    await _cartItemRepository.UpdateAsync(cartItem);
                }
                else
                {
                    // Create new cart item
                    cartItem = new CartItem
                    {
                        CartId = cart.Id,
                        ProductId = addToCartDto.ProductId,
                        Quantity = addToCartDto.Quantity,
                        UnitPrice = product.DiscountPrice.HasValue && product.DiscountPrice.Value > 0 
                            ? product.DiscountPrice.Value 
                            : product.Price,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    
                    cartItem.TotalPrice = cartItem.UnitPrice * cartItem.Quantity;

                    await _cartItemRepository.AddAsync(cartItem);
                }

                // Update cart total
                await _cartRepository.UpdateCartTotalAsync(cart.Id);

                // Get the updated cart item with product details
                var updatedCartItem = await _cartItemRepository.GetByIdAsync(cartItem.Id);
                return _mapper.Map<CartItemDto>(updatedCartItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding item to cart for user ID {UserId}", userId);
                throw;
            }
        }

        public async Task<CartItemDto?> UpdateCartItemAsync(int userId, int cartItemId, UpdateCartItemDto updateCartItemDto)
        {
            try
            {
                // Get the user's cart
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null)
                {
                    return null;
                }

                // Get the cart item
                var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
                if (cartItem == null || cartItem.CartId != cart.Id)
                {
                    return null;
                }

                // Get the product to check stock
                var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
                if (product == null)
                {
                    throw new ArgumentException($"Product with ID {cartItem.ProductId} not found");
                }

                // Check if product has enough stock
                if (product.Stock < updateCartItemDto.Quantity)
                {
                    throw new InvalidOperationException($"Not enough stock available. Available: {product.Stock}");
                }

                // Update the cart item
                cartItem.Quantity = updateCartItemDto.Quantity;
                cartItem.TotalPrice = cartItem.UnitPrice * cartItem.Quantity;
                cartItem.UpdatedAt = DateTime.UtcNow;

                await _cartItemRepository.UpdateAsync(cartItem);

                // Update cart total
                await _cartRepository.UpdateCartTotalAsync(cart.Id);

                // Get the updated cart item with product details
                var updatedCartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
                return _mapper.Map<CartItemDto>(updatedCartItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating cart item ID {CartItemId} for user ID {UserId}", cartItemId, userId);
                throw;
            }
        }

        public async Task<bool> RemoveCartItemAsync(int userId, int cartItemId)
        {
            try
            {
                // Get the user's cart
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null)
                {
                    return false;
                }

                // Get the cart item
                var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
                if (cartItem == null || cartItem.CartId != cart.Id)
                {
                    return false;
                }

                // Remove the cart item
                await _cartItemRepository.DeleteAsync(cartItemId);

                // Update cart total
                await _cartRepository.UpdateCartTotalAsync(cart.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cart item ID {CartItemId} for user ID {UserId}", cartItemId, userId);
                throw;
            }
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            try
            {
                // Get the user's cart
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null)
                {
                    return false;
                }

                // Remove all cart items
                var result = await _cartItemRepository.RemoveAllCartItemsAsync(cart.Id);
                
                if (result)
                {
                    // Update cart total to zero
                    cart.TotalAmount = 0;
                    cart.UpdatedAt = DateTime.UtcNow;
                    await _cartRepository.UpdateAsync(cart);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cart for user ID {UserId}", userId);
                throw;
            }
        }

        public async Task<int> GetCartItemCountAsync(int userId)
        {
            try
            {
                // Get the user's cart
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null)
                {
                    return 0;
                }

                // Get the count of items in the cart
                return await _cartItemRepository.GetCartItemCountAsync(cart.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cart item count for user ID {UserId}", userId);
                throw;
            }
        }
    }
} 