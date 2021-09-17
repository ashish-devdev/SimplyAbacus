using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTriggerActions : MonoBehaviour
{

    public static event Action OnBeginDrag;
    public static event Action OnDrop;
    public Text numberText;
    public static string pickedNumber;

    public void InvokeOnBeginDrag()
    {
        pickedNumber = numberText.text;
        OnBeginDrag.Invoke();
        print("BeginDrag");
    }



    public void InvokeOnDrop()
    {
        OnDrop.Invoke();
    }
}
