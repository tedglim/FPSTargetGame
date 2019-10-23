using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage00ManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Stage00;
    [SerializeField]
    private GameObject[] Stage00Targets;
    [SerializeField]
    private GameObject Stage01;
    [SerializeField]
    private GameObject[] Stage01Targets;
    public string stage01TitleText;
    public string stage01SubtitleText;
    [SerializeField]
    private GameObject Stage02;
    [SerializeField]
    private GameObject[] Stage02Targets;
    public string stage02TitleText;
    public string stage02SubtitleText;
    [SerializeField]
    private GameObject Stage03;
    [SerializeField]
    private GameObject[] Stage03Targets;
    public string stage03TitleText;
    public string stage03SubtitleText;
    [SerializeField]
    private Text stageTitle;
    [SerializeField]
    private Text stageSubtitle;
    [SerializeField]
    private GameObject startPlatform;
    [SerializeField]
    private Text timeText;
    private float currentTime = 0.0f;
    [SerializeField]
    private Text bestTimeText;
    private int totalTargets;
    private int targetsDestroyed;
    [HideInInspector]
    public bool isGameOver;
    [SerializeField]
    private GameObject menu;
    private int shotsTaken;
    private int shotsHit;


    // Start is called before the first frame update
    void Start()
    {
        totalTargets = Stage00Targets.Length + Stage01Targets.Length + Stage02Targets.Length + Stage03Targets.Length;
        targetsDestroyed = 0;
        currentTime = 0;
        shotsTaken = 0;
        shotsHit = 0;
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameOver();
        }        
    }

    private void upTimer()
    {
        currentTime += Time.deltaTime;
        timeText.text = FormatTime(currentTime);
    }

    private string FormatTime(float time)
    {
         int minutes = (int) time / 60 ;
         int seconds = (int) time - 60 * minutes;
         int milliseconds = (int) (1000 * (time - minutes * 60 - seconds));
         return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds );
    }

    public void GameOver()
    {
        isGameOver = true;
        if(currentTime < PlayerPrefs.GetFloat("Best", Mathf.Infinity))
        {
            PlayerPrefs.SetFloat("Best", currentTime);
            //set efficiency with best time
        }
        bestTimeText.text = FormatTime(PlayerPrefs.GetFloat("Best"));
        Debug.Log("best time: " + bestTimeText.text);
        Debug.Log("shots hit" + shotsHit);
        Debug.Log("shots taken" + shotsTaken);
        //bestTimeText = FormatTime(PlayerPrefs.GetFloat())
        //show your time
        //show your efficiency

        //show best time
        //show efficiency with best time
        // isGameOver = true;
        // menu.SetActive(true);
        // SceneManager.LoadSceneAsync(0);
    }

    public void CountShots()
    {
        shotsTaken++;
    }

    public void CountShotsHit()
    {
        shotsHit++;
    }

    public void ChangeLevel()
    {
        targetsDestroyed++;
        if (targetsDestroyed == Stage00Targets.Length)
        {
            StartCoroutine(TransitionEffects(Stage00, Stage01, stage01TitleText, stage01SubtitleText));
        } else if (targetsDestroyed == Stage00Targets.Length + Stage01Targets.Length)
        {
            StartCoroutine(TransitionEffects(Stage01, Stage02, stage02TitleText, stage02SubtitleText));
        } else if (targetsDestroyed == Stage00Targets.Length + Stage01Targets.Length + Stage02Targets.Length)
        {
            StartCoroutine(TransitionEffects(Stage02, Stage03, stage03TitleText, stage03SubtitleText));
        } else if (targetsDestroyed == Stage00Targets.Length + Stage01Targets.Length + Stage02Targets.Length + Stage03Targets.Length)
        {
            print("end");
            GameOver();
        }
    }

    IEnumerator TransitionEffects(GameObject prevStage, GameObject nextStage, string title, string subtitle)
    {
        SoundManagerScript.PlaySound("sectionComplete");
        Renderer rend = startPlatform.GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.green);
        prevStage.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        nextStage.SetActive(true);
        rend.material.SetColor("_Color", Color.white);
        stageTitle.text = title;
        stageSubtitle.text = subtitle;
        //change section text
    }
}
