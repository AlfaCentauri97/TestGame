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

    private void Update()
    {
        if (isActivated && Input.GetMouseButtonDown(0))
        {
            ToggleDialogues();
        }
    }

    public void ToggleDialogues()
    {
        isActivated = !isActivated;
        DialoguesUI.gameObject.SetActive(isActivated);
    }

    public void InitializeDialogue(string npcDialogueText, Sprite npcDialogueSprite)
    {
        dialogueText.text = npcDialogueText;
        dialogueImage.sprite = npcDialogueSprite;
        Debug.Log("Dialogue initialized.");
    }
}