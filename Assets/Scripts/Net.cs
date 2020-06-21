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

    private PolygonCollider2D col;
     
     
    void Awake()
    {
        thisCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        m_Manager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        col = GetComponent<PolygonCollider2D>();
        
        // Add listener to the event
        jump_Event.AddListener(UpdateLine);
        landing_Event.AddListener(RemoveLine);
    }

    private Vector3 playerPos;

    void RemoveLine()
    {
        StartCoroutine(RemoveNet(1));

        /*
        linePoints.Clear();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0,playerPos);
        lastPos = playerPos;
        */
    }

    System.Collections.IEnumerator RemoveNet(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        linePoints.Clear();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0,playerPos);
//        lastPos = playerPos;
    }

    private List<Vector2> colPoints;
    void UpdateLine()
    {
        playerPos = Player.transform.position;

        startWidth = .1f;
        endWidth = .2f;
        
//        Debug.Log("las pos is "+ lastPos);
        float dist = Vector3.Distance(lastPos, playerPos);
        if(dist <= threshold)
            return;
         
        lastPos = playerPos;

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
}
