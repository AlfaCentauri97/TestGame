using UnityEngine;

public class CursorController : MonoBehaviour
{
    public RectTransform cursor;
    public bool isCursorActive = false;
    public GameObject cursorObject;

    void Update()
    {
        /*if (isCursorActive)
        {
            Vector2 mousePosition = Input.mousePosition;
            
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                cursor.parent as RectTransform,
                mousePosition,
                null,
                out canvasPosition
            );
            
            cursor.localPosition = canvasPosition;
        }*/
    }

    public void ToggleCursor()
    {
        isCursorActive = !isCursorActive;

        if (isCursorActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    public void ToggleCursor(bool isActive)
    {
        isCursorActive = isActive;

        if (isCursorActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}