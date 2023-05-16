// w:h 148:35
using Roguelike;

static class Program
{
    private static int windowWidth = Console.WindowWidth;
    private static int windowHeight = Console.WindowHeight;

    public static Person player = new Hero();

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
        Map.Start();
        GameObject testObj = new ModAround();
        player.SetCell(Map.GetCell(new vec2(3,4)));
        Map.SetGameobjectInCell(new vec2(4,3), testObj);
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
                        break;
                    case ConsoleKey.LeftArrow:
                        Cursor.Move(CursorMoveDirection.left);
                        break;
                    case ConsoleKey.UpArrow:
                        Cursor.Move(CursorMoveDirection.up);
                        break;
                    case ConsoleKey.DownArrow:
                        Cursor.Move(CursorMoveDirection.down);
                        break;
                    case ConsoleKey.Enter:
                        Cursor.Select();
                        break;
                }
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
}