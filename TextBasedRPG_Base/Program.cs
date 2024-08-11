using System.Diagnostics.Metrics;
using System.Xml.Linq;
using TextBasedRPG_Base.MainClasses;
using TextBasedRPG_Base.SubClasses;


// Setups: //
Console.ForegroundColor = ConsoleColor.White;
Navigation.SetupRooms();
Navigation.SetupItems();

// temp: //

SceneManager.player.AddWeapon(Weapon.GenerateNewWeapon(1));
SceneManager.player.AddWeapon(Weapon.GenerateNewWeapon(1));
SceneManager.player.AddItemPlayer(new Item("testItem", ItemEffect.InstantHeal));
SceneManager.player.AddItemPlayer(new Item("testItem", ItemEffect.InstantHeal));

SceneManager.player.GainXP(10000);

Console.Clear();

while (true)
{
    if (SceneManager.player.isAlive == true)
        Navigation.Explore();
    else
        SceneManager.GameOver();
}



// Proper code:

//bool isPlaying = true;
//SceneManager.MainMenu();

//while (isPlaying)
//{
//    if (SceneManager.player.isAlive == true)
//        Navigation.Explore();
//    else
//        SceneManager.GameOver();
//}



//Boss boss = new Boss("koda", 20, 10, new Weapon("Tail", 20, 5), 20);
//boss.PrintStats();

//boss.weapons[0].RemoveDurability(1);
//boss.weapons[0].PrintWeapon();