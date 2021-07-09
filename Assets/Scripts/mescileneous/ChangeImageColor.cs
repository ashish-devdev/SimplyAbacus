using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageColor : MonoBehaviour
{
    public List<Color> coloR;
    int totalChild;
    private void OnEnable()
    {
        totalChild = transform.childCount;
        for (int i = 0; i < totalChild; i++)
        {
            if (coloR.Count != 0)
            { 
            transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = coloR[i % coloR.Count];
            transform.GetChild(i).transform.GetChild(0).GetComponent<ParticleSystem>().startColor = coloR[i % coloR.Count];
            }
        }
    }
}
