using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueCalculator : MonoBehaviour
{
    public BeedData_1 BD1;
    public BeedData_2 BD2;
    public TMP_Text Value_text;
    public static double value=0;
    public static decimal value1=0;
    public double d = 3.1482571;
    public int numberOfDecimalPlaces;
    public string decimalPlaceString;
    public bool once = true;
    void Start()
    {
        decimalPlaceString = "F2";
        numberOfDecimalPlaces = 2;
    }

    // Update is called once per frame
    void Update()
    {
        value = 0;
        value1 = 0;
        for(int i=0;i<BD1.beeds.Count;i++)
        {
            value = value + (Mathf.Pow(10, (BD1.beeds[i].x - numberOfDecimalPlaces))*BD1.beeds[i].state);     
            value1 = value1 + (decimal)(Mathf.Pow(10, (BD1.beeds[i].x - numberOfDecimalPlaces))*BD1.beeds[i].state);

        }
        once= false;
        for(int i=0;i<BD2.beeds.Count;i++)
        {
            value = value + (Mathf.Pow(10, (BD2.beeds[i].x - numberOfDecimalPlaces))*BD2.beeds[i].state);
            value1 = value1 + (decimal)(Mathf.Pow(10, (BD2.beeds[i].x - numberOfDecimalPlaces))*BD2.beeds[i].state);
        }

        Value_text.text = value.ToString(decimalPlaceString);


    }
}
