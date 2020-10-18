using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorBehaviour : MonoBehaviour
{
    public Sprite on;
    public Sprite off;
    public float blinkDelay = 0.25f;

    public GameObject battery33;
    public GameObject battery23;
    public GameObject battery13;

    private int batteryLevel = 3;
    private float batteryDepletionSpeed = 30.0f;
    private float batteryDepletionTimer = 0.0f;

    private float blinkTimer = 0.0f;
    private string status = "off";
    private bool recentStatusChange = false;
    static private bool deadBattery = false;

    static public bool IsBatteryDead()
    {
        return deadBattery;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onColdLoot += SetStatusOff;
        GameEvents.current.onWarmLoot += SetStatusBlinking;
        GameEvents.current.onHotLoot += SetStatusOn;

        batteryDepletionTimer = batteryDepletionSpeed;
        UpdateBatterySprite();
        deadBattery = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (batteryLevel > 0)
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

            batteryDepletionTimer -= Time.deltaTime;
            if (batteryDepletionTimer <= 0.0f)
            {
                batteryLevel -= 1;
                batteryDepletionTimer = batteryDepletionSpeed;
                UpdateBatterySprite();
                //Debug.Log(batteryLevel);
            }
        }
        
        else
        {
            deadBattery = true;
            GetComponent<SpriteRenderer>().sprite = off;
        }
    }

    public void UpgradeRangeMultiplier(float newMultiplier)
    {
        SphereCollider[] detectorRanges = GetComponentsInChildren<SphereCollider>();
        Debug.Log(detectorRanges.Length);
        foreach (SphereCollider collider in detectorRanges)
        {
            collider.radius = collider.radius * newMultiplier;
        }
    }

    private void UpdateBatterySprite()
    {
        if (batteryLevel == 3)
        {
            battery33.SetActive(true);
            battery23.SetActive(false);
            battery13.SetActive(false);
        }
        else if (batteryLevel == 2)
        {
            battery33.SetActive(false);
            battery23.SetActive(true);
            battery13.SetActive(false);
        }
        else if (batteryLevel == 1)
        {
            battery33.SetActive(false);
            battery23.SetActive(false);
            battery13.SetActive(true);
        }
        else if (batteryLevel == 0)
        {
            battery33.SetActive(false);
            battery23.SetActive(false);
            battery13.SetActive(false);
        }
    }

    public void SetBatteryDepletionSpeed(float speed)
    {
        batteryDepletionSpeed = speed;
        UpdateBatterySprite();
    }

    public float GetBatteryDepletionSpeed()
    {
        return batteryDepletionSpeed;
    }

    public void BatteryRefill()
    {
        batteryLevel = 3;
        UpdateBatterySprite();
    }

    private void SetStatusOff()
    {
        //Debug.Log("Cold");
        status = "off";
        this.transform.parent.GetComponent<movement>().SetCanDig(false);
        recentStatusChange = true;
    }

    private void SetStatusBlinking()
    {
        //Debug.Log("Warm");
        status = "blink";
        this.transform.parent.GetComponent<movement>().SetCanDig(false);
        recentStatusChange = true;
    }

    private void SetStatusOn()
    {
        //Debug.Log("Hot");
        status = "on";
        this.transform.parent.GetComponent<movement>().SetCanDig(true);
        recentStatusChange = true;
    }

    public string GetStatus()
    {
        return status;
    }
}
