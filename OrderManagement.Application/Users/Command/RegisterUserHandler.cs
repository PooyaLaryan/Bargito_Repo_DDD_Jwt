using MediatR;
using OrderManagement.Domain.Dtos;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Users.Command;

namespace OrderManagement.Application.Users.Command;

public record RegisterUserResult(Guid id);
public record RegisterUserCommand(string FullName, string Email, string Password, UserRole UserRole) : IRequest<RegisterUserResult>;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly IUserCommandRepository _userCommandRepository;

    public RegisterUserHandler(IUserCommandRepository userCommandRepository)
    {
        _userCommandRepository = userCommandRepository;
    }

    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        string hashed = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User(request.FullName, request.Email, hashed, request.UserRole);
        var id = await _userCommandRepository.RegisterAsync(user);

        return new RegisterUserResult(id);
    }
}
