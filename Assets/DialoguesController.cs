using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguesController : MonoBehaviour
{
    public Transform DialoguesUI;
    public bool isActivated = false;

    public TextMeshProUGUI dialogueText;
    public Image dialogueImage;

    private List<string> dialogueHolder;
    private NPCController currentNPC;
    private int index;
    private void Update()
    {
        if (isActivated && Input.GetMouseButtonDown(0))
        {
            NextDialogue(dialogueHolder);
        }
    }

    public void ToggleDialogues()
    {
        isActivated = !isActivated;
        DialoguesUI.gameObject.SetActive(isActivated);
    }

    public void InitializeDialogue(List<string> npcDialogueText, Sprite npcDialogueSprite, NPCController NPC)
    {
        index = 0;
        dialogueText.text = npcDialogueText[index];
        dialogueImage.sprite = npcDialogueSprite;
        dialogueHolder = npcDialogueText;
        currentNPC = NPC;
        Debug.Log("Dialogue initialized.");
    }
    
    public void NextDialogue(List<string> npcDialogueText)
    {

        if (index < npcDialogueText.Count - 1)
        {
            index++;
            dialogueText.text = npcDialogueText[index];
        }
        else
        {
            ToggleDialogues();
            currentNPC.isDialoguesActive = false;
        }
    }
}