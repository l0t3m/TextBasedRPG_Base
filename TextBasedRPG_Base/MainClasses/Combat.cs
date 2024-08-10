﻿using System;
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

            while (SceneManager.player.isAlive && SceneManager.currentEnemy != null)
            {
                if (!SceneManager.currentEnemy.isAlive)
                    break;

                try
                {
                    int choice = int.Parse(Console.ReadLine()); Console.Clear();

                    switch (choice)
                    {
                        case 1:
                            StartNormalAttack(); break;
                        case 2:
                            StartWeaponAttack(); break;
                        case 3:
                            StartUseItem(); break;
                        case 4:
                            StartFlee(); Navigation.Explore(); break;
                        default:
                            Functions.PrintFight(); break; 
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

            if (Random.Shared.Next(0, 100) < 80 && SceneManager.currentEnemy != null)
            {
                Weapon newW = Weapon.GenerateNewWeapon(SceneManager.currentEnemy.level);

                Console.WriteLine("You found a sword!");
                newW.PrintWeapon();

                SceneManager.player.AddWeapon(newW);
                Console.WriteLine("\nPress enter to continue."); Console.ReadLine(); Console.Clear();
            }
            return true;
        }

        public static void StartNormalAttack() 
        {
            SceneManager.player.AttackEnemy(SceneManager.player.baseDMG); Console.ReadLine();

            StartEnemyAttack();
            Console.Clear(); Functions.PrintFight();
        }

        public static void StartWeaponAttack() 
        {
            Functions.PrintFightMembers();

            Console.WriteLine("\nEnter the number of the weapon you want to use");
            SceneManager.player.PrintWeapons();

            try
            {
                int choice = int.Parse(Console.ReadLine()); Console.Clear();
                Weapon chosenWeapon = SceneManager.player.weapons[choice-1];

                if (chosenWeapon != null)
                {
                    SceneManager.player.AttackEnemy(chosenWeapon);
                    Console.ReadLine();
                }
            }
            catch
            {
                Console.Clear(); StartWeaponAttack();
            }

            StartEnemyAttack();
            Console.Clear(); Functions.PrintFight();
        }
        
        public static void StartUseItem()
        {

        }

        public static void StartFlee()
        {
            if (Random.Shared.Next(0, 100) < 20)
            {
                Console.WriteLine("As you attempt to flee, the enemy delivers a final blow...");
                SceneManager.currentEnemy.AttackPlayer();
                Console.ReadLine(); Console.Clear();
            }

            SceneManager.currentEnemy = null;
        }



        public static void StartEnemyAttack()
        {
            if (SceneManager.currentEnemy.isAlive)
            {
                SceneManager.currentEnemy.AttackPlayer();
                Console.ReadLine(); Console.Clear();
            }
        }
    }
}
