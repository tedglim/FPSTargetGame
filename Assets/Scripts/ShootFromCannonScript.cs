using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootFromCannonScript : MonoBehaviour
{
    public GameObject cannonModel;
    private Transform cannonTransform;

    public ParticleSystem cannonParticleShooter;
    public ParticleSystem chargingParticle;
    public ParticleSystem chargedParticle;
    public ParticleSystem lineParticles;
    public ParticleSystem chargedCannonParticle;
    // public ParticleSystem chargedEmission;
    // public ParticleSystem muzzleFlash;

    public bool activateCharge;
    public bool charging;
    public bool charged;
    public float holdTime = 1;
    public float chargeTime = .5f;

    private float holdTimer;
    private float chargeTimer;

    public float punchStrength = .2f;
    public int punchVibrato = 5;
    public float punchDuration = .3f;
    [Range(0, 1)]
    public float punchElasticity = .5f;

    // Start is called before the first frame update
    void Start()
    {
        cannonTransform = cannonModel.transform;  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();

            holdTimer = Time.time;
            activateCharge = true;
        }

        //RELEASE
        if (Input.GetMouseButtonUp(0))
        {
            activateCharge = false;

            if (charging)
            {
                chargedCannonParticle.Play();
                charging = false;
                charged = false;
                chargedParticle.transform.DOScale(0, .05f).OnComplete(()=>chargedParticle.Clear());
                lineParticles.Stop();
            }
        }

        //HOLD CHARGE
        if (activateCharge && !charging)
        {
            if (Time.time - holdTimer > holdTime)
            {
                charging = true;
                chargingParticle.Play();
                lineParticles.Play();
                chargeTimer = Time.time;

            }
        }

        //CHARGING
        if (charging && !charged)
        {
            if (Time.time - chargeTimer > chargeTime)
            {
                charged = true;
                chargedParticle.Play();
                chargedParticle.transform.localScale = Vector3.zero;
                chargedParticle.transform.DOScale(1, .4f).SetEase(Ease.OutBack);
            }
        }
    }

    void Shoot()
    {
        cannonTransform.DOComplete();
        cannonTransform.DOPunchPosition(new Vector3(0, 0, -punchStrength), punchDuration, punchVibrato, punchElasticity);

        cannonParticleShooter.Play();
    }
}
