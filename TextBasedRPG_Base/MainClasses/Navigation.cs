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
        private static Room Toilet = new Room("the toilet");
        private static Room Stairs = new Room("front of the stairs");
        private static Room LivingRoom = new Room("the living room");
        private static Room DiningTable = new Room("front of the dining area", 1, 3);
        private static Room Hallway = new Room("the hallway");
        private static Room BackEntranceArea = new Room("the back entrance area", 7, 9);
        private static Room Kitchen = new Room("the kitchen", 4, 6);
        private static Room Miklat = new Room("the Miklat", 10, 13);



        // ------------------------------------- Setup: ------------------------------------- //
        public static void SetupRooms()
        {
            // Each room's connected rooms:
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

            SceneManager.currentRoom = EntranceArea; // change if you want to start in a different room
            Koda.isBossRoom = true;
            Koda.boss = new Boss("Koda", 75, 15, 15);
            LivingRoom.isSafeZone = true;

            // Each room descriptions:
            EntranceArea.Description = "This area is tidy, with the exception of a few shoes scattered by the door.";
            EntranceArea.ItemFindDescription = "Your eyes fall on a pair of shoes, and you decide to look inside one of them, finding";
            Koda.Description = "A beautiful white Samoyed is staring at you with a look of clear dissatisfaction on its face.";
            Koda.canItemsSpawn = false;
            Toilet.Description = "An ordinary white toilet sits facing a small sink, with a shelf positioned above it.";
            Toilet.canItemsSpawn = false;
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
        }

        public static void SetupItems()
        {
            DiningTable.AddRoomItem(new Item("eye drops", ItemEffect.InstantDamage));
            LivingRoom.AddRoomItem(new Item("salmon treats", ItemEffect.InstantHeal));
            Miklat.AddRoomItem(new Item("dog food", ItemEffect.InstantHeal));
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
                Console.Clear();
            }
        }

        private static void Move()
        {
            Console.Clear();
            Functions.PrintRoom();
            Functions.PrintAndColor("\nYou chose to leave the room, which path will you take?", "leave");

            Console.WriteLine($"1. Stay");
            Dictionary<int, Room> roomDict = new Dictionary<int, Room>();
            int counter = 2;
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
                if (roomDict[choice].isBossRoom && roomDict[choice].boss.isAlive)
                {
                    if (SceneManager.player.level >= roomDict[choice].boss.level)
                    {
                        SceneManager.currentEnemy = roomDict[choice].boss;
                        Combat.StartBossFight();
                    }
                    else
                    {
                        Functions.TypeLine("As you get closer to Koda, he grows more intimidating with each step. You shake your head, your tail drooping between your legs.");
                        Functions.PrintAndColorType("\nIn that moment, you decide to turn around and avoid facing him.", "turn around and avoid facing him", ConsoleColor.DarkRed);
                        Console.WriteLine("\nPress enter to continue."); Console.ReadLine(); Console.Clear();
                    }
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
            // Discovers the status of the room
            SceneManager.currentRoom.discoveredStatus = true;

            if (SceneManager.currentRoom.isDangerous == true)
                Functions.PrintAndColor("This area feels dangerous...", "dangerous", SceneManager.currentRoom.statusColor);
            else if (SceneManager.currentRoom.isSafeZone == false)
                Functions.PrintAndColor("This area looks neutral but still not safe enough.", "neutral", SceneManager.currentRoom.statusColor);
            else
                Functions.PrintAndColor("This area looks safe, enemies can't reach this area.", "safe", SceneManager.currentRoom.statusColor);

            // Finding items if there are any
            if (SceneManager.currentRoom.ItemsArr != null)
            {
                if (SceneManager.currentRoom.ItemsArr.Count(n => n == null) == 0)
                {
                    Console.WriteLine();
                    ItemFindingMenu(SceneManager.currentRoom.ItemsArr[0]);
                }
            }

            Console.WriteLine("\nPress enter to continue."); Console.ReadLine(); Console.Clear();
        }

        private static void LookForEnemies()
        {
            if (Random.Shared.Next(0, 100) < 95)
            {
                if (Combat.StartFight(Enemy.GenerateNewEnemy()) == false) // if player has died during enemy fight
                    DoRespawn();
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
            Functions.PrintAndColor("A day has passed...", null, ConsoleColor.DarkYellow);
            Functions.PrintAndColor("\nHP has been restored to max", null, ConsoleColor.Green);
            Functions.PrintAndColor("\nPress enter to wake up.", null, ConsoleColor.DarkGray);
            Console.ReadLine(); Console.Clear();
        }

        private static void Stats()
        {
            SceneManager.player.PrintStats();
            Console.WriteLine("\nPress enter to continue."); Console.ReadLine(); Console.Clear();
        }

        public static void DoRespawn()
        {
            Console.WriteLine("\nPress enter to continue."); Console.ReadLine(); Console.Clear();

            SceneManager.player.DoRest();
            SceneManager.currentRoom = LivingRoom;
        }



        // ------------------------------ Sub-Menu Methods: ------------------------------ //
        private static void ItemFindingMenu(Item item)
        {
            Functions.PrintItemFindingMenu();
            try
            {
                int choice = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        if (SceneManager.player.IsItemInventoryFull())
                            ItemSwitchMenu(item);
                        else
                        {
                            Functions.PrintAndColor($"You nod slightly, a faint purr escaping as you move closer to the {item.name}, then delicately pick it up with your mouth.",
                            item.name, ConsoleColor.Yellow);
                            SceneManager.currentRoom.RemoveRoomItem(item);
                            SceneManager.player.AddItemPlayer(item);
                        }
                        break;
                    case 2:
                        Functions.PrintAndColor($"You shake your head and walk away, leaving the {item.name} behind.",
                            item.name, ConsoleColor.Yellow);
                        break;
                    default:
                        ItemFindingMenu(item);
                        break;
                }
            }
            catch
            {
                Console.Clear(); ItemFindingMenu(item);
            }
        }

        private static void ItemSwitchMenu(Item newItem)
        {
            Functions.PrintAndColor($"You nod slightly, a faint purr escaping as you move closer to the {newItem.name}, then delicately pick it up with your mouth.",
                            newItem.name, ConsoleColor.Yellow);
            Functions.PrintAndColor($"\nSwitching {newItem.name} with...?", newItem.name, ConsoleColor.Yellow);
            SceneManager.player.PrintItems();

            try
            {
                int backChoice = SceneManager.player.itemInventory.Count(n => n != null) + 1;
                Console.WriteLine($"| {backChoice}. Go back");
                int choice = int.Parse(Console.ReadLine()); Console.Clear();
                if (choice == backChoice)
                    ItemFindingMenu(newItem);

                Item oldItem = SceneManager.player.itemInventory[choice - 1];
                if (oldItem != null)
                {
                    SceneManager.player.SwitchItemPlayer(oldItem, newItem);
                    SceneManager.currentRoom.RemoveRoomItem(newItem);
                    SceneManager.currentRoom.AddRoomItem(oldItem);
                }
            }
            catch
            {
                Console.Clear(); ItemSwitchMenu(newItem);
            }
        }

        public static void WeaponFindingMenu(Weapon weapon)
        {
            Functions.PrintAndColor($"You let out a relieved purr after defeating the enemy, then suddenly spot a {weapon.name}.\n", weapon.name, ConsoleColor.Yellow);
            weapon.PrintWeapon();
            Functions.PrintWeaponFindingMenu();

            try
            {
                int choice = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        if (SceneManager.player.IsWeaponInventoryFull())
                        {
                            WeaponSwitchMenu(weapon);
                        }
                        else
                        {
                            Console.WriteLine("You chose to take it");
                            SceneManager.player.AddWeapon(weapon);
                        }
                        break;
                    case 2:
                        Console.WriteLine("You chose to leave it.");
                        break;
                    default:
                        WeaponFindingMenu(weapon);
                        break;
                }
            }
            catch
            {
                Console.Clear(); WeaponFindingMenu(weapon);
            }
        }

        private static void WeaponSwitchMenu(Weapon newWeapon)
        {
            Console.WriteLine($"Switching {newWeapon.name} [{newWeapon.damage} DMG | {newWeapon.durability} uses left] with...?");
            SceneManager.player.PrintWeapons();

            try
            {
                int backChoice = SceneManager.player.weapons.Count(n => n != null) + 1;
                Console.WriteLine($"| {backChoice}. Go back");
                int choice = int.Parse(Console.ReadLine()); Console.Clear();
                if (choice == backChoice)
                {
                    WeaponFindingMenu(newWeapon);
                    return;
                }

                Weapon oldWeapon = SceneManager.player.weapons[choice - 1];
                if (oldWeapon != null)
                    SceneManager.player.SwitchWeapon(oldWeapon, newWeapon);
            }
            catch
            {
                Console.Clear(); WeaponSwitchMenu(newWeapon);
            }
        }
    }
}
