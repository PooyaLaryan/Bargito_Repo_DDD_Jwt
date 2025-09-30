using MediatR;
using OrderManagement.Domain.Dtos;
using OrderManagement.Domain.Repositories.Users.Query;

namespace OrderManagement.Application.Users.Query;

public record GetAllUsersQuery() : IRequest<GetAllUsersResult>;
public record GetAllUsersResult(IReadOnlyList<UsersDto> Users);
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersResult>
{
    private readonly IUserQueryRepository _userQueryRepository;

    public GetAllUsersQueryHandler(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
    }
    public async Task<GetAllUsersResult> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userQueryRepository.GetAllUsersAsync(cancellationToken);
        return new GetAllUsersResult(users.Select(u => new UsersDto(u.Id, u.FullName, u.Email, u.Role.ToString())).ToList());
    }
}
