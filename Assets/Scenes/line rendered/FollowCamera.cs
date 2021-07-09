using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject CongratulationCanvas;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        { 
        
        
                Vector3 temp = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y,0);
                temp.z = this.transform.position.z;
                this.transform.position = cam.ScreenToWorldPoint(temp);
        print(Input.mousePosition + "camra val");
        }
            

    }
}
