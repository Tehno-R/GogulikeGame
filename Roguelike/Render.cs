using System.Text;

namespace Roguelike;

public static class Render
{
    private static List<RenderPack> renderList = new List<RenderPack>();
    private static List<StringBuilder> toRend;
    private static List<StringBuilder> roomBase;

    private static readonly int PADDING = 9;
    public static int padding = PADDING;
    public static void Start() {}

    public static void Rend()
    {
        vec2 windowSize = Program.getWindowSize();
        toRend = new List<StringBuilder>();
        for (int i = 0; i < windowSize.y; i++)
        {
            toRend.Add(new StringBuilder(new string(' ', windowSize.x)));
        }

        foreach (var t in renderList)
        {
            t.Draw(toRend);
        }
        
        BuildRoomBase();

        Console.Clear();
        for (int i = 0; i < windowSize.y; i++)
        {
            for (int j = 0; j < windowSize.x; j++)
            {
                Console.Write(toRend[i][j]);
            }
        }
    }
    
    public static void AddRendPack(RenderPack pack)
    {
        renderList.Add(pack);
    }

    public static void BuildRoomBase()
    {
        vec2 windowSize = Program.getWindowSize();
        
        if (Console.WindowHeight < padding) padding = Console.WindowHeight;
        else padding = PADDING;

        for (int i = 0; i < padding; i++)
        {
            for (int j = 0; j < windowSize.x; j++)
            {
                if (i == 0 || i == padding - 1 || j == 0 || j == windowSize.x - 1)
                {
                    toRend[i][j] = '#';
                }
            }
        }

        for (int i = padding; i < windowSize.y; i++)
        {
            for (int j = 0; j < windowSize.x; j++)
            {
                if (i == padding || i == windowSize.y - 1 || j == 0 || j == windowSize.x - 1)
                {
                    toRend[i][j] = '#';
                }
            }
        }
    }
}