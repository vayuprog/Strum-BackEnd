using AutoMapper;
using MediatR;
using Strum.Core.Interfaces.Repositories;

namespace Strum.Logic.Commands;

public class UserUpdateRequest : IRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserUpdateCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task Handle(UserUpdateRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        _mapper.Map(request, user);
        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveAsync();
    }
}
