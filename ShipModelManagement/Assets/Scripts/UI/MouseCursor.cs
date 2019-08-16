using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseCursor : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Texture2D cursorTex;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(cursorTex,Vector2.zero,CursorMode.ForceSoftware);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);

    }

    private void OnDestroy()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }
}
