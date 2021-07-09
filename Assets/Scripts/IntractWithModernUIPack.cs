using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IntractWithModernUIPack : MonoBehaviour
{
    public ButtonManager buttonManagerBasic;

    public void ChangeButtonName(string text)
    {
        buttonManagerBasic.buttonText = text;
    }

}
