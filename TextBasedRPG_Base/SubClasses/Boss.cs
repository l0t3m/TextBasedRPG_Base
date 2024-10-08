﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.MainClasses;

namespace TextBasedRPG_Base.SubClasses
{
    public class Boss : Enemy
    {
        // -------------------------- Attributes and Constructors: -------------------------- //
        public Boss(string name, int maxHP, int baseDMG, int level)
            : base(name, maxHP, baseDMG, level) { }



        // ------------------------------------ Methods: ------------------------------------ //
        public new void RemoveHP(int amount)
        {
            this.HP -= amount;

            if (this.HP <= 0)
            {
                Functions.PrintAndColor($"\nThe boss {name} was defeated.", null, ConsoleColor.DarkRed);
                this.isAlive = false;
                SceneManager.currentEnemy = null;
            }
        }

        public void AttackPlayer()
        {
            Boss boss = (Boss)SceneManager.currentEnemy;
            Player player = SceneManager.player;

            if (boss != null)
            {
                Functions.PrintAndColor($"{boss.name} has dealt {boss.baseDMG} DMG to you.",
                    $"{boss.baseDMG}", ConsoleColor.DarkRed);
                player.RemoveHP(boss.HP);
            }
        }

        public int CalculateXPWorth()
        {
            return this.level * 6 * Random.Shared.Next(1, 3);
        }
    }
}
