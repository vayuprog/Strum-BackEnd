using AutoMapper;
using MediatR;
using Strum.Core.Entities;
using Strum.Core.Interfaces.Repositories;

namespace Strum.Logic.Commands;

public class UserCreateRequest : IRequest
{
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string Email { get; set; } 
    public string PasswordHash { get; set; } 
}

public class UserCreateCommandHandler : IRequestHandler<UserCreateRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserCreateCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task Handle(UserCreateRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        await _userRepository.CreateAsync(user);
        await _userRepository.SaveAsync();
    }
    
} 