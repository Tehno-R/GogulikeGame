using System.Text;

namespace Roguelike;

interface IAttack
{
    void Attack();
}

public class Person : RenderPack // Класс описывает любое живое существо игры (игрок, враг)
{
    private int healpoints;
    private int attack;
    
    protected readonly string skin;
    private Map.Cell currentCell;

    protected Person(int hp, int atk, string skin) 
    {
        this.healpoints = hp;
        this.attack = atk;
        this.skin = skin;
    }

    // Список функций того что может происходить c Person
    // HP
    public void DealDamage(int damage)
    {
        healpoints -= damage;
        if (healpoints <= 0) Death();
    }
    public void Healing(int heal)
    {
        healpoints += heal;
    }
    public void ChangeHP(int value)
    {
        healpoints = value;
    }
    // ATK
    public void ReduceATK(int reduce)
    {
        attack -= reduce;
        if (attack < 0) attack = 0;
    }
    public void IncreaseATK(int increase)
    {
        attack += increase;
    }

    public void ChangeATK(int value)
    {
        attack = value;
        if (attack < 0) attack = 0;
    }
    //
    public void Walk(Map.Cell target)
    {
        currentCell.GetContainer().SetPerson(null);
        target.GetContainer().SetPerson(this);
    }

    public string GetSkin()
    {
        return skin;
    }
    
    protected virtual void Death(){}

    public override void Draw(List<StringBuilder> orig)
    {
        
    }
}