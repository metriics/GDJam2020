using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    List<GameObject> lootPool = new List<GameObject>();
    public int maxConcurrentLoot = 15;
    public float beachWidth = 20.0f;
    public float beachHeight = 10.0f;

    public GameObject scrapsPrefab;
    public GameObject coinPrefab;
    public GameObject braceletPrefab;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lootPool.Count < maxConcurrentLoot)
        {
            AddToPool(RandomizePosition(GetRandomLoot())); // we do it this way to avoid making unesessary empty gameobjects
            Debug.Log(lootPool[lootPool.Count - 1].transform.position.ToString());
        }
    }

    private GameObject GetRandomLoot()
    {
        float chance = Random.Range(0.0f, 100.0f);

        // ----LOOT TABLE----
        // Change spawn rates here
        if (chance <= 5.0f) // coin
        {
            return Instantiate(coinPrefab);
        }
        else if (chance <= 15.0f) // ???
        {
            return Instantiate(braceletPrefab);
        }
        else // scraps
        {
            return Instantiate(scrapsPrefab);
        }
    }

    private GameObject RandomizePosition(GameObject loot)
    {
        Vector3 randPos = new Vector3();
        randPos.x = Random.Range(-beachWidth, beachWidth);
        randPos.y = Random.Range(-beachHeight, beachHeight);
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
}
