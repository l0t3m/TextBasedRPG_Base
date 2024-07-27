using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRPG_Base.MainClasses;

namespace TextBasedRPG_Base.SubClasses
{
    public class Player : Character
    {
        // -------------------------- Attributes and Constructors: -------------------------- //
        public Player(string name) : base(name, 25) 
        {
            // needs to start with weapons: Scratch, hiss.
            // needs to have an empty list of 5 props.
        }



        // -------------------------- Methods: -------------------------- //
        public void AddMaxHP(int amount)
        {
            this.maxHP += amount;
        }

        //public void addProp(PROP) { }
        //public void removeProp(PROP) { }



        // -------------------------- TEMP: -------------------------- //



    }
}
