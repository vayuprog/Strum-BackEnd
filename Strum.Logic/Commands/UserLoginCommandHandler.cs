using AutoMapper;
using MediatR;
using Strum.Core.Entities;
using Strum.Core.Interfaces.Repositories;

namespace Strum.Logic.Commands;

public class UserLoginRequest : IRequest
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}



public class UserLoginCommandHandler : IRequestHandler<UserLoginRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserLoginCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task Handle(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        await _userRepository.CreateAsync(user);
        await _userRepository.SaveAsync();
    }
    
}