using System.Xml.Linq;
using TextBasedRPG_Base;



bool isPlaying = true;

Navigation.SetupRooms();

//Console.BackgroundColor = ConsoleColor.DarkGray;
//Console.ForegroundColor = ConsoleColor.White;

while (isPlaying)
{
    Navigation.Explore();
}








