using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HovelHighlightColoringImages : MonoBehaviour
{
    HighlightEffect highlightEffect;


    // Start is called before the first frame update
    void Start()
    {
        highlightEffect = GetComponent<HighlightEffect>();


    }

    void OnMouseOver()
    {
        if(ChangeColorPaletHighlightPosition.isDraggedFromColorPalet)
        highlightEffect.enabled = true;
        if (Input.GetMouseButtonUp(0)) 
        highlightEffect.enabled = false;
    }

    void OnMouseExit()
    {
        highlightEffect.enabled = false;

    }
}
