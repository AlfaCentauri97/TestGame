using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMgr : SingletonMonoBehaviour<GMgr>
{
    public CursorController cursorController;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuMgr.Instance.ToggleMenu();
            cursorController.ToggleCursor();
            PlayerController.Instance.TogglePlayerLock();
            Debug.Log("Game Paused.");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InventoryMgr.Instance.ToggleInventory();
            cursorController.ToggleCursor();
            PlayerController.Instance.TogglePlayerLock();
        }
    }
}
