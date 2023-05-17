namespace Roguelike;

public struct vec2
{
    public int x, y;

    public vec2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public static vec2 operator -(vec2 a, vec2 b)
    {
        return new vec2(a.x - b.x,a.y - b.y);
    }
}