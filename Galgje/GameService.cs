using DAL;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galgje
{

    public class GameService
    {
        public int MaxTries = 9;
        public int WrongTries { get; set; } = 0;
        public List<char> WrongLetters = new List<char>();
        public int TotalTries { get; set; } = 0;
        public List<char> Tried = new List<char>();
        public string? WordToGuess { get; set; }
        GameView GameView { get; set; }
        Speler HuidigeSpeler { get; set; }

        GameRepository GameRepo { get; init; } = new GameRepository();

        public GameService(GameView gameview, Speler huidigespeler)
        {
            GameView = gameview;
            HuidigeSpeler = huidigespeler;
            ResetGame();
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

        public bool NextGuess(char input)
        {
            if (IsNotGuessedBefore(input))
            {
                if (GoodGuess(input))
                {
                    return GoodGuessHandler(input);
                }
                else
                {
                    return BadGuessHandler(input);
                }
            }
            return false;
        }

        public bool IsNotGuessedBefore(char input)
        {
            if (!Tried.Contains(input))
            {
                Tried.Add(input);
                TotalTries++;
                return true;
            }
            else
            {
                    return false;
                    GameView.AlreadyGuessed();
            }
        }

        public bool GoodGuess(char input)
        {
            if (WordToGuess.Contains(input))
            {
                return true;
            }
            else
            {
                WrongLetters.Add(input);
                WrongTries++;
                return false;
            }
            
        }

        public bool GoodGuessHandler(char input)
        {
            if(WordToGuess == DisplayGoodGuesses())
            {
                GameView.GameWon(DisplayGoodGuesses(), TotalTries);
                GameRepo.VoegGameToe(new stats(true, TotalTries, WrongTries, HuidigeSpeler));
                return true;
            }
            else
            {
                GameView.ContinueAfterGoodGuess(DisplayGoodGuesses());
                return false;
            }
        }

        public bool BadGuessHandler(char input)
        {
            if(WrongTries == MaxTries)
            {
                GameView.GameLost();
                GameRepo.VoegGameToe(new stats(true, TotalTries, WrongTries, HuidigeSpeler));
                return true;
            }
            else
            {
                GameView.ContinueAfterWrongGuess(DisplayGoodGuesses(), MaxTries - WrongTries);
                return false;
            }
        }

        public void ResetGame()
        {
            WrongTries = 0;
            TotalTries = 0;
            Tried.Clear();
            WrongLetters.Clear();
            WordToGuess = GameRepo.GetWord();
        }
    }
}
