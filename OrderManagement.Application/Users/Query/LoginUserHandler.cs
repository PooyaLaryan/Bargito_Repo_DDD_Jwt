using MediatR;
using OrderManagement.Domain.Dtos;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Security;
using OrderManagement.Domain.Repositories.Users.Query;

namespace OrderManagement.Application.Users.Query;
public record LoginUserCommand(string Email, string Password) : IRequest<LoginUserResult>;
public record LoginUserResult(string Token, string FullName, UserRole Role);

public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResult>
{
    private readonly ILoginQueryRepository _loginQueryRepository;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginUserHandler(ILoginQueryRepository loginQueryRepository, ITokenGenerator tokenGenerator)
    {
        _loginQueryRepository = loginQueryRepository;
        _tokenGenerator = tokenGenerator;
    }
    public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _loginQueryRepository.GetUserByEmailAsync(request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            throw new UnauthorizedAccessException("Invalid credentials");

        var token = _tokenGenerator.GenerateToken(user);

        return new LoginUserResult
        (
            Token: token,
            FullName: user.FullName,
            Role: user.Role
        );
    }
}
