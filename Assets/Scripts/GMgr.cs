using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMgr : SingletonMonoBehaviour<GMgr>
{
    public CursorController cursorController;
    public LoadingScreenController loadingScreenController;
    
    void Awake()
    {
        OnLevelLoaded();
    }
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

    private void OnLevelLoaded()
    {
        PlayerController.Instance.TogglePlayerLock();
        loadingScreenController.ToggleLoadingScreen(true);
        StartCoroutine(UnlockPlayer());
    }
    
    private IEnumerator UnlockPlayer()
    {
        yield return new WaitForSeconds(loadingScreenController.loadingDuration + 1f);
        PlayerController.Instance.TogglePlayerLock();
    }
}
