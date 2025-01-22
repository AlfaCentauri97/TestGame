using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownInventoryController : MonoBehaviour
{
    public List<InventorySlot> playerSlots; // Assign your slots in the inspector
    private int currentlySelectedSlot = -1; // No slot selected initially

    void Update()
    {
        HandleSlotSelection();
    }

    private void HandleSlotSelection()
    {
        // Detect number key press (1-10)
        for (int i = 1; i <= 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + (i % 10))) // Map 1-10 to Alpha0-Alpha9
            {
                SelectSlot(i - 1); // Subtract 1 to convert to 0-based index
            }
        }
    }

    private void SelectSlot(int slotIndex)
    {
        // Deselect the currently selected slot
        if (currentlySelectedSlot >= 0 && currentlySelectedSlot < playerSlots.Count)
        {
            playerSlots[currentlySelectedSlot].OnPointerExit();
        }

        // Update the current slot index
        currentlySelectedSlot = slotIndex;

        // Ensure the new slot index is valid
        if (currentlySelectedSlot >= 0 && currentlySelectedSlot < playerSlots.Count)
        {
            playerSlots[currentlySelectedSlot].OnPointerEnter();
        }
    }
}