using System;
using Microsoft.EntityFrameworkCore;
using Strum.Core.Entities;

namespace Strum.Infrastructure;

public class AppContext:DbContext
{
	public AppContext()
	{
	}
	public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
		optionsBuilder.UseNpgsql("Host = localhost; Database = Strum; Username = postgres; Password = admin228");
        base.OnConfiguring(optionsBuilder);
    }
}

