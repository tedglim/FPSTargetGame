using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level01ManagerScript : MonoBehaviour
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
    private GameObject Stage04;
    [SerializeField]
    private GameObject[] Stage04Targets;
    public string stage04TitleText;
    public string stage04SubtitleText;
    [SerializeField]
    private GameObject Stage05;
    [SerializeField]
    private GameObject[] Stage05Targets;
    public string stage05TitleText;
    public string stage05SubtitleText;
    [SerializeField]
    private GameObject Stage06;
    [SerializeField]
    private GameObject[] Stage06Targets;
    public string stage06TitleText;
    public string stage06SubtitleText;
    [SerializeField]
    private GameObject Stage07;
    [SerializeField]
    private GameObject[] Stage07Targets;
    public string stage07TitleText;
    public string stage07SubtitleText;
    [SerializeField]
    private Text stageTitle;
    [SerializeField]
    private Text stageSubtitle;
    [SerializeField]
    private GameObject startPlatform;
    [SerializeField]
    private Text timeText;
    private float currentTime;
    [SerializeField]
    private Text bestTimeText;
    [SerializeField]
    private Text bestEfficiencyText;
    [SerializeField]
    private Text currentTimeText;
    [SerializeField]
    private Text currentEfficiencyText;
    private int totalTargets;
    private int targetsDestroyed;
    [HideInInspector]
    public bool isGameOver;  
    [SerializeField]
    private GameObject menu;
    private int shotsTaken;
    private int shotsHit;
    private bool paused;
    private bool alreadyOver;


    void Start()
    {
        GameEventsScript.shotCannon.AddListener(CountShots);
        GameEventsScript.hitDummy.AddListener(CountShotsHit);
        GameEventsScript.pauseGame.AddListener(isPaused);
        totalTargets = Stage00Targets.Length + Stage01Targets.Length + Stage02Targets.Length + Stage03Targets.Length + Stage04Targets.Length + Stage05Targets.Length + Stage06Targets.Length + Stage07Targets.Length;
        targetsDestroyed = 0;
        currentTime = 0;
        shotsTaken = 0;
        shotsHit = 0;
    }

    void Update()
    {
        if(paused)
        {
            return;
        }
        if(!isGameOver)
        {
            upTimer();
        } else 
        {
            GameOver();
        }
    }

    private void isPaused()
    {
        paused = !paused;
    }

    private void upTimer()
    {
        currentTime += Time.deltaTime;
        timeText.text = FormatTime(currentTime);
    }

    private string FormatTime(float time)
    {
         int minutes = (int)time / 60 ;
         int seconds = (int)time - 60 * minutes;
         int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));
         return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds );
    }

    public void GameOver()
    {
        if(!alreadyOver)
        {
            SoundManagerScript.PlaySound("levelComplete");
            alreadyOver = true;
        }
        isGameOver = true;
        GameEventsScript.gameIsOver.Invoke();
        menu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        float hitPercent = (float)shotsHit / (float)shotsTaken;
        if(currentTime < PlayerPrefs.GetFloat("Best", Mathf.Infinity))
        {
            PlayerPrefs.SetFloat("Best", currentTime);
            PlayerPrefs.SetFloat("Efficiency", hitPercent);
        }
        bestTimeText.text = FormatTime(PlayerPrefs.GetFloat("Best"));
        bestEfficiencyText.text = string.Format("Value: {0:P2}.", PlayerPrefs.GetFloat("Efficiency"));
        currentTimeText.text = FormatTime(currentTime);
        currentEfficiencyText.text = string.Format("Value: {0:P2}.", hitPercent);
    }

    private void CountShots()
    {
        shotsTaken++;
    }

    private void CountShotsHit(DummyHitData data)
    {
        shotsHit++;
        if(data.changeLevel)
        {
            ChangeLevel();
        }
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
            StartCoroutine(TransitionEffects(Stage03, Stage04, stage04TitleText, stage04SubtitleText));
        } else if (targetsDestroyed == Stage00Targets.Length + Stage01Targets.Length + Stage02Targets.Length + Stage03Targets.Length + Stage04Targets.Length)
        {
            StartCoroutine(TransitionEffects(Stage04, Stage05, stage05TitleText, stage05SubtitleText));
        } else if (targetsDestroyed == Stage00Targets.Length + Stage01Targets.Length + Stage02Targets.Length + Stage03Targets.Length + Stage04Targets.Length + Stage05Targets.Length)
        {
            StartCoroutine(TransitionEffects(Stage05, Stage06, stage06TitleText, stage06SubtitleText));
        } else if (targetsDestroyed == Stage00Targets.Length + Stage01Targets.Length + Stage02Targets.Length + Stage03Targets.Length + Stage04Targets.Length + Stage05Targets.Length + Stage06Targets.Length)
        {
            StartCoroutine(TransitionEffects(Stage06, Stage07, stage07TitleText, stage07SubtitleText));
        } else if (targetsDestroyed == Stage00Targets.Length + Stage01Targets.Length + Stage02Targets.Length + Stage03Targets.Length + Stage04Targets.Length + Stage05Targets.Length + Stage06Targets.Length + Stage07Targets.Length)
        {
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
    }
}
