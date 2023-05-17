namespace Roguelike;

public class Hero : Person, IAttack // управляемый персонаж
{
    private const string NAME = "Hero";
    private const int WIZARDHP = 10; // начальные стандартные значения
    private const int WIZARDATK = 4; // начальные стандартные значения
    private const string SKIN = "Ec  >I" +
                                "EK{  I" +
                                "Ec  >I"; // моделька персонажа

    public static bool readyAttack = false;
    private List<Person> targetToAttack = new List<Person>();

    public Hero() : base(NAME, WIZARDHP, WIZARDATK, SKIN) {}

    private bool standartA(int i, int j)
    {
        vec2 pos = currentCell.GetPosInd();
        if (i == pos.y + 1 && j == pos.x + 1)
        {
            return true;
        }
        else if (i == pos.y && j == pos.x + 1)
        {
            return true;
            
        } else if (i == pos.y - 1 && j == pos.x + 1)
        {
            return true;
            
        }
        return false;
    }
    public void Attack()
    {
        if (readyAttack)
        {
            foreach (var target in targetToAttack)
            {
                target.DealDamage(attack);
                if (target.GetHP() <= 0)
                {
                    target.Death();
                }
            }
            readyAttack = false;
            targetToAttack = new List<Person>();
            Program.uncheckAllSelected();
        }
        else
        {
            Program.uncheckAllSelected();
            for (int i = 0; i < Map.GetGridSize().y; i++)
            {
                for (int j = 0; j < Map.GetGridSize().x; j++)
                {
                    if (standartA(i, j))
                    {
                        Map.Cell temp = Map.GetCell(new vec2(j, i));
                        temp.SetSelected(true);
                        Person target = temp.GetContainer().GetPerson();
                        if (target != null)
                        {
                            targetToAttack.Add(target);
                        }
                    }
                }
            }
            readyAttack = true;
        }
    }

    public override void Death()
    {
        // stop game and show game over
    }
}