using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TargetManagerScript : MonoBehaviour
{
    [SerializeField]
    private Transform[] horizTargetLocs;
    [SerializeField]
    private Transform[] vertTargetLocs;
    [SerializeField]
    private Transform[] zTargetLocs;
    [SerializeField]
    private Transform[] section01TargetLocs;
    [SerializeField]
    private Transform[] section02TargetLocs;
    [SerializeField]
    private Transform[] section03TargetLocs;
    [SerializeField]
    private Transform[] section04TargetLocs;
    [SerializeField]
    private GameObject[] invisWalls;    
    public Transform targetContainer;
    [SerializeField]
    private GameObject horizTargetObj;
    [SerializeField]
    private GameObject vertTargetObj;
    [SerializeField]
    private GameObject zTargetObj;
    private int totalTargets;
    private int currTargets;
    [SerializeField]
    private float timeLeft;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text targetsRemainingText;
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text commandText;


    // Start is called before the first frame update
    void Start()
    {
        makeTargets();
        upTargetNum();
        totalTargets = targetContainer.childCount;
        FormatTime(timeLeft);

    }

    private void makeTargets()
    {
        foreach (Transform entry in horizTargetLocs)
        {
            GameObject obj = Instantiate(horizTargetObj, entry.position, Quaternion.identity);
            obj.transform.parent = targetContainer.transform;
        }
        
        foreach (Transform entry in vertTargetLocs)
        {
            GameObject obj = Instantiate(vertTargetObj, entry.position, Quaternion.identity);
            obj.transform.parent = targetContainer.transform;
        }

        foreach (Transform entry in zTargetLocs)
        {
            GameObject obj = Instantiate(zTargetObj, entry.position, Quaternion.identity);
            obj.transform.parent = targetContainer.transform;
        }
    }

    public void upTargetNum()
    {
        currTargets = targetContainer.childCount;
        int targetsDestroyed = totalTargets - currTargets;
        if (targetsDestroyed >= (section01TargetLocs.Length + section02TargetLocs.Length))
        {
            invisWalls[1].SetActive(false);
        } else if (targetsDestroyed >= section01TargetLocs.Length)
        {
            //remove wallsection1 entrance
            //make invis wall go away
            //change text to be "Proceed"
            commandText.text = "Proceed";
            invisWalls[0].SetActive(false);
        }
        // print(targetNum);
        //if section 1 targets done
        //make wall disappear
        //change visor text
        //if section 2 targets done
        targetsRemainingText.text = currTargets.ToString();
        if(currTargets == 0)
        {
            GameOver();
        }
    }

    private string FormatTime(float time)
    {
         int minutes = (int) time / 60 ;
         int seconds = (int) time - 60 * minutes;
         int milliseconds = (int) (1000 * (time - minutes * 60 - seconds));
         return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds );
    }

    // Update is called once per frame
    void Update()
    {
        upTimer();

        // upTargetNum();
        //update timer
        //update remaining targets
        //isGameOver

    }

    private void upTimer()
    {
        timeLeft -= Time.deltaTime;
        timeText.text = FormatTime(timeLeft);
        if (timeLeft < 0)
        {
            GameOver();
        }
    }



    void GameOver()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
