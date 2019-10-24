using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFPSController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float lerp;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, lerp);
    }
}
