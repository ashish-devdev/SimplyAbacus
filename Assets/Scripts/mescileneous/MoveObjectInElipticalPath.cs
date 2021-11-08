using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObjectInElipticalPath : MonoBehaviour
{

    public float a, b;
    public Image image;
    public float speed;
    public float tempX, tempY;
    float A, B, t = 0f;

    // Update is called once per frame
    private void OnEnable()
    {
        A = a;
    }

    void Update()
    {
        //t = t + (speed * Time.deltaTime);
        //if (t >= 1 && moveForward)
        //{
        //    moveForward = false;
        //    t = 0;
        //}

        //if (t >= 1 && !moveForward)
        //{
        //    moveForward = true;
        //    t = 0;
        //}


        //if (moveForward)
        //    A = Mathf.Lerp(a, -a, t);
        //else
        //{
        //    A = Mathf.Lerp(-a, a, t);
        //}

        #region commented
        //t2 = t2 + (speed * Time.deltaTime);
        //if (t2 >= 1 && moveUpward)
        //{
        //    moveUpward = false;
        //    t2 = 0;
        //}

        //else if (t2 >= 1 && !moveUpward)
        //{
        //    moveUpward = true;
        //    t2 = 0;
        //}


        //if (moveUpward)
        //    B = Mathf.Lerp(b, -b, t2);
        //else
        //{
        //    B = Mathf.Lerp(-b, b, t2);
        //}

        //if (!moveUpward)
        //{
        //    A = Mathf.Sqrt((1 - (Mathf.Pow(B, 2) / Mathf.Pow(b, 2))) * Mathf.Pow(a, 2));
        //}

        //if (moveUpward)
        //{
        //    A = -Mathf.Sqrt((1 - (Mathf.Pow(B, 2) / Mathf.Pow(b, 2))) * Mathf.Pow(a, 2));
        //}
        #endregion
        t = t + (speed * Time.deltaTime);


        A = tempX + Mathf.Cos(t) * a; // a is the Radius in the x direction
        B = tempY + Mathf.Sin(t) * b; // b is the  Radius in the y direction

        image.rectTransform.anchoredPosition = new Vector2(A, B);
    }
    private void LateUpdate()
    {

    }
}
