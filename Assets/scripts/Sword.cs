using UnityEngine;

public class Sword : ProjectileWeapon
{
    public override void Update()
    {
        base.Update();
        GetComponent<SpriteRenderer>().enabled = currentCooldown <= 0;
    }
}
