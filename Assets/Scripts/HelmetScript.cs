using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetScript : MonoBehaviour
{
    [SerializeField]
    private Vector2 amount;
    [SerializeField]
    private float lerp;

    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, y * amount.y, lerp), Mathf.LerpAngle(transform.localEulerAngles.y, x * amount.x, lerp), 0);
        
    }
}
