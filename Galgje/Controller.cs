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

    GameRepository GameRepo { get; set; }
    SpelerRepository SpelerRepo { get; set; }
    StatsRepository StatsRepo { get; set; }

    public Controller(GameRepository gamerepo, SpelerRepository spelerrepo, StatsRepository statsrepo)
    {
        GameRepo = gamerepo;
        SpelerRepo = spelerrepo;
        StatsRepo = statsrepo;
    }

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
        SetGuessWord();
    }

    public void SetPlayer(string name)
    {
        Speler speler = SpelerRepo.GetRealSpeler(name);
        GameSpelers.Add(speler);
    }

    internal void SetGuessWord()
    { 
        WordToGuess = GameRepo.GetWord();
    }

    public char InputValidator(string input)
    {
        char c = Input.StringToChar(input);
        Input.IsNotSpecial(c);
        Input.IsNotNumeric(c);
        return Input.ToLower(c);
    }

    public bool InputAdmin(char input)
    {
        if (!Tried.Contains(input))
        {
            Tried.Add(input);
            TotalTries++;
            if (WordToGuess.Contains(input))
            {
                return true;
            }
            WrongLetters.Add(input);
            UpWrongGuess();
            return false;
        }
        else
        {
            throw new ArgumentException("Deze heb je al eens gegokt");
        }
    }

    internal void UpWrongGuess()
    {
        WrongTries++;
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

    public bool GoodGuess()
    {
        if (WordToGuess == DisplayGoodGuesses())
        {
            SaveGame(true, TotalTries, WrongTries, HuidigeSpeler);
            Console.WriteLine($"Je hebt het woord geraden! {DisplayGoodGuesses()} in {TotalTries} beurten!");

            return true;
        }
        else
        {
            Console.WriteLine($"Lekker bezig! Die zit er in! {DisplayGoodGuesses()}");
            return false;
        }
    }

    public bool BadGuess()
    {
        if (WrongTries == MaxTries)
        {
            SaveGame(false, TotalTries, WrongTries, HuidigeSpeler);
            Console.WriteLine("Jij hangt");

            return true;
        }
        else
        {
            int TriesToGo = MaxTries - WrongTries;
            Console.WriteLine($"Helaas, Je mag het nog {TriesToGo} keer proberen. {DisplayGoodGuesses()}");
            return false;
        }
    }

    public void SaveGame(bool won, int tries, int wrongtries, Speler speler)
    {
        GameRepo.VoegGameToe(new Game(won, tries, wrongtries, speler)); // mag dat hier?
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
        List < PlayerStats > statlist = StatsRepo.GetPlayerStats();

        statlist.ForEach(x => Console.WriteLine($"Speler: {x.Name}\t Potjes gespeeld: {x.Potjes}\t Winratio: {Math.Round(x.WinRatio, 2)}%\t Pogingen nodig: {x.Pogingen}"));

    }

}

