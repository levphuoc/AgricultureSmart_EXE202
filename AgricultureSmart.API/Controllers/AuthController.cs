using AgricultureSmart.API.Models;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;

        public AuthController(IAuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Use the RoleId from the model if provided, otherwise default to Farmer (3)
            int roleId = model.RoleId != 0 ? model.RoleId : 3;

            var result = await _authService.RegisterUserAsync(
                model.Username,
                model.Email,
                model.Password,
                model.Address,
                model.PhoneNumber,
                roleId);

            if (!result.Success)
            {
                return BadRequest(new { Message = result.Message });
            }

            return Ok(new { Message = result.Message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(model.Username, model.Password);

            if (!result.Success)
            {
                return Unauthorized(new { Message = result.Message });
            }

            // Get the role name from the user's roles
            var role = result.User.UserRoles?.FirstOrDefault(ur => ur.RoleId == 2 || ur.RoleId == 3)?.Role?.Name ?? "";

            return Ok(new JwtResponse
            {
                Token = result.Token,
                Expiration = result.Expiration,
                Username = result.User.UserName,
                Email = result.User.Email,
                Role = role
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            // Since we're using JWT without refresh tokens, the actual token invalidation
            // happens on the client side by removing the stored token.
            // This endpoint is provided for any additional server-side logout logic.
            
            return Ok(new { Message = "Logged out successfully" });
        }
    }
} 