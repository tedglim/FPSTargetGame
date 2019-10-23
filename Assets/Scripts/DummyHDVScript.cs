using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHDVScript : DummyScript
{
    [SerializeField]
    private float lowSpeed;
    [SerializeField]
    private float highSpeed;
    private float currSpeed;
    private int listIndex;
    

    // Update is called once per frame
    void Update()
    {
        HMove();
    }

    protected void HMove()
    {
        if (isStart)
        {
            listIndex = UnityEngine.Random.Range(0, patrolPoints.Count);
            currSpeed = UnityEngine.Random.Range(lowSpeed, highSpeed);
            currDestination = patrolPoints[listIndex].position;
            isStart = false;
        }
        transform.position = Vector3.MoveTowards(transform.position, currDestination, currSpeed);
        if (Vector3.Distance(transform.position, currDestination) < 0.5f)
        {
            currDestination = patrolPoints[UnityEngine.Random.Range(0, patrolPoints.Count)].position;
            currSpeed = UnityEngine.Random.Range(lowSpeed, highSpeed);
        }
    }
}
