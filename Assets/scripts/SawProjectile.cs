using UnityEngine;

public class SawProjectile : Projectile
{
    protected override void Update()
    {
        base.Update();
        transform.Rotate(Vector3.forward, 360 * Time.deltaTime);
    }
}
