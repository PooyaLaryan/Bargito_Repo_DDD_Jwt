using OrderManagement.Domain.Enums;

namespace OrderManagement.Domain.Dtos;

public class UserRegisterRequest
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required UserRole Role { get; set; }
}
