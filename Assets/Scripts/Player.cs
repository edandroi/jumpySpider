using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    /*
     * check if the player is on ground
     * if on ground and tap, jump to the other ground
     * higher as you keep pressed
     * also slower?
     * check line renderer collision
     * jump only if you are on ground
     * */

    public static bool onGround = false;
    private bool jumpNow = false;

    private Rigidbody2D rb;
    private Collider2D col;

    public GameObject groundRight;
    public GameObject groundLeft;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        Jump();
    }

    private float defaultJump;
    void Jump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (onGround)
            {
//                Debug.Log("mouse down now");
                if (thisGround == grounds.groundLeft)
                {
                    // go right
                    float remainingDistance = Mathf.Abs(transform.position.x - groundRight.transform.position.x);
                    totalJumpTime = remainingDistance / jumpSpeed;
                    CalculateHeight(groundRight.transform, groundLeft.transform);
                }
                else if (thisGround == grounds.groundRight)
                {
                    //go left
                    float remainingDistance = Mathf.Abs(transform.position.x - groundLeft.transform.position.x);
                    totalJumpTime = remainingDistance / jumpSpeed;
                    CalculateHeight(groundLeft.transform, groundRight.transform);
                }
                jumpNow = true;
            }
        }

        if (jumpNow)
        {
            if (thisGround == grounds.groundLeft)
            {
                // go right
                SwitchGrounds(groundRight.transform);
            }
            else if (thisGround == grounds.groundRight)
            {
                //go left
                SwitchGrounds(groundLeft.transform);
            }
        }
    }
    public float heightModifier = 1;
    void CalculateHeight(Transform target, Transform ground)
    {
        float diff = Mathf.Abs(ground.position.x - target.position.x);
        jumpHeight = diff * heightModifier;
        Debug.Log(jumpHeight);
    }

    //sag x arti
    // sol x eksi
    
    float jumpHeight;
    public float jumpSpeed = .1f;
    private float totalJumpTime;

    void SwitchGrounds(Transform target)
    {
        float remainingDistance = Mathf.Abs(transform.position.x - target.position.x);
        float remainingTime = remainingDistance / jumpSpeed;

        transform.position = Vector3.Lerp(transform.position, target.position, jumpSpeed * Time.deltaTime);

        if (remainingTime > totalJumpTime / 2)
        {
            transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(transform.position.y, jumpHeight, jumpSpeed * Time.deltaTime), 0);
        }
        else if (remainingTime > 0)
        {
            transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(transform.position.y, target.position.y, jumpSpeed * Time.deltaTime), 0);
        }
    }

    enum grounds
    {
        groundLeft,
        groundRight
    }

    private grounds thisGround = grounds.groundLeft;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            jumpNow = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            onGround = true;
        }

        if (other.gameObject.name == "Ground Right")
        {
            thisGround = grounds.groundRight;
        }
        else if (other.gameObject.name == "Ground Left")
        {
            thisGround = grounds.groundLeft;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            onGround = false;
        }
    }
    
}

