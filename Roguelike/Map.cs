using System.Text;

namespace Roguelike;

public static class Map
{
    private static readonly vec2 mapSize = new vec2(17, 7); 
    private static Grid _grid = new Grid();

    public class Cell : RenderPack
    {
        public static readonly vec2 lenCell = new vec2(8, 5);
        private vec2 pos;
        private vec2 posInd;
        private readonly CellContainer? container = new CellContainer(); // что значит "?" сам не знаю,
                                                                         // но IDE мне подсказала что так фиксится
                                                                         // ошибка присвоение null переменной
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
        }
        public override void Draw(List<StringBuilder> orig)
        {
            vec2 endPos = new vec2(pos.x + lenCell.x, pos.y + lenCell.y);
            vec2 winSize = Program.getWindowSize();
            for (int i = pos.y; i < endPos.y && i < winSize.y; i++)
            {
                for (int j = pos.x; j < endPos.x && j < winSize.x; j++)
                {
                    if (j == pos.x || j == endPos.x - 1) orig[i][j] = '|';
                    else if (i == pos.y || i == endPos.y - 1) orig[i][j] = '-';
                }
            }

            Person drawTarget = GetContainer().GetPerson();
            if (drawTarget != null)
            {
                string template = drawTarget.GetSkin();
                for (int i = pos.y + 1, k = 0;
                     i < endPos.y - 1 &&
                     i < winSize.y &&
                     k <= template.Length / (Cell.lenCell.x - 2); i++, k++)
                {
                    for (int j = pos.x + 1, l = 0; j < endPos.x - 1 &&
                                                   j < winSize.x &&
                                                   l < template.Length / (Cell.lenCell.y - 2); j++, l++)
                    {
                        orig[i][j] = template[k * Cell.lenCell.y + l + k];
                    }
                }
            }

            // there be draw person or gameobject
            
        }
    }
    public class CellContainer
    {
        private Person _person = null;
        // private GameObject _gameObject;

        public void SetPerson(Person p)
        {
            _person = p;
        }
        public Person GetPerson()
        {
            if (_person != null) return _person;
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