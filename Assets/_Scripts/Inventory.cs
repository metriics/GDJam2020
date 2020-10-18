using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Loot> lootList;

    public Inventory()
    {
        lootList = new List<Loot>();

        //AddLoot(new Loot { lootType = Loot.LootType.metalScraps, amount = 25 });
        //AddLoot(new Loot { lootType = Loot.LootType.coin, amount = 100 });
        //AddLoot(new Loot { lootType = Loot.LootType.bracelet, amount = 1 });
    }


    public int GetNumCoins()
    {
        int coins = 0;
        foreach (Loot invLoot in lootList)
        {
            if (invLoot.lootType == Loot.LootType.coin)
            {
                coins++;
            }
        }
        return coins;
    }

    public bool CanSpend(int amount)
    {
        foreach (Loot invLoot in lootList)
        {
            if (invLoot.lootType == Loot.LootType.coin)
            {
                if (invLoot.amount >= amount) // if there is enough money, spend it
                {
                    invLoot.amount -= amount;
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsLootInInventory(Loot loot)
    {
        foreach (Loot invLoot in lootList)
        {
            if (invLoot.lootType == loot.lootType)
            {
                return true;
            }
        }

        return false;
    }

    public void AddLoot(Loot loot)
    {
        if (loot.IsStackable())
        {
            bool itemAlreadyInInventory = false;

            foreach(Loot invLoot in lootList)
            {
                if(invLoot.lootType == loot.lootType)
                {
                    invLoot.amount += loot.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                lootList.Add(loot);
            }
        }
        else
        {
            lootList.Add(loot);
        }
        //GameEvents.current.InvUpdate();
    }

    public void RemoveLoot(Loot loot)
    {
        Loot lootInInv = null;
        int removeRange = -1;
        int stopRange = -1;
        foreach (Loot invLoot in lootList)
        {
            removeRange++;
            if (invLoot.lootType == loot.lootType)
            {
                lootInInv = invLoot;
                stopRange = removeRange;
                invLoot.amount -= loot.amount;
            }
        }
        Debug.Log(lootInInv.lootType);
        if (lootInInv.amount <= 0)
        {
            lootList.RemoveRange(stopRange, 1);
        }
        //GameEvents.current.InvUpdate();
    }

    public List<Loot> GetLootList()
    {
        return lootList;
    }
}
