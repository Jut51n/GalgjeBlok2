using DAL;
using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Application;

class Program
{
    static void Main(string[] args)
    {
        Controller controller = new Controller();
        Console.WriteLine("Wat is je naam?");

        while (true) 
        {
            string name = Console.ReadLine();
            if (name != "")
            {
                controller.SetPlayer(name);
                Console.WriteLine($"Welkom, {name}! Geef een naam om nog een speler toe te voegen. Of druk enter om te starten");      
            }
            else
            {
                break;
            }
        }
        while (true) // Start een nieuwe ronde met de volgende speler
        {
            controller.NewRound();
            Console.WriteLine($"Succes {controller.HuidigeSpeler.UserName}. Woord is klaar gezet");

            while (true) // Zolang het woord nog niet geraden is
            {
                try
                {
                    Console.WriteLine("Geef een letter(of tik 'menu' voor opties)");
                    string UserInput = Console.ReadLine();

                    if (UserInput != "menu")
                    {
                        if (controller.InputAdmin(Input.Validator(UserInput)))
                            break;//Woord geraden of beurten op
                    }
                    else
                    {
                        bool menu = true;
                        while (menu == true)
                        {
                            Console.WriteLine("\n------------------------------------------------------- GalgjeMan --------------------------------------------------");
                            Console.WriteLine($"Reset game (r) | Laatste 10 Stats (s) | Beste Speler (b) | Alle Speler stats (a) | Close menu (c) | Exit Game (x)\n");
                            string input = Console.ReadLine();

                            switch (input)
                            {
                                case "r":
                                    controller.GameReset();
                                    Console.WriteLine("==>> Game Reset");
                                    break;
                                case "s":
                                    controller.GetGameStatsOver(10);
                                    break;
                                case "b":
                                    controller.GetBestPlayer();
                                    break;
                                case "a":
                                    controller.GetAllPlayers();
                                    break;
                                case "c":
                                    menu = false;
                                    continue;
                                case "x":
                                    return;
                                default:
                                    break;
                            }
                        }
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }

    }
}
