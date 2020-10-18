using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Canvas shopUI;
    public GameObject player;
    public GameObject inventory;
    public GameObject detector;

    private int detectorRangeLevel = 1;

    private float damageLvl1 = 1.0f;
    private float damageLvl2 = 1.25f;
    private float damageLvl3 = 1.5f;

    private float batteryLvl1 = 30.0f;
    private float batteryLvl2 = 35.0f;
    private float batteryLvl3 = 40.0f;

    void Start()
    {
        shopUI.gameObject.SetActive(false);
    }

   
    void Update()
    {
        
    }

    public void UpgradeShovel()
    {
        Debug.Log("shovel upgrade");

        int curLevel = player.gameObject.GetComponent<movement>().GetDigMultiplier();

        if (curLevel == 1)
        {
            if (player.GetComponent<movement>().GetInventory().CanSpend(5))
            {
                player.gameObject.GetComponent<movement>().SetDigMultiplier(2);
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        else if (curLevel == 2)
        {
            if (player.GetComponent<movement>().GetInventory().CanSpend(10))
            {
                player.gameObject.GetComponent<movement>().SetDigMultiplier(3);
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        else if (curLevel == 3)
        {
            // cant upgrade digging past 3
            Debug.Log("Can't upgrade past level 3");
        }
    }

    public void UpgradeDetector()
    {
        Debug.Log("detector upgrade");
        if (detectorRangeLevel == 1)
        {
            if (player.GetComponent<movement>().GetInventory().CanSpend(5))
            {
                detector.GetComponent<DetectorBehaviour>().UpgradeRangeMultiplier(1.25f);
                detectorRangeLevel = 2;
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        else if (detectorRangeLevel == 2)
        {
            if (player.GetComponent<movement>().GetInventory().CanSpend(10))
            {
                detector.GetComponent<DetectorBehaviour>().UpgradeRangeMultiplier(1.5f);
                detectorRangeLevel = 3;
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        else if (detectorRangeLevel == 3)
        {
            // cant upgrade range past 3
            Debug.Log("Can't upgrade past level 3");
        }
    }

    public void UpgradeDamage()
    {
        Debug.Log("damage upgrade");

        float curLevel = player.gameObject.GetComponent<movement>().GetDamageMultiplier();

        if (curLevel == damageLvl1)
        {
            if (player.GetComponent<movement>().GetInventory().CanSpend(5))
            {
                player.gameObject.GetComponent<movement>().SetDamageMultiplier(damageLvl2);
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        else if (curLevel == damageLvl2)
        {
            if (player.GetComponent<movement>().GetInventory().CanSpend(10))
            {
                player.gameObject.GetComponent<movement>().SetDamageMultiplier(damageLvl3);
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        else if (curLevel == damageLvl3)
        {
            // cant upgrade past 3
            Debug.Log("Can't upgrade past level 3");
        }
    }

    public void UpgradeBattery()
    {
        Debug.Log("battery upgrade");

        float curSpeed = detector.GetComponent<DetectorBehaviour>().GetBatteryDepletionSpeed();

        if (curSpeed == batteryLvl1)
        {
            if (player.GetComponent<movement>().GetInventory().CanSpend(5))
            {
                detector.GetComponent<DetectorBehaviour>().SetBatteryDepletionSpeed(batteryLvl2);
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        else if (curSpeed == batteryLvl2)
        {
            if (player.GetComponent<movement>().GetInventory().CanSpend(10))
            {
                detector.GetComponent<DetectorBehaviour>().SetBatteryDepletionSpeed(batteryLvl3);
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        else if (curSpeed == batteryLvl3)
        {
            // cant upgrade past 3
            Debug.Log("Can't upgrade past level 3");
        }

        detector.GetComponent<DetectorBehaviour>().BatteryRefill();
    }

    public void SellSheetMetal()
    {
        int set = player.GetComponent<movement>().GetInventory().CanSell() / 2;
        if (set > 0)
        {
            player.GetComponent<movement>().GetInventory().AddLoot(new Loot { lootType = Loot.LootType.coin, amount = set });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            shopUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shopUI.gameObject.SetActive(false);
        }
    }
}
