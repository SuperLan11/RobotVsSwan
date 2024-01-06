using UnityEngine;

public class NullWeapon : Weapon
{
    public override void Shoot()
    {
        
    }

    public override void PointAt(Vector2 target)
    {
        
    }

    public override void ResetDirection()
    {
        
    }

    public override WeaponType GetWeaponType()
    {
        return WeaponType.NONE;
    }
}
