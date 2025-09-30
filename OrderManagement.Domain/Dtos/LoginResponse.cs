using OrderManagement.Domain.Enums;

namespace OrderManagement.Domain.Dtos;

public record LoginResponse
{
    public required string Token { get; set; }
    public required string FullName { get; set; }
    public required UserRole Role { get; set; }
}
