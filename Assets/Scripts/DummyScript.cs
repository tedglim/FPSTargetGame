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
    private Stage00ManagerScript stageManagerScript;
    
    // Start is called before the first frame update
    public void Start()
    {
        currHealth = maxHealth;
        isStart = true;

        hp.enabled = false;
        hpBar.enabled = false;

        GameObject stageObj = GameObject.Find("StageManager");
        stageManagerScript = stageObj.GetComponent<Stage00ManagerScript>();
    }

    public void takeDamage(int damage)
    {
        stageManagerScript.CountShotsHit();
        currHealth = currHealth - damage;
        if (currHealth > 0)
        {
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
            Destroy(transform.gameObject);
            stageManagerScript.ChangeLevel();
        }
    }
}
