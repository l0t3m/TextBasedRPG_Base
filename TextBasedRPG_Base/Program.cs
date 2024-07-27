using System.Xml.Linq;
using TextBasedRPG_Base.MainClasses;
using TextBasedRPG_Base.SubClasses;



bool isPlaying = true;
Navigation.SetupRooms();

while (isPlaying)
{
    Navigation.Explore();
}

//Player player = new Player("lotem");
//player.AddWeapon(new Weapon("weapon1", 10, 10));
//player.PrintStats();

//player.GainXP(4000);
//player.PrintStats();

//Boss boss = new Boss("koda", 20, 10, new Weapon("Tail", 20, 5), 20);
//boss.PrintStats();

//boss.weapons[0].RemoveDurability(1);
//boss.weapons[0].PrintWeapon();