using System.Text;

namespace Roguelike;

public static class Render
{
    private static List<RenderPack> renderList = new List<RenderPack>();
    private static List<StringBuilder> toRend;
    private static List<StringBuilder> roomBase;

    private const int PADDING = 5;              // константа отступа (используется если консоль больше окна инветоря)
    public static int padding = PADDING;        // отступ в символах уделяемый для строки инвенторя
    private const int topPADDING = 98;          // константа отступа (используется если консоль больше log list)
    public static int topPadding = topPADDING;  // отступ в символах уделяемый для log list


    public static void RendText(List<string> text)
    {
        vec2 windowSize = Program.getWindowSize();
        toRend = new List<StringBuilder>();
        for (int i = 0; i < windowSize.y; i++) // prepare StringBuild (init)
        {
            toRend.Add(new StringBuilder(new string(' ', windowSize.x)));
        }
        
        int textPadding = (windowSize.y / 2) - (text.Count / 2);
        for (int i = textPadding, k = 0; i < windowSize.y && k < text.Count; i++, k++)
        {
            int strokePadding = (windowSize.x / 2) - (text[k].Length / 2);
            for (int j = strokePadding, l = 0; j < windowSize.x - 1 && l < text[k].Length; j++, l++)
            {
                toRend[i][j] = text[k][l];
            }
        }
        
        Console.Clear();
        for (int i = 0; i < windowSize.y; i++) // print result
        {
            for (int j = 0; j < windowSize.x; j++)
            {
                Console.Write(toRend[i][j]);
            }
        }
    }
    public static void RendGame()
    {
        vec2 windowSize = Program.getWindowSize();
        toRend = new List<StringBuilder>();
        for (int i = 0; i < windowSize.y; i++) // prepare StringBuild (init)
        {
            toRend.Add(new StringBuilder(new string(' ', windowSize.x)));
        }

        foreach (var t in renderList) // draw grid
        {
            t.Draw(toRend);
        }
        Cursor.Draw(toRend); // draw cursor
        BuildRoomBase(); // draw top border and center border

        Console.Clear();
        for (int i = 0; i < windowSize.y; i++) // print result
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
        if (windowSize.y <= PADDING) padding = windowSize.y - 1;
        else padding = PADDING;
        if (windowSize.x <= topPADDING) topPadding = windowSize.x - 1;
        else topPadding = topPADDING;

        for (int i = 0; i <= padding; i++)
        {
            for (int j = 0; j <= topPadding; j++)
            {
                if (i == 0 || i == padding || j == 0 || j == topPadding)
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