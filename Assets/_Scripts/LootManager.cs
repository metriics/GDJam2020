using System.Collections;
using System.Collections.Generic;
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

    public void AddToPool(GameObject obj)
    {
        lootPool.Add(obj);
    }
}
