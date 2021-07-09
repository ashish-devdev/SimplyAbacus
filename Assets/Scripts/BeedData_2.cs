using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeedData_2 : MonoBehaviour
{
    public GameObject[] BEEDS;
    public List<Beeds> beeds=new List<Beeds>();

    private void Start()
    {
        for (int i = 0; i < beeds.Count; i++)
        {
            beeds[i].beed = BEEDS[i];
            beeds[i].x = i;
            beeds[i].y = 0;
            beeds[i].state = 0;
        }
            
    }


    [System.Serializable]
    public class Beeds
    {
        public GameObject beed;
        public int x, y, state;
    }
}
