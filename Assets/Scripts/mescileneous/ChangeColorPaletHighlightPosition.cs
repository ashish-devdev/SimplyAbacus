using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition.Method;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ChangeColorPaletHighlightPosition : MonoBehaviour
{
    //Button btn;

    //private void Start()
    //{
    //    btn = GetComponent<Button>();
    //    btn.onClick.AddListener(ChangeHighLightPosition);
    //}

    public static bool isDraggedFromColorPalet;

    private void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.BeginDrag;
        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); isDraggedFromColorPalet = true; });
        trigger.triggers.Add(entry); 
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.EndDrag;
        entry2.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry2);

    }

    private void OnDragDelegate(PointerEventData data)
    {
        ChangeHighLightPosition();
    }

    public LeanRectTransformAnchoredPositionY highlight;

    public void ChangeHighLightPosition()
    {
        highlight.Data.Position = this.gameObject.GetComponent<RectTransform>().anchoredPosition.y;
        highlight.BeginAllTransitions();
    }

    private void OnDestroy()
    {
        // btn.onClick.RemoveListener(ChangeHighLightPosition);
    }
}
