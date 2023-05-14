using System.Text;

namespace Roguelike;

internal struct CursorMoveDirection
{
    public static vec2 left = new vec2(-1, 0);
    public static vec2 right = new vec2(1, 0);
    public static vec2 up = new vec2(0,-1);     // 0:0 bcs in left up corner
    public static vec2 down = new vec2(0, 1);   // 0:0 bcs in left up corner
}

public static class Cursor
{
    private static Map.Cell currentCell = Map.GetCell(new vec2(2,2));
    private static vec2 position = new vec2(currentCell.GetPos().x,
                                            currentCell.GetPos().y);

    private const char SYMBOL = (char)210;
    
    public static void Draw(List<StringBuilder> orig) // я подразумевал что всё будет рендерится от RenderPack но где-то я прокололся
    {
        vec2 winSize = Program.getWindowSize();
        if (position.y < winSize.y && position.x < winSize.x)
        {
            Boolean flag = true;
            orig[position.y][position.x] = SYMBOL;
            if (position.y + Map.Cell.lenCell.y - 1 < winSize.y)
            {
                orig[position.y + Map.Cell.lenCell.y - 1][position.x] = SYMBOL;
            }
            else
            {
                flag = false;
            }

            if (position.x + Map.Cell.lenCell.x - 1 < winSize.x)
            {
                orig[position.y][position.x + Map.Cell.lenCell.x - 1] = SYMBOL;
            }
            else
            {
                flag = false;
            }
            
            if (flag)
            {
                orig[position.y + Map.Cell.lenCell.y - 1][position.x + Map.Cell.lenCell.x - 1] = SYMBOL;
            }
        }
    }

    public static void Move(vec2 pos)
    {
        vec2 curInd = currentCell.GetPosInd();
        vec2 newInd = new vec2(curInd.x + pos.x, curInd.y + pos.y);
        if (newInd.x >= 0 && newInd.x < Map.GetGridSize().x && newInd.y >= 0 && newInd.y < Map.GetGridSize().y)
        {
            currentCell = Map.GetCell(newInd);
            position = new vec2(currentCell.GetPos().x, currentCell.GetPos().y);
            Render.Rend();
        }
    }
}