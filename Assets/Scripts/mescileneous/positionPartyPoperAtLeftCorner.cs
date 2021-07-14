using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionPartyPoperAtLeftCorner : MonoBehaviour
{
    public  GameObject ParentAbacus;
    Vector3 oldParentAbacusScale;
    Vector3 oldAbacusScale;

    public  GameObject abacus;
    public float x, y;
    float X, Y;
    public static bool oddRod; 
    private void OnEnable()
    {
        X = Camera.main.pixelWidth * x;
        Y = Camera.main.pixelHeight * y;

        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(X, Y, Camera.main.nearClipPlane));
        gameObject.transform.position = p;
    }

    private void Start()
    {
        PositionRodsOfAbacus.OnEditRod += ADJUSTABACUSPOSITIONBASEDONNUMBEROFRODS;
    }

    public void ADJUSTABACUSPOSITIONBASEDONNUMBEROFRODS()
    {
        if (gameObject.GetComponent<positionPartyPoperAtLeftCorner>().enabled)
        {

        if (oddRod)
            x = 0.517f;
        else
            x = 0.535f;

        X = Camera.main.pixelWidth * x;
        Y = Camera.main.pixelHeight * y;

        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(X, Y, Camera.main.nearClipPlane));
        gameObject.transform.position = p;
        }
    }

  



    public void ChangeAbacusSizeForPLacingAtCenter()
    {



        Invoke("InvokeChangeAbacusSizeForPLacingAtCenter", 0.07f);

    }

    public void ChangeAbacusSizeForPLacingAtCenter2()
    {



        Invoke("InvokeChangeAbacusSizeForPLacingAtCenter2", 0.07f);

    }


    void InvokeChangeAbacusSizeForPLacingAtCenter()
    {

        ParentAbacus.transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
        abacus.transform.localScale = new Vector3(1, 1, 1);
        X = Camera.main.pixelWidth * x;
        Y = Camera.main.pixelHeight * y;

        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(X, Y, Camera.main.nearClipPlane));
        gameObject.transform.position = p;
    }
    void InvokeChangeAbacusSizeForPLacingAtCenter2()
    {

        ParentAbacus.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        abacus.transform.localScale = new Vector3(1, 1, 1);
        X = Camera.main.pixelWidth * x;
        Y = Camera.main.pixelHeight * y;

        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(X, Y, Camera.main.nearClipPlane));
        gameObject.transform.position = p;
    }



    private void OnDisable()
    {
        if (ParentAbacus != null)
        {

            ParentAbacus.transform.localScale = new Vector3(1, 1, 1);
            abacus.transform.localScale = new Vector3(0.8f , 0.8f, 1);
        }
    }



}
