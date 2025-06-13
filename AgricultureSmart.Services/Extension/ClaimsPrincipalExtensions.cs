using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Extension
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>Lấy userId từ JWT. Trả về 0 nếu không tìm thấy.</summary>
        public static int GetUserId(this ClaimsPrincipal user)
        {
            // Ưu tiên chuẩn NameIdentifier, fallback “sub” hoặc “uid” tuỳ bạn ký token.
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)
                        ?? user.FindFirst("sub")
                        ?? user.FindFirst("uid");

            return idClaim != null && int.TryParse(idClaim.Value, out var id) ? id : 0;
        }
    }
}
