using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data;

public class NZWalksDbContext : DbContext
{
    public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
    {

    }

    public DbSet<Walk> Walks { get; set; }
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("NZWalks");
    }
}
