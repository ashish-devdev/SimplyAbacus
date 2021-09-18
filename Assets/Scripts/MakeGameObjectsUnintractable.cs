using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeGameObjectsUnintractable : MonoBehaviour
{
    public  List<BoxCollider> abacusBeedss = new List<BoxCollider>();
    public  List<Button> buttonss;

    public static List<BoxCollider> abacusBeeds=new List<BoxCollider>();
    public static List<Button> buttons;

    private void Start()
    {
        abacusBeeds = abacusBeedss;
        buttons = buttonss;
    
    }

    public static void MakeAbacusIntractable()
    {
        for (int i = 0; i < abacusBeeds.Count; i++)
        {
            abacusBeeds[i].enabled = true;
        }    
    
    }

    public static void MakeAbacusUnintractable()
    {
        for (int i = 0; i < abacusBeeds.Count; i++)
        {
            abacusBeeds[i].enabled = false ;
        }

    }

    public static void MakeButtonsIntractable()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }

    }

    public static void MakeButtonsUnintractable()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = false;
        }

    }

    public static void MakeAllGameObjectsAndUiUnintractable()
    {
        MakeButtonsUnintractable();
        MakeAbacusUnintractable();
    }

    public static void MakeAllGameObjectsAndUiIntractable()
    {
        MakeButtonsIntractable();
        MakeAbacusIntractable();
    }

}
