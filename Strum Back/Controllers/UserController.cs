using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strum.Core.Entities;
using Strum.Infrastructure;
using Strum.Logic.Commands;
using Strum.Security;

namespace Strum_Back.Controllers;



[ApiController]
[Route("api/user/")]
public class UserController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IMediator _mediator;
    private readonly PasswordHasher _passwordHasher;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public UserController(DataContext context, IMediator mediator, PasswordHasher passwordHasher, JwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _mediator = mediator;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
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
        // Generate a random salt
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hash the password
        var hashedPassword = await _passwordHasher.Hash(request.PasswordHash, salt);

        // Update the password in the request with the hashed password
        request.PasswordHash = Convert.ToBase64String(hashedPassword);
        request.Salt = Convert.ToBase64String(salt);
        
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
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        // Retrieve the user from the database
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
        
        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }
        
        // Generate the hash of the provided password
        byte[] salt = Convert.FromBase64String(user.Salt);
        var hashedPassword = await _passwordHasher.Hash(request.PasswordHash, salt);
        
        // Compare the hashed password with the stored hashed password
        if (user.PasswordHash != Convert.ToBase64String(hashedPassword))
        {
            return Unauthorized("Invalid username or password");
        }
        
        // If the passwords match, generate a JWT token and return it
        var token = _jwtTokenGenerator.CreateToken(user.Email);
        
        return Ok(new { Token = token });
    }  
}