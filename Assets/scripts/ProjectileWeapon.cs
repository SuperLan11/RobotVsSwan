using UnityEngine;
public class ProjectileWeapon : Weapon
{
    private GameObject pivot;
    public GameObject projectilePrefab;
    public float cooldown;
    protected float currentCooldown = 0;
    public float projectileOffset;
    public WeaponType weaponType;
    private bool isRobot;

    public virtual void Start()
    {
        pivot = transform.parent.gameObject;
    }

    public override void Shoot(bool robot)
    {
        isRobot = robot;
        if (currentCooldown > 0)
        {
            return;
        }
        currentCooldown = cooldown;
        GameObject obj = Instantiate(projectilePrefab, pivot.transform.position, Quaternion.identity);
        obj.GetComponentInChildren<BaseProjectile>().Fire(pivot.transform.up, projectileOffset, robot);
    }

    public virtual void Update()
    {
        if (currentCooldown > 0)
        {
            float multiplier = isRobot ? (1f + (Robot.instance.cooldownPercentModifier / 100)) : 1f;
            currentCooldown -= Time.deltaTime * multiplier;
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
