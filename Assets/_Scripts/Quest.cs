using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    Loot questLoot;
    Loot questReward;
    bool complete = false;
    bool spawned = false;

    public Quest()
    {
        questLoot = Loot.GenerateQuestLoot();
        questReward = Loot.GenerateRewardLoot();
    }

    public bool IsComplete()
    {
        return complete;
    }

    public bool IsSpawned()
    {
        return spawned;
    }

    public void SetComplete(bool isIt)
    {
        complete = isIt;
    }

    public void SetSpawned(bool isIt)
    {
        spawned = isIt;
    }

    public Loot GetQuestLoot()
    {
        return questLoot;
    }
    public Loot GetRewardLoot()
    {
        return questReward;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateQuest()
    {
        questLoot = Loot.GenerateQuestLoot();
        questReward = Loot.GenerateRewardLoot();
    }
}
