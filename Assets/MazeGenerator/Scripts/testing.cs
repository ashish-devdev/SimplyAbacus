using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    // Start is called before the first frame update

    
    void OnEnable()
    {
        Vector3 screen2Wrld = Camera.main.ScreenToWorldPoint(new Vector3(0f * Camera.main.pixelWidth, 0f * Camera.main.pixelHeight, 1));
        this.gameObject.transform.localPosition = new Vector3(screen2Wrld.x, screen2Wrld.y, screen2Wrld.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
