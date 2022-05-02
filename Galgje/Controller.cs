using DAL;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class Controller
{
    public List<Speler> GameSpelers = new List<Speler>();
    public int MaxTries = 9;
    public int WrongTries { get; set; } = 0;
    List<char> WrongLetters = new List<char>();
    public int TotalTries { get; set; } = 0;
    List<char> Tried = new List<char>();
    public string? WordToGuess { get; set; }
    public Speler? HuidigeSpeler { get; set; }

    GameRepository GameRepo { get; init; } = new GameRepository();
    SpelerRepository SpelerRepo { get; init; } = new SpelerRepository();
    StatsRepository StatsRepo { get; init; } = new StatsRepository();

    public void NewRound()
    {
        GameReset();
        HuidigeSpeler = GameSpelers[(GameSpelers.IndexOf(HuidigeSpeler) + 1) % GameSpelers.Count()];
    }

    public void GameReset()
    {
        WrongTries = 0;
        TotalTries = 0;
        Tried.Clear();
        WrongLetters.Clear();
        WordToGuess = GameRepo.GetWord();
    }

    public void SetPlayer(string name)
    {
        Speler speler = SpelerRepo.GetRealSpeler(name);
        GameSpelers.Add(speler);
    }

    public bool InputAdmin(char input)
    {
        if (Tried.Contains(input))
            throw new ArgumentException("Deze heb je al eens gegokt");

        Tried.Add(input);
        TotalTries++;

        if (WordToGuess.Contains(input))
        {
            if (GoodGuessHandler())
                return true;//Woord geraden
        }
        else
        {
            if (BadGuessHandler(input))
                return true; // Game Over
        }
        return false;//Volgende letter vragen
    }

    public string DisplayGoodGuesses()
    {
        string guessed = "";
        foreach (char letter in WordToGuess)
        {
            if (Tried.Contains(letter))
                guessed += letter.ToString();
            else
                guessed += ".";
        }
        return guessed;
    }

    public bool GoodGuessHandler()
    {
        if (WordToGuess == DisplayGoodGuesses())
        {
            GameRepo.VoegGameToe(new Game(true, TotalTries, WrongTries, HuidigeSpeler));
            Console.WriteLine($"Je hebt het woord geraden! {DisplayGoodGuesses()} in {TotalTries} beurten!");
            return true;
        }
        else
        {
            Console.WriteLine($"Lekker bezig! Die zit er in! {DisplayGoodGuesses()}");
            return false;
        }
    }

    public bool BadGuessHandler(char input)
    {
        WrongLetters.Add(input);
        WrongTries++;

        if (WrongTries == MaxTries)
        {
            GameRepo.VoegGameToe(new Game(false, TotalTries, WrongTries, HuidigeSpeler));
            Console.WriteLine("Jij hangt");
            return true;
        }
        else
        {
            Console.WriteLine($"Helaas, Je mag het nog {MaxTries - WrongTries} keer proberen. {DisplayGoodGuesses()}");
            return false;
        }
    }

    public void GetGameStatsOver(int aantal)
    {
        
        GameStats stats = StatsRepo.GetGameStatsOver(aantal);

        Console.WriteLine($"==>> Van de laatste {stats.AantalPotjes} potjes zijn er {stats.VerlorenPotjes} verloren");
        Console.WriteLine($"==>> Gemiddeld zijn er {stats.AantalPogingenGemiddeld} pogingen nodig");
        Console.WriteLine($"==>> en worden er {stats.VerkeerdeLettersGemiddeld} letters verkeerd gegokt\n");
    }

    public void GetBestPlayer()
    {
        PlayerStats bestplayer =  StatsRepo.GetBestPlayer();

        Console.WriteLine($"==>> De beste speler is {bestplayer.Name} met een winratio van {Math.Round(bestplayer.WinRatio, 2)}% over {bestplayer.Potjes} gespeelde potjes");
    }

    public void GetAllPlayers()
    {
        StatsRepo.GetPlayerStats()
            .ForEach(x => Console.WriteLine($"Speler: {x.Name}\t Potjes gespeeld: {x.Potjes}\t Winratio: {Math.Round(x.WinRatio, 2)}%\t Pogingen nodig: {x.Pogingen}"));
    }

}

