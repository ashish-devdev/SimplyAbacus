using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightedGlowAnimation : MonoBehaviour
{
    public Image img;
    Color color;
    float time;
    bool alphaIsIncreasing;
    LeanEvent leanEvent;
    private void OnEnable()
    {
        time = 0;
        img.color = new Vector4(img.color.r, img.color.g, img.color.b, 0);
        alphaIsIncreasing = true;
    }
    void Update()
    {
        leanEvent.BeginAllTransitions();


        if (alphaIsIncreasing)
        {
            time++;

            img.color = new Vector4(img.color.r, img.color.g, img.color.b, (Time.deltaTime * time)/2);
            print(Time.deltaTime * time);
            if (Time.deltaTime * time > 2f)
            {
                time = 0;
                alphaIsIncreasing = false;
            }


        }
        else
        {
            time++;
            img.color = new Vector4(img.color.r, img.color.g, img.color.b,1- (Time.deltaTime * time)/2);
            print(1-Time.deltaTime * time);

            if (Time.deltaTime * time > 2f)
            {
                time = 0;
                alphaIsIncreasing = true;
            }



        }



       

    }
}
