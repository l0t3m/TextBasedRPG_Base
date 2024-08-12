using System.Diagnostics.Metrics;
using System.Xml.Linq;
using TextBasedRPG_Base.MainClasses;
using TextBasedRPG_Base.SubClasses;


// Setups: //
Console.ForegroundColor = ConsoleColor.White;
Navigation.SetupRooms();
Navigation.SetupItems();

SceneManager.MainMenu();
while (true)
{
    if (SceneManager.player.isAlive == true)
        Navigation.Explore();
    else
        SceneManager.GameOver();
}