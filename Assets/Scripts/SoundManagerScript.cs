using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip fireSound, chargeSound, targetHitSound00, targetHitSound01, sectionCompleteSound, triggerSound, levelWinSound;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        fireSound = Resources.Load<AudioClip>("fireSound");
        chargeSound = Resources.Load<AudioClip>("chargeSound");
        targetHitSound00 = Resources.Load<AudioClip>("targetSound00");
        targetHitSound01 = Resources.Load<AudioClip>("targetSound01");
        sectionCompleteSound = Resources.Load<AudioClip>("sectionCompleteSound");
        triggerSound = Resources.Load<AudioClip>("triggerSound");
        levelWinSound = Resources.Load<AudioClip>("levelWinSound");
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case null:
                break;
            case "fire":
                audioSource.PlayOneShot(fireSound);
                break;
            case "charge":
                audioSource.PlayOneShot(chargeSound);
                break;
            case "targetHit00":
                audioSource.PlayOneShot(targetHitSound00);
                break;
            case "targetHit01":
                audioSource.PlayOneShot(targetHitSound01);
                break;
            case "sectionComplete":
                audioSource.PlayOneShot(sectionCompleteSound);
                break;
            case "trigger":
                audioSource.PlayOneShot(triggerSound);
                break;
            case "levelComplete":
                audioSource.PlayOneShot(levelWinSound);
                break;
        }
    }

    public static void StopSound()
    {
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
