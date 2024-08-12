using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextBasedRPG_Base.MainClasses;

namespace TextBasedRPG_Base.SubClasses
{
    public enum EnemyType
    {
        Cockroach,
        Cricket,
        Snail
    }

    public class Enemy : Character
    {
        // -------------------------- Attributes and Constructors: -------------------------- //
        protected Enemy(string name, int maxHP, int baseDMG, int level) 
            : base(name, maxHP, baseDMG, level, true) 
        {
            this.name = name;
        }



        // ----------------------------------- Generators: ----------------------------------- //

        public static Enemy GenerateNewEnemy()
        {
            int level = Random.Shared.Next(SceneManager.currentRoom.minLevel , SceneManager.currentRoom.maxLevel + 1);

            return new Enemy(
                name: GenerateName(),
                maxHP: GenerateMaxHP(level),
                baseDMG: GenerateBaseDMG(level),
                level: level
                );
        }

        private static string GenerateName()
        {
            var values = Enum.GetValues<EnemyType>();
            var colors = new string[] { "Blue", "Red", "Brown", "Black" };

            return $"{colors[Random.Shared.Next(colors.Length)]} {values.GetValue(Random.Shared.Next(values.Length))}";
        }

        /// <summary>
        /// Gets the desired enemy's level and generates maxHP using a formula, randomly changing the value for diversity.
        /// </summary>
        private static int GenerateMaxHP(int level)
        {
            int randomStat = Random.Shared.Next(0, 100);
            int baseHP = 6;

            if (randomStat < 25)
                baseHP++;
            else if (randomStat < 50)
                baseHP--;
            return (int)(baseHP + (level * 0.125) + 4 * (level - 1));
        }

        /// <summary>
        /// Gets the desired enemy's level and generates baseDMG using a formula, randomly changing the value for diversity.
        /// </summary>
        private static int GenerateBaseDMG(int level)
        {
            int randomStat = Random.Shared.Next(0, 100);
            int baseDMG = 2;

            if (randomStat < 25)
                baseDMG++;
            else if (randomStat < 50)
                baseDMG--;

            return (int)(baseDMG + (level * 0.1) + 0.85 * (level - 1));
        }


        // ------------------------------------ Methods: ------------------------------------ //

        public void RemoveHP(int amount)
        {
            this.HP -= amount;

            if (this.HP <= 0)
            {
                Functions.PrintAndColor($"\n{name} has died.", null, ConsoleColor.DarkRed);
                this.isAlive = false;
                SceneManager.currentEnemy = null;
            }
        }

        public void AttackPlayer()
        {
            Enemy enemy = SceneManager.currentEnemy;
            Player player = SceneManager.player;

            if (enemy != null)
            {
                Functions.PrintAndColor($"{enemy.name} has dealt {enemy.baseDMG} DMG to you.", $"{enemy.baseDMG} DMG", ConsoleColor.Red);
                player.RemoveHP(enemy.baseDMG);
            }
        }

        public int CalculateXPWorth()
        {
            return this.level * 5 * Random.Shared.Next(1, 3);
        }


        // -------------------------------- Personal Prints: -------------------------------- //
        public virtual void PrintStats()
        {
            Console.WriteLine("\n-----------------------------");
            Console.WriteLine($"{this.name} ({(this.isAlive == true ? "Alive" : "Dead")})");
            Console.WriteLine($"{this.HP} / {this.maxHP} HP");
            Console.WriteLine($"level {this.level}");
            Console.WriteLine($"{this.baseDMG} baseDMG");
            Console.WriteLine("-----------------------------\n");
        }
    }
}
