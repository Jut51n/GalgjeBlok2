using DAL;
using Domain;
using Galgje;
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

                    if (UserInput == "menu")
                    {
                        MenuPresenter menu = new MenuPresenter(controller, new MenuView());
                        menu.ShowMenu();
                    }
                    else
                    {
                        if (controller.InputAdmin(Input.Validator(UserInput)))
                            break;
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
