using System.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootFromCannonScript : MonoBehaviour
{
    private Vector3 cannonLocalPos;
    public GameObject cannonModel;
    private Transform cannonTransform;
    public GameObject cannonLight;

    public ParticleSystem cannonParticleShooter;
    public ParticleSystem chargingParticle;
    public ParticleSystem chargedParticle;
    public ParticleSystem lineParticles;
    public ParticleSystem chargedCannonParticle;
    public ParticleSystem chargedEmission;
    // public ParticleSystem muzzleFlash;

    public bool activateCharge;
    public bool charging;
    public bool charged;
    public float holdTime = 0.5f;
    public float chargeTime = 0.5f;

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
        cannonLocalPos = cannonTransform.localPosition;

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

                Sequence s = DOTween.Sequence();
                s.Append(cannonTransform.DOPunchPosition(new Vector3(0, 0, -punchStrength), punchDuration, punchVibrato, punchElasticity));
                Light light = cannonLight.transform.GetComponent<Light>();
                s.Append(light.DOIntensity(0.0f, 1.0f));
                // s.Join(cannonModel.GetComponentInChildren<Renderer>().material.DOColor(normalEmissionColor, "_EmissionColor", punchDuration));
                s.Join(cannonTransform.DOLocalMove(cannonLocalPos, punchDuration).SetDelay(punchDuration));
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

                cannonTransform.DOLocalMoveZ(cannonLocalPos.z - .22f, chargeTime);
                Light light = cannonLight.transform.GetComponent<Light>();
                light.color = new UnityEngine.Color(1.0f, .49f, 0.0f, 1.0f);
                light.range = 1.0f;
                light.DOIntensity(100.0f, 1.0f);
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
                chargedEmission.Play();
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
