using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strum.Core.Entities;
using Strum.Infrastructure;
using Strum.Logic.Commands;

namespace Strum_Back.Controllers;



[ApiController]
[Route("api/user/")]
public class UserController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IMediator _mediator;

    public UserController(DataContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
        
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
    public async Task AddUser([FromBody] UserCreateRequest request)
    {
        await _mediator.Send(request);
    }
    
    [HttpPut("UpdateUser")]
    public async Task UpdateUser([FromBody] UserUpdateRequest request)
    {
        await _mediator.Send(request);
    }

    [HttpDelete("DeleteUser")]
    public async Task DeleteUser([FromBody] UserDeleteRequest request)
    {
        await _mediator.Send(request);
    }
}