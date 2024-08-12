using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.SubClasses;

namespace TextBasedRPG_Base.MainClasses
{
    public static class SceneManager
    {
        // ----------------------------------- Attributes: ----------------------------------- //
        public static Player player { get; private set; } = new Player("player");
        public static Enemy currentEnemy { get; set; }
        public static Room currentRoom { get; set; }



        // ------------------------------------ Methods: ------------------------------------ //
        public static void MainMenu()
        {
            Functions.PrintMainMenu();
            try
            {
                int choice = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (choice)
                {
                    case 1:
                        CharacterCreation();
                        BackgroundStory(); // comment if don't want the backstory
                        Navigation.Explore(); break;
                    case 2:
                        Environment.Exit(0); break;
                    default:
                        MainMenu(); break;
                }
            }
            catch
            {
                Console.Clear(); MainMenu();
            }
        }

        public static void GameOver()
        {
            Console.WriteLine("Game over!");
            Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();
            MainMenu();
        }

        public static void GameWon()
        {
            Console.WriteLine("You won");
            Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();
        }

        public static void CharacterCreation()
        {
            int retryCounter = 0;
            string name = "";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("What's your character name?");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                
                if (retryCounter >= 15)
                {
                    Functions.PrintAndColor("FINE! YOUR NAME IS NOW BOB. DEAL WITH IT.", "BOB", ConsoleColor.DarkRed);
                    name = "BOB";
                    Console.ReadLine(); break;
                }
                else if (retryCounter == 10)
                {
                    Functions.PrintAndColor("GOD FUCKING DAMMIT JUST CHOOSE A NAME", null, ConsoleColor.DarkRed);
                    Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();

                }
                else if (retryCounter == 5 || retryCounter == 8)
                {
                    Console.WriteLine("Try using some other letters other than just keep pressing enter...");
                    Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();
                }
                else
                {
                    Console.WriteLine("invalid name... try again.");
                    Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();
                }
                retryCounter++;
            }

            player = new Player(name);
            Console.Clear();

            if (retryCounter >= 15)
                Functions.PrintAndColor($"Welcome \"{player.name}\". pfft what a name you chose...\n", player.name, ConsoleColor.DarkRed);
            else 
                Console.WriteLine($"Welcome {player.name}!\n");
            Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();
        }

        public static void BackgroundStory()
        {
            Console.Write("You’re a new adopted cat welcomed by the Shriker family.");
            Console.ReadLine(); Console.Write("Stepping into this new home, you sense it's a world apart from the streets you know.");
            Console.ReadLine(); Console.Write("Here, the ruthless cat dictator, Hatol, rules with an iron paw, keeping the pets in fear and the humans are unaware.\n");
            Console.ReadLine(); Console.Write("You're no stranger to defiance; the streets have taught you resilience.");
            Console.ReadLine(); Console.Write("Determined to end Hatol’s tyranny, you plan to overthrow him and bring freedom to the household.");
            Console.ReadLine(); Console.Write("The battle for justice begins now.");
            Console.ReadLine(); Console.Write("Will you become the new leader and restore peace?");
            Console.ReadLine(); Console.Write($"\nThe revolution starts today... Good luck little {player.name}!");
            Console.ReadLine(); Console.Clear();
        }
    }
}
