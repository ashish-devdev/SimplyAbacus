using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSizeAndPositionOfAbacus : MonoBehaviour
{
    Transform abacusParentTransform;
    //Transform abacusTransform;
    Vector3 initialScale;
    Vector3 initialPosition;

    public GameObject abacusParent;
    public GameObject abacus;

    public GameObject[] rods;

    public bool keepAbacusShrinked;

    private void OnEnable()
    {
        abacusParentTransform = abacusParent.transform;
       // abacusTransform = abacus.transform;
        initialPosition = abacusParentTransform.localPosition;
        initialScale = abacusParentTransform.localScale;
    }

    private void LateUpdate()
    {
        ChangeSizeOfAbacus();
    }

    public void ChangeSizeOfAbacus()
    {
        int countOfRodsEnabled = 0;

        for (int i = 0; i < rods.Length; i++)
        {
            if (rods[i].activeInHierarchy)
                countOfRodsEnabled++;

        }

        if (countOfRodsEnabled > 5|| keepAbacusShrinked)   //i added is mixedOpration to make the abacus stay in size in mixed operation even if the operation value is below 5
        {
            abacusParentTransform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
            abacusParentTransform.localPosition = new Vector3(0.93f, 1.82f, 0.41f);
        }
        else
        {
            abacusParentTransform.localScale = initialScale;
            abacusParentTransform.localPosition = initialPosition;

        }


    }

    private void OnDisable()
    {
        abacusParentTransform.localScale = initialScale;
        abacusParentTransform.localPosition =initialPosition;
    }


}
