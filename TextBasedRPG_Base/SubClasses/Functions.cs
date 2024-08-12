using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.MainClasses;

namespace TextBasedRPG_Base.SubClasses
{
    public static class Functions
    {
        // ------------------------------------- General: ------------------------------------- //
        public static void PrintRoom()
        {
            PrintAndColor(
                $"You're currently in {SceneManager.currentRoom.Name}. {SceneManager.currentRoom.status}", 
                SceneManager.currentRoom.status, 
                SceneManager.currentRoom.statusColor);

            Console.WriteLine(SceneManager.currentRoom.Description); // Room description, comment if don't want it.

            if (SceneManager.currentRoom.ConnectedRooms.Length > 1)
                PrintAndColor($"You notice {SceneManager.currentRoom.ConnectedRooms.Length} different paths to take.", SceneManager.currentRoom.ConnectedRooms.Length.ToString());
            else
                PrintAndColor($"You notice a single path you can take.", "single");

            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Examine the room");
            Console.WriteLine("2. View stats and inventory");
            Console.WriteLine("3. Leave the room");

            if (SceneManager.currentRoom.discoveredStatus)
            {
                if (SceneManager.currentRoom.isDangerous)
                {
                    if (SceneManager.currentRoom.minLevel <= SceneManager.player.level)
                        PrintAndColor($"4. Look for enemies [lvl.{SceneManager.currentRoom.minLevel}-{SceneManager.currentRoom.maxLevel}]", $"[lvl.{SceneManager.currentRoom.minLevel}-{SceneManager.currentRoom.maxLevel}]", ConsoleColor.DarkYellow);
                    else
                        PrintAndColor($"4. Look for enemies [lvl.{SceneManager.currentRoom.minLevel}-{SceneManager.currentRoom.maxLevel}]", $"[lvl.{SceneManager.currentRoom.minLevel}-{SceneManager.currentRoom.maxLevel}]", ConsoleColor.DarkRed);
                }
                if (SceneManager.currentRoom.isSafeZone)
                    Console.WriteLine("4. Take a nap on the couch");
            }
        }

        public static void PrintItemFindingMenu()
        {
            PrintAndColor($"{SceneManager.currentRoom.ItemFindDescription} {SceneManager.currentRoom.ItemsArr[0].name}.", SceneManager.currentRoom.ItemsArr[0].name, ConsoleColor.Yellow);

            Console.WriteLine("\nWhat would you like to do?");
            if (!SceneManager.player.IsItemInventoryFull())
                Console.WriteLine("1. Take it");
            else
                PrintAndColor("1. Take it and leave another item here. [Inventory's full]",
                    "[Inventory's full]", ConsoleColor.Red);
            Console.WriteLine("2. Leave it");
        }

        public static void PrintMainMenu()
        {
            Console.WriteLine("             ▄▄");
            Console.WriteLine("▀███▀▀▀██▄   ██          ██            ██");
            Console.WriteLine("  ██    ▀██▄             ██            ██");
            Console.WriteLine("  ██     ▀█████  ▄██▀████████ ▄█▀██▄ ██████  ▄██▀██▄▀███▄███");
            Console.WriteLine("  ██      ██ ██ ██▀  ██  ██  ██   ██   ██   ██▀   ▀██ ██▀ ▀▀");
            Console.WriteLine("  ██     ▄██ ██ ██       ██   ▄█████   ██   ██     ██ ██");
            Console.WriteLine("  ██    ▄██▀ ██ ██▄    ▄ ██  ██   ██   ██   ██▄   ▄██ ██");
            Console.WriteLine("▄████████▀ ▄████▄█████▀  ▀████████▀██▄ ▀████ ▀█████▀▄████▄");
            Console.WriteLine("");
            Console.WriteLine("    ______     ______        ______   __  __     ______    ");
            Console.WriteLine("   /\\  __ \\   /\\  ___\\      /\\__  _\\ /\\ \\_\\ \\   /\\  ___\\   ");
            Console.WriteLine("   \\ \\ \\/\\ \\  \\ \\  __\\      \\/_/\\ \\/ \\ \\  __ \\  \\ \\  __\\   ");
            Console.WriteLine("    \\ \\_____\\  \\ \\_\\           \\ \\_\\  \\ \\_\\ \\_\\  \\ \\_____\\ ");
            Console.WriteLine("     \\/_____/   \\/_/            \\/_/   \\/_/\\/_/   \\/_____/ ");
            Console.WriteLine("");
            Console.WriteLine("     ▀████▀  ▀████▀▀");
            Console.WriteLine("       ██      ██");
            Console.WriteLine("       ██      ██    ▄██▀██▄▀███  ▀███  ▄██▀███ ▄▄█▀██");
            Console.WriteLine("       ██████████   ██▀   ▀██ ██    ██  ██   ▀▀▄█▀   ██");
            Console.WriteLine("       ██      ██   ██     ██ ██    ██  ▀█████▄██▀▀▀▀▀▀");
            Console.WriteLine("       ██      ██   ██▄   ▄██ ██    ██  █▄   ████▄    ▄");
            Console.WriteLine("     ▄████▄  ▄████▄▄ ▀█████▀  ▀████▀███▄██████▀ ▀█████▀");

            Console.WriteLine();
            Console.WriteLine("Welcome to my game! Please choose an option by writing the number below.");
            Console.WriteLine();

            Console.WriteLine("1. New game");
            Console.WriteLine("2. Quit");
        }



        // -------------------------------------- Combat: -------------------------------------- //
        public static void PrintFight()
        {
            PrintFightMembers();
            Console.WriteLine("\nWhat action will you take?");
            Console.WriteLine("1. Attack [Normal Attack]");
            Console.WriteLine("2. Attack with a weapon");
            Console.WriteLine("3. Flee");
        }

        public static void PrintFightMembers()
        {
            PrintAndColor($"{SceneManager.player.name} [lvl.{SceneManager.player.level}]: {SceneManager.player.HP} HP | {SceneManager.player.baseDMG} DMG", $"{SceneManager.player.name}", ConsoleColor.Magenta);
            PrintAndColor($"{SceneManager.currentEnemy.name} [lvl.{SceneManager.currentEnemy.level}]: {SceneManager.currentEnemy.HP} HP | {SceneManager.currentEnemy.baseDMG} DMG", $"{SceneManager.currentEnemy.name}", ConsoleColor.DarkRed);
        }

        public static void PrintBossFight()
        {
            PrintFightMembers();

            Console.WriteLine("\nWhat action will you take?");
            Console.WriteLine("1. Attack [Normal Attack]");
            Console.WriteLine("2. Attack with a weapon");
            Console.WriteLine("3. Use item");
        }

        public static void PrintBossDialog()
        {
            Console.Write("You resolved to summon your bravery and confront Koda,"); Console.ReadLine();
            Console.Write("With your tail held high and whiskers twitching,"); Console.ReadLine();
            Console.Write("Despite being just a tiny cat..."); Console.ReadLine();
            PrintAndColor("you’re determined to put an end to Hatol’s tyranny.", null, ConsoleColor.DarkRed); Console.ReadLine();
            PrintAndColor($"Koda lifts his head, locking eyes with you as you enter his territory. \nA low growl rumbles from his throat as he stands, baring his teeth.",
                "Koda", ConsoleColor.DarkRed);

            Console.WriteLine("\nPress enter to start the fight."); Console.ReadLine();

        }

        public static void PrintWeaponFindingMenu()
        {
            Console.WriteLine("\nWhat would you like to do?");
            if (!SceneManager.player.IsWeaponInventoryFull())
                Console.WriteLine("1. Take it");
            else
                Console.WriteLine("1. Replace it with one of your weapons [Inventory's full]");
            Console.WriteLine("2. Leave it");
        }


        // -------------------------------- Functional Methods: -------------------------------- //
        public static void PrintAndColor(string text, string targetText, ConsoleColor color = ConsoleColor.Blue)
        {
            if (targetText == null)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(text);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            int index = text.IndexOf(targetText);
            if (index == -1) return;
            Console.Write(text.Substring(0, index));
            Console.ForegroundColor = color;
            Console.Write(text.Substring(index, targetText.Length));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text.Substring(index + targetText.Length, text.Length - index - targetText.Length));
        }
    }
}
