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

public class Count_5_to_9 : MonoBehaviour
{
    public Activity activityScriptInstance;
    public LeanPulse leanPulseThumbUpAnimation;
    public LeanPulse leanPulseThumbDownAnimation;
    public LeanPulse leanPulseFingerUpAnimation;
    public LeanPulse leanPulseFingerDownAnimation;
    ActivityList1 activityList1;
    ActivityList2 activityList2;
    // public LeanPulse LeanResetAnimation;
    public SpriteRenderer thumbUpAnimatingSprite;
    public SpriteRenderer thumbDownAnimatingSprite;
    public SpriteRenderer indexUpAnimatingSprite;
    public SpriteRenderer indexDownAnimatingSprite;
    //public LeanRectTransformSizeDelta loadingBar1;
    public LeanImageFillAmount loadingBar;
    public LeanEvent resetCount1Animation;
    public LeanEvent resetCount2Animation;
    public LeanEvent resetCount3Animation;
    public LeanEvent resetCount4Animation;
    public LeanEvent resetCount5Animation;
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
    public GameObject beed5;
    public GameObject beed6;
    public GameObject beed7;
    public GameObject beed8;
    public GameObject beed9;
    public GameObject beed10;
    public GameObject AnimatingSpriteGameObject;
    public GameObject Highlight1;
    public GameObject Highlight2;
    public GameObject ResetHighlight;


    public List<string> notificationData;
    public string CongratulationText;
    public bool[] completedSubTask;
    bool temp;
    public int currentTask, currentSubTask;
    LeanEvent currentResetAnimation;
    float completeBarValue = 500;
    public ValueCalculator valueCalculator;
    public LeanToggle congratulationLean;
    public LeanToggle sideNoteLean;
    public LeanToggle notificationLean;
    int SubTaskToSave;



    bool animate5Once;
    bool animate6Once;
    bool animate7Once;
    bool animate8Once;
    bool animate9Once;




    void OnEnable()
    {
        Invoke(nameof(OnEnableWithDelay), 0.1f);

        animate5Once = true;
        animate6Once = true;
        animate7Once = true;
        animate8Once = true;
        animate9Once = true;

        beed1.GetComponent<BoxCollider>().enabled = false;
        beed2.GetComponent<BoxCollider>().enabled = false;
        beed3.GetComponent<BoxCollider>().enabled = false;
        beed4.GetComponent<BoxCollider>().enabled = false;
        beed5.GetComponent<BoxCollider>().enabled = false;

        beed6.GetComponent<BoxCollider>().enabled = false;
        beed7.GetComponent<BoxCollider>().enabled = false;
        beed8.GetComponent<BoxCollider>().enabled = false;
        beed9.GetComponent<BoxCollider>().enabled = false;
        beed10.GetComponent<BoxCollider>().enabled = false;



        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].liftBeed02 == true)
                    {
                        activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
                        activityList2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j];
                    }
                }
            }
        }

        NotificationBtn.onClick.AddListener(startAnimation);

        if (activityList2.LiftingBeed22.currentSubActivity >= 5)
        {
            activityList2.LiftingBeed22.currentSubActivity = 0;
        }
        SubTaskToSave = activityList2.LiftingBeed22.currentSubActivity;

        for (int p = 0; p < Count_5_to_9_ModelData.TaskComplete.Length; p++)
        {
            Count_5_to_9_ModelData.TaskComplete = Count_5_to_9_ModelData.TaskComplete.Select(x => x = false).ToArray();
            Count_5_to_9_ModelData.barValue = 0;


        }

        for (int i = 0; i < activityList2.LiftingBeed22.currentSubActivity; i++)
        {
            Count_5_to_9_ModelData.TaskComplete[i] = true;
            Count_5_to_9_ModelData.barValue = (i + 1) * 0.2f;

        }


        NotificationBtn.onClick.AddListener(OpenSideNote);
        NotificationBtn.onClick.AddListener(MakeAllBeedUnintractive);
        NotificationBtn.onClick.AddListener(showindexFingerSpriteRenderer);



        if (Count_5_to_9_ModelData.TaskComplete[Count_5_to_9_ModelData.TaskComplete.Length - 1] == true)
        {
            Count_5_to_9_ModelData.TaskComplete = Count_5_to_9_ModelData.TaskComplete.Select(x => x = false).ToArray();
            Count_5_to_9_ModelData.barValue = 0;

        }

        completedSubTask = new bool[5];
        currentSubTask = 0;
        temp = false;
        loadingBar.Data.FillAmount = Count_5_to_9_ModelData.barValue / 5;
        loadingBar.BeginAllTransitions();
        Highlight1.SetActive(true);
        Highlight2.SetActive(true);
        NotificationBtn.onClick.AddListener(DelayedInvokeAnimation);
        //NotificationBtn.onClick.AddListener(OpenSideNote);
        for (currentTask = 0; currentTask < Count_5_to_9_ModelData.TaskComplete.Length; currentTask++)
        {
            if (Count_5_to_9_ModelData.TaskComplete[currentTask] == false)
            {
                Notification.text = notificationData[currentTask];
                break;
            }
        }

        Invoke("DelayedInvokeNotification", 0.4f);

        loadingBar.Data.FillAmount = currentTask * 1f / 5;
        loadingBar.BeginAllTransitions();

        /*indexDownAnimatingSprite.transform.SetParent(beed5.transform);
        indexDownAnimatingSprite.gameObject.SetActive(true);
        indexDownAnimatingSprite.transform.localPosition = new Vector3(-2.7f, 0, 2);    
        indexUpAnimatingSprite.transform.SetParent(beed5.transform);
        indexUpAnimatingSprite.gameObject.SetActive(true);
        indexUpAnimatingSprite.transform.localPosition = new Vector3(-2.7f, 0, 2);*/
        indexDownAnimatingSprite.enabled = false;

        thumbUpAnimatingSprite.enabled = false;

    }

    public void OnEnableWithDelay()
    {
        valueCalculator.decimalPlaceString = "F0";
    }

    public void startAnimation()
    {
        leanPulseFingerDownAnimation.RemainingPulses = 0;
        leanPulseThumbDownAnimation.RemainingPulses = 0;
        leanPulseThumbUpAnimation.RemainingPulses = 0;
        Invoke("DelayedInvokeResetAnimations", 0.5f);
    }

    public void hideindexFingerSpriteRenderer()
    {
        indexDownAnimatingSprite.gameObject.SetActive(false);
    }

    public void showindexFingerSpriteRenderer()
    {
        indexDownAnimatingSprite.gameObject.SetActive(true);
    }

    void Update()
    {
        // loadingBar.sizeDelta = new Vector2(Count_5_to_9_ModelData.barValue, loadingBar.sizeDelta.y);

        if (Input.touchCount > 0 && currentTask == 0 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
        {
            indexDownAnimatingSprite.enabled = false;

            thumbUpAnimatingSprite.enabled = false;

            Invoke("DelyedInvoke", 0.5f);
        }
        try
        {
            if (!Notification.transform.gameObject.activeInHierarchy)
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    //leanPulseFingerDownAnimation.RemainingPulses = 0;
                    //leanPulseThumbDownAnimation.RemainingPulses = 0;
                    //leanPulseThumbUpAnimation.RemainingPulses = 0;
                    //  Invoke("DelayedInvokeResetAnimations", 0.5f);

                }
        }
        catch
        {
            ;
        }

        for (currentTask = 0; currentTask < Count_5_to_9_ModelData.TaskComplete.Length; currentTask++)
        {
            if (Count_5_to_9_ModelData.TaskComplete[currentTask] == false)
            {
                Notification.text = notificationData[currentTask];
                break;
            }
        }
        SubTaskToSave = currentTask;


        if (currentTask == Count_5_to_9_ModelData.TaskComplete.Length)
        {
            //activityList1.liftBeed02[currentTask-1] = true;
            //SaveManager.Instance.saveDataToDisk(Activity.classParent);
            Congratulation.text = CongratulationText;
        }

        if (currentTask == 0)
        {
            //beed5.GetComponent<BoxCollider>().enabled = true;


            try
            {
                if (Input.touches[0].phase == TouchPhase.Ended)
                {


                    if (ValueCalculator.value == 0)
                    {


                        indexDownAnimatingSprite.transform.SetParent(beed5.transform);
                        thumbUpAnimatingSprite.transform.gameObject.SetActive(false);

                        indexDownAnimatingSprite.transform.gameObject.SetActive(true);
                        Invoke("DelayedleanPulseFingerDownAnimation", 0.5f);
                        Invoke(nameof(MakeAllBeedIntractive), 1.2f);

                        //Invoke(nameof(HideAnimatingSprites), 1.5f);



                    }
                    if (ValueCalculator.value == 5)
                    {

                        Invoke("DelayedleanPulseThumbUpAnimation", 0.5f);
                        indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                        thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                        thumbUpAnimatingSprite.transform.SetParent(beed5.transform);
                        sidenoteText.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";



                    }

                }
            }
            catch
            {
            }



            if (ValueCalculator.value == 5)
            {



                //finger up animation
            }
            if (ValueCalculator.value == 0)
            {

                sidenoteText.text = "Count to 5";//notificationData[currentTask];


                //finger down animation
            }


            Highlight1.transform.SetParent(beed5.transform);
            Highlight1.transform.localPosition = new Vector3(0, 0, 0);
            Highlight2.transform.SetParent(beed5.transform);
            Highlight2.transform.localPosition = new Vector3(0, 0, 0);
            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended && temp == true)
            {
                if (ValueCalculator.value == 0)
                {
                    temp = false;
                    completedSubTask[currentSubTask] = true;
                    currentSubTask++;

                }
                if (currentSubTask == 2)
                {
                    Count_5_to_9_ModelData.barValue = ((1 + currentTask) * 1f) / 5;
                    loadingBar.Data.FillAmount = Count_5_to_9_ModelData.barValue;
                    loadingBar.BeginAllTransitions();
                    activityList1.liftBeed02[currentTask] = true;
                    SaveManager.Instance.saveDataToDisk(Activity.classParent);
                    Invoke("DelayedInvokeNotification", 0.4f);
                    Count_5_to_9_ModelData.TaskComplete[currentTask] = true;
                    currentSubTask = 0;
                    completedSubTask = completedSubTask.Select(x => x = false).ToArray();
                }





            }


            if (ValueCalculator.value == 5 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {
                temp = true;


                currentResetAnimation = resetCount1Animation;
                Invoke("DelyedInvoke", 0.5f);



            }
        }

        else if (currentTask == 1)
        {
            Highlight2.SetActive(false);
            Highlight1.SetActive(false);
            ResetHighlight.SetActive(false);

            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended && temp == true)
            {
                //beed5.GetComponent<BoxCollider>().enabled = true;


                if (ValueCalculator.value == 5)
                {


                    //beed5.GetComponent<BoxCollider>().enabled = false;

                    //beed1.GetComponent<BoxCollider>().enabled = true;

                    Invoke("DelayedleanPulseFingerUpAnimation", 0.5f);

                    //move finger up
                }
                if (ValueCalculator.value == 1)
                {
                    //Invoke("DelayedleanPulseFingerDownAnimation", 0.5f);
                    //indexDownAnimatingSprite.transform.gameObject.SetActive(true);
                    //thumbUpAnimatingSprite.transform.gameObject.SetActive(false);
                    //Invoke("DelayedleanPulseThumbDownAnimation", 0.5f);

                    indexDownAnimatingSprite.transform.SetParent(beed1.transform);
                    //beed5.GetComponent<BoxCollider>().enabled = false;

                    //beed1.GetComponent<BoxCollider>().enabled = true;

                    //move thumb down
                }
                if (ValueCalculator.value == 6)
                {



                    //beed5.GetComponent<BoxCollider>().enabled = true;

                    //beed1.GetComponent<BoxCollider>().enabled = false;

                    Invoke("DelayedleanPulseFingerUpAnimation", 0.5f);
                    Invoke("DelayedleanPulseThumbDownAnimation", 0.5f);

                    sidenoteText.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";

                    //move finger up
                    //move thumb down
                }

                if (ValueCalculator.value == 0)
                {
                    /* Invoke("DelayedleanPulseThumbUpAnimation", 1f);
                     indexDownAnimatingSprite.transform.gameObject.SetActive(true);
                     thumbUpAnimatingSprite.transform.gameObject.SetActive(false);
                     indexDownAnimatingSprite.transform.SetParent(beed1.transform);

                     */

                    sidenoteText.text = "Count to 6"; //notificationData[currentTask];


                    //beed5.GetComponent<BoxCollider>().enabled = true;
                    //beed1.GetComponent<BoxCollider>().enabled = false;
                    temp = false;
                    completedSubTask[currentSubTask] = true;
                    currentSubTask++;
                    Highlight1.SetActive(false);
                    ResetHighlight.SetActive(false);
                }
                if (currentSubTask == 2)
                {
                    Count_5_to_9_ModelData.barValue = ((1 + currentTask) * 1f) / 5;
                    loadingBar.Data.FillAmount = Count_5_to_9_ModelData.barValue;
                    loadingBar.BeginAllTransitions();
                    Invoke("DelayedInvokeNotification", 0.4f);
                    activityList1.liftBeed02[currentTask] = true;
                    SaveManager.Instance.saveDataToDisk(Activity.classParent);
                    Count_5_to_9_ModelData.TaskComplete[currentTask] = true;
                    currentSubTask = 0;
                    completedSubTask = completedSubTask.Select(x => x = false).ToArray();
                }



            }

            if (temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {
                if (ValueCalculator.value == 0)
                {
                    if (animate6Once)
                    {
                        Highlight1.SetActive(false);
                        ResetHighlight.SetActive(false);
                        indexDownAnimatingSprite.transform.SetParent(beed5.transform);
                        // thumbUpAnimatingSprite.transform.gameObject.SetActive(false);
                        indexDownAnimatingSprite.transform.gameObject.SetActive(true);

                        sidenoteText.text = "Count to 6";

                        //beed5.GetComponent<BoxCollider>().enabled = true;
                        //beed1.GetComponent<BoxCollider>().enabled = false;
                        Invoke("DelayedleanPulseFingerDownAnimation", 0.5f);
                        Invoke(nameof(HideIndexFinger), 1.2f);



                        //  indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                        //thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                        thumbUpAnimatingSprite.transform.SetParent(beed1.transform);
                        Invoke("DelayedleanPulseThumbUpAnimation", 1.5f);
                        Invoke(nameof(HideThumb), 2.7f);

                        Invoke(nameof(HideAnimatingSprites), 3f);

                        animate6Once = false;
                    }


                }
                if (ValueCalculator.value == 5)
                {

                    //indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                    //thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                    //thumbUpAnimatingSprite.transform.SetParent(beed1.transform);
                    //Invoke("DelayedleanPulseThumbUpAnimation", 0.5f);



                    //beed5.GetComponent<BoxCollider>().enabled = false;

                    //beed1.GetComponent<BoxCollider>().enabled = true;


                }
                if (ValueCalculator.value == 1)
                {

                    //Invoke("DelayedleanPulseThumbUpAnimation", 0.5f);
                    //indexDownAnimatingSprite.transform.gameObject.SetActive(true);
                    //thumbUpAnimatingSprite.transform.gameObject.SetActive(false);
                    //indexUpAnimatingSprite.transform.SetParent(beed1.transform);
                    //Invoke("DelayedleanPulseFingerDownAnimation", 0.5f);



                    //beed5.GetComponent<BoxCollider>().enabled = false;

                    //beed1.GetComponent<BoxCollider>().enabled = true;
                    //move finger down

                }

                if (ValueCalculator.value == 6)
                {
                    //Invoke("DelayedleanPulseThumbUpAnimation", 0.5f);
                    //indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                    //thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                    //thumbUpAnimatingSprite.transform.SetParent(beed5.transform);
                    sidenoteText.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";

                }

            }

            if (ValueCalculator.value == 6 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {

                //beed5.GetComponent<BoxCollider>().enabled = true;

                //beed1.GetComponent<BoxCollider>().enabled = false;

                ResetHighlight.SetActive(false);
                Highlight1.SetActive(false);

                temp = true;
                currentResetAnimation = resetCount2Animation;
                Invoke("DelyedInvoke", 0.5f);

            }
        }

        else if (currentTask == 2)
        {
            Highlight2.SetActive(false);
            Highlight1.SetActive(false);
            ResetHighlight.SetActive(false);
            if (ValueCalculator.value == 0)
            {




                sidenoteText.text = "Count to 7";//notificationData[currentTask];


                //beed1.GetComponent<BoxCollider>().enabled = false;
                //beed5.GetComponent<BoxCollider>().enabled = true;
                //beed2.GetComponent<BoxCollider>().enabled = false;
            }
            if (ValueCalculator.value == 5)
            {





                //beed5.GetComponent<BoxCollider>().enabled = false;
                //beed2.GetComponent<BoxCollider>().enabled = true;
            }

            if (ValueCalculator.value == 2)
            {




                //beed5.GetComponent<BoxCollider>().enabled = false;
                //beed1.GetComponent<BoxCollider>().enabled = true;
            }
            if (ValueCalculator.value == 7)
            {

                sidenoteText.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";



                //beed5.GetComponent<BoxCollider>().enabled = true;
                //beed2.GetComponent<BoxCollider>().enabled = false;
            }




            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended && temp == true)
            {
                if (ValueCalculator.value == 0)
                {


                    temp = false;
                    completedSubTask[currentSubTask] = true;
                    currentSubTask++;
                    Highlight1.SetActive(false);
                    ResetHighlight.SetActive(false);
                    if (currentSubTask == 2)
                    {
                        Count_5_to_9_ModelData.barValue = ((1 + currentTask) * 1f) / 5;
                        loadingBar.Data.FillAmount = Count_5_to_9_ModelData.barValue;
                        loadingBar.BeginAllTransitions();
                        activityList1.liftBeed02[currentTask] = true;
                        SaveManager.Instance.saveDataToDisk(Activity.classParent);
                        Invoke("DelayedInvokeNotification", 0.4f);
                        Count_5_to_9_ModelData.TaskComplete[currentTask] = true;
                        currentSubTask = 0;
                        completedSubTask = completedSubTask.Select(x => x = false).ToArray();
                    }

                }

                if (ValueCalculator.value == 0)
                {

                    //indexDownAnimatingSprite.transform.SetParent(beed5.transform);
                    //thumbUpAnimatingSprite.transform.gameObject.SetActive(false);

                    //indexDownAnimatingSprite.transform.gameObject.SetActive(true);
                    //Invoke("DelayedleanPulseFingerDownAnimation", 0.5f);




                }
                if (ValueCalculator.value == 5)
                {

                    //indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                    //thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                    //thumbUpAnimatingSprite.transform.SetParent(beed2.transform);
                    //Invoke("DelayedleanPulseThumbUpAnimation", 0.5f);




                }

                if (ValueCalculator.value == 2)
                {
                    //Invoke("DelayedleanPulseFingerDownAnimation", 0.5f);
                    //indexDownAnimatingSprite.transform.gameObject.SetActive(true);
                    //thumbUpAnimatingSprite.transform.gameObject.SetActive(false);
                    //indexDownAnimatingSprite.transform.SetParent(beed2.transform);



                }
                if (ValueCalculator.value == 7)
                {
                    //Invoke("DelayedleanPulseThumbUpAnimation", 0.5f);
                    //indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                    //thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                    //thumbUpAnimatingSprite.transform.SetParent(beed5.transform);

                    sidenoteText.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";

                }


            }

            try
            {
                if (Input.touches[0].phase == TouchPhase.Ended)
                {


                    if (ValueCalculator.value == 0)
                    {
                        if (animate7Once)
                        {

                            indexDownAnimatingSprite.transform.SetParent(beed5.transform);
                            //thumbUpAnimatingSprite.transform.gameObject.SetActive(false);

                            indexDownAnimatingSprite.transform.gameObject.SetActive(true);
                            Invoke("DelayedleanPulseFingerDownAnimation", 0.5f);
                            Invoke(nameof(HideIndexFinger), 1.2f);


                            //  indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                            // thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                            thumbUpAnimatingSprite.transform.SetParent(beed2.transform);
                            Invoke("DelayedleanPulseThumbUpAnimation", 1.5f);
                            Invoke(nameof(HideThumb), 2.7f);

                            animate7Once = false;

                            Invoke(nameof(HideAnimatingSprites), 3f);

                        }


                    }
                    if (ValueCalculator.value == 5)
                    {

                        //indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                        //thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                        //thumbUpAnimatingSprite.transform.SetParent(beed2.transform);
                        //Invoke("DelayedleanPulseThumbUpAnimation", 0.5f);




                    }

                    if (ValueCalculator.value == 2)
                    {
                        //Invoke("DelayedleanPulseFingerDownAnimation", 0.5f);
                        //indexDownAnimatingSprite.transform.gameObject.SetActive(true);
                        //thumbUpAnimatingSprite.transform.gameObject.SetActive(false);
                        //indexDownAnimatingSprite.transform.SetParent(beed1.transform);



                    }
                    if (ValueCalculator.value == 7)
                    {
                        //Invoke("DelayedleanPulseThumbUpAnimation", 0.5f);
                        //indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                        //thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                        //thumbUpAnimatingSprite.transform.SetParent(beed5.transform);
                        sidenoteText.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";


                    }
                }
            }
            catch
            {
            }



            if (ValueCalculator.value == 7 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {
                //beed5.GetComponent<BoxCollider>().enabled = false;
                //beed2.GetComponent<BoxCollider>().enabled = true;

                temp = true;
                currentResetAnimation = resetCount3Animation;
                Invoke("DelyedInvoke", 0.5f);
                ResetHighlight.SetActive(false);
                Highlight1.SetActive(false);





            }
        }

        else if (currentTask == 3)
        {
            Highlight2.SetActive(false);
            Highlight1.SetActive(false);
            ResetHighlight.SetActive(false);
            try
            {
                if (Input.touches[0].phase == TouchPhase.Ended)
                {


                    if (ValueCalculator.value == 0)
                    {
                        if (animate8Once)
                        {
                            indexDownAnimatingSprite.transform.SetParent(beed5.transform);
                            // thumbUpAnimatingSprite.transform.gameObject.SetActive(false);

                            indexDownAnimatingSprite.transform.gameObject.SetActive(true);
                            Invoke("DelayedleanPulseFingerDownAnimation", 0.5f);
                            Invoke(nameof(HideIndexFinger), 1.2f);

                            // indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                            // thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                            thumbUpAnimatingSprite.transform.SetParent(beed3.transform);
                            Invoke("DelayedleanPulseThumbUpAnimation", 1.5f);
                            Invoke(nameof(HideThumb), 2.7f);

                            Invoke(nameof(HideAnimatingSprites), 3f);
                            animate8Once = false;
                        }


                        sidenoteText.text = "Count to 8";//notificationData[currentTask];



                    }
                    if (ValueCalculator.value == 5)
                    {

                        //indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                        //thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                        //thumbUpAnimatingSprite.transform.SetParent(beed3.transform);
                        //Invoke("DelayedleanPulseThumbUpAnimation", 0.5f);




                    }

                    if (ValueCalculator.value == 3)
                    {
                        //Invoke("DelayedleanPulseFingerDownAnimation", 0.5f);
                        //indexDownAnimatingSprite.transform.gameObject.SetActive(true);
                        //thumbUpAnimatingSprite.transform.gameObject.SetActive(false);
                        //indexDownAnimatingSprite.transform.SetParent(beed1.transform);



                    }
                    if (ValueCalculator.value == 8)
                    {
                        //Invoke("DelayedleanPulseThumbUpAnimation", 0.5f);
                        //indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                        //thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                        //thumbUpAnimatingSprite.transform.SetParent(beed5.transform);
                        sidenoteText.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";


                    }
                }
            }
            catch
            {
            }





            if (ValueCalculator.value == 0)
            {
                sidenoteText.text = "Count to 8";//notificationData[currentTask];

                //beed5.GetComponent<BoxCollider>().enabled = true;
                //beed1.GetComponent<BoxCollider>().enabled = false;
                //beed3.GetComponent<BoxCollider>().enabled = false;

            }
            if (ValueCalculator.value == 5)
            {
                //beed5.GetComponent<BoxCollider>().enabled = false;
                //beed3.GetComponent<BoxCollider>().enabled = true;
            }

            if (ValueCalculator.value == 8)
            {
                //beed5.GetComponent<BoxCollider>().enabled = true;
                //beed3.GetComponent<BoxCollider>().enabled = false;
                sidenoteText.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";

            }

            if (ValueCalculator.value == 3)
            {
                //beed5.GetComponent<BoxCollider>().enabled = false;
                //beed1.GetComponent<BoxCollider>().enabled = true;
            }

            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended && temp == true)
            {
                if (ValueCalculator.value == 0)
                {
                    temp = false;
                    completedSubTask[currentSubTask] = true;
                    currentSubTask++;
                    Highlight1.SetActive(false);
                    ResetHighlight.SetActive(false);

                }
                if (currentSubTask == 2)
                {
                    Count_5_to_9_ModelData.barValue = ((1 + currentTask) * 1f) / 5;
                    loadingBar.Data.FillAmount = Count_5_to_9_ModelData.barValue;
                    loadingBar.BeginAllTransitions();
                    activityList1.liftBeed02[currentTask] = true;
                    SaveManager.Instance.saveDataToDisk(Activity.classParent);
                    Invoke("DelayedInvokeNotification", 0.4f);
                    Count_5_to_9_ModelData.TaskComplete[currentTask] = true;
                    currentSubTask = 0;
                    completedSubTask = completedSubTask.Select(x => x = false).ToArray();
                }


            }

            if (ValueCalculator.value == 8 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {
                temp = true;
                ResetHighlight.SetActive(false);
                Highlight1.SetActive(false);

                currentResetAnimation = resetCount4Animation;
                Invoke("DelyedInvoke", 0.5f);
                sidenoteText.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";

            }
        }

        else if (currentTask == 4)
        {
            Highlight2.SetActive(false);
            Highlight1.SetActive(false);
            ResetHighlight.SetActive(false);
            try
            {
                if (Input.touches[0].phase == TouchPhase.Ended)
                {


                    if (ValueCalculator.value == 0)
                    {
                        if (animate9Once)
                        {

                            indexDownAnimatingSprite.transform.SetParent(beed5.transform);
                            // thumbUpAnimatingSprite.transform.gameObject.SetActive(false);

                            indexDownAnimatingSprite.transform.gameObject.SetActive(true);
                            Invoke("DelayedleanPulseFingerDownAnimation", 0.5f);
                            sidenoteText.text = "Count to 9";//notificationData[currentTask];
                            Invoke(nameof(HideIndexFinger), 1.2f);


                            // indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                            //thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                            thumbUpAnimatingSprite.transform.SetParent(beed4.transform);
                            Invoke("DelayedleanPulseThumbUpAnimation", 1.5f);
                            Invoke(nameof(HideThumb), 2.7f);

                            Invoke(nameof(HideAnimatingSprites), 3f);

                            animate9Once = false;
                        }

                    }
                    if (ValueCalculator.value == 5)
                    {


                        //indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                        //thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                        //thumbUpAnimatingSprite.transform.SetParent(beed4.transform);
                        //Invoke("DelayedleanPulseThumbUpAnimation", 0.5f);



                    }

                    if (ValueCalculator.value == 4)
                    {
                        //Invoke("DelayedleanPulseFingerDownAnimation", 0.5f);
                        //indexDownAnimatingSprite.transform.gameObject.SetActive(true);
                        //thumbUpAnimatingSprite.transform.gameObject.SetActive(false);
                        //indexDownAnimatingSprite.transform.SetParent(beed1.transform);



                    }
                    if (ValueCalculator.value == 9)
                    {
                        //Invoke("DelayedleanPulseThumbUpAnimation", 0.5f);
                        //indexDownAnimatingSprite.transform.gameObject.SetActive(false);
                        //thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
                        //thumbUpAnimatingSprite.transform.SetParent(beed5.transform);
                        sidenoteText.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";


                    }
                }
            }
            catch
            {
            }

















            if (ValueCalculator.value == 0)
            {
                sidenoteText.text = "Count to 9";//notificationData[currentTask];

                //beed1.GetComponent<BoxCollider>().enabled = false;
                //beed5.GetComponent<BoxCollider>().enabled = true;
                //beed4.GetComponent<BoxCollider>().enabled = false;
            }
            if (ValueCalculator.value == 5)
            {
                //beed5.GetComponent<BoxCollider>().enabled = false;
                //beed4.GetComponent<BoxCollider>().enabled = true;
            }
            if (ValueCalculator.value == 9)
            {
                //beed5.GetComponent<BoxCollider>().enabled = true;
                //beed4.GetComponent<BoxCollider>().enabled = false;
                sidenoteText.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";

            }
            if (ValueCalculator.value == 4)
            {
                //beed5.GetComponent<BoxCollider>().enabled = false;
                //beed1.GetComponent<BoxCollider>().enabled = true;

            }

            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended && temp == true)
            {
                if (ValueCalculator.value == 0)
                {
                    temp = false;
                    completedSubTask[currentSubTask] = true;
                    currentSubTask++;
                    ResetHighlight.SetActive(false);
                    Highlight1.SetActive(false);



                }
                if (currentSubTask == 2)
                {
                    Count_5_to_9_ModelData.barValue = ((1 + currentTask) * 1f) / 5;
                    loadingBar.Data.FillAmount = Count_5_to_9_ModelData.barValue;
                    loadingBar.BeginAllTransitions();
                    activityList1.liftBeed02[currentTask] = true;
                    SaveManager.Instance.saveDataToDisk(Activity.classParent);
                    Invoke("DelayedInvokeCongratulation", 0.4f);
                    Count_5_to_9_ModelData.TaskComplete[currentTask] = true;
                    currentSubTask = 0;
                    completedSubTask = completedSubTask.Select(x => x = false).ToArray();

                }

            }

            if (ValueCalculator.value == 9 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {
                temp = true;
                ResetHighlight.SetActive(false);
                Highlight1.SetActive(false);

                currentResetAnimation = resetCount5Animation;
                Invoke("DelyedInvoke", 0.5f);
                sidenoteText.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";

            }
        }



    }

    private void OpenSideNote()
    {
        sideNoteLean.TurnOn();// sideNote.SetActive(true);
        sidenoteText.text = notificationData[currentTask];
    }


    void DelyedInvoke()
    {
        //currentResetAnimation.BeginAllTransitions();

    }

    void DelayedInvokeNotification()
    {
        notificationLean.TurnOn();// NotificationPannel.SetActive(true);
        sideNoteLean.TurnOff();//  sideNote.SetActive(false);
        hideindexFingerSpriteRenderer();

        if (currentTask == 1)
        {
            Highlight1.transform.SetParent(beed1.transform);
            Highlight1.transform.localPosition = new Vector3(0, 0, 0);
            Highlight2.transform.SetParent(beed5.transform);
            Highlight2.transform.localPosition = new Vector3(0, 0, 0);
            ResetHighlight.transform.SetParent(beed1.transform);
            ResetHighlight.transform.localPosition = new Vector3(0, 0, 0);




        }
        else if (currentTask == 2)
        {
            Highlight1.transform.SetParent(beed2.transform);
            Highlight1.transform.localPosition = new Vector3(0, 0, 0);
            Highlight2.transform.SetParent(beed5.transform);
            Highlight2.transform.localPosition = new Vector3(0, 0, 0);
            ResetHighlight.transform.SetParent(beed1.transform);
            ResetHighlight.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (currentTask == 3)
        {
            Highlight1.transform.SetParent(beed3.transform);
            Highlight1.transform.localPosition = new Vector3(0, 0, 0);
            Highlight2.transform.SetParent(beed5.transform);
            Highlight2.transform.localPosition = new Vector3(0, 0, 0);
            ResetHighlight.transform.SetParent(beed1.transform);
            ResetHighlight.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (currentTask == 4)
        {
            Highlight1.transform.SetParent(beed4.transform);
            Highlight1.transform.localPosition = new Vector3(0, 0, 0);
            Highlight2.transform.SetParent(beed5.transform);
            Highlight2.transform.localPosition = new Vector3(0, 0, 0);
            ResetHighlight.transform.SetParent(beed1.transform);
            ResetHighlight.transform.localPosition = new Vector3(0, 0, 0);
        }


    }

    void DelayedInvokeCongratulation()
    {
        congratulationLean.TurnOn();// CongratulationPannel.SetActive(true);
        hideindexFingerSpriteRenderer();
        activityList2.LiftingBeed22.bestTime = 1;
        activityList2.LiftingBeed22.bestTime_string = "Completed";

        //activityList1.liftBeed01 = true;
        SaveManager.Instance.saveDataToDisk(Activity.classParent);
        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);

        sideNoteLean.TurnOff();//sideNote.SetActive(false);





    }

    private void OnDisable()
    {

        beed1.GetComponent<BoxCollider>().enabled = true;
        beed2.GetComponent<BoxCollider>().enabled = true;
        beed3.GetComponent<BoxCollider>().enabled = true;
        beed4.GetComponent<BoxCollider>().enabled = true;
        beed5.GetComponent<BoxCollider>().enabled = true;

        beed6.GetComponent<BoxCollider>().enabled = true;
        beed7.GetComponent<BoxCollider>().enabled = true;
        beed8.GetComponent<BoxCollider>().enabled = true;
        beed9.GetComponent<BoxCollider>().enabled = true;
        beed10.GetComponent<BoxCollider>().enabled = true;

        Highlight1.SetActive(false);
        Highlight2.SetActive(false);


        valueCalculator.decimalPlaceString = "F2";

        NotificationBtn.onClick.RemoveListener(startAnimation);

        thumbUpAnimatingSprite.transform.gameObject.SetActive(false);
        indexDownAnimatingSprite.transform.gameObject.SetActive(false);

        NotificationBtn.onClick.RemoveListener(showindexFingerSpriteRenderer);
        NotificationBtn.onClick.RemoveListener(OpenSideNote);
        NotificationBtn.onClick.RemoveListener(MakeAllBeedUnintractive);


        activityList2.LiftingBeed22.currentSubActivity = SubTaskToSave;


        SaveManager.Instance.saveDataToDisk(Activity.classParent);
        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);

        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);

    }

    public void DelayedleanPulseThumbUpAnimation()
    {
        indexDownAnimatingSprite.transform.gameObject.SetActive(false);
        thumbUpAnimatingSprite.transform.localPosition = new Vector3(-1.25f, -0.25f, 2f);
        thumbUpAnimatingSprite.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
        leanPulseThumbUpAnimation.RemainingPulses = 1;
        //CancelInvoke("DelayedleanPulseThumbUpAnimation");
        if (leanPulseThumbUpAnimation.RemainingPulses <= 0)
        {
        }

        if (leanPulseThumbUpAnimation.RemainingPulses == 5)
        {
            thumbUpAnimatingSprite.transform.localPosition = new Vector3(-1.25f, -0.25f, 2f);
            thumbUpAnimatingSprite.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        }
        if (currentTask == 0)
        {
            leanPulseThumbUpAnimation.RemainingPulses = 5;
            thumbUpAnimatingSprite.transform.gameObject.SetActive(true);
            thumbUpAnimatingSprite.enabled = true;

        }

    }
    public void DelayedleanPulseThumbDownAnimation()
    {
        CancelInvoke("DelayedleanPulseThumbDownAnimation");
        leanPulseThumbDownAnimation.RemainingPulses = 1;
    }
    public void DelayedleanPulseFingerUpAnimation()
    {
        CancelInvoke("DelayedleanPulseFingerUpAnimation");
        leanPulseFingerUpAnimation.RemainingPulses = 1;

    }
    public void DelayedleanPulseFingerDownAnimation()
    {
        indexDownAnimatingSprite.transform.localPosition = new Vector3(-1.25f, -2.8f, 2f);
        indexDownAnimatingSprite.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        leanPulseFingerDownAnimation.RemainingPulses = 1;
        if (currentTask == 0)
        {
            leanPulseFingerDownAnimation.RemainingPulses = 5;
            indexDownAnimatingSprite.transform.gameObject.SetActive(true);
            indexDownAnimatingSprite.enabled = true;

        }

        CancelInvoke("DelayedleanPulseFingerDownAnimation");
        if (leanPulseFingerDownAnimation.RemainingPulses <= 0)
        {
        }

    }

    public void DelayedInvokeResetAnimations()
    {
        CancelInvoke("DelayedInvokeResetAnimations");
        leanPulseFingerDownAnimation.RemainingPulses = 1;
        leanPulseFingerDownAnimation.RemainingTime = 0;
        indexDownAnimatingSprite.enabled = true;
        leanPulseThumbDownAnimation.RemainingPulses = 1;
        leanPulseThumbDownAnimation.RemainingTime = 0;

        thumbUpAnimatingSprite.enabled = true;
        // leanPulseThumbUpAnimation.RemainingPulses = 1;
        leanPulseThumbUpAnimation.RemainingTime = 0;

        thumbUpAnimatingSprite.enabled = true;
    }

    public void HideAnimatingSprites()
    {
        indexDownAnimatingSprite.transform.gameObject.SetActive(false);
        print("Calling Hide animation");
        indexUpAnimatingSprite.transform.gameObject.SetActive(false);
        thumbDownAnimatingSprite.transform.gameObject.SetActive(false);
        thumbUpAnimatingSprite.transform.gameObject.SetActive(false);

        thumbUpAnimatingSprite.enabled = false;

        indexDownAnimatingSprite.enabled = false;


        MakeAllBeedIntractive();
    }

    public void MakeAllBeedIntractive()
    {
        beed1.GetComponent<BoxCollider>().enabled = true;
        beed2.GetComponent<BoxCollider>().enabled = true;
        beed3.GetComponent<BoxCollider>().enabled = true;
        beed4.GetComponent<BoxCollider>().enabled = true;
        beed5.GetComponent<BoxCollider>().enabled = true;

        beed6.GetComponent<BoxCollider>().enabled = true;
        beed7.GetComponent<BoxCollider>().enabled = true;
        beed8.GetComponent<BoxCollider>().enabled = true;
        beed9.GetComponent<BoxCollider>().enabled = true;
        beed10.GetComponent<BoxCollider>().enabled = true;

    }

    public void MakeAllBeedUnintractive()
    {
        beed1.GetComponent<BoxCollider>().enabled = false;
        beed2.GetComponent<BoxCollider>().enabled = false;
        beed3.GetComponent<BoxCollider>().enabled = false;
        beed4.GetComponent<BoxCollider>().enabled = false;
        beed5.GetComponent<BoxCollider>().enabled = false;

        beed6.GetComponent<BoxCollider>().enabled = false;
        beed7.GetComponent<BoxCollider>().enabled = false;
        beed8.GetComponent<BoxCollider>().enabled = false;
        beed9.GetComponent<BoxCollider>().enabled = false;
        beed10.GetComponent<BoxCollider>().enabled = false;

    }

    public void HideIndexFinger()
    {
        indexDownAnimatingSprite.transform.gameObject.SetActive(false);
    }
    public void HideThumb()
    {
        thumbUpAnimatingSprite.transform.gameObject.SetActive(false);
    }

    public void DelayedInvokeAnimation()
    {
        /* CancelInvoke("DelayedInvokeAnimation");
         Highlight1.SetActive(true);
         leanPulseAnimation.RemainingPulses = 5;
         animatingSprite.enabled = true;*/
    }
}


