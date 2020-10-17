using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    Transform itemSlotContainer;
    Transform itemSlotTemplate;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    // Start is called before the first frame update
    void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    private void Start()
    {
        //GameEvents.current.onInvUpdate += RefreshInventoryItems;
    }

    private void RefreshInventoryItems()
    {
        if (itemSlotContainer != null)
        {
            foreach (Transform child in itemSlotContainer)
            {
                if (child == itemSlotTemplate)
                {
                    continue;
                }
                Destroy(child.gameObject);
            }
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 60.0f;

        foreach (Loot loot in inventory.GetLootList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("item").GetComponent<Image>();
            image.sprite = loot.GetSprite();

            TextMeshProUGUI textUI = itemSlotRectTransform.Find("amount").GetComponent<TextMeshProUGUI>();
            if (loot.amount > 1)
            {
                textUI.SetText(loot.amount.ToString());
            }
            else
            {
                textUI.SetText("");
            }

            x++;
            if (x > 4)
            {
                x = 0;
                y--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RefreshInventoryItems();
    }
}
