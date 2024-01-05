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
        Debug.Log("Started red swan!");
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

    // Update is called once per frame
    void Update()
    {
        animateSwan();
        // slowly move swan towards target
        transform.position = Vector2.MoveTowards(transform.position, targetObj.position, 0.01f);
    }
}