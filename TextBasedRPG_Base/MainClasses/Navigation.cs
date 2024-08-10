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

            //SceneManager.currentRoom = EntranceArea;
            SceneManager.currentRoom = DiningTable; // debug
            Koda.isBossRoom = true;
            LivingRoom.isSafeZone = true;

            // Description of the room.
            EntranceArea.Description = "This area is tidy, with the exception of a few shoes scattered by the door.";
            EntranceArea.ItemFindDescription = "Your eyes fall on a pair of shoes, and you decide to look inside one of them, finding";
            
            Koda.Description = "A beautiful white Samoyed is staring at you with a look of clear dissatisfaction on its face.";
            Koda.canItemsSpawn = false;
            
            Toilet.Description = "An ordinary white toilet sits facing a small sink, with a shelf positioned above it.";
            Toilet.ItemFindDescription = "You move closer to the shelf but can't see what's on it. You carefully hop onto the shelf, eventually locating";
            
            Stairs.Description = "A long set of light gray marble stairs.";
            Stairs.canItemsSpawn = false;
            
            LivingRoom.Description = "The room features an 'L' shaped black sofa, littered with scratch marks scattered across its surface. A small white coffee table sits at the center, and a large TV faces the sofa. The overall design of the space is modern, standing out in contrast to the more traditional decor of the other rooms on that floor.";
            LivingRoom.ItemFindDescription = "You spot two drawers in the coffee table; one of them is slightly open. You manage to slide it fully open and find";
            
            DiningTable.Description = "A long white dining table, flanked by three white chairs on each side, with scratch marks covering the tops of the chairs. In the center of the table, a beautiful decorative bowl with a marble texture adds an elegant touch to the otherwise simple setup.";
            DiningTable.ItemFindDescription = "You glance inside the decorative bowl and spot";
            
            Hallway.Description = "A short hallway connects several rooms, lined with a cozy carpet that adds warmth and comfort to the space.";
            Hallway.canItemsSpawn = false;
            
            BackEntranceArea.Description = "A large wooden door with scratches on its lower part stands near two refrigerators, one black and one white.";
            BackEntranceArea.ItemFindDescription = "You spot a small gap behind the white refrigerator, and find";
            
            Kitchen.Description = "The kitchen is filled with bright wood cabinets and a variety of appliances. A wide, short window lets in a gentle stream of light, brightening the space.";
            Kitchen.ItemFindDescription = "You hop onto the counter and explore the window sill, where you locate";
            
            Miklat.Description = "A small, cramped room filled with shelves and cluttered with various items and junk.";
            Miklat.ItemFindDescription = "You scan the shelves and spot";


            // Items of each room.
            BackEntranceArea.ItemsArr = ["blue squicky ball"];
            DiningTable.ItemsArr = ["eye drops"];
            Miklat.ItemsArr = ["dog food"];
            EntranceArea.ItemsArr = ["a shoe lace"];
        }



        // ------------------------------------ Methods: ------------------------------------ //
        public static void Explore()
        {
            Functions.PrintRoom();

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
                    default:
                        Explore(); break;

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
            Functions.PrintRoom();
            Functions.PrintAndColor("\nYou chose to leave the room, which path will you take?", "leave");

            Dictionary<int, Room> roomDict = new Dictionary<int, Room>();
            int counter = 2;

            Console.WriteLine($"1. Stay");
            foreach (Room room in SceneManager.currentRoom.ConnectedRooms)
            {
                roomDict.Add(counter, room);
                Functions.PrintAndColor($"{counter}. {room.Name} {room.status}", (string)room.status, room.statusColor);
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
                    Functions.PrintAndColor("Acess denied, your level is too low.", null, ConsoleColor.DarkRed);
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
                Functions.PrintAndColor("This area feels dangerous...", "dangerous", SceneManager.currentRoom.statusColor);
            else if (SceneManager.currentRoom.isSafeZone == false)
                Functions.PrintAndColor("This area looks neutral but still not safe enough.", "neutral", SceneManager.currentRoom.statusColor);
            else
                Functions.PrintAndColor("This area looks safe, enemies can't reach this area.", "safe", SceneManager.currentRoom.statusColor);



            if (SceneManager.currentRoom.ItemsArr != null && SceneManager.currentRoom.ItemsArr.Length > 0)
            {
                Functions.PrintAndColor($"\n{SceneManager.currentRoom.ItemFindDescription} {SceneManager.currentRoom.ItemsArr[0]}.", SceneManager.currentRoom.ItemsArr[0], ConsoleColor.Yellow);
                // add a condition to check what to do with the item found. + switch case
            }


            Console.WriteLine("\nPress enter to continue."); Console.ReadLine(); Console.Clear();
        }

        private static void LookForEnemies()
        {
            if (Random.Shared.Next(0, 100) < 95)
            {
                if (Combat.StartFight(Enemy.GenerateNewEnemy()) == false)
                {
                    SceneManager.GameOver();
                }
            }
            else
            {
                Console.WriteLine("You didn't find any enemies.");
                Console.WriteLine("\nPress enter to continue."); Console.ReadLine(); Console.Clear();
            }
        }

        private static void SafeZone()
        {
            SceneManager.player.DoRest();

            Console.WriteLine("You settled onto the couch, feeling relaxed and secure. As you closed your eyes, you quickly drifted off to sleep.");
            Console.WriteLine("\nA day has passed...");
            Functions.PrintAndColor("\nHP has been restored to max", null, ConsoleColor.Green);
            Functions.PrintAndColor("\nPress enter to wake up.", null, ConsoleColor.DarkGray);
            Console.ReadLine(); Console.Clear();
        }

        private static void Stats()
        {
            SceneManager.player.PrintStats();
            Console.WriteLine("\nPress enter to continue."); Console.ReadLine(); Console.Clear();
        }
        


        // ------------------------------------- TEMP: ------------------------------------- //

    }
}
