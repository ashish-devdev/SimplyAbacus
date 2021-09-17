using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HovelHighlightColoringImages : MonoBehaviour
{
    HighlightEffect highlightEffect;
    Camera cam;
    Ray ray;
    Vector3 PaintPosition;

    // Start is called before the first frame update
    void Start()
    {
        highlightEffect = GetComponent<HighlightEffect>();
        cam = Camera.main;

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CancelInvoke(nameof(MouseExit));

            PaintPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y + (Screen.height * 10 / 100), Input.mousePosition.z);
            ray = cam.ScreenPointToRay(PaintPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    MouseOver();
                }
                else
                    MouseExit();
            }
            else
            {
                MouseExit();
            }
        }
        if (Input.GetMouseButtonUp(0))
        { 
            Invoke(nameof(MouseExit),0.1f);
        }


    }




    void MouseOver()
    {
        if (ChangeColorPaletHighlightPosition.isDraggedFromColorPalet)
        { 
        highlightEffect.enabled = true;
        }
        if (Input.GetMouseButtonUp(0))
            highlightEffect.enabled = false;
    }

    void MouseExit()
    {
        highlightEffect.enabled = false;

    }
    void OnMouseExit()
    {
        highlightEffect.enabled = false;

    }
}
