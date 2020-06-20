using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class Net : MonoBehaviour
{
    List<Vector3> linePoints = new List<Vector3>();
    LineRenderer lineRenderer;
    public float startWidth = 1.0f;
    public float endWidth = 1.0f;
    public float threshold = 0.001f;
    Camera thisCamera;
    int lineCount = 0;

    private bool drawNow = false;

    public UnityEvent jump_Event = new UnityEvent();
    public UnityEvent landing_Event = new UnityEvent();
    

    GameObject GameManager;

    private GameManager m_Manager;

    private GameObject Player;
 
    Vector3 lastPos = Vector3.one * float.MaxValue;
     
     
    void Awake()
    {
        thisCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        m_Manager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        // Add listener to the event
        jump_Event.AddListener(UpdateLine);
        landing_Event.AddListener(RemoveLine);
        
    }

    private Vector3 playerPos;

    void RemoveLine()
    {
        StartCoroutine(RemoveNet(2));

        linePoints.Clear();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0,playerPos);
        lastPos = playerPos;
    }

    System.Collections.IEnumerator RemoveNet(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    
    void UpdateLine()
    {
        playerPos = Player.transform.position;

        startWidth = .2f;
        endWidth = .4f;
        
//        Debug.Log("las pos is "+ lastPos);
        float dist = Vector3.Distance(lastPos, playerPos);
        if(dist <= threshold)
            return;
         
        lastPos = playerPos;
        
        Debug.Log(linePoints.Count);
        if (linePoints== null)
            Debug.Log("no line points now");
        
        if(linePoints == null)
            linePoints = new List<Vector3>();
        linePoints.Add(playerPos);
        lineRenderer.SetWidth(startWidth, endWidth);
        lineRenderer.SetVertexCount(linePoints.Count);
 
        for(int i = lineCount; i < linePoints.Count; i++)
        {
            lineRenderer.SetPosition(i, linePoints[i]);
        }
        lineCount = linePoints.Count;
    }
    
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        // We touch them only once
        if (other.gameObject.CompareTag("Dot1"))
        {
            if (!touchDot1)
            {
                drawNow = !drawNow;
                touchDot1 = true;
            }
        }

        if (other.gameObject.CompareTag("Dot2"))
        {
            if (!touchDot2)
            {
                drawNow = !drawNow;
                touchDot2 = true;
            }
        }

        if (drawNow)
        {
            if (other.gameObject.CompareTag("Blue"))
            {
                startWidth += 0.05f;
            }

            if (other.gameObject.CompareTag("Orange"))
            {
                endWidth += 0.005f;
            }
            if (other.gameObject.CompareTag("Yellow"))
            {
                endWidth -= 0.01f;
                startWidth -= 0.02f;
            }

            startWidth = Mathf.Clamp(startWidth,0.1f, 5f);
            endWidth = Mathf.Clamp(endWidth,0.1f, 5f);
            
        }
    }

    /*
    void ConnectDots()
    {
        if (touchDot1)
        {
                dot1 = true;
            m_Manager.currentGameState = global::GameManager.GameStates.drawing;
        }

        if (touchDot2)
        {
                dot2 = true;
            m_Manager.currentGameState = global::GameManager.GameStates.drawing;
        }

        if (dot1 && dot2)
        {
            m_Manager.currentGameState = global::GameManager.GameStates.end;
        }
    }
    */

}
