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

        public int itemInventorySlots { get; private set; } = 3;
        public Item[] itemInventory { get; private set; }

        public Player(string name) 
            : base(name: name, maxHP: 10, baseDMG: 4, weaponSlots: 3)
        {
            this.xp = 0; // is this line rly needed here or replace to line 14?
            itemInventory = new Item[itemInventorySlots]; // amount of slots
        }



        // ---------------------------------------  Methods: --------------------------------------- //

        public override void RemoveHP(int amount)
        {
            this.HP -= amount;

            if (this.HP <= 0)
            {
                Functions.PrintAndColor($"{name} has died", null, ConsoleColor.Magenta);
                this.isAlive = false;
            }
        }

        public void DoRest()
        {
            this.HP = this.maxHP;
            daysCounter++;
        }



        // ------------------------------------- Item Methods: ------------------------------------- //
        
        public bool AddItemPlayer(Item item) // true if successfully added, false if failed.
        {
            SortItemInventory();
            if (this.itemInventory.Count(n => n == null) != 0) // checks if there are any empty slots
            {
                for (int i = 0; i < this.itemInventorySlots; i++)
                {
                    if (this.itemInventory[i] == null) // checks if index is null (slot is empty)
                    {
                        this.itemInventory[i] = item;
                        Functions.PrintAndColor($"\nAdded {item.name} to your item inventory", null, ConsoleColor.Green);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RemoveItemPlayer(Item item) // true if removed successfully, false if failed.
        {
            SortItemInventory();
            if (this.itemInventory.Contains(item))
            {
                for (int i = 0;i < this.itemInventorySlots;i++)
                {
                    if (this.itemInventory[i] == item)
                    {
                        this.itemInventory[i] = null;
                        Functions.PrintAndColor($"\nRemoved {item.name} from your item inventory", null, ConsoleColor.DarkRed);
                        return true;
                    }
                }
            }
            
            return false;
        }

        public bool SwitchItemPlayer(Item oldItem, Item newItem) // true if switched successfully, false if failed.
        {
            SortItemInventory();
            for (int i = 0; i < this.itemInventorySlots; i++)
            {
                if (this.itemInventory[i] == oldItem)
                {
                    this.itemInventory[i] = newItem;
                    Functions.PrintAndColor($"Switched {oldItem.name} with {newItem.name}", null, ConsoleColor.Green);
                    return true;
                }
            }
            return false;
        }

        public void SortItemInventory() // sorts the items, using every empty slot
        {
            Item[] itemsArr = new Item[this.itemInventorySlots];

            foreach (Item item in this.itemInventory)
            {
                if (item != null)
                {
                    for (int i = 0; i <= this.itemInventorySlots; i++)
                    {
                        if (itemsArr[i] == null)
                        {
                            itemsArr[i] = item;
                            break;
                        }
                    }
                }
            }
            this.itemInventory = itemsArr;
        }

        public bool IsItemInventoryFull() // false if there's any empty space, true if full
        {
            SortItemInventory();
            bool isFull = true;
            foreach (Item item in this.itemInventory)
            {
                if (item == null)
                {
                    return false;
                }
            }
            return true;
        }



        // ------------------------------------ Combat Methods: ------------------------------------ //
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
            SortWeapons();

            Console.WriteLine($"\n--> WEAPONS: [{weapons.Count(n => n != null)} / {weapons.Length}]");
            
            for ( int i = 0; i < weapons.Length; i++ )
            {
                if (weapons[i] != null)
                {
                    Console.WriteLine($"| {i + 1}. {weapons[i].name}: {weapons[i].damage} DMG | {weapons[i].durability} uses left");
                }
            }
        }

        public void PrintItems()
        {
            SortItemInventory();

            Console.WriteLine($"\n--> ITEMS: [{itemInventory.Count(n => n != null)} / {itemInventorySlots}]");

            for ( int i = 0; i < itemInventorySlots; i++ )
            {
                if (itemInventory[i] != null)
                {
                    Console.WriteLine($"| {i + 1}. {itemInventory[i].name}: {itemInventory[i].effect}");
                }
            }
            // items
        }
    }
}
