using System;
using Microsoft.EntityFrameworkCore;
using Strum.Core.Entities;

namespace Strum.Infrastructure;

public class AppContext:DbContext
{
	public AppContext()
	{
	}
	public DbSet<User> Users { get; set; } = null!;
	public DbSet<Messages> Messages { get; set; } = null!;
	public DbSet<Vacancy> Vacancies { get; set; } = null!;
	public DbSet<Notification> Notifications { get; set; } = null!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
		optionsBuilder.UseNpgsql("jdbc:postgresql://localhost/Strum?password=admin228&user=postgres");
        base.OnConfiguring(optionsBuilder);
    }
}

