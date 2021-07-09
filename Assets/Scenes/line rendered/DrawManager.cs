using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public GameObject drawPrefab;
    public GameObject CongratulationCanvas;
    GameObject trail;
    Plane planeObj;
    private Vector3 startPos;

    void Start()
    {
        planeObj = new Plane(Camera.main.transform.forward * -1, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!CongratulationCanvas.activeInHierarchy)
        {


            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
            {
                trail = (GameObject)Instantiate(drawPrefab, this.transform.position, Quaternion.identity);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (planeObj.Raycast(ray, out float hit))
                {
                    print("hrllo");
                    startPos = ray.GetPoint(hit);
                }


            }
            else if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (planeObj.Raycast(ray, out float hit))
                {
                    print("hrllo");
                    trail.transform.position = ray.GetPoint(hit);
                }


            }
        }

    }
}
