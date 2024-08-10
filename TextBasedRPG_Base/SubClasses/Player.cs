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
        public void AttackEnemy(int damage)
        {
            Enemy enemy = SceneManager.currentEnemy;

            if (enemy != null) 
            {
                Functions.PrintAndColor($"You've dealt {damage} DMG to the {enemy.name}.", $"{damage} DMG", ConsoleColor.Red);
                enemy.RemoveHP(damage);

                if (enemy.isAlive == false)
                {
                    this.AddHP((int)(enemy.maxHP / 4));
                    this.GainXP(enemy.CalculateXPWorth());
                }
            }
        }

        public void AttackEnemy(Weapon weapon)
        {
            AttackEnemy(weapon.damage + this.baseDMG);

            if (weapon.RemoveDurability())
                this.DestroyWeapon(weapon);
        }

        public override void RemoveHP(int amount)
        {
            this.HP -= amount;

            if (this.HP <= 0)
            {
                Functions.PrintAndColor($"{name} has died", null, ConsoleColor.Magenta);
                this.isAlive = false;
            }
        }

        //public void addItem(ITEM) { }
        //public void removeItem(ITEM) { }

        public void DoRest()
        {
            this.HP = this.maxHP;
            daysCounter++;
        }

        // ------------------------------------ Weapon Methods: ------------------------------------ //
        public void SortWeapons() // returns an array with weapons sorted
        {
            Weapon[] weaponsArr = new Weapon[this.weapons.Length];

            foreach (Weapon weapon in this.weapons)
            {
                if (weapon != null)
                {
                    for (int i = 0; i < weaponsArr.Length; i++)
                    {
                        if (weaponsArr[i] == null)
                        {
                            weaponsArr[i] = weapon;
                            break;
                        }
                    }
                }
            }

            this.weapons = weaponsArr;
        }

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

        public void DestroyWeapon(Weapon targetWeapon)
        {
            for (int i = 0; i < this.weapons.Length; i++)
            {
                if (targetWeapon == this.weapons[i])
                {
                    this.weapons[i] = null;
                    break;
                }
            }
            this.SortWeapons();
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

        private void GainXP(int xpAmount)
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



        // ------------------------------------- TEMP: ------------------------------------- //
        public override void PrintStats()
        {
            PrintInfo();

            Console.WriteLine("\n--> STATS:");
            Console.WriteLine($"| [lvl.{this.level}] {this.name}");
            Console.WriteLine($"| {this.HP}/{this.maxHP} HP, {this.xp}/{CalculateNextLevelXP()} XP");
            Console.WriteLine($"| {this.baseDMG} base DMG");

            PrintWeapons();

            PrintItems();
        }

        private void PrintInfo()
        {
            Console.WriteLine("--> Info:");
            Console.WriteLine($"| Current in {SceneManager.currentRoom.Name}");
            Console.WriteLine($"| {this.daysCounter} days has passed");
        }

        public void PrintWeapons()
        {
            this.SortWeapons();

            Console.WriteLine($"\n--> WEAPONS: [{this.weapons.Count(n => n != null)} / {this.weapons.Length}]");
            
            for ( int i = 0; i < weapons.Length; i++ )
            {
                if (weapons[i] != null)
                {
                    Console.WriteLine($"| {i + 1}. {weapons[i].name}: {weapons[i].damage} DMG | {weapons[i].durability} uses left");
                }
            }
        }

        private void PrintItems()
        {
            Console.WriteLine($"\nItems: [] WIP");
            // items
        }
    }
}
