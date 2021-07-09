using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetProperPosOfResult : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform myRectTransform;

    private void OnEnable()
    {
        myRectTransform=GetComponent<RectTransform>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myRectTransform.localPosition = new Vector3(myRectTransform.localPosition.x, -50, myRectTransform.localPosition.z); 
    }
}
