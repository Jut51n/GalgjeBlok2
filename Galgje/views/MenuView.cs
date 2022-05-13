using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galgje
{
    public class MenuView
    {
        public string Show()
        {
            Console.WriteLine("\n------------------------------------------------------- GalgjeMan --------------------------------------------------");
            Console.WriteLine($"Reset game (r) | Laatste 10 Stats (s) | Beste Speler (b) | Alle Speler stats (a) | Close menu (c) | Exit Game (x)\n");
            return Console.ReadLine();
        }
    }
}
