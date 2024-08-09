using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.SubClasses;

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
            Koda.ConnectedRooms = [Toilet, Stairs, EntranceArea, DiningTable];
            Toilet.ConnectedRooms = [Koda];
            Stairs.ConnectedRooms = [Koda];
            LivingRoom.ConnectedRooms = [EntranceArea, DiningTable];
            DiningTable.ConnectedRooms = [EntranceArea, Koda, Hallway, LivingRoom];
            Hallway.ConnectedRooms = [BackEntranceArea, Kitchen, Miklat, EntranceArea, DiningTable];
            BackEntranceArea.ConnectedRooms = [Hallway, Miklat, Kitchen];
            Kitchen.ConnectedRooms = [Hallway, Miklat, BackEntranceArea];
            Miklat.ConnectedRooms = [Hallway, BackEntranceArea, Kitchen];

            SceneManager.currentRoom = EntranceArea;
            Koda.isBossRoom = true;
            LivingRoom.isSafeZone = true;

            // Description of the room.
            EntranceArea.Description = "description";
            Koda.Description = "description";
            Toilet.Description = "description";
            Stairs.Description = "description";
            LivingRoom.Description = "description";
            DiningTable.Description = "description";
            Hallway.Description = "description";
            BackEntranceArea.Description = "description";
            Kitchen.Description = "description";
            Miklat.Description = "description";


            // Items of each room.
            Koda.ItemsArr = ["BLUE BALL"];
            DiningTable.ItemsArr = ["EYE DROPS"];
            Miklat.ItemsArr = ["DOG FOOD"];
            EntranceArea.ItemsArr = ["DOG FOOD"];
        }



        // ------------------------------------ Methods: ------------------------------------ //
        public static void Explore()
        {
            Prints.PrintRoom();

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
                    case 4:
                        if (SceneManager.currentRoom.discoveredStatus)
                        {
                            if (SceneManager.currentRoom.isDangerous)
                                LookForEnemies();
                            if (SceneManager.currentRoom.isSafeZone)
                                SafeZone();
                        }
                        break;
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
            Prints.PrintRoom();
            Prints.PrintAndColor("\nYou chose to leave the room, which path will you take?", "leave");

            Dictionary<int, Room> roomDict = new Dictionary<int, Room>();
            int counter = 2;

            Console.WriteLine($"1. Stay");
            foreach (Room room in SceneManager.currentRoom.ConnectedRooms)
            {
                roomDict.Add(counter, room);
                Prints.PrintAndColor($"{counter}. {room.Name} {room.status}", (string)room.status, room.statusColor);
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
                if (roomDict[choice].isBossRoom == true)
                {
                    // if player's level >= boss level, let in
                    // else "Access denied, your level is too low."
                    Prints.PrintAndColor("Acess denied, your level is too low.", null, ConsoleColor.DarkRed);
                    Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();
                }
                else
                    SceneManager.currentRoom = roomDict[choice];
            }
            catch
            {
                Move();
            }
        }

        private static void Examine()
        {
            SceneManager.currentRoom.discoveredStatus = true;

            if (SceneManager.currentRoom.isDangerous == true)
                Prints.PrintAndColor("This area feels dangerous...", "dangerous", SceneManager.currentRoom.statusColor);
            else if (SceneManager.currentRoom.isSafeZone == false)
                Prints.PrintAndColor("This area looks neutral but still not safe enough.", "neutral", SceneManager.currentRoom.statusColor);
            else
                Prints.PrintAndColor("This area looks safe, enemies can't reach this area.", "safe", SceneManager.currentRoom.statusColor);



            if (SceneManager.currentRoom.ItemsArr != null && SceneManager.currentRoom.ItemsArr.Length > 0)
            {
                Prints.PrintAndColor($"You found {SceneManager.currentRoom.ItemsArr[0]}.", SceneManager.currentRoom.ItemsArr[0], ConsoleColor.Yellow);
                // add a condition to check what to do with the item found. + switch case
            }


            Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();
        }

        private static void LookForEnemies()
        {
            // insert chance to actually find an enemy.
            if (Combat.StartFight(Enemy.GenerateNewEnemy()) == false)
            {
                SceneManager.GameOver();
            }
        }

        private static void SafeZone()
        {
            Prints.PrintSafeZone();

            try
            {
                int choice = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (choice)
                {
                    case 1:
                        SceneManager.player.DoRest();
                        Console.WriteLine("You decided to rest for the day. HP has restored to max.");
                        Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();
                        break;
                    case 2:
                        Explore();
                        break;
                }
            }
            catch
            {
                Console.Clear(); SafeZone();
            }

            Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();
        }

        private static void Stats()
        {
            SceneManager.player.PrintStats();
            Console.WriteLine("Press enter to continue."); Console.ReadLine(); Console.Clear();
        }
        


        // ------------------------------------- TEMP: ------------------------------------- //

    }
}
