using System.Text;

namespace Roguelike;

public static class Map
{
    private static readonly vec2 mapSize = new vec2(17, 5); 
    private static Grid _grid = new Grid();
    private class Cell : RenderPack
    {
        public static readonly vec2 lenCell = new vec2(8, 6);

        private vec2 pos;

        public Cell(vec2 position = new vec2())
        {
            this.pos = position;
        }
        public override void Draw(List<StringBuilder> orig)
        {
            int endX = pos.x + lenCell.x;
            int endY = pos.y + lenCell.y;
            vec2 winSize = Program.getWindowSize();
            for (int i = pos.y; i < endY && i < winSize.y; i++)
            {
                for (int j = pos.x; j < endX && j < winSize.x; j++)
                {
                    if (j == pos.x || j == endX - 1) orig[i][j] = '|';
                    else if (i == pos.y || i == endY - 1) orig[i][j] = '-';
                }
            }
        }
    }
    class Grid : RenderPack
    {
        private vec2 startPos = new vec2(0, Render.padding);
        private static vec2 gridSize = new vec2(mapSize.x, mapSize.y);
        private List<List<Cell>> allCell = new List<List<Cell>>();

        public Grid()
        {
            for (int i = 0; i < gridSize.y; i++)
            {
                allCell.Add(new List<Cell>());
                for (int j = 0; j < gridSize.x; j++)
                {
                    allCell[i].Add(new Cell(new vec2(startPos.x + (Cell.lenCell.x - 1) * j,
                                                                startPos.y + (Cell.lenCell.y - 1) * i)));
                }
            }
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

    public static void Start()
    {
        Render.AddRendPack(_grid);
    }
}