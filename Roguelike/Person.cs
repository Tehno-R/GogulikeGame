using System.Text;

namespace Roguelike;

interface IAttack
{
    void Attack();
}

public class Person // Класс описывает любое живое существо игры (игрок, враг)
{
    private string name;
    protected int healpoints;
    protected int attack;
    
    private readonly string skin;
    protected Map.Cell currentCell = null;

    protected Person(string name, int hp, int atk, string skin)
    {
        this.name = name;
        this.healpoints = hp;
        this.attack = atk;
        this.skin = skin;
    }

    public void SetCell(Map.Cell cl)
    {
        if (currentCell != null)
        {
            currentCell.GetContainer().SetPerson(null);
        }
        currentCell = cl;
        cl.GetContainer().SetPerson(this);
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
    public virtual void Walk(Map.Cell target)
    {
        currentCell.GetContainer().SetPerson(null);
        currentCell = target;
        currentCell.GetContainer().SetPerson(this);
        Render.RendGame();
    }

    public string GetSkin()
    {
        return skin;
    }

    public (vec2, string) GetCharach() // ! в c# можно возвращать несколько переменных !
    {
        return (new vec2(healpoints, attack), name);
    }

    public vec2 GetPosId()
    {
        return currentCell.GetPosInd();
    }

    public int GetHP()
    {
        return healpoints;
    }

    public virtual void Death()
    {
        currentCell.GetContainer().SetPerson(null);
        LevelGenerator.ReduceEnemy();
    }
}