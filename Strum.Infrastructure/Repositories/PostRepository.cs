using Strum.Core.Interfaces.Repositories;
using Strum.Core.Entities;
using Strum.Core.Interfaces.Repositories;

namespace Strum.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly DataContext _context;
    
    public PostRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task CreateAsync(Post post)
    {
        if (post == null)
            throw new ArgumentException("user cannot be null");
        await _context.Post.AddAsync(post);
    }

    public async Task SaveAsync()
    {
        throw new Exception("nothing to save");
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Post post)                   
    {
        if (post == null)
            throw new ArgumentException("user cannot be null");
        _context.Post.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Post post)
    {
        throw new NotImplementedException();
    }
}