using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    List<GameObject> lootPool = new List<GameObject>();
    public int maxConcurrentLoot = 100;
    public float minWidth = -10.0f;
    public float maxWidth = 58.0f;
    public float maxHeight = 4.6f;
    public float minHeight = -4.2f;

    public GameObject scrapsPrefab;
    public GameObject coinPrefab;
    public GameObject weddingRingPrefab;
    public GameObject cellPhonePrefab;
    public GameObject sunGlassesPrefab;
    public GameObject necklacePrefab;
    public GameObject braceletPrefab;

    public GameObject digSite;

    public GameObject detector;


    void Start()
    {
        GameEvents.current.onDugUp += DeleteDug;
    }

    // Update is called once per frame
    void Update()
    {
        if (lootPool.Count < maxConcurrentLoot)
        {
            if (QuestManager.GetQuest().IsSpawned())
            {
                AddToPool(RandomizePosition(GetRandomLoot())); // we do it this way to avoid making unesessary empty gameobjects
                //Debug.Log(lootPool[lootPool.Count - 1].transform.position.ToString());
            }
            else
            {
                AddToPool(RandomizePosition(GetRandomLoot()));
            }
        }
    }

    private GameObject GetRandomLoot()
    {
        //float chance = Random.Range(0.0f, 100.0f);

        return Instantiate(digSite);

        //// ----LOOT TABLE----
        //// Change spawn rates here
        //if (chance <= 5.0f) // coin
        //{
        //    return Instantiate(coinPrefab);
        //}
        //else if (chance <= 15.0f) // ???
        //{
        //    return Instantiate(braceletPrefab);
        //}
        //else // scraps
        //{
        //    return Instantiate(scrapsPrefab);
        //}
    }

    private GameObject GetQuestLoot(Loot loot)
    {
        switch (loot.lootType)
        {
            default:
            case Loot.LootType.weddingRing:
                return Instantiate(weddingRingPrefab);
            case Loot.LootType.cellPhone:
                return Instantiate(cellPhonePrefab);
            case Loot.LootType.sunGlasses:
                return Instantiate(sunGlassesPrefab);
            case Loot.LootType.necklace:
                return Instantiate(necklacePrefab);
            case Loot.LootType.bracelet:
                return Instantiate(braceletPrefab);
        }
    }

    private GameObject RandomizePosition(GameObject loot)
    {
        Vector3 randPos = new Vector3();
        randPos.x = Random.Range(minWidth, maxWidth);
        randPos.y = Random.Range(minHeight, maxHeight);
        loot.transform.position = randPos;
        return loot;
    }

    private void AddToPool(GameObject obj)
    {
        lootPool.Add(obj);
        lootPool[lootPool.Count - 1].transform.parent = this.transform; // set object as child of LootPool
    }

    public void AddQuestLootToPool(GameObject obj)
    {
        if (obj.tag != "Loot") // makes sure we aren't adding non-loot objects
        {
            Debug.LogError("Untagged loot object.", obj);
            return;
        }
        else if (!obj.GetComponent<Loot>()) // same as above, object must have Loot.cs script
        {
            Debug.LogError("Improper loot object.", obj);
            return;
        }
        else if (obj.GetComponent<Loot>().GetLootID() < 0) // cant add quest item that is not tracked
        {
            
            Debug.LogError("Quest loot item MUST be tracked.", obj);
            return;
        }
        else
        {
            AddToPool(RandomizePosition(obj));
        }
    }

    public bool DetectorStatusOn()
    {
        string status = detector.GetComponent<DetectorBehaviour>().GetStatus();
        
        if (status == "on")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DeleteDug()
    {
        List<GameObject> tempList = new List<GameObject>();

        foreach (GameObject loot in lootPool)
        {
            if (loot.GetComponent<Loot>().amIHot())
            {
                tempList.Add(loot);
            }
        }

        foreach(GameObject loot in tempList)
        {
            lootPool.Remove(loot);
            loot.GetComponent<Loot>().DestroySelf();
        }
    }
}
