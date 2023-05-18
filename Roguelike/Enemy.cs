namespace Roguelike;

public class Zombie : Person
{
    private const string NAME = "Zombie";
    private const int ZOMBIEHP = 20;
    private const int ZOMBIEATK = 6;

    private const string SKIN = "ooZZoo" +
                                "oZooZo" +
                                "ZooooZ";

    public Zombie() : base(NAME,ZOMBIEHP, ZOMBIEATK, SKIN)
    {
    }
}

public class Skeleton : Person
{
    private const string NAME = "Skeleton";
    private const int SKELETONHP = 10;
    private const int SKELETONATK = 4;

    private const string SKIN = "ooSSoo" +
                                "oS][So" +
                                "..uu..";

    public Skeleton() : base(NAME, SKELETONHP, SKELETONATK, SKIN)
    {
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

    public Necromant() : base(NAME,NECROMANTHP, NECROMANTATK, SKIN)
    {
    }
}