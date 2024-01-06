using UnityEngine;
public class ProjectileWeapon : Weapon
{
    private GameObject pivot;
    public GameObject projectilePrefab;
    public float cooldown;
    protected float currentCooldown = 0;
    public float projectileOffset;
    public WeaponType weaponType;

    public virtual void Start()
    {
        pivot = transform.parent.gameObject;
    }

    public override void Shoot()
    {
        if (currentCooldown > 0)
        {
            return;
        }
        currentCooldown = cooldown;
        GameObject obj = Instantiate(projectilePrefab, pivot.transform.position, pivot.transform.rotation);
        obj.GetComponentInChildren<IProjectile>().Fire(pivot.transform.up, projectileOffset);
    }

    public virtual void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public override void PointAt(Vector2 target)
    {
        float angle = Vector2.SignedAngle(Vector2.up, target - (Vector2)pivot.transform.position);
        pivot.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public override void ResetDirection()
    {
        pivot.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override WeaponType GetWeaponType()
    {
        return weaponType;
    }
}
