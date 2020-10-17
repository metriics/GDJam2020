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
        Debug.Log(other.tag);
        // check collider tag to ensure we only call this when detector collider triggers it
        GameEvents.current.WarmLoot();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        // check collider tag to ensure we only call this when detector collider triggers it
        GameEvents.current.ColdLoot();
    }
}
