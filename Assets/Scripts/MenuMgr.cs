using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMgr : SingletonMonoBehaviour<MenuMgr>
{
    public Transform MenuUI;
    public bool isActivated = false;
    public void ToggleMenu()
    {
        isActivated = !isActivated;
        bool isActive = MenuUI.gameObject.activeSelf;
        MenuUI.gameObject.SetActive(!isActive);
    }
    public void ResumeClick()
    {
        ToggleMenu();
        PlayerController.Instance.TogglePlayerLock();
        GMgr.Instance.cursorController.ToggleCursor();
        isActivated = false;
    }
    public void LoadClick()
    {
        Debug.Log("Load game...");
    }
    public void SaveClick()
    {
        Debug.Log("Save game...");
    }
    public void OptionsClick()
    {
        Debug.Log("Options...");
    }
    public void QuitClick()
    {
        Application.Quit();
    }
}
