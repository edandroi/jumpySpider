using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalManager : MonoBehaviour
{
    public GameObject goal1;
    public GameObject goal2;

    public float speed = 2f;

    public int patternNum;

    public UnityEvent newGoal_Event = new UnityEvent();
    void Start()
    {
        newGoal_Event.AddListener(InstantiateGoalObj);
    }

    // Update is called once per frame
    void Update()
    {
    }

    GameObject thisTarget = new GameObject();
    void InstantiateGoalObj()
    {
        Vector2 instantiatePos = Camera.main.ScreenToWorldPoint(new Vector3(0,-Screen.height));

        switch (patternNum)
        {
            case 0: 
                thisTarget = Instantiate(goal1, new Vector2(0, instantiatePos.y * .4f ), Quaternion.identity);
                break;
            case 1:
                thisTarget = Instantiate(goal2, new Vector2(0, instantiatePos.y *.4f), Quaternion.identity);
                break;
        }
    }

}
