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
        private static Room Toilet = new Room("the toilet");
        private static Room Stairs = new Room("front of the stairs");
        private static Room LivingRoom = new Room("the living room");
        private static Room DiningTable = new Room("front of the dining area");
        private static Room Hallway = new Room("the hallway");
        private static Room BackEntranceArea = new Room("the back entrance area");
        private static Room Kitchen = new Room("the kitchen");
        private static Room Miklat = new Room("the Miklat");

        private static Room currentRoom { get; set; }



        // ------------------------------------- Setup: ------------------------------------- //
        /// <summary>
        /// Creates all of the first floor Room objects.
        /// </summary>
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

            currentRoom = EntranceArea;

            // Description of the room.
            // --WIP--

            // Props of each room.
            Koda.PropsArr = ["BLUE BALL"];
            DiningTable.PropsArr = ["EYE DROPS"];
            Miklat.PropsArr = ["DOG FOOD"];
            EntranceArea.PropsArr = ["DOG FOOD"];
        }



        // ------------------------------------ Methods: ------------------------------------ //
        /// <summary>
        /// The main function of this clas, manages the exploring mode.
        /// </summary>
        public static void Explore()
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
                        Stats(); break;
                    case 3:
                        Move(); break;
                }
            }
            catch
            {
                Explore();
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
            foreach (Room room in currentRoom.ConnectedRooms)
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
                currentRoom = roomDict[choice];
            }
            catch
            {
                Move();
            }
        }

        private static void PrintRoom()
        {
            PrintAndColor($"You're currently in {currentRoom.Name}.", currentRoom.Name);
            // Print the current room description.

            if (currentRoom.ConnectedRooms.Length > 1)
            {
                PrintAndColor($"You notice {currentRoom.ConnectedRooms.Length} different paths to take.", currentRoom.ConnectedRooms.Length.ToString());
            }
            else
            {
                PrintAndColor($"You notice a single path you can take.", "single");
            }

            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Examine the room.");
            Console.WriteLine("2. Check stats.");
            Console.WriteLine("3. Leave the room.");
        }

        private static void Examine()
        {
            if (currentRoom.PropsArr != null && currentRoom.PropsArr.Length > 0)
            {
                PrintAndColor($"You found {currentRoom.PropsArr[0]}.", currentRoom.PropsArr[0], ConsoleColor.Yellow);
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
