using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickableType
{
    Heavy,
    Light,
    Key
}

public class Pickable : MonoBehaviour
{
    public PickableType pickableType;
    public Outline outline;
    public bool isPickedUp;

    public void Selected()
    {
        Debug.Log($"{name} selected.");
    }

    public void OutlineAnimation(bool isOutline)
    {
        if(isPickedUp) return;
        
        if (outline != null)
        {
            outline.enabled = isOutline;
        }
    }

    public void PickedUp(Transform carryPosition)
    {
        if (isPickedUp) return;

        isPickedUp = true;
        OutlineAnimation(false);
        Debug.Log($"{name} picked up.");
        transform.SetParent(carryPosition);
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void OnDrop()
    {
        Debug.Log($"{name} dropped.");
        isPickedUp = false;
        OutlineAnimation(false);
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        TransformPosition();
    }

    public void TransformPosition()
    {
        Debug.Log($"{name} position adjusted after drop.");
    }
}