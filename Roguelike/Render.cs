
using System.Text;

namespace Roguelike;

public static class Render
{
    private static List<RenderPack> renderList = new List<RenderPack>();
    private static List<StringBuilder> toRend;
    private static List<StringBuilder> roomBase;

    public static void Start()
    {
        BuildRoomBase();
    }
    
    public static void Rend()
    {
        vec2 windowSize = Program.getWindowSize();
        toRend = Enumerable.Repeat(new StringBuilder(new string(' ', windowSize.x )), windowSize.y).ToList();;
        
        BuildRoomBase();
        toRend = roomBase;
        Console.Clear();
        for (int i = 0; i < windowSize.y; i++)
        {
            for (int j = 0; j < windowSize.x; j++)
            {
                Console.Write(toRend[i][j]);
            }
        }
    }
    
    static void AddRendPack(RenderPack pack)
    {
        renderList.Add(pack);
    }

    private static void BuildRoomBase()
    {
        roomBase = new List<StringBuilder>();
        vec2 windowSize = Program.getWindowSize();

        for (int i = 0; i < windowSize.y; i++)
        {
            roomBase.Add(new StringBuilder(new string(' ', windowSize.x)));
        }

        int padding = 10;
        if (Console.WindowHeight < padding) padding = Console.WindowHeight;
        
        for (int i = 0; i < padding; i++)
        {
            for (int j = 0; j < windowSize.x; j++)
            {
                if (i == 0 || i == padding - 1 || j == 0 || j == windowSize.x - 1)
                {
                    roomBase[i][j] = '#';
                }
            }
        }

        for (int i = padding; i < windowSize.y; i++)
        {
            for (int j = 0; j < windowSize.x; j++)
            {
                if (i == padding || i == windowSize.y - 1 || j == 0 || j == windowSize.x - 1)
                {
                    roomBase[i][j] = '#';
                }
            }
        }
    }
}