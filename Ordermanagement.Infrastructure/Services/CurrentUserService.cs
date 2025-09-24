using Microsoft.AspNetCore.Http;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Users;
using System.Security.Claims;

namespace Ordermanagement.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var ctx = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("No HttpContext");
            var sub = ctx.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? ctx.User.FindFirst("sub")?.Value
            ?? throw new InvalidOperationException("User id claim not found");
            return Guid.Parse(sub);
        }
    }

    public UserRole Role
    {
        get
        {
            var ctx = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("No HttpContext");
            var roleStr = ctx.User.FindFirst(ClaimTypes.Role)?.Value ?? ctx.User.FindFirst("role")?.Value;
            if (string.IsNullOrWhiteSpace(roleStr)) throw new InvalidOperationException("Role claim not found");


            return Enum.TryParse<UserRole>(roleStr, ignoreCase: true, out var role) ? role : throw new InvalidOperationException("Invalid role claim");
        }
    }
}
