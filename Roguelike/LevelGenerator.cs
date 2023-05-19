namespace Roguelike;

public static class LevelGenerator
{
    public static Random rnd = new Random();

    private static Map.Grid grid = Map.GetGrid();
    private static Hero player = Program.player;

    private static int enemyCount = 0;
    private static int enemyExist = 0;

    private static int currentLevel = 0;
    private static List<Zombie> zombies = new List<Zombie>();
    private static List<Skeleton> skeletons = new List<Skeleton>();
    private static List<Necromant> necromants = new List<Necromant>();
    public static void GenerateNewLevel(int enemCnt)
    {
        currentLevel++;
        enemyCount += enemCnt;
        zombies = new List<Zombie>();
        skeletons = new List<Skeleton>();
        necromants = new List<Necromant>();
        grid = Map.GenNewGrid();
        Render.SetRenderPack(grid);
        player.SetCell(Map.GetCell(new vec2(3,4)));
        for (int i = 0; i < enemyCount; i++)
        {
            int rndNum = rnd.Next(3);
            switch (rndNum)
            {
                case 0:
                    Zombie newZ = new Zombie();
                    zombies.Add(newZ);
                    GenerateNewEnemy(newZ);
                    break;
                case 1:
                    Skeleton newS = new Skeleton();
                    skeletons.Add(newS);
                    GenerateNewEnemy(newS);
                    break;
                case 2:
                    Necromant newN = new Necromant();
                    necromants.Add(newN);
                    GenerateNewEnemy(newN);
                    break;
            }
        }
        for (int i = 0; i < rnd.Next(3); i++)
        {
            int rndNum = rnd.Next(1);
            switch (rndNum)
            {
                case 0:
                    GenerateNewObject(new ModAround());
                    break;
            }
        }
    }

    public static void GenerateNewEnemy(Person enemy)
    {
        bool flag = true;
        while (flag)
        {
            vec2 pos = new vec2(rnd.Next(5, 17), rnd.Next(0, 8));
            Map.Cell cell = Map.GetCell(pos);
            if (cell.GetContainer().GetPerson() == null)
            {
                enemy.SetCell(cell);
                enemyExist++;
                flag = false;
            }    
        }
    }
    private static void GenerateNewObject(GameObject obj)
    {
        bool flag = true;
        while (flag)
        {
            vec2 pos = new vec2(rnd.Next(5, 17), rnd.Next(0, 8));
            Map.Cell cell = Map.GetCell(pos);
            if (cell.GetContainer().GetPerson() == null)
            {
                obj.SetCell(cell);
                flag = false;
            }    
        }
    }

    public static void ReduceEnemy()
    {
        enemyExist--;
    }

    public static (List<Zombie>, List<Skeleton>, List<Necromant>) GetEnemyLists()
    {
        return (zombies, skeletons, necromants);
    }
    public static List<Zombie> GetZombiesList()
    {
        return zombies;
    }
    public static List<Skeleton> GetSkeletonsList()
    {
        return skeletons;
    }
    public static List<Necromant> GetNecromantsList()
    {
        return necromants;
    }

    public static void RefreshEnemyLists()
    {
        zombies = new List<Zombie>();
        skeletons = new List<Skeleton>();
        necromants = new List<Necromant>();
        for (int i = 0; i < Map.GetGridSize().y; i++)
        {
            for (int j = 0; j < Map.GetGridSize().x; j++)
            {
                Person enemy = Map.GetCell(new vec2(j, i)).GetContainer().GetPerson();
                if (enemy == null) continue;
                switch (enemy.GetCharach().Item2)
                {
                    case "Zombie":
                        zombies.Add((Zombie)enemy);
                        break;
                    case "Skeleton":
                        skeletons.Add((Skeleton)enemy);
                        break;
                    case "Necromant":
                        necromants.Add((Necromant)enemy);
                        break;
                }
            }
        }
    }

    public static int GetCountExistEnemy()
    {
        return enemyExist;
    }
}