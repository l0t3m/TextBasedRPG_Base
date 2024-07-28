using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.MainClasses;

namespace TextBasedRPG_Base.SubClasses
{
    public class Player : Character
    {
        // -------------------------- Attributes and Constructors: -------------------------- //
        public int xp {  get; private set; }

        public Player(string name) 
            : base(name: name, maxHP: 25, baseDMG: 10, weaponSlots: 3)
        {
            // needs to have an empty list of 5 props.
            this.xp = 0;
        }



        // ------------------------------------ Methods: ------------------------------------ //
        public void AddMaxHP(int amount)
        {
            this.maxHP += amount;
        }

        //public void addProp(PROP) { }
        //public void removeProp(PROP) { }

        /// <summary> Adds a weapon to the first empty slot. </summary>
        /// <returns> True if added successfully, False if there is no empty slots. </returns>
        public bool AddWeapon(Weapon weapon)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (this.weapons[i] == null)
                {
                    this.weapons[i] = weapon;
                    return true;
                }
            }
            return false;
        }

        /// <summary> Puts the given weapon in the desired index. </summary>
        /// <returns> The replaced weapon. </returns>
        public Weapon SwitchWeapon(Weapon newWeapon, int index)
        {
            Weapon oldWeapon = this.weapons[index];
            this.weapons[index] = newWeapon;
            return oldWeapon;
        }



        // ----------------------------------- XP Methods: ----------------------------------- //
        /// <returns>How much XP is needed until the next level.</returns>
        private int CalculateNextLevelXP()
        {
            return (int)(this.level * 9 + (Math.Pow(this.level, 2)));
        }

        /// <returns> How much XP is needed to level up, using the current xp of the object.</returns>
        private int CalculateUntilNextLevelXP()
        {
            return CalculateNextLevelXP() - this.xp;
        }

        /// <summary>
        /// Adds XP to the object and levels up accordingly.
        /// </summary>
        public void GainXP(int xpAmount)
        {
            this.xp += xpAmount;
            int xpDif = CalculateUntilNextLevelXP();

            while (xpDif <= 0)
            {
                this.xp = Math.Abs(xpDif);
                LevelUp();
                xpDif = CalculateUntilNextLevelXP();        
            }
        }

        /// <summary>
        /// Levels up the player and changes its stats accordingly.
        /// </summary>
        private void LevelUp()
        {
            this.level++;
            // change the player's stats according to the level.
        }



        // ------------------------------------- TEMP: ------------------------------------- //

        /// <summary>
        /// Prints the current object's stats using its attributes.
        /// </summary>
        public override void PrintStats()
        {
            Console.WriteLine("\n-----------------------------");
            Console.WriteLine($"{this.name} ({(this.isAlive == true ? "Alive" : "Dead")})");
            Console.WriteLine($"{this.HP} / {this.maxHP} HP");
            Console.WriteLine($"level {this.level} ({this.xp} / {CalculateNextLevelXP()})");
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
