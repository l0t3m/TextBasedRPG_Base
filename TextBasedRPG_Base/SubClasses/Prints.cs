using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.MainClasses;

namespace TextBasedRPG_Base.SubClasses
{
    public static class Prints
    {
        // Navigation:
        public static void PrintRoom()
        {
            if (SceneManager.currentRoom.discoveredIfDangerous)
            {
                if (SceneManager.currentRoom.isDangerous)
                    PrintAndColor($"You're currently in {SceneManager.currentRoom.Name}. [Dangerous]", "[Dangerous]", ConsoleColor.DarkRed);
                else
                    PrintAndColor($"You're currently in {SceneManager.currentRoom.Name}. [Safe]", "[Safe]");
            }
            else
            {
                PrintAndColor($"You're currently in {SceneManager.currentRoom.Name}.", SceneManager.currentRoom.Name, ConsoleColor.DarkGray);
            }

            // Print the current room description.

            if (SceneManager.currentRoom.ConnectedRooms.Length > 1)
            {
                PrintAndColor($"You notice {SceneManager.currentRoom.ConnectedRooms.Length} different paths to take.", SceneManager.currentRoom.ConnectedRooms.Length.ToString());
            }
            else
            {
                PrintAndColor($"You notice a single path you can take.", "single");
            }

            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Examine the room");
            Console.WriteLine("2. Check stats");
            Console.WriteLine("3. Leave the room");

            if (SceneManager.currentRoom.discoveredIfDangerous && SceneManager.currentRoom.isDangerous)
            {
                if (SceneManager.currentRoom.minLevel > SceneManager.player.level)
                    PrintAndColor($"4. Look for enemies [lvl.{SceneManager.currentRoom.minLevel}-{SceneManager.currentRoom.maxLevel}]", $"[lvl.{SceneManager.currentRoom.minLevel}-{SceneManager.currentRoom.maxLevel}]", ConsoleColor.DarkGreen);
                else
                    PrintAndColor($"4. Look for enemies [lvl.{SceneManager.currentRoom.minLevel}-{SceneManager.currentRoom.maxLevel}]", $"[lvl.{SceneManager.currentRoom.minLevel}-{SceneManager.currentRoom.maxLevel}]", ConsoleColor.DarkRed);
            }
        }



        // Combat:
        public static void PrintFight()
        {
            Prints.PrintAndColor($"{SceneManager.player.name}: {SceneManager.player.HP} HP | {SceneManager.player.baseDMG} DMG", $"{SceneManager.player.name}", ConsoleColor.Magenta);
            Prints.PrintAndColor($"{SceneManager.currentEnemy.name}: {SceneManager.currentEnemy.HP} HP | {SceneManager.currentEnemy.baseDMG} DMG", $"{SceneManager.currentEnemy.name}", ConsoleColor.Red);

            Console.WriteLine("\nWhat action will you take?");
            Prints.PrintAndColor($"1. Attack [{SceneManager.player.baseDMG} DMG]", $"{SceneManager.player.baseDMG} DMG", ConsoleColor.DarkRed);
            Console.WriteLine("2. Flee");
        }



        // Other:
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
    


        // menus:
        public static void PrintMainMenu()
        {
            Prints.PrintAndColor("Dictator of the house ^._.^", "^._.^", ConsoleColor.Cyan);
            Console.WriteLine("1. New game");
            Console.WriteLine("2. Quit");
        }
    }
}
