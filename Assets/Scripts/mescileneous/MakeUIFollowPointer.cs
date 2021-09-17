using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeUIFollowPointer : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    RectTransform rectTransform;
    Camera camMain;
    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        camMain = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {  // Vector3 a= camMain.ScreenToWorldPoint(Input.mousePosition);
            rectTransform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y+(Screen.height*10/100),10);//.x, camMain.ScreenToWorldPoint(Input.mousePosition).y, camMain.ScreenToWorldPoint(Input.mousePosition).z);
        }
    }
}
