using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redSwanScript : MonoBehaviour
{

    public Rigidbody2D rigidBody;
    public Rigidbody2D targetObj;

    public SpriteRenderer swanRender;

    public Sprite swanIdle;
    public Sprite swanLaunch;
    double timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        swanRender = gameObject.GetComponent<SpriteRenderer>();
    }

    void animateSwan()
    {
        timer += Time.deltaTime;
        // swap between idle and launch sprite every 3 seconds        
        if (timer > 3f)
        {
            if (swanRender.sprite == swanIdle)
            {
                swanRender.sprite = swanLaunch;
            }
            else if (swanRender.sprite == swanLaunch)
            {
                swanRender.sprite = swanIdle;
            }
            timer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        IProjectile projectile = other.gameObject.GetComponentInChildren<IProjectile>();
        if (projectile != null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        animateSwan();
        // slowly move swan towards target
        transform.position = Vector2.MoveTowards(transform.position, targetObj.position, Time.deltaTime);
    }
}