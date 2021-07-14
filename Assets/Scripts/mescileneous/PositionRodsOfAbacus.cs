using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PositionRodsOfAbacus : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject decimalDot;
    public int startingRod;
    public int endingRod;
    public ValueCalculator valueCalculator;
    public GameObject leftOuterPeice;
    public GameObject rightOuterPeice;
    public List<GameObject> Rods=new List<GameObject>();
    float orignalPositionOfLeftPeice;
    float orignalPositionOfRightPeice;

    Vector3 orinalPosOfDecimalDot;
    float[] orignalXPositions;
    float[] tempXPositions;
    int centralRodNum;
    public static event Action OnEditRod;


    void OnEnable()
    {
        orignalXPositions = new float[Rods.Count];
        for (int i = 0; i < Rods.Count; i++)
        {
            orignalXPositions[i] = Rods[i].transform.localPosition.x;
        }
        orignalPositionOfLeftPeice = leftOuterPeice.transform.localPosition.x;
        orignalPositionOfRightPeice = rightOuterPeice.transform.localPosition.x;
        orinalPosOfDecimalDot = decimalDot.transform.localPosition;


        //  print(orignalPositionOfRightPeice);
        centralRodNum = Rods.Count / 2;
        Invoke(nameof(DelayedEditRod), 0.05f);
    }
    public void DelayedEditRod()
    {
        EditRod();
    }

    public void EditRod()
    {

       

        if ((endingRod - startingRod) % 2 == 0)
        {
            positionPartyPoperAtLeftCorner.oddRod = true;
            OnEditRod?.Invoke();

        }
        else
        {
            positionPartyPoperAtLeftCorner.oddRod = false;
            OnEditRod?.Invoke();
        }

        int mid = (startingRod + endingRod) / 2;
        int shiftValue = mid - startingRod;

        int z = 0;
        for (int i = 0; i < Rods.Count; i++)
        {
            if (i >= startingRod && i <= endingRod)
            {





                Rods[i].transform.localPosition = new Vector3(orignalXPositions[centralRodNum - shiftValue + z], Rods[i].transform.localPosition.y, Rods[i].transform.localPosition.z);
                try
                {
                    if ((startingRod + endingRod) % 2 != 0)
                    {
                        leftOuterPeice.transform.localPosition = new Vector3(orignalXPositions[centralRodNum + shiftValue + 2]+0.6f, leftOuterPeice.transform.localPosition.y, leftOuterPeice.transform.localPosition.z);

                    }
                    else
                    { 
                        leftOuterPeice.transform.localPosition = new Vector3(orignalXPositions[centralRodNum + shiftValue + 1]+0.6f, leftOuterPeice.transform.localPosition.y, leftOuterPeice.transform.localPosition.z);
                    }

                }
                catch
                {
                    leftOuterPeice.transform.localPosition = new Vector3(orignalPositionOfLeftPeice, leftOuterPeice.transform.localPosition.y, leftOuterPeice.transform.localPosition.z);
                }


                try 
                {
                    if ((startingRod + endingRod) % 2 != 0)
                    {
                        rightOuterPeice.transform.localPosition = new Vector3(orignalXPositions[centralRodNum - shiftValue - 1]-0.6f, rightOuterPeice.transform.localPosition.y, rightOuterPeice.transform.localPosition.z);

                    }
                    else
                    {
                        rightOuterPeice.transform.localPosition = new Vector3(orignalXPositions[centralRodNum - shiftValue - 1]-0.6f, rightOuterPeice.transform.localPosition.y, rightOuterPeice.transform.localPosition.z);
                    }

                }
                catch 
                {
                    rightOuterPeice.transform.localPosition = new Vector3(orignalPositionOfRightPeice, rightOuterPeice.transform.localPosition.y, rightOuterPeice.transform.localPosition.z);
                }

                z++;

                //Rods[i].transform.localPosition = new Vector3(orignalXPositions[i - startingRod], Rods[i].transform.localPosition.y, Rods[i].transform.localPosition.z);
                //try
                //{
                //    leftOuterPeice.transform.localPosition = new Vector3(orignalXPositions[i - startingRod + 1], leftOuterPeice.transform.localPosition.y, leftOuterPeice.transform.localPosition.z);
                //}
                //catch
                //{
                //    leftOuterPeice.transform.localPosition = new Vector3(orignalPositionOfLeftPeice, leftOuterPeice.transform.localPosition.y, leftOuterPeice.transform.localPosition.z);

                //}
                Rods[i].SetActive(true);
            }
            else
            {
                Rods[i].SetActive(false);
            }
        }


        tempXPositions = new float[Rods.Count];
        for (int i = 0; i < Rods.Count; i++)
        {
            tempXPositions[i] = Rods[i].transform.localPosition.x;
        }

        decimalDot.transform.localPosition = new Vector3(Rods[valueCalculator.numberOfDecimalPlaces].transform.localPosition.x-0.25f, decimalDot.transform.localPosition.y, decimalDot.transform.localPosition.z);

    }

    public void PlaceAtRightOfMiddle(int midle)
    {

    }


    private void OnDisable()
    {
        for (int i = 0; i < Rods.Count; i++)
        {

            Rods[i].transform.localPosition = new Vector3(orignalXPositions[i], Rods[i].transform.localPosition.y, Rods[i].transform.localPosition.z);
            Rods[i].SetActive(true);
            leftOuterPeice.transform.localPosition = new Vector3(orignalPositionOfLeftPeice, leftOuterPeice.transform.localPosition.y, leftOuterPeice.transform.localPosition.z);
            rightOuterPeice.transform.localPosition = new Vector3(orignalPositionOfRightPeice, rightOuterPeice.transform.localPosition.y, rightOuterPeice.transform.localPosition.z);

        }
        decimalDot.transform.localPosition = new Vector3(orinalPosOfDecimalDot.x, orinalPosOfDecimalDot.y, orinalPosOfDecimalDot.z);
    }


}
