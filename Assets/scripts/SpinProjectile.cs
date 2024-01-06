using UnityEngine;

public class SpinProjectile : Projectile
{
    public int spinRate;

    protected override void Update()
    {
        base.Update();
        transform.Rotate(Vector3.forward, spinRate * Time.deltaTime);
    }
    
}
