using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.SubClasses;

namespace TextBasedRPG_Base.MainClasses
{
    public abstract class Character
    {
        // -------------------------- Attributes and Constructors: -------------------------- //
        protected string name;
        public int HP { protected set; get; }
        protected int maxHP;
        protected int baseDMG { set; get; }
        public Weapon[] weapons { protected set; get; }
        // props
        public bool isAlive { protected set; get; }
        public int level { protected set; get; }


        public Character(string name, int maxHP, int baseDMG, Weapon[] weapons, int level) // for Enemy and Boss class.
        {
            this.name = name;
            this.maxHP = maxHP;
            this.HP = maxHP;
            this.isAlive = true;
            this.level = level;

            this.baseDMG = baseDMG;
            this.weapons = weapons;
            // props
        }
        
        public Character(string name, int maxHP, int baseDMG, int weaponSlots) // for Player class.
        {
            this.name = name;
            this.maxHP = maxHP;
            this.HP = maxHP;
            this.isAlive = true;
            this.level = 1;

            this.baseDMG = baseDMG;
            this.weapons = new Weapon[weaponSlots];
            // props
        }



        // ------------------------------------ Methods: ------------------------------------ //
        /// <summary>
        /// Adds HP to the object, considering its maxHP attribute.
        /// </summary>
        public void AddHP(int amount)
        {
            this.HP += amount;
            if (this.HP < this.maxHP)
                this.HP = this.maxHP;
        }

        /// <summary>
        /// Removes HP from the object, changes its isAlive attribute if needed.
        /// </summary>
        public void RemoveHP(int amount)
        {
            this.HP -= amount;
            if (this.HP <= 0)
            {
                Console.WriteLine($"{name} has died.");
                this.isAlive = false;
            }
        }



        // ------------------------------------- TEMP: ------------------------------------- //
        /// <summary>
        /// Prints the current object's stats using its attributes.
        /// </summary>
        public virtual void PrintStats()
        {
            Console.WriteLine("\n-----------------------------");
            Console.WriteLine($"{this.name} ({(this.isAlive == true ? "Alive" : "Dead")})");
            Console.WriteLine($"{this.HP} / {this.maxHP} HP");
            Console.WriteLine($"level {this.level}");
            Console.WriteLine($"{this.weapons.Length} weapon slots");

            foreach (Weapon weapon in this.weapons)
            {
                if (weapon != null)
                    weapon.PrintWeapon();
            }

            // props
            Console.WriteLine("-----------------------------\n");
        }
    }
}
