using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Shoot(bool robot);
    
    public abstract void PointAt(Vector2 target);

    public abstract void ResetDirection();

    public abstract WeaponType GetWeaponType();
}
