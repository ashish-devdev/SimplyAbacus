using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAlphaValue : MonoBehaviour
{
    public List<GameObject> objectList; 
    Image img;
    private void OnEnable()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i].activeInHierarchy)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
                break;
            }
            else
            { 
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0.5f);

            }
        }
    }
}
