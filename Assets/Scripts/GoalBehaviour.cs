using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehaviour : MonoBehaviour
{
    private GoalManager m_GoalManager;
    public float speedModifier = 1;
    float speed;
    void Start()
    {
        m_GoalManager = FindObjectOfType<GoalManager>();
        speed = m_GoalManager.speed * speedModifier;
    }
    
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
