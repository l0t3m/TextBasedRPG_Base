using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_Base.MainClasses
{
    public class Room
    {
        // -------------------------- Attributes and Constructors: -------------------------- //
        public string Name { get; }
        public Room[] ConnectedRooms { get; set; } // add room descriptions

        public bool isDangerous { get; }
        public bool discoveredIfDangerous { get; set; }

        public int minLevel { get; set; }
        public int maxLevel { get; set; }



        public string[] ItemsArr { get; set; } // transfer props into a class

        public Room(string name)
        {
            Name = name;
            this.isDangerous = false;
            this.discoveredIfDangerous = false;
        }

        public Room(string name, int minLevel, int maxLevel)
        {
            Name = name;
            this.isDangerous = true;
            this.discoveredIfDangerous = false;
            this.minLevel = minLevel;
            this.maxLevel = maxLevel;
        }



        // ------------------------------------ Methods: ------------------------------------ //



        // ------------------------------------- TEMP: ------------------------------------- //
    }
}
