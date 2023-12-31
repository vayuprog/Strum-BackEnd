﻿//using System;
using Microsoft.EntityFrameworkCore;

namespace Strum.Core.Entities;


public class User
{
	public int Id { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string Email { get; set; } 
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public string ResetToken { get; set; }
}

