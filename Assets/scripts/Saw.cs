using UnityEngine;

public class Saw : ProjectileWeapon
{
    public Sprite emptySaw;
    private Sprite loadedSaw;
    private float shootOffset = 1.85f;
    private float originalOffset;
    public override void Start()
    {
        base.Start();
        loadedSaw = GetComponent<SpriteRenderer>().sprite;
        originalOffset = transform.localPosition.y;
    }
    public override void Update()
    {
        base.Update();
        GetComponent<SpriteRenderer>().sprite = currentCooldown > 0 ? emptySaw : loadedSaw;
        transform.localPosition = new Vector3(transform.localPosition.x, originalOffset - (currentCooldown > 0 ? shootOffset : 0), transform.localPosition.z);
    }
}
