using Lean.Gui;
using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdjustScrollPosition : MonoBehaviour
{
    public float min;
    public float[] scrollPositions;
    public RectTransform scrollRectTransform;
    public LeanToggle leanToggle;
    public LeanRectTransformAnchoredPositionX leanRectTransformAnchoredPositionX;
    public RectTransform[] scrollContents;
    UnityAction onDrag;
    public Button leftButton;
    public Button rightButton;
    int currentScrollIndex;
    void Start()
    {


    }

    private void OnEnable()
    {
        for (int i = 0; i < scrollContents.Length; i++)
        {
            scrollPositions[i] =-(i*570f);
        }
            currentScrollIndex = 0;
    }

    void Update()
    {
        if (currentScrollIndex < 1)
        {
            rightButton.interactable = false;
        }
        else
        {
            rightButton.interactable = true;
        }

        if (currentScrollIndex >= scrollPositions.Length-1)
        {
            leftButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
        }
    }

    public void AdjustScrollViewPosition()
    {
        if (onDrag == null)
        {
            onDrag += BeginLeanTransitions;
            onDrag.Invoke();
        }

    }

    public void ChangeContent(int val)
    {
        currentScrollIndex += val;
        leanRectTransformAnchoredPositionX.Data.Position  = scrollPositions[currentScrollIndex];
        leanRectTransformAnchoredPositionX.BeginAllTransitions();
    }


    public void BeginLeanTransitions()
    {

        leanRectTransformAnchoredPositionX.Data.Position = ReturnTheClosestScrollContentPos();
        leanRectTransformAnchoredPositionX.BeginAllTransitions();
    }

    public float ReturnTheClosestScrollContentPos()
    {
        min = 99999;
        int minIndex=0;
        print(scrollRectTransform.anchoredPosition.x);
        for (int i = 0; i < scrollPositions.Length; i++)
        {

            if (scrollRectTransform.anchoredPosition.x - scrollPositions[i] < 0)
            {
                if (-(scrollRectTransform.anchoredPosition.x - scrollPositions[i]) <= min)
                {
                    min = -(scrollRectTransform.anchoredPosition.x - scrollPositions[i]);
                    minIndex = i;
                }

            }
            else
            {
                if (scrollRectTransform.anchoredPosition.x - scrollPositions[i] <= min)
                {
                    min = (scrollRectTransform.anchoredPosition.x - scrollPositions[i]);
                    minIndex = i;

                }

            }

            //print(scrollRectTransform.anchoredPosition.x - scrollPositions[i]);

            //if (scrollRectTransform.anchoredPosition.x - scrollPositions[i] <= min)
            //{
            //    min = scrollPositions[i];

            //}





        }

        print(-scrollPositions[minIndex]+"  "+minIndex);
        return scrollPositions[minIndex];
    }
}
