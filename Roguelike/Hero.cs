namespace Roguelike;

public class Hero : Person, IAttack // управляемый персонаж
{
    private const string NAME = "Hero";
    private const int WIZARDHP = 10; // начальные стандартные значения
    private const int WIZARDATK = 4; // начальные стандартные значения
    private const string SKIN = "Ec  >I" +
                                "EK{  I" +
                                "Ec  >I"; // моделька персонажа

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
        vec2 curIdCell = currentCell.GetPosInd();

        for (int i = 0; i < Map.GetGridSize().y; i++)
        {
            for (int j = 0; j < Map.GetGridSize().x; j++)
            {
                if (Map.GetCell(curIdCell).GetContainer().GetPerson() != null)
                {
                    if (standartA(i, j))
                    {
                        Map.GetCell(new vec2(j, i)).SetSelected(true);
                    }
                }
            }
        }
    }

    protected override void Death()
    {
        // stop game and show game over
    }
}