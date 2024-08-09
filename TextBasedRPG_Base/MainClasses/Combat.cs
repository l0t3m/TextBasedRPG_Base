using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.SubClasses;

namespace TextBasedRPG_Base.MainClasses
{
    public static class Combat
    {
        /// <returns>True if won, false if defeated.</returns>
        public static bool StartFight(Enemy enemy)
        {
            SceneManager.currentEnemy = enemy;
            Functions.PrintAndColor($"While looking around for enemies, you encounter a {enemy.name}\n", $"{enemy.name}", ConsoleColor.DarkRed);
            Functions.PrintFight();

            while (SceneManager.player.isAlive && SceneManager.currentEnemy.isAlive && SceneManager.currentEnemy != null)
            {
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    Console.Clear();

                    switch (choice)
                    {
                        case 1:
                            if (SceneManager.player.isAlive)
                            {
                                SceneManager.player.AttackEnemy();
                                Console.ReadLine();
                            }

                            if (SceneManager.currentEnemy.isAlive)
                            {
                                SceneManager.currentEnemy.AttackPlayer();
                                Console.ReadLine(); Console.Clear();
                            }

                            Console.Clear(); Functions.PrintFight();
                            break;

                        case 2:
                            if (Random.Shared.Next(1, 100) < 20)
                            {
                                Console.WriteLine("As you attempt to flee, the enemy delivers a final blow...");
                                SceneManager.currentEnemy.AttackPlayer();
                                Console.ReadLine(); Console.Clear();
                            }

                            SceneManager.currentEnemy = null;
                            Navigation.Explore();
                            break;
                        default:
                            Functions.PrintFight();
                            break; 
                    }
                }
                catch
                {
                    Console.Clear(); Functions.PrintFight();
                }
            }

            if (SceneManager.player.isAlive == false)
                return false;
            Console.Clear();

            if (Random.Shared.Next(1, 100) < 40)
            {
                Console.WriteLine("You found a sword!"); // add sword
                Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();
            }
            return true;
        }
    }
}
