using System;
using Microsoft.EntityFrameworkCore;
using Strum.Core.Entities;
using Strum.Infrastructure.Configurations;

namespace Strum.Infrastructure;

public class DataContext : DbContext
{
	

	
	public DataContext()
	{
		
	}
	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{
	}
	
	public DbSet<User> Users { get; set; }
	public DbSet<Messages> Messages { get; set; } 
	public DbSet<Vacancy> Vacancies { get; set; } 
	public DbSet<Notification> Notifications { get; set; } 
	public DbSet<Musician> Musicians { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Host=localhost;Database=Strum;Username=postgres;Password=admin228");
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new UserConfiguration());
	}
}
//jdbc:postgresql://localhost/Strum?password=admin228&user=postgres
