using Homework3.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework3.Repositories;

public class DBRepository: DbContext
{
    public DbSet<User> Users { get; set; } 
    public DbSet<Post> Posts { get; set; } 
    public DbSet<Comment> Comments { get; set; } 
    public DBRepository()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=homework3;Username=kaspi;Password=");
    }
    
}