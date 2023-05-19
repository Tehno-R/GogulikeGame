namespace Roguelike;

public class Zombie : Person
{
    private const string NAME = "Zombie";
    private const int ZOMBIEHP = 20;
    private const int ZOMBIEATK = 3;

    private const string SKIN = "ooZZoo" +
                                "oZooZo" +
                                "ZooooZ";

    public Zombie() : base(NAME,ZOMBIEHP, ZOMBIEATK, SKIN)
    {
    }

    public override void Walk(Map.Cell target)
    {
        vec2 curPos = this.GetPosId();
        vec2 dif = target.GetPosInd() - curPos;
        if (Math.Abs(dif.x) <= 1 && Math.Abs(dif.y) <= 1) Attack();
        else if (Math.Abs(dif.x) > 1)
        {
            int dir;
            if (dif.x > 0) dir = 1;
            else dir = -1;
            Map.Cell tagetCell = Map.GetCell(new vec2(curPos.x + dir, curPos.y));
            if (tagetCell.GetContainer().GetPerson() == null)
            {
                currentCell.GetContainer().SetPerson(null);
                currentCell = tagetCell;
                currentCell.GetContainer().SetPerson(this);
            }
        }
        else if (Math.Abs(dif.y) > 1)
        {
            int dir;
            if (dif.y > 0) dir = 1;
            else dir = -1;
            Map.Cell tagetCell = Map.GetCell(new vec2(curPos.x, curPos.y + dir));
            if (tagetCell.GetContainer().GetPerson() == null)
            {
                currentCell.GetContainer().SetPerson(null);
                currentCell = tagetCell;
                currentCell.GetContainer().SetPerson(this);
            }
        }
    }

    public void Attack()
    {
        (vec2 tagterHP_ATK, string name) = Program.player.GetCharach();
        Render.AddLog("Zombie attack: hero " +
                      + tagterHP_ATK.x + " -> " + (tagterHP_ATK.x - attack));
        Program.player.DealDamage(attack);
    }

    public void Death()
    {
        LevelGenerator.RefreshEnemyLists();
        Render.AddLog("Zombie is death");
    }
}

public class Skeleton : Person
{
    private const string NAME = "Skeleton";
    private const int SKELETONHP = 10;
    private const int SKELETONATK = 1;

    private const string SKIN = "ooSSoo" +
                                "oS][So" +
                                "..uu..";

    public Skeleton() : base(NAME, SKELETONHP, SKELETONATK, SKIN)
    {
    }

    public override void Walk(Map.Cell target)
    {
        vec2 curPos = this.GetPosId();
        vec2 dif = target.GetPosInd() - curPos;
        if (Math.Abs(dif.x) <= 4 && Math.Abs(dif.y) <= 4) Attack();
        else if (Math.Abs(dif.x) > 4)
        {
            int dir;
            if (dif.x > 0) dir = 1;
            else dir = -1;
            Map.Cell tagetCell = Map.GetCell(new vec2(curPos.x + dir, curPos.y));
            if (tagetCell.GetContainer().GetPerson() == null)
            {
                currentCell.GetContainer().SetPerson(null);
                currentCell = tagetCell;
                currentCell.GetContainer().SetPerson(this);
            }
        }
        else if (Math.Abs(dif.y) > 4)
        {
            int dir;
            if (dif.y > 0) dir = 1;
            else dir = -1;
            Map.Cell tagetCell = Map.GetCell(new vec2(curPos.x, curPos.y + dir));
            if (tagetCell.GetContainer().GetPerson() == null)
            {
                currentCell.GetContainer().SetPerson(null);
                currentCell = tagetCell;
                currentCell.GetContainer().SetPerson(this);
            }
        }
    }

    private void Attack()
    {
        (vec2 tagterHP_ATK, string name) = Program.player.GetCharach();
        Render.AddLog("Skeleton attack: hero " +
                      + tagterHP_ATK.x + " -> " + (tagterHP_ATK.x - attack));
        Program.player.DealDamage(attack);
    }
    
    public void Death()
    {
        LevelGenerator.RefreshEnemyLists();
        Render.AddLog("Skeleton is death");
    }
    
}

public class Necromant : Person
{
    private const string NAME = "Necromant";
    private const int NECROMANTHP = 10;
    private const int NECROMANTATK = 4;

    private const string SKIN = "ooYoYo" +
                                "oIzmmo" +
                                "oonono";

    private int counter = 0;

    public Necromant() : base(NAME,NECROMANTHP, NECROMANTATK, SKIN)
    {
    }

    public override void Walk(Map.Cell target)
    {
        vec2 curPos = this.GetPosId();
        vec2 dif = target.GetPosInd() - curPos;
        if (Math.Abs(dif.x) <= 4 && Math.Abs(dif.y) <= 4) Ability();
        else if (Math.Abs(dif.x) > 4)
        {
            int dir;
            if (dif.x > 0) dir = 1;
            else dir = -1;
            Map.Cell tagetCell = Map.GetCell(new vec2(curPos.x + dir, curPos.y));
            if (tagetCell.GetContainer().GetPerson() == null)
            {
                currentCell.GetContainer().SetPerson(null);
                currentCell = tagetCell;
                currentCell.GetContainer().SetPerson(this);
            }
        }
        else if (Math.Abs(dif.y) > 4)
        {
            int dir;
            if (dif.y > 0) dir = 1;
            else dir = -1;
            Map.Cell tagetCell = Map.GetCell(new vec2(curPos.x, curPos.y + dir));
            if (tagetCell.GetContainer().GetPerson() == null)
            {
                currentCell.GetContainer().SetPerson(null);
                currentCell = tagetCell;
                currentCell.GetContainer().SetPerson(this);
            }
        }
    }

    private void Ability()
    {
        if (counter == 5)
        {
            (vec2 tagterHP_ATK, string name) = Program.player.GetCharach();
            Render.AddLog("Zombie attack: hero " +
                          + tagterHP_ATK.x + " -> " + (tagterHP_ATK.x - attack));
            counter = 0;
            Skeleton newS = new Skeleton();
            LevelGenerator.GetSkeletonsList().Add(newS);
            LevelGenerator.GenerateNewEnemy(newS);
        }
        else
        {
            counter++;
        }
    }
    
    public void Death()
    {
        LevelGenerator.RefreshEnemyLists();
        Render.AddLog("Necromat is death");
    }
}