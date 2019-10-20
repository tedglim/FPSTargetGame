using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallSectionManagerScript : MonoBehaviour
{
    private TargetManagerScript TMScript;
    [SerializeField]
    private GameObject[] obstacles;
    [SerializeField]
    private Text commandText;
    public string command;
    public string lastCommand;
    // Start is called before the first frame update
    void Start()
    {
        GameObject parent = GameObject.Find("Environment");
        GameObject tmScript = parent.transform.Find("TargetManager").gameObject;
        TMScript = tmScript.GetComponent<TargetManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeObstacles(int targetsDestroyed, int[] targetSectionCount)
    {
        // print("targets destroyed" + targetsDestroyed);
        // print("target Section Count:" + targetSectionCount[0]);
        if (targetsDestroyed >= targetSectionCount[0] + targetSectionCount[1] + targetSectionCount[2] + targetSectionCount[3])
        {
            TMScript.GameOver();
        } else if (targetsDestroyed >= targetSectionCount[0] + targetSectionCount[1] + targetSectionCount[2])
        {
            obstacles[4].SetActive(false);
            obstacles[5].SetActive(false);
            commandText.text = lastCommand;
        } else if (targetsDestroyed >= targetSectionCount[0] + targetSectionCount[1])
        {
            obstacles[2].SetActive(false);
            obstacles[3].SetActive(false);
            commandText.text = command;
        } else if (targetsDestroyed >= targetSectionCount[0])
        {
            obstacles[0].SetActive(false);
            obstacles[1].SetActive(false);
            commandText.text = command;
        }
    }
}
