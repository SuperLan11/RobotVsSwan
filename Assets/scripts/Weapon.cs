using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Shoot();
    
    public abstract void PointAt(Vector2 target);

    public abstract void ResetDirection();
}
