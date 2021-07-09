using Lean.Gui;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Count_1_o_4_test : MonoBehaviour
{
    public GameObject[] Beeds;
    public SpriteRenderer[] images;
    public string[] informations;
    public int CurrentProblem;
    Ray ray;
    int value;
    public bool[] subtaskCompleted;
    int currentSubtask;
    bool waitingForAnimation;
    public TextMeshProUGUI tempAbacusValue;
    public GameObject NotificationPannel;
    public TextMeshProUGUI NotificationText;  
    public GameObject CongratulationPannel;
    public TextMeshProUGUI CongratulationText;
    public GameObject SideNote;
    public TextMeshProUGUI SideNoteText;
    public Button btn;
    public LeanPulse Animation;

    bool temp;
    private void OnEnable()
    {
        currentSubtask = 0;
        value = 0;
        CurrentProblem = 0;
        waitingForAnimation = false;
        btn.onClick.AddListener(OpenSideNote);
        NotificationText.text = informations[CurrentProblem];
        SideNoteText.text = informations[CurrentProblem];

    }



    private void Start()
    {
        subtaskCompleted = new bool[4];
    }

    private void Update()
    {
        if (CurrentProblem >= 4 && temp==false)
        {
            Invoke("ShowCongratulationMesssage", 0.5f);
            temp = true;
        }


        if (Input.GetMouseButtonDown(0))
        {
            CancelInvoke("DelayedInvokeSpriteAnimation");
            for (int i = 0; i < images.Length; i++)
            {
                images[i].enabled = false;
                Animation.RemainingPulses = 0;
            }

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hit = new RaycastHit[1];
            Physics.RaycastNonAlloc(ray, hit);



            if (hit[0].collider != null && hit[0].collider.gameObject.CompareTag("beed") && waitingForAnimation == false)
            {
                CurrentTask(CurrentProblem);
                if (currentSubtask >= 4)
                    ChangeCurrentTask();
            }
        }
        else
        {
            Invoke("DelayedInvokeSpriteAnimation", 1f);
        }
    }
    int z;
    public void CurrentTask(int j)
    {
        //for (int i = 0; i < j+1; i++)
        //{
        //}

        value++;
        Beeds[z].GetComponent<MeshRenderer>().enabled = true;
        tempAbacusValue.text = value.ToString();

        z++;

        if (z > j)
        {
            z = 0;
            waitingForAnimation = true;
            Invoke("InvokeDelayedAnimation", 0.5f);
            currentSubtask++;

        }
        //    Beeds[j].GetComponent<MeshRenderer>().enabled = true;

        //value++;
        //Invoke("InvokeDelayedAnimation", 0.5f);
        //tempAbacusValue.text = value.ToString();
        //currentSubtask++;
        //subtaskCompleted[CurrentProblem] = true;

        //Beeds[j].GetComponent<MeshRenderer>().enabled = true;



    }
    private void ChangeCurrentTask()
    {

        CurrentProblem++;

        subtaskCompleted = new bool[4];
        waitingForAnimation = true;
        currentSubtask = 0;
        Invoke("InvokeDelayedAnimation", 0.5f);
        Invoke("InvokeDelayedNotification", 0.5f);

    }

    public void InvokeDelayedAnimation()
    {

            for (int i = 0; i < Beeds.Length; i++)
            {

                Beeds[i].GetComponent<MeshRenderer>().enabled = false;
                print("true");
            }

            value = 0;
            tempAbacusValue.text = value.ToString();
            waitingForAnimation = false;
        
    }

    void InvokeDelayedNotification()
    {
        waitingForAnimation = true;
        NotificationText.text = informations[CurrentProblem];
        NotificationPannel.SetActive(true);
        SideNoteText.text = informations[CurrentProblem];
        SideNote.gameObject.SetActive(false);


    }

    private void OpenSideNote()
    {
        for (int i = 0; i < Beeds.Length; i++)
        {

            Beeds[i].GetComponent<MeshRenderer>().enabled = false;
            print("true");
        }
        SideNote.SetActive(true);
        waitingForAnimation = false;
    }

    private void OnDisable()
    {
        btn.onClick.RemoveListener(OpenSideNote);
        waitingForAnimation = false;


    }

    void DelayedInvokeSpriteAnimation()
    {
        CancelInvoke("DelayedInvokeSpriteAnimation");
        Animation.RemainingPulses = 3;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].enabled = true;
        }

    }
    void ShowCongratulationMesssage()
    {
        CancelInvoke("ShowCongratulationMesssage");
        CongratulationPannel.SetActive(true);
        CongratulationText.text = "Congratulation you have successfully completetd this section";
        SideNote.SetActive(false);
    }
}
