using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private bool isMenuOn;
    private bool gameOver;

    void Start()
    {
        GameEventsScript.gameIsOver.AddListener(isGameOver);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !gameOver)
        {
            if(!isMenuOn)
            {
                showMenu();
            } else 
            {
                transform.Find("Retry").gameObject.SetActive(false);
                transform.Find("Quit").gameObject.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isMenuOn = !isMenuOn;
            }
            GameEventsScript.pauseGame.Invoke();
        }
    }

    private void isGameOver()
    {
        gameOver = true;
        showMenu();
    }

    private void showMenu()
    {
        transform.Find("Retry").gameObject.SetActive(true);
        transform.Find("Quit").gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isMenuOn = !isMenuOn;
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
