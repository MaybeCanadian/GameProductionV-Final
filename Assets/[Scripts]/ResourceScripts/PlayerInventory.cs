using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [Header("Inventory References")]
    public GameObject inventoryUI;
    public List<InventoryUISlot> inventoryUISlots;
    public bool InventoryOpen = false;

    [Header("Player inventory")]
    public List<InvenotoryItems> items;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddItem(ResourceItem item, int amount)
    {
        foreach(InvenotoryItems invItem in items)
        {
            if(invItem.item == item)
            {
                invItem.amount += amount;

                UpdateUISlots();
                return;
            }
        }

        if (items.Count > inventoryUISlots.Count)
        {
            Debug.Log("Inventory full");
            return;
        }

        InvenotoryItems tempItem = new InvenotoryItems();
        tempItem.item = item;
        tempItem.amount = amount;

        items.Add(tempItem);
    }

    public bool LoseItem(ResourceItem item, int amount)
    {
        foreach(InvenotoryItems invItem in items)
        {
            if(invItem.item == item)
            {
                if(invItem.amount >= amount)
                {
                    invItem.amount -= amount;
                    if(invItem.amount <= 0)
                    {
                        items.Remove(invItem);
                    }

                    return true;
                }

                return false;
            }
        }

        return false;
    }

    private void UpdateUISlots()
    {
        int itt = 0;

        foreach(InventoryUISlot slot in inventoryUISlots)
        {
            if(items.Count > itt)
            {
                InvenotoryItems item = items[itt];
                if(item != null)
                {
                    slot.SetItemName(item.item.DisplayName);
                    slot.SetItemCount(item.amount);

                    if(item.item.UIImage)
                    {
                        slot.SetItemSprite(item.item.UIImage);
                    }

                    slot.UpdateSlot();
                    slot.SlotVisible(true);
                }
                else
                {
                    slot.SlotVisible(false);
                }
            }
            else
            {
                slot.SlotVisible(false);
            }

            itt++;
        }
    }

    public void OnInventoryButtonPressed()
    {
        if(!InventoryOpen)
        {
            if(GameStateMachine.GetInstance().GetState() == GameStates.GAME)
            {
                inventoryUI.SetActive(true);
                InventoryOpen = true;
                GameStateMachine.GetInstance().ChangeState(GameStates.INVENTORY);
                if (InventoryOpen)
                    UpdateUISlots();
            }
        }
        else {
            if(GameStateMachine.GetInstance().GetState() == GameStates.INVENTORY)
            {
                inventoryUI.SetActive(false);
                InventoryOpen = false;
                GameStateMachine.GetInstance().ChangeState(GameStates.GAME);
            }
        }
    }

    public bool CheckIfHasItem(ResourceItem item, int amount)
    {
        foreach(InvenotoryItems invItem in items)
        {
            if(invItem.item == item)
            {
                if(invItem.amount >= amount)
                {
                    return true;
                }

                return false;
            }
        }

        return false;
    }

    public bool RemoveItem(ResourceItem item, int amount)
    {
        foreach(InvenotoryItems invItem in items)
        {
            if(invItem.item == item)
            {

                invItem.amount -= amount;
                return true;
            }
        }

        return false;
    }

    public bool RemoveAllItemsInList(List<Materials> mats)
    {
        foreach(Materials mat in mats)
        {
            if(!CheckIfHasItem(mat.item, mat.amount))
            {
                return false;
            }
        }

        foreach(Materials mat in mats)
        {
            RemoveItem(mat.item, mat.amount);
        }

        return true;
    }
}

[System.Serializable]
public class InvenotoryItems
{
    public ResourceItem item;
    public int amount = 0;
}