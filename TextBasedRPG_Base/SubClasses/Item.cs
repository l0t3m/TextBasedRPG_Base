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

        public string GetEffect(ItemEffect effect)
        {
            return "";
        }
    }
}
