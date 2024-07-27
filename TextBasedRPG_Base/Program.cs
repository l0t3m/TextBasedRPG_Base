using System.Xml.Linq;
using TextBasedRPG_Base.MainClasses;



bool isPlaying = true;
Navigation.SetupRooms();

while (isPlaying)
{
    Navigation.Explore();
}
