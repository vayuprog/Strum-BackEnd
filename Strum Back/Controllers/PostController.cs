using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp.Portable;
using Strum.Core.Entities;
using Strum.Infrastructure;
using Strum_Back.Models;

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

    [HttpGet("GetPosts")]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        var posts = await _context.Post.ToListAsync();

        if (posts == null || posts.Count == 0)
        {
            return NotFound("No posts found");
        }

        return Ok(posts);
    }


    [HttpPost("AddPost")]
    public async Task<ActionResult<Post>> AddPost([FromForm] PostRequestModel postRequest)
    {
        if (postRequest == null)
        {
            return BadRequest("Invalid post data");
        }

        byte[] imageData = null;

        if (postRequest.PostImage != null)
        {
            using (var binaryReader = new BinaryReader(postRequest.PostImage.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)postRequest.PostImage.Length);
            }
        }

        var newPost = new Post
        {
            Text = postRequest.Text,
            UserId = postRequest.UserId,
            PostImage = imageData,
            DatePosted = DateTime.UtcNow
        };

        // Add the new post to the context and save changes
        _context.Post.Add(newPost);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPosts), new { id = newPost.Id }, newPost);
    }


    [HttpPut("UpdatePost/{postId}")]
    public async Task<ActionResult<Post>> UpdatePost(int postId, [FromBody] PostEditModel updatedPost)
    {
        var existingPost = await _context.Post.FindAsync(postId);

        if (existingPost == null)
        {
            return NotFound("Post not found");
        }
        byte[] imageData = null;

        if (updatedPost.PostImage != null)
        {
            using (var binaryReader = new BinaryReader(updatedPost.PostImage.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)updatedPost.PostImage.Length);
            }
        }
        // Update the properties you want to modify
        existingPost.Text = updatedPost.Text;
        existingPost.PostImage = imageData;

        _context.Entry(existingPost).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(existingPost);
    }

    [HttpDelete("DeletePost/{postId}")]
    public async Task<ActionResult> DeletePost(int postId)
    {
        var postToDelete = await _context.Post.FindAsync(postId);

        if (postToDelete == null)
        {
            return NotFound("Post not found");
        }

        _context.Post.Remove(postToDelete);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}