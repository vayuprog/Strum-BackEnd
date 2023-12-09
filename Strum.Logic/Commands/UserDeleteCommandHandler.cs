using MediatR;
using Strum.Core.Interfaces.Repositories;

namespace Strum.Logic.Commands;

public class UserDeleteRequest : IRequest
{
    public int Id { get; set; }
}

public class UserDeleteCommandHandler : IRequestHandler<UserDeleteRequest>
{
    private readonly IUserRepository _userRepository;

    public UserDeleteCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UserDeleteRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        await _userRepository.DeleteAsync(user);
        await _userRepository.SaveAsync();
    }
}