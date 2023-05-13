namespace Roguelike;

public class Zombie : Person
{
    private const int ZOMBIEHP = 20;
    private const int ZOMBIEATK = 6;

    public Zombie(int hp, int atk) : base(ZOMBIEHP, ZOMBIEATK)
    {
    }
}

public class Skeleton : Person
{
    private const int SKELETONHP = 10;
    private const int SKELETONATK = 4;

    public Skeleton(int hp, int atk) : base(SKELETONHP, SKELETONATK)
    {
    }
}

public class Necromant : Person
{
    private const int NECROMANTHP = 10;
    private const int NECROMANTATK = 4;

    public Necromant(int hp, int atk) : base(NECROMANTHP, NECROMANTATK)
    {
    }
}