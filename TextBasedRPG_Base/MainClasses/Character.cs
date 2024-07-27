using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_Base.MainClasses
{
    public abstract class Character
    {
        // -------------------------- Attributes and Constructors: -------------------------- //
        protected string name;
        public int HP { protected set; get; }
        protected int maxHP;
        // weapons
        // props
        public bool isAlive { protected set; get; }

        public Character(string name, int maxHP) // add weapon[] + prop[]
        {
            this.name = name;
            this.maxHP = maxHP;
            this.HP = maxHP;
            // weapons
            // props
            isAlive = true;
        }



        // ------------------------------------ Methods: ------------------------------------ //
        public void AddHP(int amount)
        {
            this.HP += amount;
            if (this.HP < this.maxHP)
                this.HP = this.maxHP;
        }

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
        public void PrintStats()
        {
            Console.WriteLine("\n-----------------------------");
            Console.WriteLine($"name = {this.name}");
            Console.WriteLine($"maxHP = {this.maxHP}");
            Console.WriteLine($"HP = {this.HP}");
            Console.WriteLine($"isAlive = {this.isAlive}");
            // weapons
            // props
            Console.WriteLine("-----------------------------\n");
        }



    }
}
