using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platforms : MonoBehaviour
{
    public UnityEvent movePlatform_Event = new UnityEvent();
    public UnityEvent moveLeft_Event = new UnityEvent();
    public UnityEvent moveRight_Event = new UnityEvent();

    public float speed= .5f;

    public enum platform
    {
        left,
        right
    }

    public platform thisPlatform;

    void Start()
    {
       movePlatform_Event.AddListener(MoveGround);
       moveLeft_Event.AddListener(MoveLeftGround);
       moveRight_Event.AddListener(MoveRightGround);
    }

    
    void Update()
    {
        if (moveNow)
        {
            if (transform.position.y != targetPos.y*.3f)
            {
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, targetPos.y, speed), 0);
            }
        }
    }

    private bool moveNow = false;
    void MoveLeftGround()
    {
        // move if I am the left one
        if (thisPlatform == platform.left )
        {
            CalculateNewPos();
            moveNow = true;
        }
    }

    void MoveRightGround()
    {
        if (thisPlatform == platform.right )
        {
            CalculateNewPos();
            moveNow = true;
        }
    }

    void MoveGround()
    {
        // move if I am the left one
        if (thisPlatform == platform.left )
        {
            CalculateNewPos();
            moveNow = true;
        }
        else if (thisPlatform == platform.right )
        {
            CalculateNewPos();
            moveNow = true;
        }
    }

    private Vector3 targetPos;
    void CalculateNewPos()
    {
        Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height));
        targetPos = new Vector2(transform.position.x, Random.Range(-screenSize.y, screenSize.y)*.8f);
    }
}
