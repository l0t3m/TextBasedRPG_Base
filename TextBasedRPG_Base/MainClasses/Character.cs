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
        public string name { protected set;  get; }
        public int HP { protected set; get; }
        public int maxHP { protected set; get; }
        public int baseDMG { protected set; get; }
        public Weapon[] weapons { protected set; get; }
        public bool isAlive { protected set; get; }
        public int level { protected set; get; }


        public Character(string name, int maxHP, int baseDMG, Weapon[] weapons, int level) // for Boss class.
        {
            this.name = name;
            this.maxHP = maxHP;
            this.HP = maxHP;
            this.isAlive = true;
            this.level = level;

            this.baseDMG = baseDMG;
            this.weapons = weapons;
            // item
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
            // item
        }



        // ------------------------------------ Methods: ------------------------------------ //
        public void AddHP(int amount)
        {
            this.HP += amount;
            if (this.HP > this.maxHP)
                this.HP = this.maxHP;
            Functions.PrintAndColor($"\n{name} has gained {amount} HP. ", null, ConsoleColor.Green);
        }

        public virtual void RemoveHP(int amount)
        {
            this.HP -= amount;
            
            if (this.HP <= 0)
            {
                Functions.PrintAndColor($"\n{name} has died", null, ConsoleColor.DarkRed);
                this.isAlive = false;
            }
        }



        // ------------------------------------- TEMP: ------------------------------------- //
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

            // items
            Console.WriteLine("-----------------------------\n");
        }
    }
}
