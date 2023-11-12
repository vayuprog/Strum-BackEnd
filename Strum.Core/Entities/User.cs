﻿//using System;
using Microsoft.EntityFrameworkCore;

namespace Strum.Core.Entities;

[Keyless]
public class User
{
	public int UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

}
