namespace Roguelike;

public class Hero : Person, IAttack // управляемый персонаж
{
    private const int WIZARDHP = 10; // начальные стандартные значения
    private const int WIZARDATK = 4; // начальные стандартные значения
    private const string SKIN = "Ec  >I" +
                                "EK{  I" +
                                "Ec  >I"; // моделька персонажа

    public Hero() : base(WIZARDHP, WIZARDATK, SKIN) {}
    
    public void Attack()
    {
        //throw new NotImplementedException();
    }

    protected override void Death()
    {
        // stop game and show game over
    }
}