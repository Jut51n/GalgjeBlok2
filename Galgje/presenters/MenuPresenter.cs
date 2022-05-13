using DAL;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galgje
{
    public class MenuPresenter
    {
        Controller Control { get; set; }
        MenuView Menu { get; set; }

        public MenuPresenter(Controller controller, MenuView menuview)
        {
            Control = controller;
            Menu = menuview;
        }
        
        public void ShowMenu()
        {
            bool menu = true;
            while (menu == true)
            {
                string input = Menu.Show();
 
                StatsPresenter statspresenter = new StatsPresenter(new StatsRepository(), new StatsView());
                switch (input)
                {

                    case "r":
                        Control.GameReset();
                        Console.WriteLine("==>> Game Reset");
                        break;
                    case "s":
                        statspresenter.GetGameStatsOver(10);
                        break;
                    case "b":
                        statspresenter.GetBestPlayer();
                        break;
                    case "a":
                        statspresenter.GetAllPlayers();
                        break;
                    case "c":
                        menu = false;
                        continue;
                    default:
                        break;
                }
            }
        }
    }
}
