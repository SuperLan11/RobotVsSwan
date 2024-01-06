using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjectile
{
    public Vector2 velocity = Vector2.zero;
    public float speed;
    public int damage;

    void Start()
    {
        Destroy(transform.root.gameObject, 5);
    }
    protected virtual void Update()
    {
        transform.position += (Vector3) velocity * Time.deltaTime * speed;
    }

    public override void Fire(Vector2 direction, float offset)
    {
        velocity = direction;
        transform.position += (Vector3) direction * offset;
    }

    public override int GetDamage()
    {
        return damage;
    }
}
