using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaceAtBottomRightOfScreen : MonoBehaviour
{
#if UNITY_EDITOR
    private Vector2 resolution;
#endif //UNITY_EDITOR

    public float threshold;
    public Text screenwidth;
    public Text screenHeight;
    public Text ScreenPixelWidth;
    public Text ScreenPixelHeight;
    public Text viewPortWidth;
    public Text viewPortHeight;

    void Start()
    {




    }




    private void OnEnable()
    {

#if UNITY_EDITOR
        resolution = new Vector2(Screen.width, Screen.height);
#endif //UNITY_EDITOR

        ChangeResolution();
    }

    void ChangeResolution()
    {
        //screenwidth.text = Screen.width.ToString();
        //screenHeight.text = Screen.height.ToString();
        //ScreenPixelWidth.text = Camera.main.pixelWidth.ToString();
        //ScreenPixelHeight.text = Camera.main.pixelHeight.ToString();
        //viewPortWidth.text = Camera.main.pixelRect.width.ToString();
        //viewPortHeight.text = "+"+Camera.main.pixelRect.height.ToString();
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.pixelWidth / Camera.main.pixelHeight; // basically height * screen aspect ratio
       // float Hight_intensity = Mathf.InverseLerp( 479, 1440, Camera.main.pixelHeight);
                                                                                 this.gameObject.transform.localScale = new Vector3(width*threshold / 2, width *threshold / 2, 1);


        //if (Hight_intensity < 0.3f)
        //{
        //    Hight_intensity = 0.3f;
        //    this.gameObject.transform.localScale = new Vector3(height / 2,height/2,1);
        //}
        //else
        //{
        //    this.gameObject.transform.localScale = Vector3.one * Hight_intensity * threshold;

        //}
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth*0.75f, Camera.main.pixelHeight*0.4f, Camera.main.nearClipPlane));
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
