using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverNumberImage : MonoBehaviour,IPointerDownHandler
{
    public GameObject numberListParent;
    public Text dummyNumberText;
    public GameObject dummyNumber_Alpha;
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    private void OnEnable()
    {
        EventTriggerActions.OnBeginDrag += ChangeTheTextOfTheDummyNumber; 
        EventTriggerActions.OnBeginDrag += MakeDummyNumberVisible; 
        EventTriggerActions.OnDrop += MakeDummyNumberInVisible; 
        
    }

    private void EventTriggerActions_OnBeginDrag()
    {
        throw new System.NotImplementedException();
    }

    public void MakeDummyNumberVisible()
    {
        Invoke(nameof(InvokeMakeDummyNumberVisibleWithDelay), 0.1f);
       // dummyNumber.SetActive(true);
    }
    public void InvokeMakeDummyNumberVisibleWithDelay() 
    {
        dummyNumber_Alpha.SetActive(true);
    }

    public void MakeDummyNumberInVisible()
    {
        dummyNumber_Alpha.SetActive(false);
    }

    public void ChangeTheTextOfTheDummyNumber()
    {
        dummyNumberText.text = EventTriggerActions.pickedNumber;
    }

    private void OnDisable()
    {
        EventTriggerActions.OnBeginDrag -= ChangeTheTextOfTheDummyNumber;
        EventTriggerActions.OnDrop -= MakeDummyNumberInVisible;
        EventTriggerActions.OnBeginDrag -= MakeDummyNumberVisible;


    }


}
