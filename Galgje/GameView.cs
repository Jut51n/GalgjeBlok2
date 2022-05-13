using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galgje
{
    public class GameView
    {

        public void AlreadyGuessed()
        {
            Console.WriteLine("Deze heb je al eens gegokt");
        }

        public void GameWon(string word, int tries)
        {
            Console.WriteLine($"Je hebt het woord geraden! {word} in {tries} beurten!");
        }

        public void GameLost()
        {
            Console.WriteLine("Jij hangt!");
        }

        public void ContinueAfterGoodGuess(string word)
        {
            Console.WriteLine($"Lekker bezig! Die zit er in! {word}");
        }

        public void ContinueAfterWrongGuess(string word, int triesleft)
        {
            Console.WriteLine($"Helaas, Je mag het nog {triesleft} keer proberen. {word}");
        }
    }
}
