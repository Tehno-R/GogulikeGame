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
        Hero player = new Hero();
        Map.SetPersonInCell(new vec2(2,4), player);
        
        while (true)
        {
            if (windowWidth != Console.WindowWidth || windowHeight != Console.WindowHeight)
            {
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