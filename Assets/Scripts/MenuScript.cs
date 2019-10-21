using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private bool isMenuOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(!isMenuOn)
            {
                // print("on");
                // transform.gameObject.SetActive(true);
                transform.Find("Retry").gameObject.SetActive(true);
                transform.Find("Quit").gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isMenuOn = !isMenuOn;
            } else 
            {
                // print("off");
                transform.Find("Retry").gameObject.SetActive(false);
                transform.Find("Quit").gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
                // transform.gameObject.SetActive(false);
                isMenuOn = !isMenuOn;
            }
        }
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
