using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallScript : MonoBehaviour
{
    void OnParticleCollision(GameObject particle)
    {
        Physics.IgnoreCollision(particle.GetComponent<Collider>(), transform.gameObject.GetComponent<Collider>());
    }
}
