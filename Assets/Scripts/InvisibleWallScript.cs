using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject particle)
    {
        Physics.IgnoreCollision(particle.GetComponent<Collider>(), transform.gameObject.GetComponent<Collider>());
    }
}
