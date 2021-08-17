using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeTheGivenObjectasedOnSCreen : MonoBehaviour
{

    public float x,y;
    public float z;



    private void OnEnable()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.pixelWidth / Camera.main.pixelHeight;
        this.gameObject.transform.localScale = new Vector3(width * x / 2, width * y / 2, z);
    }
}
