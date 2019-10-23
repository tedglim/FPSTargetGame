using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private bool isMenuOn = false;
    private Stage00ManagerScript stageManagerScript;

    void Start()
    {
        GameObject stageObj = GameObject.Find("StageManager");
        stageManagerScript = stageObj.GetComponent<Stage00ManagerScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !stageManagerScript.isGameOver)
        {
            if(!isMenuOn)
            {
                transform.Find("Retry").gameObject.SetActive(true);
                transform.Find("Quit").gameObject.SetActive(true);

                // transform.Find("Quit").gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isMenuOn = !isMenuOn;
            } else 
            {
                transform.Find("Retry").gameObject.SetActive(false);
                transform.Find("Quit").gameObject.SetActive(false);

                // transform.Find("Quit").gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isMenuOn = !isMenuOn;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
