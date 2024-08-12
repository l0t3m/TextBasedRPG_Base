using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            Functions.PrintAndColor($"While looking around for enemies, you encounter a [lvl.{enemy.level}] {enemy.name}\n", $"[lvl.{enemy.level}] {enemy.name}", ConsoleColor.DarkRed);
            Console.WriteLine("Press enter to continue."); Console.ReadLine();

            bool fled = false;
            int currentLvl = SceneManager.currentEnemy.level;

            while (SceneManager.player.isAlive && SceneManager.currentEnemy != null && !fled)
            {
                Console.Clear(); Functions.PrintFight();

                if (SceneManager.currentEnemy == null)
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
                            fled = true;
                            StartFlee(); break;
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
            if (Random.Shared.Next(0, 100) < 80 && fled == false)
            {
                Weapon newW = Weapon.GenerateNewWeapon(currentLvl);
                Navigation.WeaponFindingMenu(newW);
                Console.WriteLine("\nPress enter to continue."); Console.ReadLine(); Console.Clear();
            }
            return true;
        }

        public static bool StartBossFight()
        {
            Functions.PrintBossDialog(); // comment if you don't want dialog

            while (SceneManager.player.isAlive && SceneManager.currentEnemy != null)
            {
                Console.Clear(); Functions.PrintBossFight();
                try
                {
                    int choice = int.Parse(Console.ReadLine()); Console.Clear();
                    switch (choice)
                    {
                        case 1:
                            StartNormalAttack();
                            break;
                        case 2:
                            StartWeaponAttack();
                            break;
                        case 3:
                            StartUseItem();                            
                            break;
                        default:
                            Functions.PrintBossFight(); break;
                    }
                }
                catch
                {
                    Console.Clear(); Functions.PrintBossFight();
                }
            }

            if (SceneManager.player.isAlive == false)
                return false;
            if (SceneManager.currentEnemy == null)
                SceneManager.GameWon();
            return true;
        }

        public static void StartNormalAttack() 
        {
            SceneManager.player.AttackEnemy(SceneManager.player.baseDMG); Console.ReadLine();

            if (SceneManager.currentEnemy != null)
            {
                if (!SceneManager.currentEnemy.isDistracted)
                    StartEnemyAttack();
                else
                {
                    Functions.PrintAndColor($"{SceneManager.currentEnemy.name} is distracted and didn't attack!", "is distracted");
                    Console.ReadLine();
                    SceneManager.currentEnemy.SetDistracted(false);
                }
            }
            Console.Clear();
        }

        public static void StartWeaponAttack() 
        {
            Functions.PrintFightMembers();
            
            Console.WriteLine("\nEnter the number of the weapon you want to use");
            SceneManager.player.PrintWeapons();

            try
            {
                int backChoice = SceneManager.player.weapons.Count(n => n != null) + 1;
                Console.WriteLine($"| {backChoice}. Go back");
                int choice = int.Parse(Console.ReadLine()); Console.Clear();
                if (choice == backChoice)
                {
                    Functions.PrintFight();
                    return;
                }
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

            if (SceneManager.currentEnemy != null)
            {
                if (!SceneManager.currentEnemy.isDistracted)
                    StartEnemyAttack();
                else
                {
                    Functions.PrintAndColor($"{SceneManager.currentEnemy.name} is distracted and didn't attack!", "is distracted");
                    Console.ReadLine();
                    SceneManager.currentEnemy.SetDistracted(false);
                }
            }
                
            Console.Clear();
        }
        
        public static void StartUseItem()
        {
            Functions.PrintFightMembers();

            Console.WriteLine("\nEnter the number of the item you want to use");
            SceneManager.player.PrintItems();

            try
            {
                int backChoice = SceneManager.player.itemInventory.Count(n => n != null) + 1;
                Console.WriteLine($"| {backChoice}. Go back");
                int choice = int.Parse(Console.ReadLine()); Console.Clear();
                if (choice == backChoice)
                {
                    Functions.PrintFight();
                    return;
                }
                Item item = SceneManager.player.itemInventory[choice - 1];
                if (item != null)
                {
                    Functions.PrintAndColor($"You have used {item.name}!", item.name);
                    if (item.UseItem())
                        SceneManager.player.itemInventory[choice - 1] = null;
                    else
                        Functions.PrintAndColor($"Item failed to use!", null, ConsoleColor.Red);
                    Console.ReadLine();
                }
            }
            catch
            {
                Console.Clear(); StartUseItem();
            }
          
            Console.Clear();
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
            SceneManager.currentEnemy.AttackPlayer();
            Console.ReadLine(); Console.Clear();
        }
    }
}
