using Strum.Core.Entities;

namespace Strum.Core.Interfaces.Repositories;

public interface IUserRepository
{
    public Task CreateAsync(User user);
    public Task SaveAsync();
    public Task DeleteAsync(User user);
    public Task UpdateAsync(User user);
    public Task<User> GetByIdAsync(int id);
    
}