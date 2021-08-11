using Lean.Gui;
using Lean.Transition.Method;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Count_1_to_4 : MonoBehaviour
{
    public Activity activityScriptInstance;
    ActivityList1 activityList1;
    ActivityList2 activityList2;
    public LeanPulse leanPulseAnimation;
    public LeanPulse LeanResetAnimation;
    public SpriteRenderer animatingSprite;
    public SpriteRenderer indexAnimatingSprite;
    public LeanImageFillAmount loadingBar;

    public LeanEvent resetCount1Animation;
    public LeanEvent resetCount2Animation;
    public LeanEvent resetCount3Animation;
    public LeanEvent resetCount4Animation;
    public TextMeshProUGUI Notification;
    public TextMeshProUGUI sidenoteText;
    public TextMeshProUGUI Congratulation;

    public GameObject NotificationPannel;
    public Button NotificationBtn;
    public GameObject CongratulationPannel;


    public GameObject sideNote;
    public GameObject beed1;
    public GameObject beed2;
    public GameObject beed3;
    public GameObject beed4;
    public GameObject[] Beeds;
    public GameObject AnimatingSpriteGameObject;

    public GameObject Highlight1;
    public GameObject Highlight2;
    public GameObject ResetHighlight;


    public List<string> notificationData;
    public string CongratulationText;
    public bool[] completedSubTask;
    bool temp;
    int currentTask, currentSubTask;
    public LeanEvent currentResetAnimation;
    float completeBarValue = 500;

    public LeanToggle congratulationLean;
    public LeanToggle sideNoteLean;
    public LeanToggle notificationLean;

    public List<GameObject> noMoveMentBeeds;
    public List<GameObject> MoveMentBeeds;

    public ValueCalculator valueCalculator;
    int SubTaskToSave;

    void OnEnable()
    {
        Invoke(nameof(OnEnableAfterDelay), 0.1f);
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].liftBeed01 == true)
                    {
                        activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
                        activityList2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j];

                    }
                }
            }
        }

        if (activityList2.liftingBeed21.currentSubActivity >= 4)
        {
            activityList2.liftingBeed21.currentSubActivity = 0;
        }
        SubTaskToSave = activityList2.liftingBeed21.currentSubActivity;

        for (int p = 0; p < Count_1_to_4_ModelData.TaskComplete.Length; p++)
        {

            Count_1_to_4_ModelData.TaskComplete = Count_1_to_4_ModelData.TaskComplete.Select(x => x = false).ToArray();
            Count_1_to_4_ModelData.barValue = 0;

        }

        for (int i = 0; i < activityList2.liftingBeed21.currentSubActivity; i++)
        {
            Count_1_to_4_ModelData.TaskComplete[i] = true;
            Count_1_to_4_ModelData.barValue = (i+1)*0.25f;

        }

        if (Count_1_to_4_ModelData.TaskComplete[Count_1_to_4_ModelData.TaskComplete.Length - 1] == true)
        {
            Count_1_to_4_ModelData.TaskComplete = Count_1_to_4_ModelData.TaskComplete.Select(x => x = false).ToArray();
            Count_1_to_4_ModelData.barValue = 0;
        }
        completedSubTask = new bool[4];
        currentSubTask = 0;
        temp = false;
        loadingBar.Data.FillAmount = Count_1_to_4_ModelData.barValue;
        loadingBar.BeginAllTransitions();

        Highlight1.SetActive(true);
        NotificationBtn.onClick.AddListener(DelayedInvokeAnimation);
        NotificationBtn.onClick.AddListener(OpenSideNote);
        //Highlight2.SetActive(true);

        ResetHighlight.transform.SetParent(beed1.transform);
        indexAnimatingSprite.transform.SetParent(beed1.transform);
        ResetHighlight.transform.localPosition = new Vector3(0, 0, 0);
        indexAnimatingSprite.transform.localPosition = new Vector3(-0.82f, -2.22f, 2f);
        for (currentTask = 0; currentTask < Count_1_to_4_ModelData.TaskComplete.Length; currentTask++)
        {
            if (Count_1_to_4_ModelData.TaskComplete[currentTask] == false)
            {
                sidenoteText.text = notificationData[currentTask];

                Highlight1.transform.SetParent(Beeds[currentTask].transform);
                Highlight1.transform.localPosition = new Vector3(0, 0, 0);

                AnimatingSpriteGameObject.transform.SetParent(Beeds[currentTask].transform);
                AnimatingSpriteGameObject.transform.localPosition = new Vector3(-2.7f, 0, 2);
                break;
                #region abc
                //if (currentTask == 1)
                //{
                //    Highlight1.transform.SetParent(beed2.transform);
                //    Highlight1.transform.localPosition = new Vector3(0, 0, 0);

                //    AnimatingSpriteGameObject.transform.SetParent(beed2.transform);
                //    AnimatingSpriteGameObject.transform.localPosition = new Vector3(1, 0, -1);
                //    break;
                //}
                //if (currentTask == 2)
                //{
                //    Highlight1.transform.SetParent(beed3.transform);
                //    Highlight1.transform.localPosition = new Vector3(0, 0, 0);

                //    AnimatingSpriteGameObject.transform.SetParent(beed3.transform);
                //    AnimatingSpriteGameObject.transform.localPosition = new Vector3(1, 0, -1);
                //    break;
                //}
                //if (currentTask == 3)
                //{
                //    Highlight1.transform.SetParent(beed4.transform);
                //    Highlight1.transform.localPosition = new Vector3(0, 0, 0);

                //    AnimatingSpriteGameObject.transform.SetParent(beed4.transform);
                //    AnimatingSpriteGameObject.transform.localPosition = new Vector3(1, 0, -1);
                //    break;
                //}
                #endregion
            }
        }

        for (int i = 0; i < noMoveMentBeeds.Count; i++)
        {
            noMoveMentBeeds[i].GetComponent<BoxCollider>().enabled = false;
        }
        for (int i = 0; i < MoveMentBeeds.Count; i++)
        {
            MoveMentBeeds[i].GetComponent<BoxCollider>().enabled = false;
        }


    }

    public void OnEnableAfterDelay()
    {
        valueCalculator.decimalPlaceString = "F0";
    }

    void Update()
    {
        //loadingBar.sizeDelta = new Vector2(Count_1_to_4_ModelData.barValue, loadingBar.sizeDelta.y);
        /*   if (NotificationPannel.activeInHierarchy|| CongratulationPannel.activeInHierarchy)
           {
               animatingSprite.enabled = false;

           }*/
        //  else { animatingSprite.enabled = true; }

        if (ValueCalculator.value == 0)
        {
            indexAnimatingSprite.transform.gameObject.SetActive(false);
            temp = false;
            ResetHighlight.SetActive(false);
            animatingSprite.transform.gameObject.SetActive(true);
            //animatingSprite.enabled = true;



        }
        else
        {
            CancelInvoke("DelayedInvokeAnimation");
            Highlight1.SetActive(false);
            if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended))
            {
                Invoke("DelyedInvoke", 0.5f);

            }

            else if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
            {
                LeanResetAnimation.RemainingPulses = 0;
                animatingSprite.transform.gameObject.SetActive(false);
                indexAnimatingSprite.transform.gameObject.SetActive(false);

                //Invoke("DelyedInvoke",0.5f);
            }
        }

        for (currentTask = 0; currentTask < Count_1_to_4_ModelData.TaskComplete.Length; currentTask++)
        {
            if (Count_1_to_4_ModelData.TaskComplete[currentTask] == false)
            {
                Notification.text = notificationData[currentTask];

                break;
            }


        }
        SubTaskToSave = currentTask;
        if (currentTask == Count_1_to_4_ModelData.TaskComplete.Length)
        {
            Congratulation.text = CongratulationText;
        }

        if (currentTask == 0)
        {
            for (int i = 0; i < MoveMentBeeds.Count; i++)
            {
                MoveMentBeeds[i].GetComponent<BoxCollider>().enabled = false;
            }
            MoveMentBeeds[0].GetComponent<BoxCollider>().enabled = true;

            if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended))
            {
                if (NotificationPannel.activeInHierarchy == false)
                    Invoke("DelayedInvokeAnimation", 1f);
            }
            else if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
            {
                leanPulseAnimation.RemainingPulses = 0;
                animatingSprite.enabled = false;
                CancelInvoke("DelayedInvokeAnimation");
            }



            if (ValueCalculator.value == 1 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {



                temp = true;
                completedSubTask[currentSubTask] = true;
                currentSubTask++;

                //currentResetAnimation = resetCount1Animation;
                //Invoke("DelyedInvoke", 0.5f);
                //if (currentSubTask == 4 && ValueCalculator.value == 0)
                //{
                //    Count_1_to_4_ModelData.barValue = 0.25f * completeBarValue;
                //    loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
                //    loadingBar.BeginAllTransitions();
                //    Invoke("DelayedInvokeNotification", 1f);
                //    Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
                //    currentSubTask = 0;
                //    completedSubTask = completedSubTask.Select(x => x = false).ToArray();



                //}

            }
            if (currentSubTask == 2 && ValueCalculator.value == 0)
            {
                Count_1_to_4_ModelData.barValue = 0.25f * 1;
                loadingBar.Data.FillAmount = Count_1_to_4_ModelData.barValue;
                loadingBar.BeginAllTransitions();
                Invoke("DelayedInvokeNotification", 0.2f);
                activityList1.liftBeed01[currentTask] = true;
                Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
                currentSubTask = 0;
                completedSubTask = completedSubTask.Select(x => x = false).ToArray();



            }
        }

        else if (currentTask == 1)
        {
            for (int i = 0; i < MoveMentBeeds.Count; i++)
            {
                MoveMentBeeds[i].GetComponent<BoxCollider>().enabled = false;
            }
            MoveMentBeeds[1].GetComponent<BoxCollider>().enabled = true;


            if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended))
            {
                if (NotificationPannel.activeInHierarchy == false)
                    Invoke("DelayedInvokeAnimation", 1f);
            }
            else if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
            {
                leanPulseAnimation.RemainingPulses = 0;
                animatingSprite.enabled = false;
                CancelInvoke("DelayedInvokeAnimation");
            }

            if (ValueCalculator.value == 2)
            {
                for (int i = 0; i < MoveMentBeeds.Count; i++)
                {
                    MoveMentBeeds[i].GetComponent<BoxCollider>().enabled = false;
                }
                MoveMentBeeds[0].GetComponent<BoxCollider>().enabled = true;
            }

            if (ValueCalculator.value == 2 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {



                temp = true;
                completedSubTask[currentSubTask] = true;
                currentSubTask++;
                //currentResetAnimation = resetCount2Animation;
                //Invoke("DelyedInvoke", 0.5f);
                //if (currentSubTask == 4)
                //{
                //    Count_1_to_4_ModelData.barValue = 0.5f * completeBarValue;
                //    loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
                //    loadingBar.BeginAllTransitions();
                //    Invoke("DelayedInvokeNotification", 1f);
                //    Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
                //    currentSubTask = 0;
                //    completedSubTask = completedSubTask.Select(x => x = false).ToArray();

                //}

            }
            if (currentSubTask == 2 && ValueCalculator.value == 0)
            {
                Count_1_to_4_ModelData.barValue = 0.5f * 1;
                loadingBar.Data.FillAmount = Count_1_to_4_ModelData.barValue;
                loadingBar.BeginAllTransitions();
                Invoke("DelayedInvokeNotification", 0.2f);
                activityList1.liftBeed01[currentTask] = true;
                Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
                currentSubTask = 0;
                completedSubTask = completedSubTask.Select(x => x = false).ToArray();

            }
        }

        else if (currentTask == 2)
        {
            for (int i = 0; i < MoveMentBeeds.Count; i++)
            {
                MoveMentBeeds[i].GetComponent<BoxCollider>().enabled = false;
            }
            MoveMentBeeds[2].GetComponent<BoxCollider>().enabled = true;

            if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended))
            {
                if (NotificationPannel.activeInHierarchy == false)
                    Invoke("DelayedInvokeAnimation", 1f);
            }
            else if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
            {
                leanPulseAnimation.RemainingPulses = 0;
                animatingSprite.enabled = false;
                CancelInvoke("DelayedInvokeAnimation");
            }

            if (ValueCalculator.value == 3)
            {
                for (int i = 0; i < MoveMentBeeds.Count; i++)
                {
                    MoveMentBeeds[i].GetComponent<BoxCollider>().enabled = false;
                }
                MoveMentBeeds[0].GetComponent<BoxCollider>().enabled = true;
            }

            if (ValueCalculator.value == 3 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {

                temp = true;
                completedSubTask[currentSubTask] = true;
                currentSubTask++;
                //currentResetAnimation = resetCount3Animation;
                //Invoke("DelyedInvoke", 0.5f);
                //if (currentSubTask == 4 &&  ValueCalculator.value == 0)
                //{

                //    Count_1_to_4_ModelData.barValue = 0.75f * completeBarValue;
                //    loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
                //    loadingBar.BeginAllTransitions();

                //    Invoke("DelayedInvokeNotification", 1f);
                //    Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
                //    currentSubTask = 0;
                //    completedSubTask = completedSubTask.Select(x => x = false).ToArray();
                //}

            }

            if (currentSubTask == 2 && ValueCalculator.value == 0)
            {

                Count_1_to_4_ModelData.barValue = 0.75f * 1;
                loadingBar.Data.FillAmount = Count_1_to_4_ModelData.barValue;
                loadingBar.BeginAllTransitions();

                Invoke("DelayedInvokeNotification", 0.2f);
                activityList1.liftBeed01[currentTask] = true;
                Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
                currentSubTask = 0;
                completedSubTask = completedSubTask.Select(x => x = false).ToArray();
            }
        }

        else if (currentTask == 3)
        {
            for (int i = 0; i < MoveMentBeeds.Count; i++)
            {
                MoveMentBeeds[i].GetComponent<BoxCollider>().enabled = false;
            }
            MoveMentBeeds[3].GetComponent<BoxCollider>().enabled = true;

            if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended))
            {
                if (NotificationPannel.activeInHierarchy == false)
                    Invoke("DelayedInvokeAnimation", 1f);
            }
            else if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
            {
                leanPulseAnimation.RemainingPulses = 0;
                animatingSprite.enabled = false;
                CancelInvoke("DelayedInvokeAnimation");
            }

            if (ValueCalculator.value == 4)
            {
                for (int i = 0; i < MoveMentBeeds.Count; i++)
                {
                    MoveMentBeeds[i].GetComponent<BoxCollider>().enabled = false;
                }
                MoveMentBeeds[0].GetComponent<BoxCollider>().enabled = true;
            }

            if (ValueCalculator.value == 4 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {

                temp = true;
                completedSubTask[currentSubTask] = true;
                currentSubTask++;
                //currentResetAnimation = resetCount4Animation;
                //Invoke("DelyedInvoke", 0.5f);
                //if (currentSubTask == 4 && ValueCalculator.value == 0)
                //{
                //    Count_1_to_4_ModelData.barValue = 1f * completeBarValue;
                //    loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
                //    loadingBar.BeginAllTransitions();
                //    Invoke("DelayedInvokeCongratulation", 1f);
                //    Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
                //    currentSubTask = 0;
                //    completedSubTask = completedSubTask.Select(x => x = false).ToArray();

                //}

            }
            if (currentSubTask == 2 && ValueCalculator.value == 0)
            {
                Count_1_to_4_ModelData.barValue = 1f * 1;
                loadingBar.Data.FillAmount = Count_1_to_4_ModelData.barValue;
                loadingBar.BeginAllTransitions();
                Invoke("DelayedInvokeCongratulation", 0.2f);
                activityList1.liftBeed01[currentTask] = true;
                Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
                currentSubTask = 0;
                completedSubTask = completedSubTask.Select(x => x = false).ToArray();

            }
        }



    }

    private void OpenSideNote()
    {
        sideNoteLean.TurnOn();// sideNote.SetActive(true);
        animatingSprite.enabled = true;
    }

    void DelyedInvoke()
    {
        CancelInvoke("DelyedInvoke");
        ResetHighlight.SetActive(true);
        indexAnimatingSprite.transform.gameObject.SetActive(true);

        //currentResetAnimation.BeginAllTransitions();
        LeanResetAnimation.RemainingPulses = 3;
        // if the first beed is at the top stop and hide the thumb animation and start and dislay the index finger animation . and aslo highlight the first animation

    }

    void DelayedInvokeNotification()
    {
        sidenoteText.text = notificationData[currentTask];
        sideNoteLean.TurnOff();// sideNote.SetActive(false);
        CancelInvoke("DelayedInvokeAnimation");
        notificationLean.TurnOn();// NotificationPannel.SetActive(true);
        if (currentTask == 1)
        {
            Highlight1.transform.SetParent(beed2.transform);
            Highlight1.transform.localPosition = new Vector3(0, 0, 0);
            AnimatingSpriteGameObject.transform.SetParent(beed2.transform);
            AnimatingSpriteGameObject.transform.localPosition = new Vector3(-2.7f, 0, 2);

        }
        else if (currentTask == 2)
        {
            Highlight1.transform.SetParent(beed3.transform);
            Highlight1.transform.localPosition = new Vector3(0, 0, 0);
            AnimatingSpriteGameObject.transform.SetParent(beed3.transform);
            AnimatingSpriteGameObject.transform.localPosition = new Vector3(-2.7f, 0, 2);
        }
        else if (currentTask == 3)
        {
            Highlight1.transform.SetParent(beed4.transform);
            Highlight1.transform.localPosition = new Vector3(0, 0, 0);
            AnimatingSpriteGameObject.transform.SetParent(beed4.transform);
            AnimatingSpriteGameObject.transform.localPosition = new Vector3(-2.7f, 0, 2);
        }
    }

    void DelayedInvokeCongratulation()
    {
        sideNoteLean.TurnOff();//sideNote.SetActive(false);
        CancelInvoke("DelayedInvokeAnimation");
        congratulationLean.TurnOn();// CongratulationPannel.SetActive(true);
        activityList2.liftingBeed21.bestTime = 1;
        activityList2.liftingBeed21.bestTime_string = "Completed";
        //activityList1.liftBeed01 = true;
        SaveManager.Instance.saveDataToDisk(Activity.classParent);


        leanPulseAnimation.RemainingPulses = 0;
        animatingSprite.enabled = false;

    }

    private void OnDisable()
    {
        valueCalculator.decimalPlaceString = "F2";

        for (int i = 0; i < noMoveMentBeeds.Count; i++)
        {
            noMoveMentBeeds[i].GetComponent<BoxCollider>().enabled = true;
        }
        for (int i = 0; i < MoveMentBeeds.Count; i++)
        {
            MoveMentBeeds[i].GetComponent<BoxCollider>().enabled = true;
        }

        NotificationBtn.onClick.RemoveListener(OpenSideNote);
        try
        {
            sideNoteLean.TurnOff();// sideNote.SetActive(false);
        }
        catch
        {
            ;
        }
        activityList2.liftingBeed21.currentSubActivity = SubTaskToSave;
        //activityList1.liftBeed01 = true;
        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
        SaveManager.Instance.saveDataToDisk(Activity.classParent);
        NotificationBtn.onClick.RemoveListener(DelayedInvokeAnimation);
        if (Highlight1 != null)
            Highlight1.SetActive(false);
        CancelInvoke("DelayedInvokeAnimation");
        ResetHighlight.SetActive(false);

        //Highlight2.SetActive(false);
    }

    public void DelayedInvokeAnimation()
    {
        CancelInvoke("DelayedInvokeAnimation");
        Highlight1.SetActive(true);
        leanPulseAnimation.RemainingPulses = 5;
        animatingSprite.enabled = true;
    }


}

