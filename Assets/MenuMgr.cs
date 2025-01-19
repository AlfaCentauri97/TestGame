using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMgr : SingletonMonoBehaviour<MenuMgr>
{
    public Transform MenuUI;
    
    public void ToggleMenu()
    {
        bool isActive = MenuUI.gameObject.activeSelf;
        MenuUI.gameObject.SetActive(!isActive);
    }
}
