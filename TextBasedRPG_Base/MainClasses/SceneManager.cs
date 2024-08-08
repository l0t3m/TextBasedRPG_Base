using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.SubClasses;

namespace TextBasedRPG_Base.MainClasses
{
    public class SceneManager
    {
        public static readonly Player player = new Player("lotem"); // remove readonly + create character creates this field
        public static Enemy currentEnemy { get; set; }
        public static Room currentRoom { get; set; } // uppercase name

        public static void MainMenu() // WIP
        {
            Prints.PrintMainMenu();

            try
            {
                int choice = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (choice)
                {
                    case 1:
                        // prints the entire backstory.
                        // create a new character and set it in SceneManager
                        Navigation.Explore(); break;
                    case 2:
                        Environment.Exit(0); break;
                }
            }
            catch
            {
                Console.Clear(); MainMenu();
            }
        }

        public static void GameOver() // WIP
        {
            // player = null
            Console.WriteLine("Game over!");
            Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();
            MainMenu();
        }
    }
}
