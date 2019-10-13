using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizTargetScript : MonoBehaviour
{
    private Vector3 patrolPointA;
    private Vector3 patrolPointB;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float spd;
    private bool isMovingRight;
    private TargetManagerScript TMScript;
    // Start is called before the first frame update
    void Start()
    {
        isMovingRight = true;
        patrolPointA = transform.position;
        patrolPointB = transform.position;
        patrolPointA.x = patrolPointA.x + radius;
        patrolPointB.x = patrolPointB.x - radius;
        GameObject parent = GameObject.Find("Environment");
        GameObject tmScript = parent.transform.Find("TargetManager").gameObject;
        TMScript = tmScript.GetComponent<TargetManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        float distanceA = Math.Abs(patrolPointA.x - transform.position.x);
        float distanceB = Math.Abs(patrolPointB.x - transform.position.x);

        if (isMovingRight && distanceA > 1.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPointA, spd);
        } else if (!isMovingRight && distanceB > 1.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPointB, spd);
        } else 
        {
            isMovingRight = !isMovingRight;
        }
    }

    void OnParticleCollision(GameObject particle)
    {
        transform.parent = null;
        Destroy(transform.gameObject);
        print("message some manager");
        TMScript.upTargetNum();
    }
}
