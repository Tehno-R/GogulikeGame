using System.Text;

namespace Roguelike;

public class GameObject
{
    private string name;
    private string descript;
    
    protected readonly string skin;
    private Map.Cell currentCell;

    public GameObject(string name, string dscrpt, string skin)
    {
        this.name = name;
        this.skin = skin;
        this.descript = dscrpt;
    }

    public string GetSkin()
    {
        return skin;
    }
    
    public (string name, string description) GetCharach()
    {
        return (name, descript);
    }

    public virtual void GetObj() {}
}

public class ModAround : GameObject
{
    private const string NAME = "BattleFury";
    private const string SKIN = @"    /\" +
                                @"---/\ " +
                                @"  /\  ";

    private const string DESCRIPTION = "Make ur attack round";
    
    public ModAround() : base(NAME, DESCRIPTION, SKIN)
    { }

}