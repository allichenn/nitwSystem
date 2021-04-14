using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // The player's maximum movement speed
    public float speed;

    // The player's current horizontal velocity
    float hVel;

    // The player's horizontal acceleration
    public float hAcc;

    // The horizontal direction the player is moving (-1 for left, 1 for right, 0 for nowhere)
    float moveDir;

    // Stores the player's jump input
    bool jump;

    // The player's maximum jump height
    public float jumpHeight;

    // Stores the player's insantaneous jump velocity
    float jumpVel;

    // Stores whether or not the player's feet are touching the floor
    bool onFloor;

    // The player's rigidbody and box collider (feet)
    Rigidbody2D myBody;
    BoxCollider2D myCollider;



    // Called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Called once per frame
    void Update()
    {  
        CheckKeys(); 
    }

    // Called at a fixed rate
    void FixedUpdate()
    {
        HandleMovement();
    }

    // Checks for keyboard input
    void CheckKeys()
    {
        if (Input.GetKey(KeyCode.D))
        {
            moveDir = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDir = -1;
        }
        else
        {
            moveDir = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    // Moves the player
    void HandleMovement()
    {
        // Changes the player's horizontal velocity based on the player's move direction (as determined by checkKeys)
        if (moveDir == 1)
        {
            if (hVel < speed){
                hVel += hAcc;
            }
        }
        else if (moveDir == -1)
        {
            if (hVel > (-1 * speed))
            {
                hVel -= hAcc;
            }
        }
        else
        {
            if ((.1 > hVel) && (-.1 < hVel))
            {
                hVel = 0;
            }
            else if (hVel > 0)
            {
                hVel -= hAcc;
            }
            else if (hVel < 0)
            {
                hVel += hAcc;
            }
        }

        // Changes the player's vertical velocity when they jump

        jumpVel = myBody.velocity.y;

        if (jump == true)
        {
            jumpVel += jumpHeight;
            jump = false;
        }

        // Updates the player's actual velocity
        myBody.velocity = new Vector3(hVel * speed, jumpVel, 0);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Floor")
        {
            onFloor = true;
        }
    }
}
