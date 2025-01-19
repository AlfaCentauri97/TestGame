using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    [Header("Settings")]
    public bool isPickableNecessary = false;
    [ShowIf("isPickableNecessary")] public Pickable pickableNeeded;

    public Door door;
    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isPickableNecessary)
        {
            Pickable pickable = other.GetComponent<Pickable>();

            if (pickable != null && pickable == pickableNeeded)
            {
                PlatformAnimation();
            }
        }
        else
        {
            PlatformAnimation();
        }
    }

    public void PlatformAnimation()
    {
        if (isActivated) return;

        isActivated = true;
        
        transform.DOMoveY(transform.position.y - 0.15f, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            if(door != null)
                door.Open();
        });
    }
}