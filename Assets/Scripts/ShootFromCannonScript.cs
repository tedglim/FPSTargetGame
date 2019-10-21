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
    [SerializeField]
    private GameObject[] cannonLights;
    // public GameObject cannonLight00;
    // public GameObject cannonLight01;

    public ParticleSystem cannonParticleShooter;
    public ParticleSystem chargingParticle;
    public ParticleSystem chargedParticle;
    public ParticleSystem lineParticles;
    public ParticleSystem chargedCannonParticle;
    public ParticleSystem chargedEmission;
    public ParticleSystem muzzleFlash;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cannonTransform = cannonModel.transform;
        cannonLocalPos = cannonTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();

        } else if (Input.GetMouseButtonDown(1))
        {
            holdTimer = Time.time;
            activateCharge = true;
        }

        //HOLD CHARGE
        if (activateCharge && !charging)
        {
            if (Time.time - holdTimer > holdTime)
            {
                SoundManagerScript.PlaySound("charge");
                chargeTimer = Time.time;                
                charging = true;
                chargingParticle.Play();
                lineParticles.Play();

                cannonTransform.DOLocalMoveZ(cannonLocalPos.z - .22f, chargeTime);
                foreach (GameObject cannonLight in cannonLights)
                {
                    Light light = cannonLight.transform.GetComponent<Light>();
                    light.color = new UnityEngine.Color(1.0f, .49f, 0.0f, 1.0f);
                    light.range = 0.5f;
                    light.DOIntensity(50.0f, 1.0f);
                }
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

        //RELEASE
        if (Input.GetMouseButtonUp(1))
        {
            SoundManagerScript.StopSound();
            activateCharge = false;
            if (!charged)
            {
                cannonParticleShooter.Play();
                muzzleFlash.Play();
                SoundManagerScript.PlaySound("fire");
            } else if (charged) {
                chargedCannonParticle.Play();
                muzzleFlash.Play();
                SoundManagerScript.PlaySound("fire");
            }
                charging = false;
                charged = false;
                chargedParticle.transform.DOScale(0, .05f).OnComplete(()=>chargedParticle.Clear());
                chargedParticle.Stop();
                lineParticles.Stop();

                Sequence s = DOTween.Sequence();
                s.Append(cannonTransform.DOPunchPosition(new Vector3(0, 0, -punchStrength), punchDuration, punchVibrato, punchElasticity));
                foreach (GameObject cannonLight in cannonLights)
                {
                    Light light = cannonLight.transform.GetComponent<Light>();
                    s.Append(light.DOIntensity(0.0f, 1.0f));
                }            
                s.Join(cannonTransform.DOLocalMove(cannonLocalPos, punchDuration).SetDelay(punchDuration));
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        SoundManagerScript.PlaySound("fire");
        cannonTransform.DOComplete();
        cannonTransform.DOPunchPosition(new Vector3(0, 0, -punchStrength), punchDuration, punchVibrato, punchElasticity);
        cannonParticleShooter.Play();
    }
}
