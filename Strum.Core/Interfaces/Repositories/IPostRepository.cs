using Strum.Core.Entities;

namespace Strum.Core.Interfaces.Repositories;

public interface IPostRepository
{
    public Task CreateAsync(Post post);
    public Task SaveAsync();
    public Task DeleteAsync(Post post);
    public Task UpdateAsync(Post post);
}