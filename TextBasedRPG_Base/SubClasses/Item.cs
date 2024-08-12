using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_Base.SubClasses
{
    public enum ItemEffect
    {
        InstantHeal,
        InstantDamage
        //Distract
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



        // -------------------------------- Personal Prints: -------------------------------- //
        public void PrintItem()
        {
            Console.WriteLine($"|  {name} - {this.GetEffect()} effect");
        }
    }
}
