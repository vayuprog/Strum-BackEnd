using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strum.Core.Entities;
using Strum.Infrastructure;

namespace Strum_Back.Controllers;

[ApiController]
[Route("api/post/")]

public class PostController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IMediator _mediator;
    
    public PostController(DataContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    [HttpGet("GetPost")]
    public async Task<ActionResult<IEnumerable<Post>>> GetPost()
    {
        var posts = await _context.Post.ToListAsync();

        if (posts == null || posts.Count == 0)
        {
            return NotFound("No users found");
        }

        return Ok(posts);
    }
    [HttpPost("AddPost")]
    public async Task AddPost()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("UpdatePost")]
    public async Task UpdatePost()
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("DeletePost")]
    public async Task DeletePost()
    {
        throw new NotImplementedException();
    }
}