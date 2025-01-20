using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguesMgr : SingletonMonoBehaviour<DialoguesMgr>
{
    public DialoguesController dialoguesController;

    public void StartDialogue(NPCController npcController)
    {
        dialoguesController.ToggleDialogues();
        dialoguesController.InitializeDialogue(npcController.npcText, npcController.npcSprite);
    }
}