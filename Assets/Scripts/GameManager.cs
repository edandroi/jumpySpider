using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GoalManager m_GoalManager;

    public float newTargetTime = 5f;
    private float remainingTargetTime;
    
    void Start()
    {
        m_GoalManager = FindObjectOfType<GoalManager>();
        remainingTargetTime = newTargetTime;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTargetTime -= Time.deltaTime;

        if (remainingTargetTime <= 0)
        {
            InstantiateGoalObjs();
            remainingTargetTime = newTargetTime;
        }


    }

    void InstantiateGoalObjs()
    {
//        StartCoroutine(InstantiateGoalObjs(10f));
        m_GoalManager.patternNum = 0;
        m_GoalManager.newGoal_Event.Invoke();
    }
    
}
