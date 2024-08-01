using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.SubClasses;

namespace TextBasedRPG_Base.MainClasses
{
    public static class Combat
    {
        public static bool StartFight(Enemy enemy)
        {
            Console.WriteLine("A fight has started!");
            Console.WriteLine($"{SceneManager.player.name} VS {enemy.name}");

            while (SceneManager.player.isAlive && enemy.isAlive)
            {
                SceneManager.player.RemoveHP(5);
            }
            
            return true;
        } 
    }
}
