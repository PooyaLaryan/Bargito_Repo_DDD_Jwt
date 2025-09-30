namespace OrderManagement.Domain.Dtos;

public record UsersDto(Guid Id, string FullName, string Email, string Role);
