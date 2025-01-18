using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public bool isOccupied = false;
    public Image itemIconImage;
    public Image slotSpriteToggle;
    
    public void OnPointerEnter()
    {
        if (slotSpriteToggle != null)
        {
            slotSpriteToggle.DOFade(1f, 0.15f);
        }
    }
    
    public void OnPointerExit()
    {
        if (slotSpriteToggle != null)
        {
            slotSpriteToggle.DOFade(0f, 0.15f);
        }
    }

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