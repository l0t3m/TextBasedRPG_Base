using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_Base.SubClasses
{
    public enum WeaponType
    {
        Sword,
        Knife,
        Katana,
        Machete,
        Axe,
        Spear
    }

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



        // ----------------------------------- Generators: ----------------------------------- //
        public static Weapon GenerateNewWeapon(int level)
        {
            // Base Stats of each sword.
            int baseDur = 4;
            int baseDMG = 2;

            int randomStat = Random.Shared.Next(0, 100); // Random chance to increase baseDMG
            if (randomStat < 25)
            {
                baseDMG++;

                if (Random.Shared.Next(0, 100) < 50) // Random chance to increase / decrease baseDur
                    baseDur++;
                else
                    baseDur--;
            }
            else if (randomStat < 50)  // Random chance to increase baseDMG
            {
                baseDMG += 2;

                if (Random.Shared.Next(0, 100) < 25) // Random chance to increase / decrease baseDur
                    baseDur += 2;
                else
                    baseDur++;
            }
            else
            {
                if (Random.Shared.Next(0, 100) < 60) // Random chance to increase / decrease baseDur
                    baseDur += 2;
                else
                    baseDur++;
            }

            return new Weapon(
                name: GenerateName(),
                durability: baseDur,
                damage: (int)(baseDMG + level * 0.5)
                );
        }

        private static string GenerateName()
        {
            var values = Enum.GetValues<WeaponType>();
            var bladeTypes = new string[] { "Sharp", "Keen", "Glossy", "Sparkling", "Polished", "Shiny", "Rusty", "Shattered", "Elegant", "Lightweight", "Mythical" };

            return $"{bladeTypes[Random.Shared.Next(bladeTypes.Length)]} {values.GetValue(Random.Shared.Next(values.Length))}";
        }


        // ------------------------------------ Methods: ------------------------------------ //

        /// <returns> True = the weapon should be destroyed, otherwise False </returns>
        public bool RemoveDurability() 
        {
            this.durability--;
            return this.durability <= 0;
        }



        // ------------------------------------- TEMP: ------------------------------------- //
        public void PrintWeapon()
        {
            Console.WriteLine($"| {this.name}:");
            Console.WriteLine($"|      {this.damage} DMG | {this.durability} uses left");
        }
    }
}
