using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public class GalgContext : DbContext
{
    public DbSet<Speler> spelers { get; set; }
    public DbSet<stats> games { get; set; }
    public DbSet<Word> words { get; set; }

    public GalgContext(DbContextOptions<GalgContext> options)
        : base(options)
    {
    }

    public GalgContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<PlayerStats>();
        modelBuilder.ApplyConfiguration<Speler>(new SpelerMapping());
        modelBuilder.ApplyConfiguration<stats>(new GameMapping());
    }
}

public class GalgContextFactory : IDesignTimeDbContextFactory<GalgContext>
{
    public GalgContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GalgContext>();
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=Galgje;Integrated Security=true");

        return new GalgContext(optionsBuilder.Options);
    }
}
