using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private int lootID = -1;

    public int GetLootID()
    {
        return lootID;
    }

    public void SetLootID(int id)
    {
        lootID = id;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "DetectorCollider")
        {
            GameEvents.current.WarmLoot();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DetectorCollider")
        {
            GameEvents.current.ColdLoot();
        }
    }
}
