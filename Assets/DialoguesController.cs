using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguesController : MonoBehaviour
{
    public Transform DialoguesUI;
    public Transform DialoguesTextPanel;
    public bool isActivated = false;

    public TextMeshProUGUI dialogueText;
    public Image dialogueImage;
    
    public DialoguesQuest dialoguesQuest;
    public Image dialogueQuestImage;
    
    private List<string> dialogueHolder;
    [HideInInspector]public NPCController currentNPC;
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
        dialoguesQuest.gameObject.SetActive(false);
        DialoguesTextPanel.gameObject.SetActive(true);
        isActivated = !isActivated;
        DialoguesUI.gameObject.SetActive(isActivated);
    }
    public void ToggleDialogues(bool active)
    {
        DialoguesUI.gameObject.SetActive(active);
        isActivated = active;
    }

    public void InitializeDialogue(List<string> npcDialogueText, Sprite npcDialogueSprite, NPCController NPC)
    {
        index = 0;
        dialogueText.text = npcDialogueText[index];
        dialogueImage.sprite = npcDialogueSprite;
        dialogueQuestImage.sprite = npcDialogueSprite;
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
            if (currentNPC.isQuestGiver)
            {
                ShowQuestDialog();
            }
            else
            {
                ToggleDialogues();
                currentNPC.isDialoguesActive = false;
            }
        }
    }

    private void ShowQuestDialog()
    {
        DialoguesTextPanel.gameObject.SetActive(false);
        dialoguesQuest.gameObject.SetActive(true);
        dialoguesQuest.InitializeQuestDialogue();
    }
}