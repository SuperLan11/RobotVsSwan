using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjectile
{
    public Vector2 velocity = Vector2.zero;
    public float speed;
    public int damage;
    public string sound;

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
        AudioManager.instance.Play(sound);
        velocity = direction.normalized;
        transform.position += (Vector3) (direction).normalized * offset;
        //set rotation
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        transform.parent.rotation = Quaternion.Euler(0, 0, transform.parent.rotation.eulerAngles.z + angle);
    }

    public override int GetDamage()
    {
        return damage;
    }
}
