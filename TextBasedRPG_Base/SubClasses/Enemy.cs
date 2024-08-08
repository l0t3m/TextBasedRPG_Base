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
        private Enemy(string name, int maxHP, int baseDMG, int level) 
            : base(name, maxHP, baseDMG, null, level) 
        {
            this.name = name;
        }


        public static Enemy GenerateNewEnemy()
        {
            int level = Random.Shared.Next(SceneManager.currentRoom.minLevel, SceneManager.currentRoom.maxLevel + 1);

            return new Enemy(
                name: GenerateName(),
                maxHP: GenerateMaxHP(level),
                baseDMG: GenerateBaseDMG(level),
                level: level
                );
        }



        // ------------------------------------ Methods: ------------------------------------ //

        public void AttackPlayer()
        {
            Enemy enemy = SceneManager.currentEnemy;
            Player player = SceneManager.player;

            if (enemy != null)
            {
                Prints.PrintAndColor($"{enemy.name} has dealt {enemy.baseDMG} DMG to you.", $"{enemy.baseDMG} DMG", ConsoleColor.Red);
                player.RemoveHP(player.baseDMG);
            }
        }

        private static string GenerateName()
        {
            var values = Enum.GetValues<EnemyType>();
            var colors = new string[] { "Blue", "Red", "Brown", "Black" };

            return $"{colors[Random.Shared.Next(colors.Length)]} {values.GetValue(Random.Shared.Next(values.Length))}";
        }

        private static int GenerateMaxHP(int level)
        {
            return 5+3*(level-1);
        }

        private static int GenerateBaseDMG(int level)
        {
            return (int)Math.Pow(2, (level-1)/1.5);
        }

        public int CalculateXPWorth()
        {
            return this.level * 5 * Random.Shared.Next(1, 3);
        }


        // ------------------------------------- TEMP: ------------------------------------- //
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
