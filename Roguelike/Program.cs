// w:h 148:35
using Roguelike;

static class Program
{
    private static int windowWidth = Console.WindowWidth;
    private static int windowHeight = Console.WindowHeight;

    public static Hero player = new Hero();

    static void Main(string[] args)
    {
        // Menu();
        Game();
        GameOver();
    }

    private static void Menu()
    {
        List<string> text = new List<string>();
        text.Add("Play Game");
        text.Add("");
        text.Add("Press Enter to Start");
        Render.RendMainMenu(text);
        
        Boolean flag = true;
        while (flag)
        {
            if (CheckSize())
            {
                Render.RendMainMenu(text);
            }
            else if (Console.KeyAvailable)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    flag = false;
                }
            }
        }
    }
    private static void Game()
    {
        // Render.Start();
        LevelGenerator.GenerateNewLevel(1);
        
        Render.RendGame();

        while (true)
        {
            if (CheckSize())
            {
                Render.RendGame();
            }
            else if (Console.KeyAvailable)
            {
                ConsoleKeyInfo inf = Console.ReadKey();
                switch (inf.Key)
                {
                    case ConsoleKey.RightArrow:
                        Cursor.Move(CursorMoveDirection.right);
                        uncheckAllSelected();
                        Render.RendGame();
                        break;
                    case ConsoleKey.LeftArrow:
                        Cursor.Move(CursorMoveDirection.left); ;
                        uncheckAllSelected();
                        Render.RendGame();
                        break;
                    case ConsoleKey.UpArrow:
                        Cursor.Move(CursorMoveDirection.up);
                        uncheckAllSelected();
                        Render.RendGame();
                        break;
                    case ConsoleKey.DownArrow:
                        Cursor.Move(CursorMoveDirection.down);
                        uncheckAllSelected();
                        Render.RendGame();
                        break;
                    case ConsoleKey.Enter:
                        bool temp1 = Cursor.Select();
                        if (temp1) EnemyTurn();
                        Render.RendGame();
                        break;
                    case ConsoleKey.F3:
                        bool temp2 = player.Attack();
                        Cursor.CheckInfo();
                        if (temp2) EnemyTurn();
                        Render.RendGame();
                        break;
                    // между temp1 и temp2 нет никакой разницы, я дал именам цифры только
                    // потомучто компилятор ругается что имена одинаковые
                }
            }
            if (LevelGenerator.GetCountExistEnemy() == 0)
            {
                LevelGenerator.GenerateNewLevel(LevelGenerator.rnd.Next(3));
                Render.RendGame();
            }
        }
    }
    private static void GameOver()
    {
        
    }

    private static bool CheckSize()
    {
        if (windowWidth != Console.WindowWidth || windowHeight != Console.WindowHeight)
        {
            windowWidth = Console.WindowWidth;
            windowHeight = Console.WindowHeight;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static vec2 getWindowSize()
    {
        vec2 size = new vec2(windowWidth, windowHeight);
        return size;
    }

    public static void uncheckAllSelected()
    {
        vec2 size = Map.GetGridSize();
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                Map.Cell temp = Map.GetCell(new vec2(j, i));
                temp.SetSelected(false);
                Cursor.selected = null;
                Hero.readyAttack = false;
            }
        }
        Hero.ResetTargets();
    }
    public static bool CheckWindow(int i, int j)
    {
        return i < windowHeight && j < windowWidth;
    }
    public static bool CheckWindow(vec2 vec)
    {
        return vec.y < windowHeight && vec.x < windowWidth;
    }

    public static void EnemyTurn()
    {
        Map.Cell playerCell = Map.GetCell(Program.player.GetPosId());
        (List<Zombie> z, List<Skeleton> s, List<Necromant> n) = LevelGenerator.GetEnemyLists();
        foreach (var e in z)
        {
            e.Walk(playerCell);
        }
        foreach (var e in s)
        {
            e.Walk(playerCell);
        }
        foreach (var e in n)
        {
            e.Walk(playerCell);
        }
    }
}