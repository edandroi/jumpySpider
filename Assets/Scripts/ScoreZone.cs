using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreZone : MonoBehaviour
{
    private Collider2D col;

    public Transform groundRight;
    public Transform groundLeft;
    
    public UnityEvent leftAngle_Event = new UnityEvent();
    public UnityEvent rightAngle_Event = new UnityEvent();
    void Start()
    {
        col = GetComponent<Collider2D>();
        
        leftAngle_Event.AddListener(AngleForLeftJump);
        rightAngle_Event.AddListener(AngleForRightJump);
    }
    
    void Update()
    {
        
    }

    private float currentDir;
    void AngleForLeftJump()
    {
        Debug.Log("left angle called");
        Vector2 direction = groundLeft.position - groundRight.position;
        float angle = Vector3.Angle(direction, transform.forward);
        transform.eulerAngles = new Vector3(0,0,angle);
    }
    
    void AngleForRightJump()
    {
        Vector2 direction = groundLeft.position - groundRight.position;
        float angle = Vector3.Angle(direction, transform.forward);
        transform.eulerAngles = new Vector3(0,0,angle);
    }
}
