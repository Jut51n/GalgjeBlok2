using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class GameRepository
{
    internal DbContextOptions<GalgContext> Options { get; init; } = new DbContextOptionsBuilder<GalgContext>()
        .UseSqlServer(@"Server=localhost;Database=Galgje;Integrated Security=true")
        .Options;

    public GameRepository(DbContextOptions<GalgContext> options)
    {
        Options = options;
    }

    public GameRepository() { }

    public void VoegGameToe(Game game)
    {
        using (GalgContext context = new GalgContext(Options))
        {
            context.games.Add(game);
            context.SaveChanges();
        }
    }

    public string GetWord()
    {
        using (GalgContext context = new GalgContext(Options))
        {
            Random rnd = new Random();
            int randomIndex = rnd.Next(0, context.words.Count());

            var WoordId = new SqlParameter("@Id", randomIndex);

            var result = context.words.FromSqlRaw("Getword @Id", WoordId)
                .ToList();

            return result.First().Woord;
        }
    }

}
