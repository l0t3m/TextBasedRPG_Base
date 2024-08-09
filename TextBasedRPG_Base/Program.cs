using System.Diagnostics.Metrics;
using System.Xml.Linq;
using TextBasedRPG_Base.MainClasses;
using TextBasedRPG_Base.SubClasses;


// Setups: //
Console.ForegroundColor = ConsoleColor.White;
Navigation.SetupRooms();

// temp: //
SceneManager.player.AddWeapon(new Weapon("test weapon 1", 10, 10));



bool isPlaying = true;

while (isPlaying)
{
    //SceneManager.MainMenu();

    if (SceneManager.player.isAlive == true)
        Navigation.Explore();
    else
        SceneManager.GameOver();
}


//Player player = SceneManager.player;

//for (int i = 0; i <= 101; i++)
//{
//    Enemy enemy = Enemy.GenerateNewEnemy(player.level);

//    if (i % 5 == 0)
//    {
//        player.PrintStatsSimple();
//        enemy.PrintStatsSimple();
//    }

//    player.GainXP(player.CalculateUntilNextLevelXP());
//}

//SceneManager.player.AddMaxHP(2000);
//SceneManager.player.AddHP(2000);



//Boss boss = new Boss("koda", 20, 10, new Weapon("Tail", 20, 5), 20);
//boss.PrintStats();

//boss.weapons[0].RemoveDurability(1);
//boss.weapons[0].PrintWeapon();