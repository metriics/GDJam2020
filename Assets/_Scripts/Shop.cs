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
            if (inventory.GetComponent<InventoryUI>().CanSpend(5))
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
            if (inventory.GetComponent<InventoryUI>().CanSpend(10))
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
            if (inventory.GetComponent<InventoryUI>().CanSpend(5))
            {
                detector.GetComponent<DetectorBehaviour>().UpgradeRangeMultiplier(1.5f);
                detectorRangeLevel = 2;
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        else if (detectorRangeLevel == 2)
        {
            if (inventory.GetComponent<InventoryUI>().CanSpend(10))
            {
                detector.GetComponent<DetectorBehaviour>().UpgradeRangeMultiplier(1.25f);
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
    }

    public void UpgradeBattery()
    {
        Debug.Log("battery upgrade");
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
