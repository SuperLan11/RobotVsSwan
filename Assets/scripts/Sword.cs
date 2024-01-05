using UnityEngine;
public class Sword : Weapon
{
    private GameObject pivot;
    void Start()
    {
        pivot = transform.parent.gameObject;
    }

    public override void Shoot()
    {
        
    }

    public override void PointAt(Vector2 target)
    {
        float angle = Vector2.SignedAngle(Vector2.up, target - (Vector2)pivot.transform.position);
        pivot.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
