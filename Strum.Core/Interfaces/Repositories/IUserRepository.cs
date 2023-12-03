using Strum.Core.Entities;

namespace Strum.Core.Interfaces.Repositories;

public interface IUserRepository
{
    public Task CreateAsync(User user);
    public Task SaveAsync();
}