using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;

    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            OpenDoorAnimation();
        }
    }

    private void OpenDoorAnimation()
    {
        transform.DOMoveY(transform.position.y - 5f, 1f).SetEase(Ease.OutQuad);
    }
}