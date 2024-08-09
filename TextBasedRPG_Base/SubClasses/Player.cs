using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public int daysCounter { get; private set; } = 0;

        public Player(string name) 
            : base(name: name, maxHP: 10, baseDMG: 4, weaponSlots: 3)
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
                    player.AddHP((int)(enemy.maxHP / 4));
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

        public override void RemoveHP(int amount)
        {
            this.HP -= amount;

            if (this.HP <= 0)
            {
                Prints.PrintAndColor($"{name} has died", null, ConsoleColor.Magenta);
                this.isAlive = false;
            }
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

        // debug purpose - chance to private
        public int CalculateUntilNextLevelXP() // Uses the current xp to calculate how much xp is needed to level up.
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
            this.maxHP = (int)(this.level * 3.75);
            this.baseDMG = (int)(4 + this.level * 0.5);
            this.HP = maxHP;

            //Console.WriteLine($"\nLevel up, You're now level {this.level}!");
            //Console.WriteLine($"+{(int)(this.maxHP * 0.5)} maxHP");
            //Console.WriteLine($"+{(int)(this.baseDMG * 0.75)} baseDMG");
            //Console.WriteLine($"HP has restored to max");
            //Console.WriteLine("\nPress enter to continue.");
            // debug purpose - uncomment above
        }

        public void DoRest()
        {
            this.HP = this.maxHP;
            daysCounter++;
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
            Console.WriteLine($"{this.daysCounter} Days passed");

            foreach (Weapon weapon in this.weapons)
            {
                if (weapon != null)
                    weapon.PrintWeapon();
            }

            // items
            Console.WriteLine("-----------------------------\n");
        }

        public static int DebugMaxHP(int level) // debug purpose - remove function
        {
            int tempMaxHP = 10;
            for (int i = 0; i < level - 1; i++) tempMaxHP = (int)(tempMaxHP * 1.5);
            return tempMaxHP;
        }

        public static int DebugBaseDMG(int level) // debug purpose - remove function
        {
            int tempBaseDMG = 2;
            for (int i = 0; i < level - 1; i++) tempBaseDMG = (int)(tempBaseDMG * 1.75);
            return tempBaseDMG;
        }


        public void PrintStatsSimple() // debug purpose - remove function
        {
            Prints.PrintAndColor($"level {this.level} \tMax HP: {this.maxHP} \tBase DMG: {this.baseDMG}", null, ConsoleColor.White);
        }
    }
}
