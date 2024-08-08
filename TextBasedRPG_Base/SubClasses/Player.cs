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
            : base(name: name, maxHP: 10, baseDMG: 2, weaponSlots: 3)
        {
            // needs to have an empty list of 5 items.
            this.xp = 0;
        }



        // ------------------------------------ Methods: ------------------------------------ //
        public void AttackEnemy()
        {
            Enemy enemy = SceneManager.currentEnemy;
            Player player = SceneManager.player;

            if (enemy != null) 
            {
                Prints.PrintAndColor($"You've dealt {player.baseDMG} DMG to the {enemy.name}.", $"{player.baseDMG} DMG", ConsoleColor.Red);
                enemy.RemoveHP(player.baseDMG);

                if (enemy.isAlive == false)
                {
                    player.GainXP(enemy.CalculateXPWorth());
                }
            }
        }
        


        public void AddMaxHP(int amount)
        {
            this.maxHP += amount;
        }

        public void AddBaseDMG(int amount)
        {
            this.baseDMG += amount;
        }

        //public void addItem(ITEM) { }
        //public void removeItem(ITEM) { }


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
        private int CalculateNextLevelXP() // Returns how much xp is needed until the next level.
        {
            return (int)(this.level * 9 + (Math.Pow(this.level, 2)));
        }

        private int CalculateUntilNextLevelXP() // Uses the current xp to calculate how much xp is needed to level up.
        {
            return CalculateNextLevelXP() - this.xp;
        }

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

        private void LevelUp()
        {
            this.level++;
            this.AddMaxHP((int)(this.maxHP * 0.5));
            this.AddBaseDMG((int)(this.baseDMG * 0.75));
        }



        // ------------------------------------- TEMP: ------------------------------------- //

        public override void PrintStats()
        {
            Console.WriteLine("\n-----------------------------");
            Console.WriteLine($"{this.name} ({(this.isAlive == true ? "Alive" : "Dead")})");
            Console.WriteLine($"{this.HP} / {this.maxHP} HP");
            Console.WriteLine($"level {this.level} ({this.xp} / {CalculateNextLevelXP()})");
            Console.WriteLine($"{this.weapons.Length} weapon slots");
            Console.WriteLine($"{this.baseDMG} baseDMG");

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
