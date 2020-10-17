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
                    Debug.Log("Start");
                    digGame.SetState(true);
                }
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

    public void SetIsDigging(bool digging)
    {
        isDigging = digging;
    }

    public int GetDigMultiplier()
    {
        return digUpgrade;
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
        Inventory inv = this.GetComponent<movement>().GetInventory();
        inv.AddLoot(Loot.GenerateLoot());
        GameEvents.current.ColdLoot();
    }
}
