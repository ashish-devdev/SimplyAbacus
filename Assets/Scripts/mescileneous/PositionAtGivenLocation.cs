using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PositionAtGivenLocation : MonoBehaviour
{
#if UNITY_EDITOR
    private Vector2 resolution;
#endif //UNITY_EDITOR

    public float threshold;
    Text screenwidth;
    Text screenHeight;
    Text ScreenPixelWidth;
    Text ScreenPixelHeight;
    Text viewPortWidth;
    Text viewPortHeight;

    public float posX, posY;

    void OnEnable()
    {



#if UNITY_EDITOR
        resolution = new Vector2(Screen.width, Screen.height);
#endif //UNITY_EDITOR

        ChangeResolution();

    }

    void ChangeResolution()
    {

        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.pixelWidth / Camera.main.pixelHeight;

      //  this.gameObject.transform.localScale = new Vector3(width * threshold / 2, width * threshold / 2, 1);
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth * posX, Camera.main.pixelHeight * posY, Camera.main.nearClipPlane));
        this.gameObject.transform.localPosition = new Vector3(cameraPosition.x, cameraPosition.y, this.gameObject.transform.localPosition.z);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (resolution.x != Screen.width || resolution.y != Screen.height)
        {
            resolution.x = Screen.width;
            resolution.y = Screen.height;
            ChangeResolution();

        }
#endif //UNITY_EDITOR
    }
}
