using Microsoft.EntityFrameworkCore;
using Strum.Core.Entities;
using Strum.Core.Interfaces.Repositories;

namespace Strum.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    
    public UserRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task CreateAsync(User user)
    {
        if (user == null)
            throw new ArgumentException("user cannot be null");
        await _context.Users.AddAsync(user);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}

