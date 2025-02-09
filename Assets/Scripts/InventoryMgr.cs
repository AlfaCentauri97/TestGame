using System.Collections.Generic;
using UnityEngine;

public class InventoryMgr : SingletonMonoBehaviour<InventoryMgr>
{
    public Transform InventoryUI;
    public Inventory Inventory;

    public void ToggleInventory()
    {
        bool isActive = InventoryUI.gameObject.activeSelf;
        InventoryUI.gameObject.SetActive(!isActive);
    }

    public void ToggleInventory(bool isActive)
    {
        InventoryUI.gameObject.SetActive(isActive);
    }

    public void AddItemToInventory(Item item)
    {
        foreach (var slot in Inventory.InventorySlots)
        {
            if (!slot.isOccupied)
            {
                slot.UpdateSlot(item);

                item.gameObject.SetActive(false);

                Debug.Log($"Item {item.name} added to inventory slot.");
                return;
            }
        }

        Debug.LogWarning("Inventory is full! Could not add item.");
    }

    public void OnSlotClicked(InventorySlot slot)
    {
        if (slot != null && slot.item != null)
        {
            Debug.Log($"Slot clicked: Item name is {slot.item.name}");
        }
        else
        {
            Debug.Log("Slot clicked: No item in this slot.");
        }
    }
}