using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    public Vector2 velocity = Vector2.zero;
    public float speed;

    void Start()
    {
        Destroy(transform.root.gameObject, 5);
    }
    protected virtual void Update()
    {
        transform.position += (Vector3) velocity * Time.deltaTime * speed;
    }

    public void Fire(Vector2 direction, float offset)
    {
        velocity = direction;
        transform.position += (Vector3) direction * offset;
    }
}
