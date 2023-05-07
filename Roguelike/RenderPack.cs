using System.Text;

namespace Roguelike;

public abstract class RenderPack
{
    public abstract void Draw(List<StringBuilder> orig);
}