//using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Strum.Core.Enums;

namespace Strum.Core.Entities;

public class User
{
	public int Id { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string Email { get; set; } 
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public string? Ganre { get; set; }
    public string? Expirience { get; set; }
    public string? Instrument { get; set; }
    public List<Comment>? Comments { get; set; }
    public List<Post>? Posts { get; set; }
    public string? Description { get; set; }

    public Region? UserRegion { get; set; }
    //public string ResetToken { get; set; }
}

