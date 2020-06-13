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

    public float jumpForce;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        Debug.Log("jumpNow is " +jumpNow);
        Jump();
    }

    void Jump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (onGround)
            {
                jumpNow = true;
                Vector2 jumpSpeed = Vector2.up * jumpForce;
                rb.AddForce(jumpSpeed);
            }
        }

        if (jumpNow)
        {
            if (thisGround == grounds.groundLeft)
            {
                SwitchGrounds(groundRight.transform);
            }
            else if (thisGround == grounds.groundRight)
            {
                SwitchGrounds(groundLeft.transform);
            }
        }
    }

    void SwitchGrounds(Transform target)
    {
        if (transform.position != target.position)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed);
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

    /*
void CheckGround()
{
    ContactFilter2D mFilter = new ContactFilter2D();
    mFilter.SetLayerMask(8);
    Debug.Log(mFilter);
    if (Physics2D.IsTouching(col, mFilter))
    {
        onGround = true;
    }
    else
    {
        onGround = false;
    }
}
*/
}
