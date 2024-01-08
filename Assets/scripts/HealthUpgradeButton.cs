public class HealthUpgradeButton : UpgradeButton
{
    public override void Upgrade(int amount)
    {
        Robot.instance.maxHealth += amount;
    }
}
