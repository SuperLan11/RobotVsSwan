
using System.Collections;
using UnityEngine;

public class HeadProjectileWeapon : ProjectileWeapon
{
    public int shotsPerBar;
    public bool firing = false;


    IEnumerator ShootCoroutine(int amount)
    {
        firing = true;
        for (int i = 0; i < amount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponentInChildren<BaseProjectile>().Fire(transform.up + Random.Range(-0.35f, 0.35f) * transform.right, 0, true);
            ChargeMeter.instance.progress -= 1f / (shotsPerBar * 3);
            yield return new WaitForSeconds(0.1f);
        }

        firing = false;
    }
    public override void Shoot(bool robot)
    {
        int bars = (int)(ChargeMeter.instance.progress * 3);
        if (!firing && bars > 0)
        {
            StartCoroutine(ShootCoroutine(bars * shotsPerBar));
        }
    }
}
