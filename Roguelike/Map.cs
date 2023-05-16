using System.Text;

namespace Roguelike;

public static class Map
{
    private static readonly vec2 mapSize = new vec2(17, 8); 
    private static Grid _grid = new Grid();

    public class Cell : RenderPack
    {
        public static readonly vec2 lenCell = new vec2(8, 5);
        private vec2 pos;
        private vec2 posInd;
        private readonly CellContainer? container = new CellContainer(); // что значит "?" сам не знаю,
                                                                         // но IDE мне подсказала что так фиксится
                                                                         // ошибка присвоение null переменной
        private bool selected = false;

        private vec2 endPos;
        private vec2 winSize;
        public CellContainer GetContainer()
        {
            return container;
        }
        public vec2 GetPos()
        {
            return pos;
        }
        public vec2 GetPosInd()
        {
            return posInd;
        }
        
        public Cell(vec2 position, vec2 ind)
        {
            this.pos = position;
            this.posInd = ind;
            endPos = new vec2(pos.x + lenCell.x, pos.y + lenCell.y);
        }

        private bool checkWindow(int i, int j)
        {
            return i < winSize.y && j < winSize.x;
        }
        public override void Draw(List<StringBuilder> orig)
        {
            winSize = Program.getWindowSize();
            for (int i = pos.y; i < endPos.y && i < winSize.y; i++)
            {
                for (int j = pos.x; j < endPos.x && j < winSize.x; j++)
                {
                    if (j == pos.x || j == endPos.x - 1) orig[i][j] = '|';
                    else if (i == pos.y || i == endPos.y - 1) orig[i][j] = '-';
                }
            }

            string template = null;
            Person drawTargetP = GetContainer().GetPerson();
            GameObject drawTargetG = GetContainer().GetGameObject();
            if (drawTargetP != null)
            {
                template = drawTargetP.GetSkin();
            }
            else if (drawTargetG != null)
            {
                template = drawTargetG.GetSkin();
            }
            if (template != null)
            {
                for (int i = pos.y + 1, k = 0;
                     i < endPos.y - 1 &&
                     k <= template.Length / (Cell.lenCell.x - 2); i++, k++)
                {
                    for (int j = pos.x + 1, l = 0; j < endPos.x - 1 &&
                                                   checkWindow(i, j) &&
                                                   l < template.Length / (Cell.lenCell.y - 2); j++, l++)
                    {
                        orig[i][j] = template[k * Cell.lenCell.y + l + k];
                    }
                }
            }
            if (selected) {drawSelection(orig);}
        }

        private void drawSelection(List<StringBuilder> orig)
        {
            for (int i = pos.y + 1; i < endPos.y - 1; i++)
            {
                for (int j = pos.x + 1; j < endPos.x - 1 && checkWindow(i, j); j++)
                {
                    orig[i][j] = (char)254;
                }
            }            
        }

        public void setSelected(bool s)
        {
            this.selected = s;
        }
    }
    public class CellContainer
    {
        private Person _person = null;
        private GameObject _gameobj = null;
        // private GameObject _gameObject;

        public void SetPerson(Person p)
        {
            _person = p;
            // if (_gameobj != null)
            // {
            //     // p.addGameObject
            // }
        }
        public Person GetPerson()
        {
            if (_person != null) return _person;
            else return null;
        }
        public void SetGameObject(GameObject obj)
        {
            _gameobj = obj;
        }
        public GameObject GetGameObject()
        {
            if (_gameobj != null) return _gameobj;
            else return null;
        }
    }
    private class Grid : RenderPack
    {
        private vec2 startPos = new vec2(0, Render.padding);
        private static readonly vec2 gridSize = new vec2(mapSize.x, mapSize.y);
        private List<List<Cell>> allCell = new List<List<Cell>>();

        public Grid()
        {
            for (int i = 0; i < gridSize.y; i++)
            {
                allCell.Add(new List<Cell>());
                for (int j = 0; j < gridSize.x; j++)
                {
                    allCell[i].Add(new Cell(new vec2(startPos.x + (Cell.lenCell.x - 1) * j,
                                                                startPos.y + (Cell.lenCell.y - 1) * i),
                                                               new vec2(j,i)));
                }
            }
        }
        public Cell GetCell(vec2 pos)
        {
            return allCell[pos.y][pos.x];
        }
        public vec2 GetSize()
        {
            return gridSize;
        }
        public override void Draw(List<StringBuilder> orig)
        {
            for (int i = 0; i < gridSize.y; i++)
            {
                for (int j = 0; j < gridSize.x; j++)
                {
                    allCell[i][j].Draw(orig);
                }
            }
        }
    }

    public static void SetPersonInCell(vec2 pos, Person p)
    {
        _grid.GetCell(pos).GetContainer().SetPerson(p);
    }
    public static void SetPersonInCell(Cell pos, Person p)
    {
        pos.GetContainer().SetPerson(p);
    }
    public static void SetGameobjectInCell(vec2 pos, GameObject g)
    {
        _grid.GetCell(pos).GetContainer().SetGameObject(g);
    }
    public static Cell GetCell(vec2 index)
    {
        return _grid.GetCell(index);
    }

    public static vec2 GetGridSize()
    {
        return _grid.GetSize();
    }
    public static void Start()
    {
        Render.AddRendPack(_grid);
    }
}