using OrderManagement.Domain.Enums;

namespace OrderManagement.Domain.Services;

public interface ICurrentUserService
{
    Guid UserId { get; }
    UserRole Role { get; }
}
