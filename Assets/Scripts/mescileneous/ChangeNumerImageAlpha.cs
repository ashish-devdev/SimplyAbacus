using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNumerImageAlpha : MonoBehaviour
{
    float previousAlphaValue;

    public GameObject cell;
    public CanvasGroup numberImageParentCanvasGroup;
    CanvasGroup scrollCanvasGroup;
    private void OnEnable()
    {
        scrollCanvasGroup = cell.transform.parent.parent.GetComponent<CanvasGroup>();
    }
    /*
  public CanvasGroup ScrollCanvasGroup
    {
        get 
        {
            return scrollCanvasGroup;
        }

        set 
        {

            if (scrollCanvasGroup.alpha != previousAlphaValue)
            {
                previousAlphaValue = scrollCanvasGroup.alpha;
                numberImageParentCanvasGroup.alpha = scrollCanvasGroup.alpha;
            }
            scrollCanvasGroup = value;
            print("canvas group");
        }
    }

    */

    // Update is called once per frame
    void Update()
    {
        numberImageParentCanvasGroup.alpha=scrollCanvasGroup.alpha;
    }



    private void OnDisable()
    {
        numberImageParentCanvasGroup.alpha = 0;
    }
}
