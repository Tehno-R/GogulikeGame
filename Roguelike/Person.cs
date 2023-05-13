using System.Text;

namespace Roguelike;

interface IAttack
{
    void Attack();
    void Ability();
}

public class Person : RenderPack
{
    private int healpoints;
    private int attack;

    //
    //
    //
    //
    //
    public Person(int hp, int atk)
    {
        this.healpoints = hp;
        this.attack = atk;
    }

    public override void Draw(List<StringBuilder> orig)
    {
        //throw new NotImplementedException();
    }
}