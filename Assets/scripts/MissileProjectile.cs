using UnityEngine;

public class MissileProjectile : BaseProjectile
{
    private float angle;
    public int damage;
    public override void Fire(Vector2 direction, float offset)
    {
        transform.position += (Vector3) direction * offset;
        angle = Vector2.SignedAngle(Vector2.up, direction);
    }

    public override int GetDamage()
    {
        return damage;
    }

    void Start()
    {
        Destroy(gameObject, 5);
    }

    public void Update()
    {
        Swan closestSwan = null;
        float closestDistance = float.MaxValue;
        foreach (Swan swan in FindObjectsOfType<Swan>())
        {
            float distance = Vector2.Distance(transform.position, swan.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSwan = swan;
            }
        }

        if (closestSwan != null)
        {
            float angleToSwan = Vector2.SignedAngle(Vector2.up,
                (Vector2)closestSwan.transform.position - (Vector2)transform.position);
            angle = Mathf.MoveTowardsAngle(angle, angleToSwan, 360 * Time.deltaTime * 0.3f);
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position += transform.up * Time.deltaTime * 5;
        
    }
}
