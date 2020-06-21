using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platforms : MonoBehaviour
{
    public UnityEvent moveLeft_Event;
    public UnityEvent moveRight_Event;

    float speed;

    private ScoreZone ScoreZone;
    public enum platform
    {
        left,
        right
    }

    public platform thisPlatform;

    private GameManager m_Manager;
    void Start()
    {
        moveLeft_Event = new UnityEvent();
        moveRight_Event = new UnityEvent();
        moveLeft_Event.AddListener(MoveLeftGround);
        moveRight_Event.AddListener(MoveRightGround);

       m_Manager = FindObjectOfType<GameManager>();
       speed = m_Manager.groundSpeed;
       
       ScoreZone = FindObjectOfType<ScoreZone>();
    }

    
    void Update()
    {
        if (moveNow)
        {
            Debug.Log("update");
            if (transform.position.y != targetPos.y*.3f)
            {
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, targetPos.y, speed), 0);
            }
            else
            {
                Debug.Log("time to evoke");
                // hareket eden bosta kalan platform, yani orasi yeni hedef
                if (thisPlatform == platform.left)
                {
//                    Debug.Log("time to evoke");
                    ScoreZone.leftAngle_Event.Invoke();
                }
                if (thisPlatform == platform.right)
                {
                    ScoreZone.rightAngle_Event.Invoke();
                }

                moveNow = false;
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


    private Vector3 targetPos;
    void CalculateNewPos()
    {
        Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height));
        targetPos = new Vector2(transform.position.x, Random.Range(-screenSize.y, screenSize.y)*.8f);
    }
}
