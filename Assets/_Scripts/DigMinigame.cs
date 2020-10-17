using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigMinigame : MonoBehaviour
{
    public GameObject player;
    public int decreaseMultiplier = 5;
    static bool active = false;
    char whichLetterNext = 'Q';
    static float progress = 0.0f;
    int multiplier = 5;
    int finished = 100;

    // Start is called before the first frame update
    void Start()
    {
        multiplier = 5 * player.GetComponent<movement>().GetDigMultiplier();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {

            Transform digUI = player.transform.Find("Dig").GetComponent<Transform>();
            digUI.gameObject.SetActive(true);

            progress -= decreaseMultiplier * Time.deltaTime;
            if(progress <= 0.0f)
            {
                progress = 0.0f;
            }

            if (Input.GetKeyDown(KeyCode.Q) && whichLetterNext == 'Q')
            {
                TextMeshPro textUI = player.transform.Find("Dig").transform.Find("Text").GetComponent<TextMeshPro>();
                textUI.SetText("E");
                whichLetterNext = 'E';
                progress += multiplier;
            }
            if (Input.GetKeyDown(KeyCode.E) && whichLetterNext == 'E')
            {
                TextMeshPro textUI = player.transform.Find("Dig").transform.Find("Text").GetComponent<TextMeshPro>();
                textUI.SetText("Q");
                whichLetterNext = 'Q';
                progress += multiplier;
            }

            if (progress >= finished)
            {
                progress = 0;
                active = false;
                player.GetComponent<movement>().SetIsDigging(false);
                player.GetComponent<movement>().SetCanDig(false);
                digUI.gameObject.SetActive(false);
                GameEvents.current.DugUp();
            }
        }
    }

    public void SetState(bool activeState)
    {
        active = activeState;
    }

    public void Stop()
    {
        progress = 0;
        active = false;
    }

    static public float GetProgress()
    {
        return progress;
    }
}
