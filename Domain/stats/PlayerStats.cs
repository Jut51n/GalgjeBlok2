using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class PlayerStats
{
    public string Name { get; set; }
    public int Potjes { get; set; }
    public decimal WinRatio { get; set; }
    public decimal Pogingen { get; set; }
    public int Gewonnen { get; set; }

    public PlayerStats(string name, int games, decimal winratio, decimal pogingen, int gewonnen)
    {
        Name = name;
        Potjes = games;
        WinRatio = winratio;
        Pogingen = pogingen;
        Gewonnen = gewonnen;
    }

}


