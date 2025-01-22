using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguesRange : MonoBehaviour
{
    private NPCController npcController;
    private void Start()
    {
        npcController = GetComponentInParent<NPCController>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && npcController.isDialoguesActive)
        {
            DialoguesMgr.Instance.dialoguesController.ToggleDialogues(false);
            npcController.isDialoguesActive = false;
        }
    }
}
