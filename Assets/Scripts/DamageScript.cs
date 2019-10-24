using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    [SerializeField]
    private int normalDamage;
    [SerializeField]
    private int chargeDamage;
    private DummyScript dummyScript;

    void Start()
    {
        dummyScript = gameObject.GetComponentInParent<DummyScript>();
    }

    void OnParticleCollision(GameObject particle)
    {
        if (particle.name == "NormalBeamParticle")
        {
            dummyScript.takeDamage(normalDamage);
        } else if (particle.name == "ChargedBeamParticle")
        {
            dummyScript.takeDamage(chargeDamage);
        }
    }
}
