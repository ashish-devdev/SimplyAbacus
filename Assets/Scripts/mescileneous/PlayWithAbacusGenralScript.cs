using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayWithAbacusGenralScript : MonoBehaviour
{
    public Button LeftShift;
    public Button RightShift;
    public Reset reset;
    public ValueCalculator valueCalculator;
    public PositionRodsOfAbacus positionRodsOfAbacus;
    public int currentPosition;
    string[] strPosition = { "F0", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8" };
    public TextMeshProUGUI abacusvalueText;

    private void OnEnable()
    {
        Invoke("DelayEnable", 0.1f);

    }


    public void DelayEnable()
    {
        currentPosition = 5;

        LeftShift.interactable = true;
        RightShift.interactable = true;
        reset.RESET();
        LeftShift.onClick.AddListener(ShiftLeftDecimalPoint);
        RightShift.onClick.AddListener(ShiftRightDecimalPoint);

        valueCalculator.numberOfDecimalPlaces = currentPosition;
        valueCalculator.decimalPlaceString = strPosition[currentPosition];
        positionRodsOfAbacus.EditRod();
    }



    public void ShiftLeftDecimalPoint()
    {
        currentPosition++;
        ChangeShiftButtonIntractibility();

        valueCalculator.numberOfDecimalPlaces = currentPosition;
        valueCalculator.decimalPlaceString = strPosition[currentPosition];
        positionRodsOfAbacus.EditRod();

    }
    public void ShiftRightDecimalPoint()
    {
        currentPosition--;
        ChangeShiftButtonIntractibility();

        valueCalculator.numberOfDecimalPlaces = currentPosition;
        valueCalculator.decimalPlaceString = strPosition[currentPosition];
        positionRodsOfAbacus.EditRod();
    }


    public void ChangeShiftButtonIntractibility()
    {
        if (currentPosition == 0)
        {
            RightShift.interactable = false;

        }
        else
        {
            RightShift.interactable = true;

        }

        if (currentPosition == 8)
        {
            LeftShift.interactable = false;
        }
        else
        {
            LeftShift.interactable = true;

        }

    }



    private void Update()
    {
        abacusvalueText.text = ValueCalculator.value1.ToString(strPosition[currentPosition]);
    }

    private void OnDisable()
    {
        valueCalculator.numberOfDecimalPlaces = 2;
        valueCalculator.decimalPlaceString = "F2";

        LeftShift.onClick.RemoveListener(ShiftLeftDecimalPoint);
        RightShift.onClick.RemoveListener(ShiftRightDecimalPoint);

        positionRodsOfAbacus.EditRod();
    }

}
