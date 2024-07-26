using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_Base
{
    public class Room
    {
        public string Name { get; }
        public Room[] ConnectedRooms { get; set; } // add room descriptions
        public string[] PropsArr { get; set; } // transfer props into a class

        public Room(string name) 
        { 
            this.Name = name;
        }
    }
}
