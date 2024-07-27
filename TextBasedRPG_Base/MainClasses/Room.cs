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
        public string[] PropsArr { get; set; } // transfer props into a class

        public Room(string name)
        {
            Name = name;
        }



        // ------------------------------------ Methods: ------------------------------------ //



        // ------------------------------------- TEMP: ------------------------------------- //
    }
}
