﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public enum LootType
    {
        metalScraps,
        coin,
        weddingRing,
        cellPhone,
        sunGlasses,
        necklace,
        bracelet
    }

    public LootType lootType;
    public int amount;

    private int lootID = -1;

    public int GetLootID()
    {
        return lootID;
    }

    public void SetLootID(int id)
    {
        lootID = id;
    }

    public Sprite GetSprite()
    {
        switch (lootType)
        {
            default:
            case LootType.metalScraps:
                return LootAssets.Instance.metalScrapsSprite;
            case LootType.coin:
                return LootAssets.Instance.coinSprite;
            case LootType.weddingRing:
                return LootAssets.Instance.weddingRingSprite;
            case LootType.cellPhone:
                return LootAssets.Instance.cellPhoneSprite;
            case LootType.sunGlasses:
                return LootAssets.Instance.sunGlassesSprite;
            case LootType.necklace:
                return LootAssets.Instance.necklaceSprite;
            case LootType.bracelet: 
                return LootAssets.Instance.braceletSprite;
        }
    }

    public bool IsStackable()
    {
        switch (lootType)
        {
            default:
            case LootType.metalScraps:
                return true;
            case LootType.coin:
                return true;
            case LootType.weddingRing:
                return false;
            case LootType.cellPhone:
                return false;
            case LootType.sunGlasses:
                return false;
            case LootType.necklace:
                return false;
            case LootType.bracelet:
                return false;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "DetectorCollider")
        {
            GameEvents.current.WarmLoot();
            DetectorBehaviour detector = new DetectorBehaviour();
            detector.SetDigSite(other.gameObject.transform.position);
        }

        if(other.tag == "Player")
        {
            Debug.Log("Testing");
            Inventory inv = other.gameObject.GetComponent<movement>().GetInventory();
            inv.AddLoot(this);
            Debug.Log("Added: " + this.lootType);
            //DestroySelf();
            GameEvents.current.ColdLoot();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DetectorCollider")
        {
            GameEvents.current.ColdLoot();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
