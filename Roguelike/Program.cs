// w:h 148:35
using Roguelike;

class Program
{
    private static int windowWidth = Console.WindowWidth;
    private static int windowHeight = Console.WindowHeight;

    static void Main(string[] args)
    {
        // Render.Start();
        Map.Start();
        Render.Rend();
        
        while (true)
        {
            if (windowWidth != Console.WindowWidth || windowHeight != Console.WindowHeight)
            {
                // if (Console.WindowWidth < 120 - 3) windowWidth = 120 - 3;
                // else windowWidth = Console.WindowWidth;
                // if (Console.WindowHeight < 30) windowHeight = 30;
                // else windowHeight = Console.WindowHeight; 
                windowWidth = Console.WindowWidth;
                windowHeight = Console.WindowHeight;
                Render.Rend();
            }
        }
    }

    public static vec2 getWindowSize()
    {
        vec2 size = new vec2(windowWidth, windowHeight);
        return size;
    }
}