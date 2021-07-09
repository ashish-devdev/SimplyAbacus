using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPaletManager : MonoBehaviour
{
    public static Color currentColor;
    public static int currentColorIndex;

    void OnEnable()
    {
        currentColor = new Color32(255, 0, 0,255);
        currentColorIndex = 1;

    }

    public void VoidMakeCuurentColorAsRed()
    {
        currentColor = new Color32(255, 0, 0,255);
        currentColorIndex = 1;

    }
    public void VoidMakeCuurentColorAsYellow()
    {
        currentColor = new Color32(255, 211, 0,255);
        currentColorIndex = 2;


    }
    public void VoidMakeCuurentColorAsBlue()
    {
        currentColor = new Color32(0, 170, 255,255);
        currentColorIndex = 3;


    }
    public void VoidMakeCuurentColorAsGreen()
    {
        currentColor = new Color32(17, 188, 0,255);
        currentColorIndex = 4;


    }
    public  void VoidMakeCuurentColorAsOrange()
    {
        currentColor = new Color32(255, 112, 0,255);
        currentColorIndex = 5;


    }
    public void VoidMakeCuurentColorAsBrown()
    {
        currentColor = new Color32(106, 58, 25,255);
        currentColorIndex = 6   ;


    }



    void Update()
    {

    }
}
