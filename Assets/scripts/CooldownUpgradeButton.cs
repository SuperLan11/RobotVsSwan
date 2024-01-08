public class CooldownUpgradeButton : UpgradeButton
{
    public override void Upgrade(int amount)
    {
        Robot.instance.cooldownPercentModifier += amount;
    }
}
