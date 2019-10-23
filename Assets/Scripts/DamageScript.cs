using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    [SerializeField]
    private int normalDamage;
    [SerializeField]
    private int chargeDamage;
    private DummyScript script;

    void Start()
    {
        script = gameObject.GetComponentInParent<DummyScript>();
    }

    void OnParticleCollision(GameObject particle)
    {
        if (particle.name == "NormalBeamParticle")
        {
            script.takeDamage(normalDamage);
        } else if (particle.name == "ChargedBeamParticle")
        {
            script.takeDamage(chargeDamage);
        }
    }
}
