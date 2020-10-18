using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public static SpawnEnemy instance = null;
    public GameObject player;
    static Vector3 spawnPos;
    public GameObject crabPrefab;
    public GameObject jellyPrefab;
    public List<GameObject> enemyList;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onEnemySpawn += _SpawnEnemy;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private GameObject GetRandomEnemy()
    {
        float chance = Random.Range(0.0f, 100.0f);

        // ----LOOT TABLE----
        // Change spawn rates here
        if (chance <= 50.0f)
        {
            return Instantiate(crabPrefab);
        }
        else
        {
            return Instantiate(jellyPrefab);
        }
    }

    private GameObject SetPosition(GameObject enemy)
    {
        enemy.transform.position = spawnPos;
        return enemy;
    }

    private void AddToList(GameObject enemy)
    {
        enemyList.Add(enemy);
        enemyList[enemyList.Count - 1].transform.parent = this.transform; // set object as child of LootPool
    }

    private void _SpawnEnemy()
    {
        AddToList(SetPosition(GetRandomEnemy())); // we do it this way to avoid making unesessary empty gameobjects
    }

    static public void SetPosition(Vector3 pos)
    {
        spawnPos = pos;
    }
}
