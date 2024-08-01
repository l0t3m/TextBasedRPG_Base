using System.Diagnostics.Metrics;
using System.Xml.Linq;
using TextBasedRPG_Base.MainClasses;
using TextBasedRPG_Base.SubClasses;


Navigation.SetupRooms();

SceneManager.player.AddWeapon(new Weapon("test weapon 1", 10, 10));





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

bool isPlaying = true;



while (isPlaying)
{
    Navigation.Explore();
}

//
//player.PrintStats();

//player.GainXP(4000);
//player.PrintStats();

//Boss boss = new Boss("koda", 20, 10, new Weapon("Tail", 20, 5), 20);
//boss.PrintStats();

//boss.weapons[0].RemoveDurability(1);
//boss.weapons[0].PrintWeapon();