namespace Roguelike;

public static class LevelGenerator
{
    static Random rnd = new Random();

    private static Map.Grid grid = Map.GetGrid();
    private static Hero player = Program.player;

    private static int enemyCount = 0;
    private static int enemyExist = 0;

    private static int currentLevel = 1;
    public static void GenerateNewLevel(int enemCnt)
    {
        enemyCount += enemCnt;
        grid = new Map.Grid();
        player.SetCell(Map.GetCell(new vec2(3,4)));
        for (int i = 0; i < enemyCount; i++)
        {
            int rndNum = rnd.Next(3);
            switch (rndNum)
            {
                case 0:
                    GenerateNewEnemy(new Zombie());
                    break;
                case 1:
                    GenerateNewEnemy(new Skeleton());
                    break;
                case 2:
                    GenerateNewEnemy(new Necromant());
                    break;
            }
        }
    }

    private static void GenerateNewEnemy(Person enemy)
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

    public static void ReduceEnemy()
    {
        enemyExist--;
        if (enemyExist == 0)
        {
            GenerateNewLevel(rnd.Next(3));
            currentLevel++;
        }
    }
}