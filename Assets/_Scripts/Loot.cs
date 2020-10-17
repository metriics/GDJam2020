using System.Collections;
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
    bool isHot = false;

    private int lootID = -1;

    public int GetLootID()
    {
        return lootID;
    }

    public void SetLootID(int id)
    {
        lootID = id;
    }

    static public Loot GenerateLoot()
    {
        Loot loot;
        float chance = Random.Range(0.0f, 100.0f);

        if (chance <= 5.0f) // coin
        {
            loot = new Loot { lootType = Loot.LootType.coin, amount = 1 };
        }
        else if (chance <= 15.0f) // ???
        {
            loot = new Loot { lootType = Loot.LootType.bracelet, amount = 1 };
        }
        else // scraps
        {
            loot = new Loot { lootType = Loot.LootType.metalScraps, amount = 1 };
        }

        return loot;
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
        if (other.tag == "WarmCollider")
        {
            GameEvents.current.WarmLoot();
        }

        if (other.tag == "HotCollider")
        {
            GameEvents.current.HotLoot();
            isHot = true;
        }


        // TODO: replace this with digging action elsewhere
        if(other.tag == "Player")
        {
            Debug.Log("Updated curItem");
            other.gameObject.GetComponent<movement>().SetCurItem(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WarmCollider")
        {
            GameEvents.current.ColdLoot();
            isHot = false;
        }

        if (other.tag == "HotCollider")
        {
            GameEvents.current.WarmLoot();
            isHot = false;
        }
    }

    public bool amIHot()
    {
        return isHot;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
