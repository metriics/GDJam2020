using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Canvas shopUI;

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
    }

    public void UpgradeDetector()
    {
        Debug.Log("detector upgrade");
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
