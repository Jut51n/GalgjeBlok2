using DAL;
using Galgje;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class Controller
{
    public List<Speler> GameSpelers = new List<Speler>();
    public Speler? HuidigeSpeler { get; set; }

    SpelerRepository SpelerRepo { get; init; } = new SpelerRepository();
    GameService Game { get; set; }
    

    public void NewRound()
    {
        HuidigeSpeler = GameSpelers[(GameSpelers.IndexOf(HuidigeSpeler) + 1) % GameSpelers.Count()];
        GameService game = new GameService(new GameView(), HuidigeSpeler);
        Game = game;
    }

    public void GameReset()
    {
        Game.ResetGame();
    }

    public void SetPlayer(string name)
    {
        Speler speler = SpelerRepo.GetRealSpeler(name);
        GameSpelers.Add(speler);
    }

    public bool InputAdmin(char input)
    {
        return Game.NextGuess(input);
    }




}

