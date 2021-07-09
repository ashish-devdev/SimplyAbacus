using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;

public class Reset : MonoBehaviour
{
    public BeedData_1 BD1;
    public BeedData_2 BD2;
    public AudioClip reset_Audio;


    public void RESET()
    {
        for (int i = 0; i < BD1.BEEDS.Length; i++)
        {
            BD1.beeds[i].x = i / 4;
            BD1.beeds[i].y = i % 4;
            BD1.beeds[i].state = 0;
            BD1.BEEDS[i].gameObject.transform.localPosition= new Vector3(-0.2795157f, ((i % 4)* 0.708428f) + 2.255421f, -9.051434f);
        }

        for (int i = 0; i < BD2.BEEDS.Length; i++)
        {
            BD2.beeds[i].x = i;
            BD2.beeds[i].y = 0;
            BD2.beeds[i].state = 0;
            BD2.BEEDS[i].gameObject.transform.localPosition= new Vector3(-0.2795157f, 6.987983f, -9.051434f);
        }
       // SoundManager.Instance.Play(reset_Audio);


        
    }


    void Start()
    {
        ResetBar.OnReset+=RESET;
    }



   
}
