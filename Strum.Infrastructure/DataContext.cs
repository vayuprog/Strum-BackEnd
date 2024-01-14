using System;
using Microsoft.EntityFrameworkCore;
using Strum.Core.Entities;
using Strum.Infrastructure.Configurations;

namespace Strum.Infrastructure;

public class DataContext : DbContext
{
	

	
	public DataContext()
	{
        Database.EnsureCreated();
    }
	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{
        Database.EnsureCreated();
    }
	
	public DbSet<User> Users { get; set; }
	//public DbSet<Messages> Messages { get; set; } 
	public DbSet<Vacancy> Vacancies { get; set; } 
	public DbSet<Comment> Comments { get; set; }
	public DbSet<Musician> Musicians { get; set; }
	public DbSet<Post> Post { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Host=localhost;Database=Strum;Username=postgres;Password=admin228");
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        modelBuilder.Entity<Post>()
               .HasOne(p => p.User)
               .WithMany(u => u.Posts)
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
	}
}
//jdbc:postgresql://localhost/Strum?password=admin228&user=postgres
