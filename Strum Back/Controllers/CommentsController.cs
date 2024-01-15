using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp.Portable;
using Strum.Core.Entities;
using Strum.Infrastructure;
using Strum_Back.Models;

namespace Strum_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly DataContext _context;

        public CommentsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
          if (_context.Comments == null)
          {
              return NotFound();
          }
            return await _context.Comments.ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
          if (_context.Comments == null)
          {
              return NotFound();
          }
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, [FromBody] CommentRequestModel comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            // Check if the current user is the owner of the comment
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var existingComment = await _context.Comments
                .Include(c => c.User) // Include the User in the query
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingComment == null)
            {
                return NotFound();
            }

            if (existingComment.User == null || existingComment.User.Email != currentUserId)
            {
                return Forbid("You do not have permission to edit this comment");
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(CommentRequestModel comment)
        {
            var user = _context.Users.First(x => x.Id == comment.UserId);
            var post = _context.Post.First(x => x.Id == comment.PostId);
            if (comment.PostId != null)
            {
                // коментар для поста
                var newComment = new Comment
                {
                    Title = comment.Title,
                    UserId = comment.UserId,
                    PostId = comment.PostId.Value,
                    DatePosted = DateTime.UtcNow,
                    User = user,
                    Post = post
                };

                _context.Comments.Add(newComment);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetComment", new { id = newComment.Id }, newComment);
            }
            else if (comment.VacancyId != null)
            {
                // коментар для вакансії
                var newComment = new Comment
                {
                    Title = comment.Title,
                    UserId = comment.UserId,
                    VacancyId = comment.VacancyId.Value,
                    DatePosted = DateTime.UtcNow
                };

                _context.Comments.Add(newComment);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetComment", new { id = newComment.Id }, newComment);
            }
            else
            {
                return BadRequest("Не вказано PostId чи VacancyId");
            }
        }



        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var comment = await _context.Comments
                .Include(c => c.User) // Include the User in the query
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            // Check if the current user is the owner of the comment
            if (comment.User == null || comment.User.Email != currentUserId)
            {
                return Forbid("You do not have permission to delete this comment");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return (_context.Comments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
