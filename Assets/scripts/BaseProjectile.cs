using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    public abstract void Fire(Vector2 direction, float offset, bool robot);

    public abstract int GetDamage();
}