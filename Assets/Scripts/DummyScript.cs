using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DummyScript : MonoBehaviour
{
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private Image hp;
    [SerializeField]
    private int maxHealth;
    private int currHealth;

    public List<Transform> patrolPoints;

    [HideInInspector]
    public bool isStart;
    [HideInInspector]
    public Vector3 currDestination;
    
    public void Start()
    {
        currHealth = maxHealth;
        isStart = true;

        hp.enabled = false;
        hpBar.enabled = false;
    }

    public void takeDamage(int damage)
    {
        currHealth = currHealth - damage;
        if (currHealth > 0)
        {
            GameEventsScript.hitDummy.Invoke(new DummyHitData(false));
            if (damage == 1 || damage == 3){
                SoundManagerScript.PlaySound("targetHit00");
            } else if (damage == 4 || damage == 8)
            {
                SoundManagerScript.PlaySound("targetHit01");
            }
            hp.enabled = true;
            hpBar.enabled = true;
            hp.fillAmount = (float)currHealth / (float)maxHealth;
        } else 
        {
            GameEventsScript.hitDummy.Invoke(new DummyHitData(true));
            Destroy(transform.gameObject);
        }
    }
}
