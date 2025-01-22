using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [TextArea]
    public List<string> npcTexts;
    public Sprite npcSprite;
    public bool isQuestGiver = false;

    [HideInInspector]public bool isDialoguesActive = false;
    public void Interact()
    {
        Debug.Log("Interacting with NPC.");

        if(!isDialoguesActive)
        {
            isDialoguesActive = true;
            DialoguesMgr.Instance.StartDialogue(this);
        }
    }
}