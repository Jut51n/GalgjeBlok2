using DAL;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class Controller
{
    public List<Speler> spelers = new List<Speler>();
    public int MaxTries = 9;
    public int WrongTries { get; set; } = 0;
    List<char> WrongLetters = new List<char>();
    public int TotalTries { get; set; } = 0;
    List<char> Tried = new List<char>();
    public string? WordToGuess { get; set; }
    public Speler? HuidigeSpeler { get; set; }


    public void NewRound(string word)
    {
        GameReset(word);
        HuidigeSpeler = spelers[(spelers.IndexOf(HuidigeSpeler) + 1) % spelers.Count()];
    }

    public void GameReset(string word)
    {
        WrongTries = 0;
        TotalTries = 0;
        Tried.Clear();
        WrongLetters.Clear();
        SetGuessWord(word);
    }

    public void SetPlayer(string name)
    {
        SpelerRepository repo = new SpelerRepository();

        Speler tempspeler = new Speler(name);
        Speler speler = repo.GetRealSpeler(tempspeler);
        this.spelers.Add(speler);
    }

    internal void SetGuessWord()
    {
        WordToGuess = word.ToLower();
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
            SaveGame(new Game(true, TotalTries, WrongTries, HuidigeSpeler));
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
            SaveGame(new Game(false, TotalTries, WrongTries, HuidigeSpeler));
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

    public void SaveGame(Game game)
    {
        GameRepository repo = new GameRepository();
        repo.VoegGameToe(game);
    }

    public void GetGameStatsOver(int aantal)
    {
        StatsRepository repo = new StatsRepository();
        GameStats stats = repo.GetGameStatsOver(aantal);

        Console.WriteLine($"==>> Van de laatste {stats.AantalPotjes} potjes zijn er {stats.VerlorenPotjes} verloren");
        Console.WriteLine($"==>> Gemiddeld zijn er {stats.AantalPogingenGemiddeld} pogingen nodig");
        Console.WriteLine($"==>> en worden er {stats.VerkeerdeLettersGemiddeld} letters verkeerd gegokt\n");
    }

    public void GetBestPlayer()
    {
        StatsRepository repo = new StatsRepository();
        PlayerStats bestplayer =  repo.GetBestPlayer();

        Console.WriteLine($"==>> De beste speler is {bestplayer.Name} met een winratio van {Math.Round(bestplayer.WinRatio, 2)}% over {bestplayer.Potjes} gespeelde potjes");
    }

    public void GetAllPlayers()
    {
        StatsRepository repo = new StatsRepository();
        List < PlayerStats > statlist = repo.GetPlayerStats();

        statlist.ForEach(x => Console.WriteLine($"Speler: {x.Name}\t Potjes gespeeld: {x.Potjes}\t Winratio: {Math.Round(x.WinRatio, 2)}%\t Pogingen nodig: {x.Pogingen}"));

    }

}

