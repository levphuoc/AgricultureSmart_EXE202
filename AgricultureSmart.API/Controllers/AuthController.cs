/*using AgricultureSmart.API.Models;
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

            return Ok(new JwtResponse
            {
                Token = result.Token,
                Expiration = result.Expiration,
                Username = result.User.UserName,
                Email = result.User.Email,
                Role = result.RoleName
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
} */


using AgricultureSmart.API.Models;
using AgricultureSmart.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthServices authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
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

            // Set access token in HTTP-only cookie
            SetAccessTokenCookie(result.Token, result.Expiration);

            // Generate and set refresh token in HTTP-only cookie
            var refreshToken = _authService.GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(7); // 7 days for refresh token
            SetRefreshTokenCookie(refreshToken, refreshTokenExpiration);

            // Return user info without exposing tokens in response body

            return Ok(new JwtResponse
            {
                Token = result.Token,
                Expiration = result.Expiration,
                Username = result.User.UserName,
                Email = result.User.Email,
                Role = result.RoleName,
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            // Get the refresh token from the cookie
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized(new { Message = "Refresh token not found" });
            }

            // Validate the refresh token and generate new tokens
            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (!result.Success)
            {
                // Clear cookies if refresh token is invalid
                ClearAuthCookies();
                return Unauthorized(new { Message = result.Message });
            }

            // Set new access token in HTTP-only cookie
            SetAccessTokenCookie(result.Token, result.Expiration);

            // Set new refresh token in HTTP-only cookie
            var newRefreshToken = _authService.GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);
            SetRefreshTokenCookie(newRefreshToken, refreshTokenExpiration);

            return Ok(new { Message = "Token refreshed successfully" });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Clear the auth cookies
            ClearAuthCookies();

            return Ok(new { Message = "Logged out successfully" });
        }

        /*private void SetAccessTokenCookie(string token, DateTime expiration)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to true in production with HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = expiration,
                Path = "/"
            };

            Response.Cookies.Append("accessToken", token, cookieOptions);
        }

        private void SetRefreshTokenCookie(string token, DateTime expiration)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to true in production with HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = expiration,
                Path = "/"
            };

            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }*/

        private void ClearAuthCookies()
        {
            Response.Cookies.Delete("accessToken", new CookieOptions { Path = "/" });
            Response.Cookies.Delete("refreshToken", new CookieOptions { Path = "/" });
        }

        [HttpGet("check-cookies")]
        public IActionResult CheckCookies()
        {
            var hasAccessToken = Request.Cookies.TryGetValue("accessToken", out var accessToken);
            var hasRefreshToken = Request.Cookies.TryGetValue("refreshToken", out var refreshToken);

            return Ok(new
            {
                HasAccessToken = hasAccessToken,
                HasRefreshToken = hasRefreshToken,
                AccessTokenValue = hasAccessToken ? accessToken : "N/A",
                RefreshTokenValue = hasRefreshToken ? refreshToken : "N/A"
            });
        }

        /*private CookieOptions BuildCookieOptions(DateTime expires)
        {
            bool isHttps = HttpContext.Request.IsHttps;

            return new CookieOptions
            {
                HttpOnly = true,
                Secure = isHttps,
                SameSite = isHttps ? SameSiteMode.None : SameSiteMode.Lax,
                Expires = expires,
                Path = "/"
            };
        }*/

        private CookieOptions BuildCookieOptions(DateTime expires)
        {
            bool isHttps = HttpContext.Request.IsHttps;

            return new CookieOptions
            {
                HttpOnly = true,
                Secure = false, 
                SameSite = SameSiteMode.None, 
                Expires = expires,
                Path = "/"
            };
        }


        private void SetAccessTokenCookie(string token, DateTime expiration)
        {
            Response.Cookies.Append("accessToken", token, BuildCookieOptions(expiration));
        }

        private void SetRefreshTokenCookie(string token, DateTime expiration)
        {
            Response.Cookies.Append("refreshToken", token, BuildCookieOptions(expiration));
        }

    }
}
