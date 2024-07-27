using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_Base.SubClasses
{
    public class Weapon
    {
        // -------------------------- Attributes and Constructors: -------------------------- //
        public string name { get; private set; }
        public int durability { get; private set; }
        public int damage { get; private set; }

        public Weapon(string name, int durability, int damage)
        {
            this.name = name;
            this.durability = durability;
            this.damage = damage;
        }



        // ------------------------------------ Methods: ------------------------------------ //

        /// <returns> True = the weapon should be destroyed, otherwise False </returns>
        public bool RemoveDurability(int amount) 
        {
            this.durability -= amount;
            return this.durability <= 0;
        }



        // ------------------------------------- TEMP: ------------------------------------- //
        public void PrintWeapon()
        {
            Console.WriteLine($"\t- {this.name} | {this.damage} dmg | {this.durability} uses left");
        }
    }
}
