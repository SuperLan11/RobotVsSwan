public class DamageUpgradeButton : UpgradeButton
{
    public override void Upgrade(int amount)
    {
        Robot.instance.damagePercentModifier += amount;
    }
}
