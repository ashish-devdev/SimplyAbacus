using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFontSize : MonoBehaviour
{
    public Text textResult;
    string initialText;

    void Start()
    {
        initialText = textResult.text;
    }


    void Update()
    {
        if (string.Compare(textResult.text, initialText) != 0)
        {
            textResult.fontSize = 300;        
        }
    }
}
