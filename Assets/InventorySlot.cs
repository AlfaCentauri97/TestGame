using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public bool isOccupied = false;
    public Image itemIconImage;
    public Image slotSpriteToggle;
    
    public void UpdateSlot(Item newItem)
    {
        item = newItem;
        isOccupied = true;
        
        if (newItem != null && newItem.itemIcon != null)
        {
            itemIconImage.enabled = true;
            itemIconImage.sprite = newItem.itemIcon;
        }
        else
        {
            ClearSlot();
        }
    }
    
    public void ClearSlot()
    {
        item = null;
        isOccupied = false;
        itemIconImage.sprite = null;
        itemIconImage.enabled = false;
    }
}