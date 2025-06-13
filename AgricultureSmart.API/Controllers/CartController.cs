using AgricultureSmart.Services.Extension;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.CartModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Farmer")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/Cart
        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            int userId = User.GetUserId();
            if (userId == 0) return Unauthorized();

            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }

        // POST: api/Cart
        [HttpPost]
        public async Task<ActionResult<CartItemDto>> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                int userId = User.GetUserId();
                if (userId == 0) return Unauthorized();

                var cartItem = await _cartService.AddToCartAsync(userId, addToCartDto);
                return Ok(cartItem);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Cart/items/5
        [HttpPut("items/{cartItemId}")]
        public async Task<ActionResult<CartItemDto>> UpdateCartItem(int cartItemId, [FromBody] UpdateCartItemDto updateCartItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                int userId = User.GetUserId();
                if (userId == 0) return Unauthorized();

                var cartItem = await _cartService.UpdateCartItemAsync(userId, cartItemId, updateCartItemDto);
                if (cartItem == null)
                {
                    return NotFound();
                }

                return Ok(cartItem);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Cart/items/5
        [HttpDelete("items/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            int userId = User.GetUserId();
            if (userId == 0) return Unauthorized();

            var result = await _cartService.RemoveCartItemAsync(userId, cartItemId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Cart
        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            int userId = User.GetUserId();
            if (userId == 0) return Unauthorized();

            var result = await _cartService.ClearCartAsync(userId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/Cart/count
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetCartItemCount()
        {
            int userId = User.GetUserId();
            if (userId == 0) return Unauthorized();

            var count = await _cartService.GetCartItemCountAsync(userId);
            return Ok(count);
        }
    }
} 