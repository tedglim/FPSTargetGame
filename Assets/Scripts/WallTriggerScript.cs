using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallTriggerScript : MonoBehaviour
{
    private GameObject player;
    private Text commandText;
    private Text titleText;
    [SerializeField]
    private string command;
    [SerializeField]
    private string title;
    // private Text commandText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FPSController");
        GameObject obj = GameObject.Find("UICanvas");
        GameObject commandTextObj = obj.transform.Find("HorizBars/Command").gameObject;
        commandText = commandTextObj.GetComponent<Text>();
        GameObject titleTextObj = obj.transform.Find("HorizBars/Title").gameObject;
        titleText = titleTextObj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            SoundManagerScript.PlaySound("trigger");
            commandText.text = command;
            titleText.text = title;
            transform.gameObject.SetActive(false);
        }
    }
}
