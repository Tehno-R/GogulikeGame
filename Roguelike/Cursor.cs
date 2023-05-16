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
    public static Map.Cell selected;

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
        ResetSelect();

        vec2 curInd = currentCell.GetPosInd();
        vec2 newInd = new vec2(curInd.x + pos.x, curInd.y + pos.y);
        if (newInd.x >= 0 && newInd.x < Map.GetGridSize().x && newInd.y >= 0 && newInd.y < Map.GetGridSize().y)
        {
            currentCell = Map.GetCell(newInd);
            position = new vec2(currentCell.GetPos().x, currentCell.GetPos().y);
            CheckInfo();
        }
    }

    private static void CheckInfo() // Если дергать разрешение консоли то почемуто не все стерается
    {
        Person targetP = currentCell.GetContainer().GetPerson();
        GameObject targetG = currentCell.GetContainer().GetGameObject();
        List<string> infoText = new List<string>();
        if (targetP != null)
        {
            (vec2 info, string name) = targetP.GetCharach(); // ! в c# можно возвращать несколько переменных !
            infoText.Add("name: " + name);
            infoText.Add("hp: " + info.x);
            infoText.Add("atc: " + info.y);

            Render.SetStatus(infoText);
        }
        else if (targetG != null)
        {
            (string name, string dscrpt) = targetG.GetCharach(); // ! в c# можно возвращать несколько переменных !
            infoText.Add("name: " + name);
            if (dscrpt.Length > 12)
            {
                infoText.Add("desc: " + dscrpt.Substring(0, 12));
                int i = 13;
                while (i != dscrpt.Length)
                {
                    if ((i - 13) % 18 == 0 && (i - 13) % 18 != 0)
                    {
                        infoText.Add(dscrpt.Substring(i - 18, 18));
                    }
                    i++;
                }
                infoText.Add(dscrpt.Substring(i - ((i - 13) % 18), (i - 13) % 18));
            }

            Render.SetStatus(infoText);
        }
        else
        {
            Render.SetStatus(null);
        }
    }

    public static void Select()
    {
        if (selected != null)
        {
            Program.player.Walk(selected);
            ResetSelect();
            Program.uncheckAllSelected();
        }
        else
        {
            selected = currentCell;
            Map.ResetSelected();
            selected.SetSelected(true);
        }
    }

    private static void ResetSelect()
    {
        if (selected != null)
        {
            selected.SetSelected(false);
            selected = null;
        }
    }
}