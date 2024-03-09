using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverActivate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform panel;
    private TextMeshProUGUI text;

    
    private void Awake()
    {
        panel = transform.Find("Panel").GetComponent<RectTransform>();
        text = panel.Find("Text").GetComponent<TextMeshProUGUI>();
        panel.gameObject.SetActive(false);
        
        
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Activate the object
        panel.gameObject.SetActive(true);
    }

    // Called when the mouse exits the area of the image
    public void OnPointerExit(PointerEventData eventData)
    {
        
        panel.gameObject.SetActive(false);
    }
}
