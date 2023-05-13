namespace Roguelike;

public class Wizard : Person, IAttack
{
    
    private const int WIZARDHP = 10;
    private const int WIZARDATK = 4;

    public Wizard(int hp, int atk) : base(WIZARDHP, WIZARDATK)
    {
        
    }
    
    public void Attack() // thunder strike on random cells
    {
        //throw new NotImplementedException();
    }

    public void Ability() // count up thunders strikes for 1 turn
    {
        //throw new NotImplementedException();
    }
}