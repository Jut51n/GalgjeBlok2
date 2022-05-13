using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galgje
{
    public class StatsView
    {

        public void ShowLastGameStats(GameStats stats)
        {
            Console.WriteLine($"==>> Van de laatste {stats.AantalPotjes} potjes zijn er {stats.VerlorenPotjes} verloren");
            Console.WriteLine($"==>> Gemiddeld zijn er {stats.AantalPogingenGemiddeld} pogingen nodig");
            Console.WriteLine($"==>> en worden er {stats.VerkeerdeLettersGemiddeld} letters verkeerd gegokt\n");
        }

       public void ShowBestPlayer(PlayerStats bestplayer)
        {
            Console.WriteLine($"==>> De beste speler is {bestplayer.Name} met een winratio van {Math.Round(bestplayer.WinRatio, 2)}% over {bestplayer.Potjes} gespeelde potjes");
        }
    
        public void ShowAllPlayers(List<PlayerStats> statslist)
        {
            statslist.ForEach(x => Console.WriteLine($"Speler: {x.Name}\t Potjes gespeeld: {x.Potjes}\t Winratio: {Math.Round(x.WinRatio, 2)}%\t Pogingen nodig: {x.Pogingen}"));
        }

    }
}
