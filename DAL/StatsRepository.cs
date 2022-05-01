using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class StatsRepository
{
    internal DbContextOptions<GalgContext> Options { get; init; } = new DbContextOptionsBuilder<GalgContext>()
        .UseSqlServer(@"Server=localhost;Database=Galgje;Integrated Security=true")
        .Options;

    public StatsRepository(DbContextOptions<GalgContext> options)
    {
        Options = options;
    }

    public StatsRepository() { }

    public GameStats GetGameStatsOver(int aantal)
    {
        using GalgContext context = new GalgContext(Options);

        if (aantal > context.games.Count())
        {
            aantal = context.games.Count();
        }

        var result = (from s in context.games
                      orderby s.datetime descending
                      select new {s.WrongLettersGuessed,s.Tries,s.Won}).Take(aantal).ToList();

        double verkeerd = Math.Round(result.Average(s => s.WrongLettersGuessed), 1);
        double pogingennodig = Math.Round(result.Average(s => s.Tries), 1);
        int potjesverloren = result.Where(s => s.Won == false).Count();

        return new GameStats(aantal, potjesverloren, verkeerd, pogingennodig);
    }

    public PlayerStats GetBestPlayer()
    {
        using GalgContext context = new GalgContext(Options);

        var result = GetPlayerStats();

        var player = result.OrderByDescending(x => x.WinRatio).First();

        return player;
    }

    public List<PlayerStats> GetPlayerStats()
    {
        List<PlayerStats> StatsList = new List<PlayerStats>();
        using GalgContext Context = new GalgContext(Options);


        var result = (from s in Context.games.Include(x => x.Speler)
                      group s by s.Speler.UserName
                      into statgroup
                      select new
                      {
                          Id = statgroup.Key,
                          Winratio = (decimal)statgroup.Count(x => x.Won) / statgroup.Count() * 100,
                          Gespeeld = statgroup.Count(),
                          Pogingen = (decimal)statgroup.Average(x => x.Tries),
                          Gewonnen = statgroup.Count(x => x.Won)
                      }).ToList();

        foreach (var player in result)
        {
            PlayerStats Stats = new PlayerStats(player.Id, player.Gespeeld, player.Winratio, player.Pogingen, player.Gewonnen);
            StatsList.Add(Stats);
        }

        List<PlayerStats> ReturnList = StatsList.OrderByDescending(x => x.WinRatio).ToList();
        return ReturnList;
    }


}
