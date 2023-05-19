using System.Text;

namespace Roguelike;

public static class Render
{
    private static RenderPack renderGrid = null;            // ссылка на главную сетку
    private static List<StringBuilder> toRend;              // хранит в себе итоговый вывод
    private static List<StringBuilder> roomBase;            // паттерн обрамления
    private static List<string> ObjStatus = null;           // хранит инфу о выделенном предмете
    private static List<string> log = new List<string>();   // история действий

    private const int PADDING = 7;              // константа отступа (используется если консоль больше окна инветоря)
    public static int padding = PADDING;        // отступ в символах уделяемый для строки инвенторя
    private const int topPADDING = 98;          // константа отступа (используется если консоль больше log list)
    public static int topPadding = topPADDING;  // отступ в символах уделяемый для log list


    public static void RendMainMenu(List<string> text)
    {
        vec2 windowSize = Program.getWindowSize();
        toRend = new List<StringBuilder>();
        for (int i = 0; i < windowSize.y; i++) // prepare StringBuild (init)
        {
            toRend.Add(new StringBuilder(new string(' ', windowSize.x)));
        }
        
        int textPadding = (windowSize.y / 2) - (text.Count / 2);
        if (textPadding < 0) textPadding = 0;
        for (int i = textPadding, k = 0; i < windowSize.y && k < text.Count; i++, k++)
        {
            int strokePadding = (windowSize.x / 2) - (text[k].Length / 2);
            if (strokePadding < 0) strokePadding = 0;
            for (int j = strokePadding, l = 0; j < windowSize.x - 1 && l < text[k].Length; j++, l++)
            {
                toRend[i][j] = text[k][l];
            }
        }
        
        // print result
        Console.Clear();
        for (int i = 0; i < windowSize.y; i++) 
        {
            for (int j = 0; j < windowSize.x; j++)
            {
                Console.Write(toRend[i][j]);
            }
        }
    }
    // функция для простого вывода текста по центру эерана
    public static void RendText(List<string> text, vec2 startPos, vec2 endPos)
    {
        if (text == null) return;
        vec2 windowSize = Program.getWindowSize();

        int textPadding = ((endPos.y - startPos.y) / 2) - (text.Count / 2);
        if (textPadding < 0) textPadding = 0;
        for (int i = textPadding + startPos.y, k = 0; i < endPos.y - 1 && k < text.Count && i < windowSize.y; i++, k++)
        {
            int strokePadding = ((endPos.x - startPos.x) / 2) - (text[k].Length / 2);
            if (strokePadding < 0) strokePadding = 0;
            for (int j = strokePadding + startPos.x, l = 0; j < endPos.x && l < text[k].Length && j < windowSize.x; j++, l++)
            {
                toRend[i][j] = text[k][l];
            }
        }
    }
    public static void RendGame()
    {
        // init
        vec2 windowSize = Program.getWindowSize();
        toRend = new List<StringBuilder>();
        for (int i = 0; i < windowSize.y; i++) // prepare StringBuild (init)
        {
            toRend.Add(new StringBuilder(new string(' ', windowSize.x)));
        }
        // playground
        renderGrid.Draw(toRend);
        // gameobject panel
        vec2 cellSize = Hero.ArtPanelGrid.GetCellSize();
        for (int e = 0; e < Hero.ArtPanelGrid.GetLength(); e++)
        {
            vec2 startPos = new vec2(0 + (cellSize.x - 1) * e,0);
            string template = Hero.ArtPanelGrid.GetArtefact(e).GetSkin();
            for (int i = startPos.y + 1 + 1, k = 0;
                 i < padding - 1 &&
                 k < template.Length / (Map.Cell.lenCell.x - 2); i++, k++)
            {
                for (int j = startPos.x + 1 + 2, l = 0; j < startPos.x + cellSize.x - 1 &&
                                               Program.CheckWindow(i, j) &&
                                               l < template.Length / (Map.Cell.lenCell.y - 2); j++, l++)
                {
                    toRend[i][j] = template[k * Map.Cell.lenCell.y + l + k];
                }
            }
        }
        // cursor
        Cursor.Draw(toRend); // draw cursor
        // info text
        int endGridX = Map.GetGridSize().x * (Map.Cell.lenCell.x - 1);
        RendText(ObjStatus, new vec2(topPadding + 1, 0), new vec2(endGridX, padding));
        // log
        for (int i = padding + 1, l = 0; l < 30 && i < windowSize.y && l < log.Count; i++, l++)
        {
            for (int j = topPADDING + 23, k = 0; j < windowSize.x - 1 && k < log[l].Length; j++, k++)
            {
                toRend[i][j] = log[l][k];
            }
        }
        // border
        BuildRoomBase(); // draw top border and center border
        // print result
        Console.Clear();
        for (int i = 0; i < windowSize.y; i++) 
        {
            for (int j = 0; j < windowSize.x; j++)
            {
                Console.Write(toRend[i][j]);
            }
        }
    }
    public static void BuildRoomBase() // рисует обрамление (рамочки)
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
    public static void SetRenderPack(RenderPack pack) // сохраняет сетку для вывода
    {
        renderGrid = pack;
    }

    public static void SetStatus(List<string> info) // установить инфу о обьекте
    {
        ObjStatus = info;
    }

    public static void AddLog(string lg) // добавить строку действия
    {
        if (log.Count == 10)
        {
            log.RemoveAt(0);
        }
        log.Add(lg);
    }

}