using Microsoft.EntityFrameworkCore;
using PromomashTestApp.Infrastructure.Entities;

namespace PromomashTestApp.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        DbPath = Path.Join(path, "database.db");
    }
    
    public string DbPath { get; }
    
    public virtual DbSet<Country> Countries { get; set; } = null!;
    public virtual DbSet<Province> Provinces { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
