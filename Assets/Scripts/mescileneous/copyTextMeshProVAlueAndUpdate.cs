using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class copyTextMeshProVAlueAndUpdate : MonoBehaviour
{
    public TextMeshProUGUI operationResult;
    public TextMeshProUGUI self;
    public bool isAbacusValue;
   public  bool ismultiplication;
    public bool ishandCounting;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!ismultiplication)
        if (operationResult.text.Contains("="))
        {
            operationResult.text= operationResult.text.Replace("=", "");
        }
        self.text = "" + operationResult.text;
        if(ismultiplication)
            self.text = "Result: " + operationResult.text;

        if (isAbacusValue)
        {
            self.text =  operationResult.text;

        }

        if (ishandCounting)
        {
            self.text = decimal.Parse(operationResult.text).ToString("F0");
        }
    }
}
