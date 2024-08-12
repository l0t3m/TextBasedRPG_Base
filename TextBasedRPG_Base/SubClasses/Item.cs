using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.MainClasses;

namespace TextBasedRPG_Base.SubClasses
{
    public enum ItemEffect
    {
        InstantHeal,
        InstantDamage,
        Distract
    }

    public class Item
    {
        // -------------------------- Attributes and Constructors: -------------------------- //
        public string name { get; private set; }
        public ItemEffect effect { get; private set; }

        public Item(string name, ItemEffect effect)
        {
            this.name = name;
            this.effect = effect;
        }



        // ----------------------------------- Generators: ----------------------------------- //
        public string GetEffect()
        {
            int pos = 0;
            string effectString = this.effect.ToString();
            for (int i = 0; i < effectString.Length; i++)
            {
                if (i != 0 && char.IsUpper(effectString[i]))
                    pos = i;
            }
            if (pos == 0)
                return effectString;

            return effectString.Substring(0, pos) + ' ' + effectString.Substring(pos, effectString.Length - pos);
        }

        public bool UseItem()
        {
            switch (effect)
            {
                case ItemEffect.InstantHeal:
                    Player player = SceneManager.player;
                    bool worked = player.HP < player.maxHP;
                    int prevHP = player.HP;
                    if (worked)
                    {
                        player.AddHP(player.maxHP / 2,  true);
                        Functions.PrintAndColor($"You have healed for {player.HP-prevHP} HP", (player.HP-prevHP).ToString(), ConsoleColor.Green);
                    }
                    else
                        Functions.PrintAndColor("You are already at max HP!", null, ConsoleColor.Red);
                    return worked;
                case ItemEffect.InstantDamage:
                    int damageToDeal = (int)(SceneManager.player.baseDMG * 1.8);
                    Functions.PrintAndColor($"You have dealt {damageToDeal} damage to {SceneManager.currentEnemy.name}!", damageToDeal + " damage", ConsoleColor.Red);
                    ((Boss)SceneManager.currentEnemy).RemoveHP(damageToDeal);
                    return true;
                case ItemEffect.Distract:
                    worked = SceneManager.currentEnemy.SetDistracted(true);
                    if (worked)
                        Functions.PrintAndColor($"You have distracted {SceneManager.currentEnemy.name} for 1 turn!", "1 turn");
                    else
                        Functions.PrintAndColor("Enemy is already distracted!", null, ConsoleColor.Red);
                    return worked;
            }
            return false;
        }


        // -------------------------------- Personal Prints: -------------------------------- //
        public void PrintItem()
        {
            Console.WriteLine($"|  {name} - {this.GetEffect()} effect");
        }
    }
}
