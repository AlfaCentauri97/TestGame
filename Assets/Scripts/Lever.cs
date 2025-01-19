using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Outline outline;
    public Door door;

    public void Interact()
    {
        door.Open();
    }
    
    public void OutlineAnimation(bool isOutline)
    {
        if (outline != null)
        {
            outline.enabled = isOutline;
        }
    }
}
