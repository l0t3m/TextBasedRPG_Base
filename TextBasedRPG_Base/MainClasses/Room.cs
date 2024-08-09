using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.SubClasses;

namespace TextBasedRPG_Base.MainClasses
{
    public class Room
    {
        // -------------------------- Attributes and Constructors: -------------------------- //
        public string Name { get; }
        public string Description { get; set; }
        public Room[] ConnectedRooms { get; set; } // add room descriptions

        // status related:
        public bool isDangerous { get; } = false;
        public bool isSafeZone { get; set; } = false;
        public bool isNeutral { get; } = false;
        //public bool discoveredStatus { get; set; } = false; // right line
        public bool discoveredStatus { get; set; } = true; // wrong line - debug purpose


        // Boss related:
        public int minLevel { get; set; }
        public int maxLevel { get; set; }
        public bool isBossRoom { get; set; }


        // Item related:
        public string[] ItemsArr { get; set; } // transfer props into a class

        public ConsoleColor statusColor { get => GetStatusColor(); }
        public string status { get =>  GetStatus(); }



        // CONSTRUCTORS:
        public Room(string name) // normal room (no enemies nor safezone)
        {
            Name = name;
            this.isNeutral = true;
        }

        public Room(string name, int minLevel, int maxLevel) // enemies
        {
            Name = name;
            this.isDangerous = true;
            this.minLevel = minLevel;
            this.maxLevel = maxLevel;
        }



        // ------------------------------------ Methods: ------------------------------------ //



        // ------------------------------------- TEMP: ------------------------------------- //
        private string GetStatus()
        {
            if (isBossRoom)
                return ($"[Boss lvl.-]");
            if (discoveredStatus) //uppercase the first letter
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
