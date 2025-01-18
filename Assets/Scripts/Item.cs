using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable,
    Quest,
    Misc
}
public class Item : MonoBehaviour
{
    public string ItemID { get; private set; }
    public ItemType itemType;
    public Outline outline;
    public Sprite itemIcon;
    
    public void Selected()
    {
        Debug.Log($"{name} selected.");
    }

    public void OutlineAnimation(bool isOutline)
    {
        if (outline != null)
        {
            outline.enabled = isOutline;
        }
    }

    public void PickUp()
    {
        InventoryMgr.Instance.AddItemToInventory(this);
    }
}
