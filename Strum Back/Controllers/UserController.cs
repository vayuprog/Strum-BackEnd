using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strum.Core.Entities;
using Strum.Infrastructure;

namespace Strum_Back.Controllers;



[ApiController]
[Route("api/user/")]
public class UserController : ControllerBase
{
    private readonly DataContext _context;

    public UserController(DataContext context)
    {
        _context = context;
    }
    
    [HttpGet("GetUser")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();

        if (users == null || users.Count == 0)
        {
            return NotFound("No users found");
        }

        return Ok(users);
    }
    [HttpPost("AddUser")]
    public IActionResult AddUser([FromBody] User user)
    {
        try
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            //return Ok(user);
            return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}