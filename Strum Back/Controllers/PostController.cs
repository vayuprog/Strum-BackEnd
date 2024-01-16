using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp.Portable;
using Strum.Core.Entities;
using Strum.Infrastructure;
using Strum_Back.Models;
using System.Security.Claims;

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
    public async Task<ActionResult<Post>> AddPost([FromBody] PostRequestModel postRequest)
    {
        if (postRequest == null)
        {
            return BadRequest("Invalid post data");
        }

        if (!ModelState.IsValid)
        {
            // Handle invalid model state
            return BadRequest(ModelState);
        }
        //byte[]? imageData = null;

        //if (postRequest.PostImage != null)
        //{
        //    using (var binaryReader = new BinaryReader(postRequest.PostImage.OpenReadStream()))
        //    {
        //        imageData = binaryReader.ReadBytes((int)postRequest.PostImage.Length);
        //    }
        //}

        var user = _context.Users.First(x => x.Id == postRequest.UserId);
        var newPost = new Post
        {
            Text = postRequest.Text,
            UserId = postRequest.UserId,
            /*PostImage = imageData,*/ // Set imageData to null if PostImage is not provided
            User = user,
            DatePosted = DateTime.UtcNow
        };

        // Add the new post to the context and save changes
        _context.Post.Add(newPost);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPosts), new { id = newPost.Id, text = newPost.Text });
    }


    [HttpPut("UpdatePost/{postId}")]
    public async Task<ActionResult<Post>> UpdatePost(int postId, [FromBody] PostRequestModel updatedPost)
    {
        var existingPost = await _context.Post
            .Include(p => p.User) // Include the User in the query
            .FirstOrDefaultAsync(p => p.Id == postId);

        if (existingPost == null)
        {
            return NotFound("Post not found");
        }

        // Get the current user ID from the claims
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Check if the current user is the owner of the post
        if (existingPost.User == null || existingPost.User.Email != currentUserId)
        {
            return Forbid("You do not have permission to edit this post");
        }

        //if (updatedPost.PostImage != null)
        //{
        //    using (var binaryReader = new BinaryReader(updatedPost.PostImage.OpenReadStream()))
        //    {
        //        existingPost.PostImage = binaryReader.ReadBytes((int)updatedPost.PostImage.Length);
        //    }
        //}

        // Update other properties
        existingPost.Text = updatedPost.Text;

        _context.Entry(existingPost).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(existingPost);
    }


    [HttpGet("GetPostsByUser/{userId}")]
    public async Task<ActionResult<IEnumerable<Post>>> GetPostsByUser(int userId)
    {
        var posts = await _context.Post
            .Where(p => p.UserId == userId)
            .ToListAsync();

        if (posts == null || posts.Count == 0)
        {
            return NotFound("No posts found for the specified user");
        }

        return Ok(posts);
    }


    [HttpDelete("DeletePost/{postId}")] // Ensure users are authenticated
    public async Task<ActionResult> DeletePost(int postId)
    {
        // Get the current user ID from the claims
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Find the post with the specified ID
        var postToDelete = await _context.Post
            .Include(p => p.User) // Include the User in the query
            .FirstOrDefaultAsync(p => p.Id == postId);

        if (postToDelete == null)
        {
            return NotFound("Post not found");
        }

        // Check if the current user is the owner of the post
        if (postToDelete.User == null || postToDelete.User.Email != currentUserId)
        {
            return Forbid("You do not have permission to delete this post");
        }

        _context.Post.Remove(postToDelete);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}