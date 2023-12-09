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
    
    public async Task DeleteAsync(User user)
    {
        if (user == null)
            throw new ArgumentException("user cannot be null");
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        if (user == null)
            throw new ArgumentException("user cannot be null");
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        // Implementation depends on your data access technology
        // For example, if you're using Entity Framework, it might look like this:
        if (id == null)
            throw new ArgumentException("user cannot be null");
        return await _context.Users.FindAsync(id);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}