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
    int timer = 0;

    // Start is called before the first frame update
    void Start()
    {        
        Debug.Log("Started red swan!");
        swanRender = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
        // move swan at angle

        timer += 1;
        // swap between idle and launch sprite every 3 seconds (placeholder code)
        if (timer > 180)
        {            
            if (swanRender.sprite == swanIdle)
            {
                swanRender.sprite = swanLaunch;
            }
            else if(swanRender.sprite == swanLaunch)
            {
                swanRender.sprite = swanIdle;
            }            
            timer = 0;
        }        

        // get position coordinates of this redSwan
        double currentX = rigidBody.position.x;
        double currentY = rigidBody.position.y;

        // get position coordinates of the boss
        double targetX = targetObj.position.x;
        double targetY = targetObj.position.y;

        // find the x and y distance to the boss
        double positionDiffX = currentX - targetX;
        double positionDiffY = currentY - targetY;        

        // confusing using down and left, make more readable
        // float cast might be bad practice
        // has minor issue of the axes moving at same speed

        // set x velocity of swan
        if(positionDiffX > 0)
        {
            // use Time.deltaTime to slow velocity based on framerate
            rigidBody.position += Vector2.left * Time.deltaTime;
        }
        else if(positionDiffX <= 0)
        {
            rigidBody.position += Vector2.right * Time.deltaTime;
        }

        // set y velocity of swan
        if (positionDiffY > 0)
        {
            rigidBody.position += Vector2.down * Time.deltaTime;
        }
        else if(positionDiffY <= 0)
        {
            rigidBody.position += Vector2.up * Time.deltaTime;
        }                       
    }
}
