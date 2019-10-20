using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorizTargetScript : MonoBehaviour
{
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private Image hp;
    [SerializeField]
    private int maxHealth;
    private int currHealth;
    [SerializeField]
    private float spd;
    [SerializeField]
    private float patrolInterval;
    private float nextTurn;
    private bool isMovingRight;
    private TargetManagerScript TMScript;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        hp.enabled = false;
        hpBar.enabled = false;
        nextTurn = patrolInterval/2;
        int rand = UnityEngine.Random.Range(0, 2);
        if (rand == 0)
        {
            isMovingRight = true;
        } else {
            isMovingRight = false;
        }
        GameObject parent = GameObject.Find("Environment");
        GameObject tmScript = parent.transform.Find("TargetManager").gameObject;
        TMScript = tmScript.GetComponent<TargetManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if(Time.time < nextTurn)
        {
            if(isMovingRight)
            {
                transform.Translate(Vector3.right * spd * Time.deltaTime, Space.World);
            } else
            {
                transform.Translate(Vector3.left * spd * Time.deltaTime, Space.World);
            }        
        } else 
        {
            nextTurn = patrolInterval + Time.time;
            isMovingRight = !isMovingRight;
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
        TMScript.upTargetNum();
    }
}
