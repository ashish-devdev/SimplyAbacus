using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ResetBar : MonoBehaviour
{
    public static event Action OnReset;
    int draged = 1;
    Vector2 start, end;
    public bool swipe_SatredOn_Bar = false, swipe_EndedOn_Bar = false, swiped_Outside_Bar = false;
    Ray ray;
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
        // OnReset.AddListener(Invoke_OnReset);
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
        {
            ray = camera.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("ResetBar"))
                {
                    start.x = camera.ScreenToWorldPoint(Input.touches[0].position).x;
                    start.y = camera.ScreenToWorldPoint(Input.touches[0].position).y;
                    swipe_SatredOn_Bar = true;
                    swiped_Outside_Bar = false;
                    end.x = start.x;
                    end.y = start.y;

                }
                else
                {
                    swipe_SatredOn_Bar = false;

                }
            }
        }

        if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Moved)
        {
            end.x = camera.ScreenToWorldPoint(Input.touches[0].position).x;
            end.y = camera.ScreenToWorldPoint(Input.touches[0].position).y;
            ray = camera.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("ResetBar"))
                {
                    Debug.DrawRay(camera.transform.position, camera.ScreenToWorldPoint(Input.touches[0].position) * 1000, Color.red);
                }
                else
                {
                    swiped_Outside_Bar = true;

                }

            }
            else
            {
                swiped_Outside_Bar = true;
            }

        }






        if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
        {
            if (swiped_Outside_Bar == false && (end.x - start.x) >= 1 && swipe_SatredOn_Bar == true)
            {
                Invoke_OnReset();
            }
        }




    }


    void Invoke_OnReset()
    {
        OnReset();
    }
}
