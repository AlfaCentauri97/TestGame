using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [TextArea]
    public string npcText;
    public Sprite npcSprite;

    public void Interact()
    {
        Debug.Log("Interacting with NPC.");
        DialoguesMgr.Instance.StartDialogue(this);
    }
}