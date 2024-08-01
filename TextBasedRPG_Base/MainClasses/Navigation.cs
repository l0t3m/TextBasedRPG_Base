using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_Base.MainClasses
{
    public static class Navigation
    {
        // -------------------------- Attributes and Constructors: -------------------------- //
        private static Room EntranceArea = new Room("the entrance area");
        private static Room Koda = new Room("Koda's territory");
        private static Room Toilet = new Room("the toilet", 18, 23);
        private static Room Stairs = new Room("front of the stairs");
        private static Room LivingRoom = new Room("the living room");
        private static Room DiningTable = new Room("front of the dining area", 1, 3);
        private static Room Hallway = new Room("the hallway");
        private static Room BackEntranceArea = new Room("the back entrance area", 7, 10);
        private static Room Kitchen = new Room("the kitchen", 3, 7);
        private static Room Miklat = new Room("the Miklat", 10, 15 );



        // ------------------------------------- Setup: ------------------------------------- //
        public static void SetupRooms()
        {
            // ConnectedRooms to each room.
            EntranceArea.ConnectedRooms = [Koda, LivingRoom, DiningTable, Hallway];
            Koda.ConnectedRooms = [Toilet, Stairs, EntranceArea];
            Toilet.ConnectedRooms = [Koda];
            Stairs.ConnectedRooms = [Koda];
            LivingRoom.ConnectedRooms = [EntranceArea];
            DiningTable.ConnectedRooms = [EntranceArea];
            Hallway.ConnectedRooms = [BackEntranceArea, Kitchen, Miklat, EntranceArea];
            BackEntranceArea.ConnectedRooms = [Hallway];
            Kitchen.ConnectedRooms = [Hallway];
            Miklat.ConnectedRooms = [Hallway];

            SceneManager.currentRoom = EntranceArea;

            // Description of the room.
            // --WIP--

            // Items of each room.
            Koda.ItemsArr = ["BLUE BALL"];
            DiningTable.ItemsArr = ["EYE DROPS"];
            Miklat.ItemsArr = ["DOG FOOD"];
            EntranceArea.ItemsArr = ["DOG FOOD"];
        }



        // ------------------------------------ Methods: ------------------------------------ //
        public static void Explore() // The main function of this class.
        {
            PrintRoom();

            try
            {
                int choice = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (choice)
                {
                    case 1:
                        Examine(); break;
                    case 2:
                        LookForEnemies(); break;
                    case 3:
                        Stats(); break;
                    case 4:
                        Move(); break;
                }
            }
            catch
            {
                Console.Clear(); Explore();
            }
        }

        private static void Move()
        {
            Console.Clear();
            PrintRoom();
            PrintAndColor("\nYou chose to leave the room, which path will you take?", "leave");

            Dictionary<int, Room> roomDict = new Dictionary<int, Room>();
            int counter = 2;

            Console.WriteLine($"1. Stay.");
            // Add a (boss) warning and prevent access to Koda's room.
            foreach (Room room in SceneManager.currentRoom.ConnectedRooms)
            {
                roomDict.Add(counter, room);
                Console.WriteLine($"{counter}. {room.Name}");
                counter++;
            }

            try
            {
                int choice = int.Parse(Console.ReadLine());

                Console.Clear();
                if (choice == 1)
                {
                    Explore();
                }
                SceneManager.currentRoom = roomDict[choice];
            }
            catch
            {
                Move();
            }
        }

        private static void PrintRoom()
        {
            if (SceneManager.currentRoom.discoveredIfDangerous)
            {
                if (SceneManager.currentRoom.isDangerous)
                    PrintAndColor($"You're currently in {SceneManager.currentRoom.Name}. [Dangerous]", SceneManager.currentRoom.Name, ConsoleColor.Red);
                else
                    PrintAndColor($"You're currently in {SceneManager.currentRoom.Name}. [Safe]", SceneManager.currentRoom.Name);
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
            Console.WriteLine("1. Examine the room.");
            Console.WriteLine("2. Look for enemies.");
            Console.WriteLine("3. Check stats.");
            Console.WriteLine("4. Leave the room.");
        }

        private static void Examine()
        {
            if (SceneManager.currentRoom.ItemsArr != null && SceneManager.currentRoom.ItemsArr.Length > 0)
            {
                PrintAndColor($"You found {SceneManager.currentRoom.ItemsArr[0]}.", SceneManager.currentRoom.ItemsArr[0], ConsoleColor.Yellow);
                // add a condition to check what to do with the item found. + switch case
            }
            else
            {
                Console.WriteLine("You didn't find anything useful.");
            }

            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            Console.Clear();
        }

        private static void LookForEnemies()
        {
            SceneManager.currentRoom.discoveredIfDangerous = true;
            if (SceneManager.currentRoom.isDangerous == false)
            {
                Console.WriteLine("This area feels safe, enemies can't reach this area.");
                Console.ReadLine(); Console.Clear();
                return;
            }

            //Combat.StartFight(new Enemy())

            Console.WriteLine("You chose to look for enemies.");
            // insert chance to actually find an enemy.


        }

        private static void Stats()
        {
            Console.WriteLine("You chose to check your stats.");
            // add stats print in here

            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            Console.Clear();
        }
        


        // ------------------------------------- TEMP: ------------------------------------- //

        // move this function into another class. SceneManager (?)
        public static void PrintAndColor(string text, string targetText, ConsoleColor color = ConsoleColor.Blue)
        {
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
