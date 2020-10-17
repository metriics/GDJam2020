using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorBehaviour : MonoBehaviour
{
    public Sprite on;
    public Sprite off;
    public float blinkDelay = 0.25f;

    private float blinkTimer = 0.0f;
    private string status = "off";
    private bool recentStatusChange = false;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onColdLoot += SetStatusOff;
        GameEvents.current.onWarmLoot += SetStatusBlinking;
        GameEvents.current.onHotLoot += SetStatusOn;
    }

    // Update is called once per frame
    void Update()
    {
        if (recentStatusChange)
        {
            if (status == "off")
            {
                GetComponent<SpriteRenderer>().sprite = off;
            }
            else if (status == "blink")
            {
                GetComponent<SpriteRenderer>().sprite = on;
                blinkTimer = 0.0f;
            }
            else if (status == "on")
            {
                GetComponent<SpriteRenderer>().sprite = on;
            }

            recentStatusChange = false;
        }

        if (status == "blink")
        {
            blinkTimer += Time.deltaTime;
            if (blinkTimer < blinkDelay)
            {
                GetComponent<SpriteRenderer>().sprite = on;
            }
            else if (blinkTimer > blinkDelay && blinkTimer < blinkDelay * 2)
            {
                GetComponent<SpriteRenderer>().sprite = off;
            }
            else
            {
                blinkTimer = 0.0f;
            }
        }
    }

    private void SetStatusOff()
    {
        Debug.Log("Cold");
        status = "off";
        this.transform.parent.GetComponent<movement>().SetCanDig(false);
        recentStatusChange = true;
    }

    private void SetStatusBlinking()
    {
        Debug.Log("Warm");
        status = "blink";
        this.transform.parent.GetComponent<movement>().SetCanDig(false);
        recentStatusChange = true;
    }

    private void SetStatusOn()
    {
        Debug.Log("Hot");
        status = "on";
        this.transform.parent.GetComponent<movement>().SetCanDig(true);
        recentStatusChange = true;
    }
}
