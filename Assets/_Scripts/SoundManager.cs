using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip hit1, lowBattery, beep;
    static AudioSource audioSrc;

    void Start()
    {
        hit1 = Resources.Load<AudioClip>("Hit1");
        lowBattery = Resources.Load<AudioClip>("Low_Battery");
        beep = Resources.Load<AudioClip>("Detector_Beep");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public static void playSound(string sfx) {
        switch (sfx) {
            case "beep":
                audioSrc.PlayOneShot(beep);
                break;
            case "lowBattery":
                audioSrc.PlayOneShot(lowBattery);
                break;
            case "hit":
                audioSrc.PlayOneShot(hit1);
                break;
        }

    }
}
