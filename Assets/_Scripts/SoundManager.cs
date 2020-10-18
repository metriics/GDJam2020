using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip hit1, hit2, hit3, hit4, lowBattery, beep;
    static AudioSource audioSrc;

    void Start()
    {
        hit1 = Resources.Load<AudioClip>("Hit1");
        hit2 = Resources.Load<AudioClip>("Hit2");
        hit3 = Resources.Load<AudioClip>("Hit3");
        hit4 = Resources.Load<AudioClip>("Hit4");
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
                switch (Random.Range(1, 4)) {
                    case 1:
                        audioSrc.PlayOneShot(hit1);
                        break;
                    case 2:
                        audioSrc.PlayOneShot(hit2);
                        break;
                    case 3:
                        audioSrc.PlayOneShot(hit3);
                        break;
                    case 4:
                        audioSrc.PlayOneShot(hit4);
                        break;
                }
                break;
        }

    }
}
