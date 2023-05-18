namespace Roguelike;

public class Hero : Person, IAttack // управляемый персонаж
{
    public static class ArtPanelGrid
    {
        private static readonly vec2 cellSize = new vec2(10, 8);
        private static List<GameObject> artefacts = new List<GameObject>();

        public static GameObject GetArtefact(int i)
        {
            return artefacts[i];
        }
        public static void AddArt(GameObject obj)
        {
            artefacts.Add(obj);
            if (obj.GetCharach().name == "BattleFury") Hero.aroundA = true;
        }
        public static int GetLength()
        {
            return artefacts.Count;
        }

        public static vec2 GetCellSize()
        {
            return cellSize;
        }
    }
    
    
    private const string NAME = "Hero";
    private const int WIZARDHP = 10; // начальные стандартные значения
    private const int WIZARDATK = 4; // начальные стандартные значения
    private const string SKIN = "Ec  >I" +
                                "EK{  I" +
                                "Ec  >I"; // моделька персонажа

    public static bool readyAttack = false;
    private static List<Person> targetToAttack = new List<Person>();

    private static bool aroundA = false;

    public Hero() : base(NAME, WIZARDHP, WIZARDATK, SKIN) {}

    private bool StandartA(int i, int j)
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

    private bool AroundA(int i, int j)
    {
        if (!aroundA) return false;
        vec2 pos = currentCell.GetPosInd();
        if ((pos.y + 1 == i) && (pos.x - 1 <= j && j <= pos.x + 1))
        {
            return true;
        }
        else if ((pos.y - 1 == i) && (pos.x - 1 <= j && j <= pos.x + 1))
        {
            return true;
        }
        else if (pos.y == i && (pos.x - 1 == j || pos.x + 1 == j))
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
                    if (StandartA(i, j) || AroundA(i, j))
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

    public override void Walk(Map.Cell target)
    {
        currentCell.GetContainer().SetPerson(null);
        currentCell = target;
        currentCell.GetContainer().SetPerson(this);
        GameObject obj = currentCell.GetContainer().GetGameObject();
        if (obj != null)
        {
            ArtPanelGrid.AddArt(obj);
            currentCell.GetContainer().SetGameObject(null);
        }
        Render.RendGame();
    }

    public static void ResetTargets()
    {
        targetToAttack = new List<Person>();
        readyAttack = false;
    }

    public override void Death()
    {
        // stop game and show game over
    }
}