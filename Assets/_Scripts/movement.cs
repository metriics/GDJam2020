using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movement : MonoBehaviour
{
    private CharacterController controller;
    public Animator animator;
    private Vector3 curMoveVector = new Vector3();
    [SerializeField] private InventoryUI inventoryUI;
    public float speed = 3.2f;

    GameObject detector;
    bool isBeingKnocked = false;
    bool isFacingRight = true;
    bool isAttacking = false;
    bool canDig = false;
    bool canHandIn = false;
    bool isDigging = false;
    bool invUIOn = false;
    float knockbackTime = 0.0f;
    float pickupTime = 0.0f;
    bool pickup = false;
    float attackTime = 0.0f;
    float damageMultiplier = 1.0f;
    Loot curItem;

    //Upgrades
    int digUpgrade = 1;
    Inventory inventory;
    public DigMinigame digGame;

    private void Start()
    {
        detector = this.transform.GetChild(0).gameObject;
        controller = GetComponent<CharacterController>();
        GameEvents.current.onEnemyAttack += Knockback;
        GameEvents.current.onDugUp += OnDugUp;
        GameEvents.current.onEnemyKilled += enemyLoot;
        inventory = new Inventory();
        inventoryUI.SetInventory(inventory);
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
                animator.SetBool("Digging", false);
                detector.SetActive(true);
                isDigging = false;
            }
        }
        else if (pickup)
        {
            pickupTime += Time.deltaTime;
            if (pickupTime >= 1.0f)
            {
                animator.SetBool("Pickup", false);
                pickup = false;
                transform.GetChild(2).gameObject.SetActive(false);
                pickupTime = 0.0f;
                detector.SetActive(true);
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

            if (curMoveVector != new Vector3(0.0f, 0.0f, 0.0f))
            {
                animator.SetFloat("Speed", 1.0f);
            }
            else
            {
                animator.SetFloat("Speed", 0.0f);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isAttacking = true;
            }
            if (Input.GetKeyDown(KeyCode.E) && canDig)
            {
                isDigging = true;
                animator.SetBool("Digging", true);
                detector.SetActive(false);
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
                animator.SetBool("Attacking", true);
                detector.SetActive(false);
                attackTime += Time.deltaTime;
                transform.GetChild(1).gameObject.SetActive(true);
                SoundManager.playSound("hit");

                if (attackTime >= 0.5f)
                {
                    animator.SetBool("Attacking", false);
                    detector.SetActive(true);
                    isAttacking = false;
                    attackTime = 0.0f;
                    transform.GetChild(1).gameObject.SetActive(false);
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

    public void SetDamageMultiplier(float mult)
    {
        damageMultiplier = mult;
    }

    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }

    private void OnDugUp()
    {
        animator.SetBool("Digging", false);
        detector.SetActive(true);
        //Will it spawn enemy? if yes return nothing
        float enemyChance = Random.Range(0.0f, 100.0f);
        if (enemyChance <= 25.0f)
        {
            //spawn enemy
            GameEvents.current.EnemySpawn();
        }
        else
        {
            Loot tempLoot;
            Inventory inv = inventory;
            if (!inv.IsLootInInventory(Loot.GiveQuestLoot(QuestManager.GetQuest().GetQuestLoot())))
            {
                tempLoot = Loot.GiveQuestLoot(QuestManager.GetQuest().GetQuestLoot());
                inv.AddLoot(tempLoot);
            }
            else
            {
                tempLoot = Loot.GenerateLoot();
                inv.AddLoot(tempLoot);
            }
            animator.SetBool("Pickup", true);
            pickup = true;
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = tempLoot.GetSprite();
            pickupTime = 0.0f;
            detector.SetActive(false);
            GameEvents.current.ColdLoot();
        }
    }

    private void enemyLoot()
    {
        Loot tempLoot;
        tempLoot = Loot.GenerateRewardLoot();
        inventory.AddLoot(tempLoot);
    }
}
