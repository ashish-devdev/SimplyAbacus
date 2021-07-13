using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRandomColor : MonoBehaviour
{
    public RawImage img;
    public Color[] color;
    private void OnEnable()
    {
        img.color = color[Random.Range(0, color.Length)];
    }
}
