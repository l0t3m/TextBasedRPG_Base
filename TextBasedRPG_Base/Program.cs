using System.Diagnostics.Metrics;
using System.Xml.Linq;
using TextBasedRPG_Base.MainClasses;
using TextBasedRPG_Base.SubClasses;


// Setups: //
Console.ForegroundColor = ConsoleColor.White;
Navigation.SetupRooms();

// temp: //
SceneManager.player.AddWeapon(new Weapon("test weapon 1", 10, 10));
//SceneManager.player.GainXP(60);

//SceneManager.player.AddMaxHP(2000);
//SceneManager.player.AddHP(2000);




bool isPlaying = true;

while (isPlaying)
{
    //SceneManager.MainMenu();

    if (SceneManager.player.isAlive == true)
        Navigation.Explore();
    else
        SceneManager.GameOver();
}








//Dictionary<int, Room> roomDict = new Dictionary<int, Room>();
//int counter = 0;

//foreach (Room room in SceneManager.currentRoom.ConnectedRooms)
//{
//    roomDict.Add(counter, room);
//    counter++;
//}

//SceneManager.currentRoom = roomDict[2];
//Console.WriteLine(SceneManager.currentRoom.Name);

//Enemy enemy = Enemy.GenerateNewEnemy();
//enemy.PrintStats();


//
//player.PrintStats();

//player.GainXP(4000);
//player.PrintStats();

//Boss boss = new Boss("koda", 20, 10, new Weapon("Tail", 20, 5), 20);
//boss.PrintStats();

//boss.weapons[0].RemoveDurability(1);
//boss.weapons[0].PrintWeapon();