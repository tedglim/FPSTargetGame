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

    [SerializeField]
    private int normalDmg;
    [SerializeField]
    private int normalHeadshotDmg;
    [SerializeField]
    private int chargeDmg;
    [SerializeField]
    private int chargeHeadshotDmg;    

    public List<Transform> patrolPoints;

    [HideInInspector]
    public bool isStart;
    [HideInInspector]
    public Vector3 currDestination;
    [SerializeField]
    private Text hitType;
    private bool isBodyShot;
    [SerializeField]
    private string bodyHitText;
    [SerializeField]
    private string headHitText;

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
            if (damage == normalDmg || damage == chargeDmg){
                SoundManagerScript.PlaySound("targetHit00");
            } else if (damage == normalHeadshotDmg || damage == chargeHeadshotDmg)
            {
                SoundManagerScript.PlaySound("targetHit01");
            }
            if (damage == normalDmg || damage == chargeDmg){
                isBodyShot = true;
                StartCoroutine(showTextTemp(isBodyShot));
            } else if (damage == normalHeadshotDmg || damage == chargeHeadshotDmg)
            {
                isBodyShot = false;
                StartCoroutine(showTextTemp(isBodyShot));
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

    IEnumerator showTextTemp(bool isBodyShot)
    {
        if(isBodyShot)
        {
            hitType.text = bodyHitText;
        } else 
        {
            hitType.text = headHitText;
        }
        yield return new WaitForSeconds(.1f);
        hitType.text = "";
    }
}
