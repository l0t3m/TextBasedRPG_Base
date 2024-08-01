using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.SubClasses;

namespace TextBasedRPG_Base.MainClasses
{
    public static class SceneManager
    {
        public static readonly Player player = new Player("lotem");
        public static Room currentRoom { get; set; }
    }
}
