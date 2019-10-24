using System.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootFromCannonScript : MonoBehaviour
{
    private Transform cannonTransform;
    [SerializeField]
    private GameObject cannonModel;
    private Vector3 cannonLocalPos;
    [SerializeField]
    private ParticleSystem cannonParticleShooter;
    [SerializeField]
    private ParticleSystem muzzleFlash;
    [SerializeField]
    private ParticleSystem chargingParticle;
    [SerializeField]
    private ParticleSystem lineParticles;
    [SerializeField]
    private ParticleSystem chargedParticle;
    [SerializeField]
    private ParticleSystem chargedEmission;
    [SerializeField]
    private ParticleSystem chargedCannonParticle;
    [SerializeField]
    private GameObject[] cannonLights;

    private bool activateCharge;
    private bool charging;
    private bool charged;
    [SerializeField]
    private float holdTime = 0.5f;
    private float holdTimer;
    [SerializeField]
    private float chargeTime = 0.5f;
    private float chargeTimer;

    [SerializeField]
    private float punchStrength = .2f;
    [SerializeField]
    private int punchVibrato = 5;
    [SerializeField]
    private float punchDuration = .3f;
    [SerializeField][Range(0, 1)]
    public float punchElasticity = .5f;

    private bool gameOver;
    private bool paused;

    void Start()
    {
        GameEventsScript.gameIsOver.AddListener(isGameOver);
        GameEventsScript.pauseGame.AddListener(isPaused);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cannonTransform = cannonModel.transform;
        cannonLocalPos = cannonTransform.localPosition;
    }

    void Update()
    {
        if(paused || gameOver)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();

        //PREP CHARGE SHOT
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
                chargeTimer = Time.time;                
                charging = true;
                chargingParticle.Play();
                lineParticles.Play();
                SoundManagerScript.PlaySound("charge");
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
                muzzleFlash.Play();
                cannonParticleShooter.Play();
                SoundManagerScript.PlaySound("fire");                
                GameEventsScript.shotCannon.Invoke();                
            } else if (charged) {
                muzzleFlash.Play();
                chargedCannonParticle.Play();
                SoundManagerScript.PlaySound("fire");
                GameEventsScript.shotCannon.Invoke(); 
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
                s.Append(light.DOIntensity(0.0f, 0.25f));
            }            
            s.Join(cannonTransform.DOLocalMove(cannonLocalPos, punchDuration).SetDelay(punchDuration));
        }
    }

    private void isPaused()
    {
        paused = !paused;
    }

    private void isGameOver()
    {
        gameOver = true;
    }

    //NORMAL SHOT
    void Shoot()
    {
        muzzleFlash.Play();
        cannonTransform.DOComplete();
        cannonTransform.DOPunchPosition(new Vector3(0, 0, -punchStrength), punchDuration, punchVibrato, punchElasticity);
        cannonParticleShooter.Play();
        SoundManagerScript.PlaySound("fire");
        GameEventsScript.shotCannon.Invoke();
    }
}
