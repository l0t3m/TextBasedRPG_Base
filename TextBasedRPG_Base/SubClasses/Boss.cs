using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.MainClasses;

namespace TextBasedRPG_Base.SubClasses
{
    public class Boss : Character
    {
        // -------------------------- Attributes and Constructors: -------------------------- //
        public Boss(string name, int maxHP, int baseDMG, Weapon weapon, int level) 
            : base(name, maxHP, baseDMG, new Weapon[] {weapon}, level)
        {
            // needs to have a list of 2 props. (heal, weaken player)
        }



        // ------------------------------------- Setup: ------------------------------------- //
        
        // add a function that generates all the bosses 
            // Koda
            // Mini
            // Hatol



        // ------------------------------------ Methods: ------------------------------------ //



        // ------------------------------------- TEMP: ------------------------------------- //
    }
}
