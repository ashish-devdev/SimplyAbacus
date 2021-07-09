using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading_Bar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject loadingBar;
    Image loadingBar_rectTransform;
    public LeanGraphicColor changeColor1;
    public LeanGraphicColor changeColor2;
    public LeanGraphicColor changeColor3;
    public LeanGraphicColor changeColor4;
    public float initialWidth, newWidth;
    public bool widthChanged;

    void Start()
    {
        loadingBar_rectTransform = loadingBar.gameObject.GetComponent<Image>();
        initialWidth = loadingBar_rectTransform.fillAmount;
        newWidth = loadingBar_rectTransform.fillAmount;
    }

    void Update()
    {
        newWidth = loadingBar_rectTransform.fillAmount;
        if (newWidth == initialWidth)
        {
            widthChanged = false;
        }
        else
        {
            widthChanged = true;
        }

        if (loadingBar_rectTransform.fillAmount <= 0.25 && widthChanged)
        {
            changeColor1.BeginAllTransitions();
        }
        else if (loadingBar_rectTransform.fillAmount<= 0.5f && widthChanged)
        {
            changeColor2.BeginAllTransitions();

        }
        else if (loadingBar_rectTransform.fillAmount <= 0.75 && widthChanged)
        {
            changeColor3.BeginAllTransitions();
        }
        else if (loadingBar_rectTransform.fillAmount<= 1 && widthChanged)
        {
            changeColor4.BeginAllTransitions();
        }
        initialWidth = loadingBar_rectTransform.fillAmount;

    }


}
