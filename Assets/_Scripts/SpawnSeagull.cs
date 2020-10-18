using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSeagull : MonoBehaviour
{
    public static SpawnSeagull instance = null;
    public GameObject player;
    public GameObject seagullPrefab;
    public List<GameObject> sEnemyList;
    public float spawnRate = 10.0f; //Seconds

    float spawnTimer = 0.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnRate)
        {
            AddToList(SetPosition(GetRandomEnemy()));
            spawnTimer = 0.0f;
        }

    }

    private GameObject GetRandomEnemy()
    {
        float chance = Random.Range(0.0f, 100.0f);

        // ----LOOT TABLE----
        // Change spawn rates here
        if (chance <= 100.0f) // coin
        {
            return Instantiate(seagullPrefab);
        }
        else if (chance <= 15.0f) // ???
        {
        }
        else // scraps
        {
        }
        return Instantiate(seagullPrefab);
    }

    private GameObject SetPosition(GameObject sEnemy)
    {
        float randX = Random.Range(seagullPrefab.GetComponent<SeagullBehaviour>().min.x, seagullPrefab.GetComponent<SeagullBehaviour>().max.x);
        sEnemy.transform.position = new Vector3(randX, 7.6f, 0.0f);
        return sEnemy;
    }

    private void AddToList(GameObject sEnemy)
    {
        sEnemyList.Add(sEnemy);
        sEnemyList[sEnemyList.Count - 1].transform.parent = this.transform; // set object as child of LootPool
    }
}
