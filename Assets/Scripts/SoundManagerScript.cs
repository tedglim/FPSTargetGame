using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip fireSound, chargeSound, targetHitSound00, targetHitSound01;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        fireSound = Resources.Load<AudioClip>("fireSound");
        chargeSound = Resources.Load<AudioClip>("chargeSound");
        targetHitSound00 = Resources.Load<AudioClip>("targetSound00");
        targetHitSound01 = Resources.Load<AudioClip>("targetSound01");

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
