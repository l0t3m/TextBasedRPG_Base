using System.Diagnostics.Metrics;
using System.Xml.Linq;
using TextBasedRPG_Base.MainClasses;
using TextBasedRPG_Base.SubClasses;


// Setups: //
Console.ForegroundColor = ConsoleColor.White;
Navigation.SetupRooms();
Navigation.SetupItems();


Player player = SceneManager.player;
player.AddWeapon(Weapon.GenerateNewWeapon(15));
player.AddWeapon(Weapon.GenerateNewWeapon(15));
player.AddWeapon(Weapon.GenerateNewWeapon(15));
player.GainXP(1960);
Console.Clear();

while (true)
{
    if (SceneManager.player.isAlive == true)
        Navigation.Explore();
    else
        SceneManager.GameOver();
}