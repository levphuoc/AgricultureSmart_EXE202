/*using AgricultureSmart.API.Models;
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

        *//*private void SetAccessTokenCookie(string token, DateTime expiration)
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
        }*//*

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

        *//*private CookieOptions BuildCookieOptions(DateTime expires)
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
        }*//*

        private CookieOptions BuildCookieOptions(DateTime expires)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
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
}*/


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

            // ? Set Access Token Cookie v?i th?i gian c? th?
            SetAccessTokenCookie(result.Token, result.Expiration);

            // ? Set Refresh Token Cookie v?i th?i gian dài h?n
            var refreshToken = _authService.GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(30); // 30 ngày
            SetRefreshTokenCookie(refreshToken, refreshTokenExpiration);

            return Ok(new JwtResponse
            {
                Username = result.User.UserName,
                Email = result.User.Email,
                Role = result.RoleName,
                Expiration = result.Expiration
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized(new { Message = "Refresh token not found" });
            }

            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (!result.Success)
            {
                ClearAuthCookies();
                return Unauthorized(new { Message = result.Message });
            }

            // ? Set cookie m?i v?i th?i gian m?i
            SetAccessTokenCookie(result.Token, result.Expiration);

            var newRefreshToken = _authService.GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(30);
            SetRefreshTokenCookie(newRefreshToken, refreshTokenExpiration);

            return Ok(new { Message = "Token refreshed successfully" });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            ClearAuthCookies();
            return Ok(new { Message = "Logged out successfully" });
        }

        private void SetAccessTokenCookie(string token, DateTime expiration)
        {
            Response.Cookies.Append("accessToken", token, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddHours(1), // 1 gi? cho access token
                HttpOnly = true, // B?o m?t - ch? server truy c?p ???c
                Secure = true, // Do ?ang dev trên localhost
                SameSite = SameSiteMode.None,
                Path = "/"
            });
        }

        private void SetRefreshTokenCookie(string token, DateTime expiration)
        {
            Response.Cookies.Append("refreshToken", token, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30), // 30 ngày cho refresh token
                HttpOnly = true, // B?o m?t - ch? server truy c?p ???c
                Secure = false, // Do ?ang dev trên localhost
                SameSite = SameSiteMode.None,
                Path = "/"
            });
        }

        private void ClearAuthCookies()
        {
            Response.Cookies.Append("accessToken", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1), // Set th?i gian trong quá kh? ?? xóa
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            });

            Response.Cookies.Append("refreshToken", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1), // Set th?i gian trong quá kh? ?? xóa
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            });
        }

        [HttpGet("check-auth")]
        public IActionResult CheckAuth()
        {
            var hasAccessToken = Request.Cookies.TryGetValue("accessToken", out var accessToken);
            var hasRefreshToken = Request.Cookies.TryGetValue("refreshToken", out var refreshToken);

            return Ok(new
            {
                IsAuthenticated = hasAccessToken,
                HasAccessToken = hasAccessToken,
                HasRefreshToken = hasRefreshToken,
                AccessTokenLength = hasAccessToken ? accessToken.Length : 0,
                RefreshTokenLength = hasRefreshToken ? refreshToken.Length : 0
            });
        }
    }
}


