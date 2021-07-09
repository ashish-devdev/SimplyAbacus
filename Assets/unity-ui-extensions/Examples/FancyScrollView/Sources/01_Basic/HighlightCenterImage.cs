using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightCenterImage : MonoBehaviour
{
    public List<Sprite> normalBoxImage;
    public List<Sprite> highlightBoxImage;
    RectTransform rectTransform;
    Image image;
    float anchorMinX;
    float anchorMaxX;
    string normalBoxImageName;
    string highlightBoxImageName;
    int index;
    public Image  outLine;

    private void OnEnable()
    {
        Invoke("OnEnableDelayInvoke", 0.01f);
       
    }

    private void LateUpdate()
    {
        try
        {
            if (image.sprite != null)
            {
                normalBoxImageName = image.sprite.name;
                for (int i = 0; i < normalBoxImage.Count; i++)
                {
                    if (normalBoxImage[i].name == normalBoxImageName)
                    {
                        index = i;
                    }
                }


                if (rectTransform.anchorMin.x > 0.42 && rectTransform.anchorMin.x < 0.55)
                {

                    if (image.sprite.name != highlightBoxImage[index].name)
                    {


                        image.sprite = highlightBoxImage[index];
                    }
                }
                else
                {
                    normalBoxImageName = image.sprite.name;
                    for (int i = 0; i < normalBoxImage.Count; i++)
                    {
                        if (normalBoxImage[i].name == normalBoxImageName)
                        {
                            index = i;
                        }
                    }
                    if (image.sprite.name != normalBoxImage[index].name)
                    {
                        image.sprite = normalBoxImage[index];
                    }
                }

            }

        }
        catch {; }
    }


    void OnEnableDelayInvoke()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        anchorMinX = rectTransform.anchorMin.x;
        anchorMaxX = rectTransform.anchorMax.x;
        if (image.sprite != null)
            normalBoxImageName = image.sprite.name;
        else
        {
            ;
        }
        for (int i = 0; i < normalBoxImage.Count; i++)
        {
            if (normalBoxImage[i].name == normalBoxImageName)
            {
                index = i;
            }
        }
        switch (index)
        {
            case 3:
                outLine.color = new Color(0.8353f, 0.159f, 0.72157f);
                break;
            case 4:
                outLine.color = new Color(0.1059f, 0.906f, 0.851f);
                break;
            case 5:
                outLine.color = new Color(0.718f, 0.53f, 0.302f);
                break;
            default:
                break;



        }


    }



}
