using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Lean.Transition.Method;
using Lean.Gui;
using System;

public class VisualHandMovements : MonoBehaviour
{
    public Image leftHand;
    public List<Sprite> leftHandImgs;

    public Image rightHand;
    public List<Sprite> rightHandImgs;

    public BeedData_1 beedData_1;
    public BeedData_2 beedData_2;

    public Button leftThumbBtn;
    public Button leftIndexBtn;
    public Button leftMiddleBtn;
    public Button leftRingBtn;
    public Button leftPinkyBtn;

    public Button rightThumbBtn;
    public Button rightIndexBtn;
    public Button rightMiddleBtn;
    public Button rightRingBtn;
    public Button rightPinkyBtn;

    public Button nextOperationBtn;


    public int currentLeftHandValue;
    public int currentRightHandValue;
    int i;
    int j;

    public List<FingerBtnPosition> leftHandFingerBtnPosition;
    public List<FingerBtnPosition> RingHandFingerBtnPosition;

    public List<LeanToggle> handLean;

    RectTransform leftThumbBtnRectTransform;
    RectTransform leftIndexBtnRectTransform;
    RectTransform leftMiddleBtnRectTransform;
    RectTransform leftRingBtnRectTransform;
    RectTransform leftPinkyBtnRectTransform;

    RectTransform rightThumbBtnRectTransform;
    RectTransform rightIndexBtnRectTransform;
    RectTransform rightMiddleBtnRectTransform;
    RectTransform rightRingBtnRectTransform;
    RectTransform rightPinkyBtnRectTransform;
    public static event Action dosomthing2;




    private void OnEnable()
    {
        currentLeftHandValue = 0;
        currentRightHandValue = 0;
        i = 0;j = 0;
        leftThumbBtnRectTransform = leftThumbBtn.gameObject.GetComponent<RectTransform>();
        leftIndexBtnRectTransform = leftIndexBtn.gameObject.GetComponent<RectTransform>();
        leftMiddleBtnRectTransform = leftMiddleBtn.gameObject.GetComponent<RectTransform>();
        leftRingBtnRectTransform = leftRingBtn.gameObject.GetComponent<RectTransform>();
        leftPinkyBtnRectTransform = leftPinkyBtn.gameObject.GetComponent<RectTransform>();
        rightThumbBtnRectTransform = rightThumbBtn.gameObject.GetComponent<RectTransform>();
        rightIndexBtnRectTransform = rightIndexBtn.gameObject.GetComponent<RectTransform>();
        rightMiddleBtnRectTransform = rightMiddleBtn.gameObject.GetComponent<RectTransform>();
        rightRingBtnRectTransform = rightRingBtn.gameObject.GetComponent<RectTransform>();
        rightPinkyBtnRectTransform = rightPinkyBtn.gameObject.GetComponent<RectTransform>();



        nextOperationBtn.onClick.AddListener(ResetHands);

    }


    private void OnDisable()
    {
        nextOperationBtn.onClick.RemoveListener(ResetHands);

    }

    [System.Serializable]
    public class FingerBtnPosition
    {
        public string fingerValue;
        public X_Y_Position thumb;
        public X_Y_Position index;
        public X_Y_Position middle;
        public X_Y_Position ring;
        public X_Y_Position pinky;

    }

    [System.Serializable]
    public class X_Y_Position
    {
        public float xPosition;
        public float yPosition;
    }

    public void ChangeLeftHandButtonPositions()
    {
        for (int i = 0; i < leftHandFingerBtnPosition.Count; i++)
        {
            if (leftHandFingerBtnPosition[i].fingerValue == currentLeftHandValue.ToString())
            {
                leftThumbBtnRectTransform.anchoredPosition = new Vector2(leftHandFingerBtnPosition[i].thumb.xPosition, leftHandFingerBtnPosition[i].thumb.yPosition);
                leftIndexBtnRectTransform.anchoredPosition = new Vector2(leftHandFingerBtnPosition[i].index.xPosition, leftHandFingerBtnPosition[i].index.yPosition);
                leftMiddleBtnRectTransform.anchoredPosition = new Vector2(leftHandFingerBtnPosition[i].middle.xPosition, leftHandFingerBtnPosition[i].middle.yPosition);
                leftRingBtnRectTransform.anchoredPosition = new Vector2(leftHandFingerBtnPosition[i].ring.xPosition, leftHandFingerBtnPosition[i].ring.yPosition);
                leftPinkyBtnRectTransform.anchoredPosition = new Vector2(leftHandFingerBtnPosition[i].pinky.xPosition, leftHandFingerBtnPosition[i].pinky.yPosition);
            }
        }

    }
    public void ChangeRightHandButtonPositions()
    {
        for (int i = 0; i < RingHandFingerBtnPosition.Count; i++)
        {
            if (RingHandFingerBtnPosition[i].fingerValue == currentRightHandValue.ToString())
            {
                rightThumbBtnRectTransform.anchoredPosition = new Vector2(RingHandFingerBtnPosition[i].thumb.xPosition, RingHandFingerBtnPosition[i].thumb.yPosition);
                rightIndexBtnRectTransform.anchoredPosition = new Vector2(RingHandFingerBtnPosition[i].index.xPosition, RingHandFingerBtnPosition[i].index.yPosition);
                rightMiddleBtnRectTransform.anchoredPosition = new Vector2(RingHandFingerBtnPosition[i].middle.xPosition, RingHandFingerBtnPosition[i].middle.yPosition);
                rightRingBtnRectTransform.anchoredPosition = new Vector2(RingHandFingerBtnPosition[i].ring.xPosition, RingHandFingerBtnPosition[i].ring.yPosition);
                rightPinkyBtnRectTransform.anchoredPosition = new Vector2(RingHandFingerBtnPosition[i].pinky.xPosition, RingHandFingerBtnPosition[i].pinky.yPosition);
            }
        }

    }


    public void ChangeCurrentLeftHandValue(int a)
    {
        if (a == -5)
        {
            beedData_2.beeds[3].state = 0;

        }
        if (a == 5)
        {
            beedData_2.beeds[3].state = 5;

        }
        if (a == 1)
        {
            beedData_1.beeds[12 + j].state = 1;
            j += 1;
        }
        if (a == -1)
        {
            j -= 1;
            beedData_1.beeds[12 + j].state = 0;

        }

        currentLeftHandValue += a;
        leftHand.sprite = leftHandImgs[currentLeftHandValue];
        ChangeLeftHandButtonPositions();

        Invoke("InvokeDoSomthing2", 0.1f);


    }
    public void ChangeCurrentRightHandValue(int a)
    {
        if (a == -5)
        {
            beedData_2.beeds[2].state = 0;

        }
        if (a == 5)
        {
            beedData_2.beeds[2].state = 5;

        }
        if (a == 1)
        {
            beedData_1.beeds[8 + i].state = 1 ;
            i += 1;
        }
        if (a == -1)
        {
            i -= 1;
            beedData_1.beeds[8 + i].state = 0;

        }
        currentRightHandValue += a;
        rightHand.sprite = rightHandImgs[currentRightHandValue];
        ChangeRightHandButtonPositions();

        Invoke("InvokeDoSomthing2", 0.1f);

    }

    public void InvokeDoSomthing2()
    {
        try 
        {
           dosomthing2();
        }
        catch {; }

    }



    public void ResetHands()
    {
        leftThumbBtnRectTransform.anchoredPosition = new Vector2(leftHandFingerBtnPosition[0].thumb.xPosition, leftHandFingerBtnPosition[0].thumb.yPosition);
        leftIndexBtnRectTransform.anchoredPosition = new Vector2(leftHandFingerBtnPosition[0].index.xPosition, leftHandFingerBtnPosition[0].index.yPosition);
        leftMiddleBtnRectTransform.anchoredPosition = new Vector2(leftHandFingerBtnPosition[0].middle.xPosition, leftHandFingerBtnPosition[0].middle.yPosition);
        leftRingBtnRectTransform.anchoredPosition = new Vector2(leftHandFingerBtnPosition[0].ring.xPosition, leftHandFingerBtnPosition[0].ring.yPosition);
        leftPinkyBtnRectTransform.anchoredPosition = new Vector2(leftHandFingerBtnPosition[0].pinky.xPosition, leftHandFingerBtnPosition[0].pinky.yPosition);

        rightThumbBtnRectTransform.anchoredPosition = new Vector2(RingHandFingerBtnPosition[0].thumb.xPosition, RingHandFingerBtnPosition[0].thumb.yPosition);
        rightIndexBtnRectTransform.anchoredPosition = new Vector2(RingHandFingerBtnPosition[0].index.xPosition, RingHandFingerBtnPosition[0].index.yPosition);
        rightMiddleBtnRectTransform.anchoredPosition = new Vector2(RingHandFingerBtnPosition[0].middle.xPosition, RingHandFingerBtnPosition[0].middle.yPosition);
        rightRingBtnRectTransform.anchoredPosition = new Vector2(RingHandFingerBtnPosition[0].ring.xPosition, RingHandFingerBtnPosition[0].ring.yPosition);
        rightPinkyBtnRectTransform.anchoredPosition = new Vector2(RingHandFingerBtnPosition[0].pinky.xPosition, RingHandFingerBtnPosition[0].pinky.yPosition);

        for (int i = 0; i < handLean.Count; i++)
        {
            handLean[i].TurnOff();
        }

    }


}
