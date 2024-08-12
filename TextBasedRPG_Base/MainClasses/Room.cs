using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.SubClasses;

namespace TextBasedRPG_Base.MainClasses
{
    public class Room
    {
        // ----------------------------------- Attributes: ----------------------------------- //
        public string Name { get; }
        public string Description { get; set; }
        public string ItemFindDescription { get; set; }
        public Room[] ConnectedRooms { get; set; }

        // --------- Status related: --------- //
        public bool isDangerous { get; } = false;
        public bool isSafeZone { get; set; } = false;
        public bool isNeutral { get; } = false;
        public bool discoveredStatus { get; set; } = false; // change to true to instantly see each room status
        public ConsoleColor statusColor { get => GetStatusColor(); }
        public string status { get => GetStatus(); }

        // ---------- Boss related: ---------- //
        public int minLevel { get; set; }
        public int maxLevel { get; set; }
        public bool isBossRoom { get; set; }
        public Boss boss { get; set; }

        // ---------- Item related: ---------- //
        public bool canItemsSpawn { get; set; } = true;
        public Item[] ItemsArr { get; private set; } = null;



        // --------------------------------- Constructors: --------------------------------- //
        public Room(string name) // for the neutral rooms
        {
            Name = name;
            this.isNeutral = true;
        }

        public Room(string name, int minLevel, int maxLevel) // for dangerous rooms
        {
            Name = name;
            this.isDangerous = true;
            this.minLevel = minLevel;
            this.maxLevel = maxLevel;
        }



        // ------------------------------------ Methods: ------------------------------------ //

        public bool AddRoomItem(Item item) // returns true if successful, false if there is already an item
        {
            if (this.canItemsSpawn)
            {
                if (this.ItemsArr == null)
                {
                    this.ItemsArr = new Item[] { item };
                    return true;
                }
            }
            return false;
        }

        public bool RemoveRoomItem(Item item) // true if successful, false if didn't find an item / room can't spawn items
        {
            if (this.canItemsSpawn)
            {
                if (this.ItemsArr.Contains(item))
                {
                    this.ItemsArr = null;
                    return true;
                }
            }
            return false;
        }



        // ------------------------------------- TEMP: ------------------------------------- //
        private string GetStatus()
        {
            if (isBossRoom)
                return ($"[Boss lvl.{boss.level}]");
            if (discoveredStatus)
            {
                if (isSafeZone)
                    return ($"[SafeZone]");
                if (isDangerous)
                    return ($"[Dangerous]");
                if (isNeutral)
                    return ($"[Neutral]");
            }
            return ("[Status Unknown]");
        }

        private ConsoleColor GetStatusColor()
        {
            if (isBossRoom)
                return ConsoleColor.DarkRed;
            if (discoveredStatus)
            {
                if (isSafeZone)
                    return ConsoleColor.DarkGreen;
                if (isDangerous)
                    return ConsoleColor.Red;
                if (isNeutral)
                    return ConsoleColor.Cyan;
            }
            return ConsoleColor.DarkGray;
        }
    }
}
