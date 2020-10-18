using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance = null;
    public GameObject player;
    static Quest curQuest;
    public SpriteRenderer UI;

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
    
    public static Quest GetQuest()
    {
        return curQuest;
    }

    // Start is called before the first frame update
    void Start()
    {
        curQuest = new Quest();
        GameEvents.current.onItemHandIn += HandIn;
    }

    // Update is called once per frame
    void Update()
    {
        UI.sprite = curQuest.GetQuestLoot().GetSprite();
    }

    private void HandIn()
    {
        if (player.GetComponent<movement>().GetInventory().IsLootInInventory(curQuest.GetQuestLoot()))
        {
            Inventory inv = player.GetComponent<movement>().GetInventory();
            inv.RemoveLoot(curQuest.GetQuestLoot());
            inv.AddLoot(curQuest.GetRewardLoot());
            curQuest.GenerateQuest();
        }
        else
        {
            Debug.Log("Do not have item");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.GetComponent<movement>().SetCanHandIn(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<movement>().SetCanHandIn(false);
        }
    }
}
