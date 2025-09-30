using MediatR;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Security;
using OrderManagement.Domain.Repositories.Users.Query;

namespace OrderManagement.Application.Users.Query;
public record LoginUserQuery(string Email, string Password) : IRequest<LoginUserResult>;
public record LoginUserResult(string Token, string FullName, UserRole Role);

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserResult>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginUserQueryHandler(IUserQueryRepository userQueryRepository, ITokenGenerator tokenGenerator)
    {
        _userQueryRepository = userQueryRepository;
        _tokenGenerator = tokenGenerator;
    }
    public async Task<LoginUserResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userQueryRepository.GetUserByEmailAsync(request.Email, cancellationToken);

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
