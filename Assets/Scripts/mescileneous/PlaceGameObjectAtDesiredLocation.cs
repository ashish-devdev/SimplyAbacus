using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceGameObjectAtDesiredLocation : MonoBehaviour
{

    public GameObject entityToPlace;
    public bool usingPercentage;
    public float x, y, z;
    float X, Y;
    public Position positionOnScreen;
    public enum Position
    {
        topLeft,
        topCenter,
        topRight,
        middleLeft,
        middleCenter,
        middleRight,
        bottomLeft,
        bottomCenter,
        bottomRight,
    }


    private void OnEnable()
    {

        Invoke(nameof(DelayedInvokeOnEnable), 0.1f);
    }

    public void DelayedInvokeOnEnable()
    {
        if (usingPercentage)
        {

            X = Camera.main.pixelWidth * x;
            Y = Camera.main.pixelHeight * y;

            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(X, Y, Camera.main.nearClipPlane));
            if (z == -1)
                entityToPlace.transform.position = p;
            else
            {
                entityToPlace.transform.position = new Vector3(p.x, p.y, z);

            }

        }
        else
        {
            Vector3 positionOfScreenPoints;
            if (positionOnScreen == Position.topLeft)
            {
                X = 0;
                Y = 1;
            }
            else if (positionOnScreen == Position.topCenter)
            {
                X = 0.5f;
                Y = 1;
            }
            else if (positionOnScreen == Position.topRight)
            {
                X = 1;
                Y = 1;
            }
            else if (positionOnScreen == Position.middleLeft)
            {
                X = 0;
                Y = 0.5f;
            }
            else if (positionOnScreen == Position.middleCenter)
            {
                X = 0.5f;
                Y = 0.5f;
            }
            else if (positionOnScreen == Position.middleRight)
            {
                X = 1;
                Y = 0.5f;
            }
            else if (positionOnScreen == Position.bottomLeft)
            {
                X = 0;
                Y = 0;
            }
            else if (positionOnScreen == Position.bottomCenter)
            {
                X = 0.5f;
                Y = 0;
            }
            else if (positionOnScreen == Position.bottomRight)
            {
                X = 1;
                Y = 0;
            }


            positionOfScreenPoints = Camera.main.ScreenToWorldPoint(new Vector3(X* Camera.main.pixelWidth, Y* Camera.main.pixelHeight, Camera.main.nearClipPlane));
            entityToPlace.transform.position = new Vector3(positionOfScreenPoints.x+x, positionOfScreenPoints.y + y, z);
        }
    }



}
