using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TargetManagerScript : MonoBehaviour
{
    [SerializeField]
    private Transform[] xTargetLocs;
    [SerializeField]
    private Transform[] yTargetLocs;
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
    public Transform targetContainer;
    [SerializeField]
    private GameObject xTargetObj;
    [SerializeField]
    private GameObject yTargetObj;
    [SerializeField]
    private GameObject zTargetObj;
    private int totalTargets;
    private int currTargets;
    [SerializeField]
    private float currentTime = 0.0f;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text targetsRemainingText;
    private int[] targetSectionCount = new int[4];
    private WallSectionManagerScript WSScript;
    public bool isGameOver;
    [SerializeField]
    private GameObject menu;


    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        GameObject parent = GameObject.Find("Environment");
        GameObject wsScript = parent.transform.Find("WallSectionManager").gameObject;
        WSScript = wsScript.GetComponent<WallSectionManagerScript>();

        targetSectionCount.SetValue(section01TargetLocs.Length, 0);
        targetSectionCount.SetValue(section02TargetLocs.Length, 1);
        targetSectionCount.SetValue(section03TargetLocs.Length, 2);
        targetSectionCount.SetValue(section04TargetLocs.Length, 3);
        
        makeTargets();
        totalTargets = targetContainer.childCount;
        upTargetNum();
        FormatTime(currentTime);
    }

    private void makeTargets()
    {
        foreach (Transform entry in xTargetLocs)
        {
            GameObject obj = Instantiate(xTargetObj, entry.position, Quaternion.identity);
            obj.transform.parent = targetContainer.transform;
        }
        
        foreach (Transform entry in yTargetLocs)
        {
            GameObject obj = Instantiate(yTargetObj, entry.position, Quaternion.identity);
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
        WSScript.removeObstacles(targetsDestroyed, targetSectionCount);
        targetsRemainingText.text = currTargets.ToString();
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
        if(!isGameOver)
        {
            upTimer();
        } else 
        {
            GameOver();
        }
    }

    private void upTimer()
    {
        currentTime += Time.deltaTime;
        timeText.text = FormatTime(currentTime);
        // if (timeLeft < 0)
        // {
        //     GameOver();
        // }
    }

    public void GameOver()
    {
        isGameOver = true;
        menu.SetActive(true);
        // SceneManager.LoadSceneAsync(0);
    }
}