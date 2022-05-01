using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class SpelerRepository
{
    internal DbContextOptions<GalgContext> Options { get; init; } = new DbContextOptionsBuilder<GalgContext>()
        .UseSqlServer(@"Server=localhost;Database=Galgje;Integrated Security=true")
        .Options;

    public SpelerRepository(DbContextOptions<GalgContext> options)
    {
        Options = options;
    }

    public SpelerRepository() { }

    public Speler GetRealSpeler(Speler speler)
    {
        using GalgContext context = new GalgContext(Options);

        if (!context.spelers.Any(s => s.UserName == speler.UserName))
        {
            VoegSpelerToe(speler);
        }

        return context.spelers.Where(s => s.UserName == speler.UserName).FirstOrDefault();
    }

    public void VoegSpelerToe(Speler speler)
    {
        using GalgContext context = new GalgContext(Options);

        context.spelers.Add(speler);
        context.SaveChanges();
    }


}
