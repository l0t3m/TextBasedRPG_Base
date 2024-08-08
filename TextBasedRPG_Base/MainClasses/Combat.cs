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
            Prints.PrintAndColor($"While looking around for enemies, you encounter a {enemy.name}\n", $"{enemy.name}", ConsoleColor.Red);
            Prints.PrintFight();

            while (SceneManager.player.isAlive && SceneManager.currentEnemy.isAlive && SceneManager.currentEnemy != null)
            {
                Console.WriteLine(SceneManager.currentEnemy.isAlive);
                Console.WriteLine(SceneManager.currentEnemy == null);
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
                                Console.ReadLine(); Console.Clear();
                            }

                            if (SceneManager.currentEnemy.isAlive)
                            {
                                SceneManager.currentEnemy.AttackPlayer();
                                Console.ReadLine(); Console.Clear();
                            }

                            Prints.PrintFight();
                            break;

                        case 2:
                            SceneManager.currentEnemy = null;
                            Navigation.Explore();
                            break;
                    }
                }
                catch
                {
                    Console.Clear(); Prints.PrintFight();
                }
            }

            if (SceneManager.player.isAlive == false)
                return false;
            Console.Clear();
            return true;
        }
    }
}
