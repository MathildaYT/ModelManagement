using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowName : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Text showTextPanel;
    public void OnPointerEnter(PointerEventData eventData)
    {
        showTextPanel.gameObject.SetActive(true);
        showTextPanel.text= transform.GetComponent<Text>().text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        showTextPanel.gameObject.SetActive(false);
    }

  
}
