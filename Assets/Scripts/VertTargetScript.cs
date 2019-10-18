using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertTargetScript : MonoBehaviour
{
    private bool isMovingUp;
    private Vector3 patrolPointA;
    private Vector3 patrolPointB;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float spd;
    private TargetManagerScript TMScript;

    [SerializeField]
    private Image hp;
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private int maxHealth;
    private int currHealth;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        hp.enabled = false;
        hpBar.enabled = false;

        int rand = UnityEngine.Random.Range(0, 2);
        if (rand == 0)
        {
            isMovingUp = true;
        } else {
            isMovingUp = false;
        }
        patrolPointA = transform.position;
        patrolPointB = transform.position;
        patrolPointA.y = patrolPointA.y + radius;
        patrolPointB.y = patrolPointB.y - radius;
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
        float distanceA = Math.Abs(patrolPointA.y - transform.position.y);
        float distanceB = Math.Abs(patrolPointB.y - transform.position.y);

        if (isMovingUp && distanceA > 1.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPointA, spd);
        } else if (!isMovingUp && distanceB > 1.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPointB, spd);
        } else 
        {
            isMovingUp = !isMovingUp;
        }
    }

    void OnParticleCollision(GameObject particle)
    {
        currHealth--;
        if (currHealth > 0)
        {
            if (particle.name == "ChargedBeamParticle")
            {
                CleanupTarget();
            }
            hp.enabled = true;
            hpBar.enabled = true;
            hp.fillAmount = (float)currHealth / (float)maxHealth;
            SoundManagerScript.PlaySound("targetHit00");
        } else {
            CleanupTarget();
        }
    }

    private void CleanupTarget()
    {
        SoundManagerScript.PlaySound("targetHit01");
        transform.parent = null;
        Destroy(transform.gameObject);
        print("message some manager");
        TMScript.upTargetNum();
    }
}
