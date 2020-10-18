using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 curMoveVector = new Vector3();
    [SerializeField] private InventoryUI inventoryUI;
    public float speed = 3.2f;
    

    bool isBeingKnocked = false;
    bool isFacingRight = true;
    bool isAttacking = false;
    bool canDig = false;
    bool canHandIn = false;
    bool isDigging = false;
    bool invUIOn = false;
    float knockbackTime = 0.0f;
    float attackTime = 0.0f;
    Loot curItem;

    //Upgrades
    int digUpgrade = 1;
    Inventory inventory;
    public DigMinigame digGame;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        GameEvents.current.onEnemyAttack += Knockback;
        GameEvents.current.onDugUp += OnDugUp;
        inventory = new Inventory();
        inventoryUI.SetInventory(inventory);
        //inventory.AddLoot(new Loot { lootType = Loot.LootType.coin, amount = 100 });
        inventory.AddLoot(new Loot { lootType = Loot.LootType.necklace, amount = 1 });
        inventory.AddLoot(new Loot { lootType = Loot.LootType.bracelet, amount = 1 });
        inventory.AddLoot(new Loot { lootType = Loot.LootType.weddingRing, amount = 1 });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            invUIOn = !invUIOn;
            inventoryUI.gameObject.SetActive(invUIOn);
        }

        if (isFacingRight)
        {
            this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            this.transform.Find("Dig").transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else
        {
            this.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            this.transform.Find("Dig").transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }

        if (isBeingKnocked)
        {
            if (knockbackTime >= 0.5f)
            {
                knockbackTime = 0.0f;
                isBeingKnocked = false;
            }
            else
            {
                knockbackTime += Time.deltaTime;
                digGame.Stop();
                isDigging = false;
            }
        }
        else if(!isDigging)
        {
            curMoveVector = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                curMoveVector += new Vector3(0.0f, 1.0f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                curMoveVector += new Vector3(-1.0f, 0.0f);
                isFacingRight = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                curMoveVector += new Vector3(0.0f, -1.0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                curMoveVector += new Vector3(1.0f, 0.0f);
                isFacingRight = true;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isAttacking = true;
            }
            if (Input.GetKeyDown(KeyCode.E) && canDig)
            {
                isDigging = true;
                if (digGame != null)
                {
                    digGame.SetState(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.E) && canHandIn)
            {
                GameEvents.current.ItemHandIn();
            }

            if (isAttacking)
            {
                attackTime += Time.deltaTime;
                transform.GetChild(0).gameObject.SetActive(true);

                if (attackTime >= 0.1)
                {
                    isAttacking = false;
                    attackTime = 0.0f;
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }

        controller.Move(curMoveVector * speed * Time.deltaTime);
    }

    void Knockback()
    {
        isBeingKnocked = true;
    }

    public bool isKnocked()
    {
        return isBeingKnocked;
    }

    public void SetCanDig(bool canIDig)
    {
        canDig = canIDig; 
    }

    public void SetCanHandIn(bool handIn)
    {
        canHandIn = handIn;
    }

    public void SetIsDigging(bool digging)
    {
        isDigging = digging;
    }

    public int GetDigMultiplier()
    {
        return digUpgrade;
    }

    public void SetDigMultiplier(int mult)
    {
        digUpgrade = mult;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public void SetCurItem(Loot loot)
    {
        curItem = loot;
    }

    public Loot GetCurItem()
    {
        return curItem;
    }

    private void OnDugUp()
    {
        //Will it spawn enemy? if yes return nothing
        float enemyChance = Random.Range(0.0f, 100.0f);
        if (enemyChance <= 50.0f)
        {
            //spawn enemy
            GameEvents.current.EnemySpawn();
        }
        else
        {
            Inventory inv = inventory;
            if (!inv.IsLootInInventory(Loot.GiveQuestLoot(QuestManager.GetQuest().GetQuestLoot())))
            {
                Loot tempLoot = Loot.GiveQuestLoot(QuestManager.GetQuest().GetQuestLoot());
                inv.AddLoot(tempLoot);
            }
            else
            {
                Loot tempLoot = Loot.GenerateLoot();
                inv.AddLoot(tempLoot);
            }
            GameEvents.current.ColdLoot();
        }
    }
}
