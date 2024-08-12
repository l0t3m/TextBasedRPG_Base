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

        public Character(string name, int maxHP, int baseDMG, int level, bool isNPC = true) // for Boss and Enemy classes
        {
            this.name = name;
            this.maxHP = maxHP;
            this.HP = maxHP;
            this.isAlive = true;
            this.level = level;

            this.baseDMG = baseDMG;
            this.weapons = weapons;
        }
        public Character(string name, int maxHP, int baseDMG, int weaponSlots) // for Player class
        {
            this.name = name;
            this.maxHP = maxHP;
            this.HP = maxHP;
            this.isAlive = true;
            this.level = 1;

            this.baseDMG = baseDMG;
            this.weapons = new Weapon[weaponSlots];
        }



        // ------------------------------------ Methods: ------------------------------------ //
        public void AddHP(int amount, bool isFromItem = false)
        {
            this.HP += amount;
            if (this.HP > this.maxHP)
                this.HP = this.maxHP;
            if (!isFromItem)
                Functions.PrintAndColor($"\n{name} has healed for {amount} HP by defeating an enemy.", null, ConsoleColor.Green);
        }



        // -------------------------------- Personal Prints: -------------------------------- //
        public virtual void PrintStats()
        {
            Console.WriteLine("\n-----------------------------");
            Console.WriteLine($"{this.name} ({(this.isAlive == true ? "Alive" : "Dead")})");
            Console.WriteLine($"{this.HP} / {this.maxHP} HP");
            Console.WriteLine($"level {this.level}");
            Console.WriteLine("-----------------------------\n");
        }
    }
}
