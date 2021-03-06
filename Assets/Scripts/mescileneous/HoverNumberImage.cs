using BizzyBeeGames.Sudoku;
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
    public GameObject eraser;
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    private void OnEnable()
    {
        EventTriggerActions.OnBeginDrag += ChangeTheTextOfTheDummyNumber; 
        EventTriggerActions.OnBeginDrag += MakeDummyNumberVisible; 
        EventTriggerActions.OnDrop += MakeDummyNumberInVisible; 
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke(nameof(InvokeMakeDummyNumberVisibleWithDelay));
            Invoke(nameof(MakeDummyNumberInVisible),0.1f);
        }

        if (PuzzleBoard.clickedOnErase == true)
        {
            eraser.gameObject.SetActive(true);
        }
        else 
            eraser.gameObject.SetActive(false);
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
        PuzzleBoard.clickedOnErase = false;
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
