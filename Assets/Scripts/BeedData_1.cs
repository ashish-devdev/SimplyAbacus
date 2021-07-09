using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeedData_1 : MonoBehaviour
{
    public GameObject[] BEEDS;
    public List<Beeds> beeds = new List<Beeds>();

    void Start()
    {
        for (int i = 0; i < beeds.Count; i++)
        {
            Beeds Beed = new Beeds()
            {   
                beed =BEEDS[i],
                x = i / 4,
                y = i % 4,
                state = 0
            };

            beeds[i] = Beed;
        }
    }

    [System.Serializable]
    public class Beeds
    {   
        public GameObject beed;
        public int x, y,state;      
    }
}

