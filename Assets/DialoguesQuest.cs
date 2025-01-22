using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguesQuest : MonoBehaviour
{
    public void InitializeQuestDialogue()
    {
        GMgr.Instance.cursorController.ToggleCursor(true);
        InventoryMgr.Instance.ToggleInventory(false);
        PlayerController.Instance.TogglePlayerLock(true);
        Debug.Log("Initializing quest dialogue.");
    }

    public void ExitButton()
    {
        DialoguesMgr.Instance.dialoguesController.currentNPC.isDialoguesActive = false;
        GMgr.Instance.cursorController.ToggleCursor(false);
        PlayerController.Instance.TogglePlayerLock(false);
        DialoguesMgr.Instance.dialoguesController.ToggleDialogues(false);
    }
}
