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
    public Transform targetContainer;
    [SerializeField]
    private GameObject horizTargetObj;
    [SerializeField]
    private GameObject vertTargetObj;
    [SerializeField]
    private float timeLeft = 30.0f;
    [SerializeField]
    private Text timeText;
    private int targetNum;
    [SerializeField]
    private Text targetsRemainingText;
    // Start is called before the first frame update
    void Start()
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

        FormatTime(timeLeft);
        upTargetNum();
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

    public void upTargetNum()
    {
        print("Was called");
        targetNum = targetContainer.childCount;
        print(targetNum);
        targetsRemainingText.text = targetNum.ToString();
        if(targetNum == 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private string FormatTime(float time)
    {
         int minutes = (int) time / 60 ;
         int seconds = (int) time - 60 * minutes;
         int milliseconds = (int) (1000 * (time - minutes * 60 - seconds));
         return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds );
    }

}
