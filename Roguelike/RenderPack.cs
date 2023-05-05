using System.Text;

namespace Roguelike;

public struct RenderPack
{
    private StringBuilder text;
    private vec2 pos;

    RenderPack(StringBuilder str, vec2 position)
    {
        this.text = str;
        this.pos = position;
    }
    RenderPack(string str, vec2 position)
    {
        this.text = new StringBuilder(str);
        this.pos = position;
    }
}