using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangActivityeBackground : MonoBehaviour
{
    public  List<Sprite> backgrounds;
    int totalBackgrounds;
    public Image backgroundImage; 
    void Start()
    {
        totalBackgrounds = backgrounds.Count;
    }

    public void ChangeBackground()
    {
        backgroundImage.sprite = backgrounds[Random.Range(0, backgrounds.Count - 1)];
    }

    void Update()
    {
        
    }
}
