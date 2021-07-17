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

    public LeanToggle congratulationLean;
    public LeanToggle sideNoteLean;
    public LeanToggle notificationLean;
    int SubTaskToSave;

    void OnEnable()
    {

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
        NotificationBtn.onClick.AddListener(OpenSideNote);
        for (currentTask = 0; currentTask < Count_5_to_9_ModelData.TaskComplete.Length; currentTask++)
        {
            if (Count_5_to_9_ModelData.TaskComplete[currentTask] == false)
            {
                Notification.text = notificationData[currentTask];
                break;
            }
        }
        


        loadingBar.Data.FillAmount = currentTask * 1f / 5;
        loadingBar.BeginAllTransitions();

        /*indexDownAnimatingSprite.transform.SetParent(beed5.transform);
        indexDownAnimatingSprite.gameObject.SetActive(true);
        indexDownAnimatingSprite.transform.localPosition = new Vector3(-2.7f, 0, 2);    
        indexUpAnimatingSprite.transform.SetParent(beed5.transform);
        indexUpAnimatingSprite.gameObject.SetActive(true);
        indexUpAnimatingSprite.transform.localPosition = new Vector3(-2.7f, 0, 2);*/

    }


    void Update()
    {
        // loadingBar.sizeDelta = new Vector2(Count_5_to_9_ModelData.barValue, loadingBar.sizeDelta.y);



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

            if (ValueCalculator.value == 5)
            {
                Invoke("DelayedleanPulseFingerUpAnimation", 1f);
                //finger up animation
            }
            if (ValueCalculator.value == 0)
            {

                Invoke("DelayedleanPulseFingerDownAnimation", 1f);

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


            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended && temp == true)
            {

                if (ValueCalculator.value == 5)
                {
                    Invoke("DelayedleanPulseFingerUpAnimation", 1f);

                    //move finger up
                }
                if (ValueCalculator.value == 1)
                {

                    Invoke("DelayedleanPulseThumbDownAnimation", 1f);

                    //move thumb down
                }
                if (ValueCalculator.value == 6)
                {
                    Invoke("DelayedleanPulseFingerUpAnimation", 1f);
                    Invoke("DelayedleanPulseThumbDownAnimation", 1f);

                    //move finger up
                    //move thumb down
                }

                if (ValueCalculator.value == 0)
                {
                    temp = false;
                    completedSubTask[currentSubTask] = true;
                    currentSubTask++;
                    Highlight1.SetActive(true);
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

                    Invoke("DelayedleanPulseFingerDownAnimation", 1f);

                    Invoke("DelayedleanPulseThumbUpAnimation", 1f);
                    //move finger down
                    //move thumb up
                }
                if (ValueCalculator.value == 5)
                {

                    Invoke("DelayedleanPulseThumbUpAnimation", 1f);
                    //move thumb up

                }
                if (ValueCalculator.value == 1)
                {
                    Invoke("DelayedleanPulseFingerDownAnimation", 1f);
                    //move finger down

                }

            }

            if (ValueCalculator.value == 6 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {
                ResetHighlight.SetActive(true);
                Highlight1.SetActive(false);

                temp = true;
                currentResetAnimation = resetCount2Animation;
                Invoke("DelyedInvoke", 0.5f);

            }
        }

        else if (currentTask == 2)
        {
            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended && temp == true)
            {
                if (ValueCalculator.value == 0)
                {
                    temp = false;
                    completedSubTask[currentSubTask] = true;
                    currentSubTask++;
                    Highlight1.SetActive(true);
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
            }

            if (ValueCalculator.value == 7 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
            {
                temp = true;
                currentResetAnimation = resetCount3Animation;
                Invoke("DelyedInvoke", 0.5f);
                ResetHighlight.SetActive(true);
                Highlight1.SetActive(false);



            }
        }

        else if (currentTask == 3)
        {
            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended && temp == true)
            {
                if (ValueCalculator.value == 0)
                {
                    temp = false;
                    completedSubTask[currentSubTask] = true;
                    currentSubTask++;
                    Highlight1.SetActive(true);
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
                ResetHighlight.SetActive(true);
                Highlight1.SetActive(false);

                currentResetAnimation = resetCount4Animation;
                Invoke("DelyedInvoke", 0.5f);

            }
        }
        else if (currentTask == 4)
        {
            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended && temp == true)
            {
                if (ValueCalculator.value == 0)
                {
                    temp = false;
                    completedSubTask[currentSubTask] = true;
                    currentSubTask++;
                    ResetHighlight.SetActive(false);
                    Highlight1.SetActive(true);



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
                ResetHighlight.SetActive(true);
                Highlight1.SetActive(false);

                currentResetAnimation = resetCount5Animation;
                Invoke("DelyedInvoke", 0.5f);

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
        activityList2.LiftingBeed22.bestTime = 1;
        activityList2.LiftingBeed22.bestTime_string = "Completed";

        //activityList1.liftBeed01 = true;
        SaveManager.Instance.saveDataToDisk(Activity.classParent);
        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);

        sideNoteLean.TurnOff();//sideNote.SetActive(false);





    }

    private void OnDisable()
    {
        Highlight1.SetActive(false);
        Highlight2.SetActive(false);
        NotificationBtn.onClick.RemoveListener(OpenSideNote);

        activityList2.LiftingBeed22.currentSubActivity = SubTaskToSave;
       

        SaveManager.Instance.saveDataToDisk(Activity.classParent);
        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);

        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);

    }

    public void DelayedleanPulseThumbUpAnimation()
    {
        /* CancelInvoke("DelayedleanPulseThumbUpAnimation");
         leanPulseThumbUpAnimation.RemainingPulses = 5;*/

    }
    public void DelayedleanPulseThumbDownAnimation()
    {
        /* CancelInvoke("DelayedleanPulseThumbDownAnimation");
         leanPulseThumbDownAnimation.RemainingPulses = 5;*/
    }
    public void DelayedleanPulseFingerUpAnimation()
    {
        /* CancelInvoke("DelayedleanPulseFingerUpAnimation");
         leanPulseFingerUpAnimation.RemainingPulses = 5;*/

    }
    public void DelayedleanPulseFingerDownAnimation()
    {
        /*
        CancelInvoke("DelayedleanPulseFingerDownAnimation");
        leanPulseThumbDownAnimation.RemainingPulses = 5;*/

    }





    public void DelayedInvokeAnimation()
    {
        /* CancelInvoke("DelayedInvokeAnimation");
         Highlight1.SetActive(true);
         leanPulseAnimation.RemainingPulses = 5;
         animatingSprite.enabled = true;*/
    }
}

//using Lean.Gui;
//using Lean.Transition.Method;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using TMPro;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;

//public class Count_5_to_9 : MonoBehaviour
//{
//    public LeanPulse leanPulseAnimation;
//    public LeanPulse LeanResetAnimation;
//    public SpriteRenderer animatingSprite;
//    public SpriteRenderer indexAnimatingSprite;
//    public LeanRectTransformSizeDelta loadingBar;

//    public LeanEvent resetCount1Animation;
//    public LeanEvent resetCount2Animation;
//    public LeanEvent resetCount3Animation;
//    public LeanEvent resetCount4Animation;
//    public TextMeshProUGUI Notification;
//    public TextMeshProUGUI sidenoteText;
//    public TextMeshProUGUI Congratulation;

//    public GameObject NotificationPannel;
//    public Button NotificationBtn;
//    public GameObject CongratulationPannel;


//    public GameObject sideNote;
//    public GameObject beed1;
//    public GameObject beed2;
//    public GameObject beed3;
//    public GameObject beed4;
//    public GameObject beed5;
//    public GameObject[] Beeds;
//    public GameObject AnimatingSpriteGameObject;

//    public GameObject Highlight1;
//    public GameObject Highlight2;
//    public GameObject ResetHighlight;


//    public List<string> notificationData;
//    public string CongratulationText;
//    public bool[] completedSubTask;
//    bool temp;
//    int currentTask, currentSubTask;
//    public LeanEvent currentResetAnimation;
//    float completeBarValue = 500;

//    void OnEnable()
//    {
//        print(Count_1_to_4_ModelData.TaskComplete[Count_1_to_4_ModelData.TaskComplete.Length - 1] + "  " + (Count_1_to_4_ModelData.TaskComplete.Length - 1));
//        if (Count_1_to_4_ModelData.TaskComplete[Count_1_to_4_ModelData.TaskComplete.Length - 1] == true)
//        {
//            Count_1_to_4_ModelData.TaskComplete = Count_1_to_4_ModelData.TaskComplete.Select(x => x = false).ToArray();
//            Count_1_to_4_ModelData.barValue = 0;
//        }
//        completedSubTask = new bool[4];
//        currentSubTask = 0;
//        temp = false;
//        loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
//        loadingBar.BeginAllTransitions();

//        Highlight1.SetActive(true);
//        NotificationBtn.onClick.AddListener(DelayedInvokeAnimation);
//        NotificationBtn.onClick.AddListener(OpenSideNote);
//        //Highlight2.SetActive(true);

//        ResetHighlight.transform.SetParent(beed1.transform);
//        indexAnimatingSprite.transform.SetParent(Beeds[4].transform);
//        ResetHighlight.transform.localPosition = new Vector3(0, 0, 0);
//        indexAnimatingSprite.transform.localPosition = new Vector3(-0.82f, -2.22f, 2.25f);
//        for (currentTask = 0; currentTask < Count_1_to_4_ModelData.TaskComplete.Length; currentTask++)
//        {
//            print(currentTask);
//            if (Count_1_to_4_ModelData.TaskComplete[currentTask] == false)
//            {
//                sidenoteText.text = notificationData[currentTask];

//                if (currentTask == 0)
//                {
//                    Highlight1.transform.SetParent(Beeds[4].transform);
//                    Highlight1.transform.localPosition = new Vector3(0, 0, 0);
//                    Highlight2.transform.SetParent(Beeds[4].transform);
//                    Highlight2.transform.localPosition = new Vector3(0, 0, 0);
//                }
//                else
//                {


//                    Highlight1.transform.SetParent(Beeds[currentTask - 1].transform);
//                    Highlight1.transform.localPosition = new Vector3(0, 0, 0);
//                    Highlight2.transform.SetParent(Beeds[4].transform);
//                    Highlight2.transform.localPosition = new Vector3(0, 0, 0);


//                }

//                AnimatingSpriteGameObject.transform.SetParent(Beeds[currentTask].transform);
//                AnimatingSpriteGameObject.transform.localPosition = new Vector3(-2.7f, 0, 2);
//                break;
//                #region abc
//                //if (currentTask == 1)
//                //{
//                //    Highlight1.transform.SetParent(beed2.transform);
//                //    Highlight1.transform.localPosition = new Vector3(0, 0, 0);

//                //    AnimatingSpriteGameObject.transform.SetParent(beed2.transform);
//                //    AnimatingSpriteGameObject.transform.localPosition = new Vector3(1, 0, -1);
//                //    break;
//                //}
//                //if (currentTask == 2)
//                //{
//                //    Highlight1.transform.SetParent(beed3.transform);
//                //    Highlight1.transform.localPosition = new Vector3(0, 0, 0);

//                //    AnimatingSpriteGameObject.transform.SetParent(beed3.transform);
//                //    AnimatingSpriteGameObject.transform.localPosition = new Vector3(1, 0, -1);
//                //    break;
//                //}
//                //if (currentTask == 3)
//                //{
//                //    Highlight1.transform.SetParent(beed4.transform);
//                //    Highlight1.transform.localPosition = new Vector3(0, 0, 0);

//                //    AnimatingSpriteGameObject.transform.SetParent(beed4.transform);
//                //    AnimatingSpriteGameObject.transform.localPosition = new Vector3(1, 0, -1);
//                //    break;
//                //}
//                #endregion
//            }
//        }
//    }



//    void Update()
//    {
//        //loadingBar.sizeDelta = new Vector2(Count_1_to_4_ModelData.barValue, loadingBar.sizeDelta.y);
//        if (NotificationPannel.activeInHierarchy)
//        {
//            animatingSprite.enabled = false;

//        }

//        if (ValueCalculator.value == 0)
//        {
//            indexAnimatingSprite.transform.gameObject.SetActive(false);
//            temp = false;
//            ResetHighlight.SetActive(false);
//            animatingSprite.transform.gameObject.SetActive(true);



//        }
//        else
//        {
//            CancelInvoke("DelayedInvokeAnimation");
//            Highlight1.SetActive(false);
//            if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended))
//            {
//                Invoke("DelyedInvoke", 0.5f);

//            }

//            else if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
//            {
//                LeanResetAnimation.RemainingPulses = 0;
//                animatingSprite.transform.gameObject.SetActive(false);
//                indexAnimatingSprite.transform.gameObject.SetActive(false);

//                //Invoke("DelyedInvoke",0.5f);
//            }
//        }

//        for (currentTask = 0; currentTask < Count_1_to_4_ModelData.TaskComplete.Length; currentTask++)
//        {
//            print(currentTask);
//            if (Count_1_to_4_ModelData.TaskComplete[currentTask] == false)
//            {
//                Notification.text = notificationData[currentTask];

//                break;
//            }
//        }
//        if (currentTask == Count_1_to_4_ModelData.TaskComplete.Length)
//        {
//            Congratulation.text = CongratulationText;
//        }

//        if (currentTask == 0)
//        {


//            if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended))
//            {
//                if (NotificationPannel.activeInHierarchy == false)
//                    Invoke("DelayedInvokeAnimation", 1f);
//            }
//            else if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
//            {
//                leanPulseAnimation.RemainingPulses = 0;
//                animatingSprite.enabled = false;
//                CancelInvoke("DelayedInvokeAnimation");
//            }



//            if (ValueCalculator.value == 1 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
//            {



//                temp = true;
//                completedSubTask[currentSubTask] = true;
//                currentSubTask++;
//                print("hello world");

//                //currentResetAnimation = resetCount1Animation;
//                //Invoke("DelyedInvoke", 0.5f);
//                //if (currentSubTask == 4 && ValueCalculator.value == 0)
//                //{
//                //    Count_1_to_4_ModelData.barValue = 0.25f * completeBarValue;
//                //    loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
//                //    loadingBar.BeginAllTransitions();
//                //    Invoke("DelayedInvokeNotification", 1f);
//                //    Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
//                //    currentSubTask = 0;
//                //    completedSubTask = completedSubTask.Select(x => x = false).ToArray();



//                //}

//            }
//            if (currentSubTask == 2 && ValueCalculator.value == 0)
//            {
//                Count_1_to_4_ModelData.barValue = 0.25f * completeBarValue;
//                loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
//                loadingBar.BeginAllTransitions();
//                Invoke("DelayedInvokeNotification", 0.2f);
//                Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
//                currentSubTask = 0;
//                completedSubTask = completedSubTask.Select(x => x = false).ToArray();



//            }
//        }

//        else if (currentTask == 1)
//        {


//            if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended))
//            {
//                if (NotificationPannel.activeInHierarchy == false)
//                    Invoke("DelayedInvokeAnimation", 1f);
//            }
//            else if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
//            {
//                leanPulseAnimation.RemainingPulses = 0;
//                animatingSprite.enabled = false;
//                CancelInvoke("DelayedInvokeAnimation");
//            }


//            if (ValueCalculator.value == 2 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
//            {
//                temp = true;
//                completedSubTask[currentSubTask] = true;
//                currentSubTask++;
//                print("hello world");
//                //currentResetAnimation = resetCount2Animation;
//                //Invoke("DelyedInvoke", 0.5f);
//                //if (currentSubTask == 4)
//                //{
//                //    Count_1_to_4_ModelData.barValue = 0.5f * completeBarValue;
//                //    loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
//                //    loadingBar.BeginAllTransitions();
//                //    Invoke("DelayedInvokeNotification", 1f);
//                //    Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
//                //    currentSubTask = 0;
//                //    completedSubTask = completedSubTask.Select(x => x = false).ToArray();

//                //}

//            }
//            if (currentSubTask == 2 && ValueCalculator.value == 0)
//            {
//                Count_1_to_4_ModelData.barValue = 0.5f * completeBarValue;
//                loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
//                loadingBar.BeginAllTransitions();
//                Invoke("DelayedInvokeNotification", 0.2f);
//                Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
//                currentSubTask = 0;
//                completedSubTask = completedSubTask.Select(x => x = false).ToArray();

//            }
//        }

//        else if (currentTask == 2)
//        {


//            if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended))
//            {
//                if (NotificationPannel.activeInHierarchy == false)
//                    Invoke("DelayedInvokeAnimation", 1f);
//            }
//            else if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
//            {
//                leanPulseAnimation.RemainingPulses = 0;
//                animatingSprite.enabled = false;
//                CancelInvoke("DelayedInvokeAnimation");
//            }

//            if (ValueCalculator.value == 3 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
//            {
//                temp = true;
//                completedSubTask[currentSubTask] = true;
//                currentSubTask++;
//                print("hello world");
//                //currentResetAnimation = resetCount3Animation;
//                //Invoke("DelyedInvoke", 0.5f);
//                //if (currentSubTask == 4 &&  ValueCalculator.value == 0)
//                //{

//                //    Count_1_to_4_ModelData.barValue = 0.75f * completeBarValue;
//                //    loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
//                //    loadingBar.BeginAllTransitions();

//                //    Invoke("DelayedInvokeNotification", 1f);
//                //    Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
//                //    currentSubTask = 0;
//                //    completedSubTask = completedSubTask.Select(x => x = false).ToArray();
//                //}

//            }

//            if (currentSubTask == 2 && ValueCalculator.value == 0)
//            {

//                Count_1_to_4_ModelData.barValue = 0.75f * completeBarValue;
//                loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
//                loadingBar.BeginAllTransitions();

//                Invoke("DelayedInvokeNotification", 0.2f);
//                Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
//                currentSubTask = 0;
//                completedSubTask = completedSubTask.Select(x => x = false).ToArray();
//            }
//        }

//        else if (currentTask == 3)
//        {

//            if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended))
//            {
//                if (NotificationPannel.activeInHierarchy == false)
//                    Invoke("DelayedInvokeAnimation", 1f);
//            }
//            else if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
//            {
//                leanPulseAnimation.RemainingPulses = 0;
//                animatingSprite.enabled = false;
//                CancelInvoke("DelayedInvokeAnimation");
//            }

//            if (ValueCalculator.value == 4 && temp == false && Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
//            {
//                temp = true;
//                completedSubTask[currentSubTask] = true;
//                currentSubTask++;
//                print("hello world");
//                //currentResetAnimation = resetCount4Animation;
//                //Invoke("DelyedInvoke", 0.5f);
//                //if (currentSubTask == 4 && ValueCalculator.value == 0)
//                //{
//                //    Count_1_to_4_ModelData.barValue = 1f * completeBarValue;
//                //    loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
//                //    loadingBar.BeginAllTransitions();
//                //    Invoke("DelayedInvokeCongratulation", 1f);
//                //    Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
//                //    currentSubTask = 0;
//                //    completedSubTask = completedSubTask.Select(x => x = false).ToArray();

//                //}

//            }
//            if (currentSubTask == 2 && ValueCalculator.value == 0)
//            {
//                Count_1_to_4_ModelData.barValue = 1f * completeBarValue;
//                loadingBar.Data.SizeDelta.x = Count_1_to_4_ModelData.barValue;
//                loadingBar.BeginAllTransitions();
//                Invoke("DelayedInvokeCongratulation", 0.2f);
//                Count_1_to_4_ModelData.TaskComplete[currentTask] = true;
//                currentSubTask = 0;
//                completedSubTask = completedSubTask.Select(x => x = false).ToArray();

//            }
//        }



//    }

//    private void OpenSideNote()
//    {
//        sideNote.SetActive(true);
//    }

//    void DelyedInvoke()
//    {
//        CancelInvoke("DelyedInvoke");
//        ResetHighlight.SetActive(true);
//        indexAnimatingSprite.transform.gameObject.SetActive(true);

//        //currentResetAnimation.BeginAllTransitions();
//        LeanResetAnimation.RemainingPulses = 3;
//        // if the first beed is at the top stop and hide the thumb animation and start and dislay the index finger animation . and aslo highlight the first animation

//    }

//    void DelayedInvokeNotification()
//    {
//        sidenoteText.text = notificationData[currentTask];
//        sideNote.SetActive(false);
//        CancelInvoke("DelayedInvokeAnimation");
//        NotificationPannel.SetActive(true);
//        if (currentTask == 1)
//        {
//            Highlight1.transform.SetParent(Beeds[0].transform);
//            Highlight1.transform.localPosition = new Vector3(0, 0, 0);
//            Highlight2.transform.SetParent(Beeds[4].transform);
//            Highlight2.transform.localPosition = new Vector3(0, 0, 0);
//            AnimatingSpriteGameObject.transform.SetParent(Beeds[0].transform);
//            AnimatingSpriteGameObject.transform.localPosition = new Vector3(-2.7f, 0, 2);

//        }
//        else if (currentTask == 2)
//        {
//            Highlight1.transform.SetParent(Beeds[1].transform);
//            Highlight1.transform.localPosition = new Vector3(0, 0, 0);
//            Highlight2.transform.SetParent(Beeds[4].transform);
//            Highlight2.transform.localPosition = new Vector3(0, 0, 0);
//            AnimatingSpriteGameObject.transform.SetParent(Beeds[1].transform);
//            AnimatingSpriteGameObject.transform.localPosition = new Vector3(-2.7f, 0, 2);
//        }
//        else if (currentTask == 3)
//        {
//            Highlight1.transform.SetParent(Beeds[2].transform);
//            Highlight1.transform.localPosition = new Vector3(0, 0, 0);
//            Highlight2.transform.SetParent(Beeds[4].transform);
//            Highlight2.transform.localPosition = new Vector3(0, 0, 0);
//            AnimatingSpriteGameObject.transform.SetParent(Beeds[2].transform);
//            AnimatingSpriteGameObject.transform.localPosition = new Vector3(-2.7f, 0, 2);
//        }  
//        else if (currentTask == 4)
//        {
//            Highlight1.transform.SetParent(Beeds[3].transform);
//            Highlight1.transform.localPosition = new Vector3(0, 0, 0);
//            Highlight2.transform.SetParent(Beeds[4].transform);
//            Highlight2.transform.localPosition = new Vector3(0, 0, 0);
//            AnimatingSpriteGameObject.transform.SetParent(Beeds[3].transform);
//            AnimatingSpriteGameObject.transform.localPosition = new Vector3(-2.7f, 0, 2);
//        }
//    }

//    void DelayedInvokeCongratulation()
//    {
//        sideNote.SetActive(false);
//        CancelInvoke("DelayedInvokeAnimation");
//        CongratulationPannel.SetActive(true);
//        leanPulseAnimation.RemainingPulses = 0;
//        animatingSprite.enabled = false;

//    }

//    private void OnDisable()
//    {
//        NotificationBtn.onClick.RemoveListener(OpenSideNote);
//        try
//        {
//            sideNote.SetActive(false);
//        }
//        catch
//        {
//            ;
//        }
//        NotificationBtn.onClick.RemoveListener(DelayedInvokeAnimation);
//        Highlight1.SetActive(false);
//        CancelInvoke("DelayedInvokeAnimation");
//        ResetHighlight.SetActive(false);

//        //Highlight2.SetActive(false);
//    }

//    public void DelayedInvokeAnimation()
//    {
//        CancelInvoke("DelayedInvokeAnimation");
//        Highlight1.SetActive(true);
//        leanPulseAnimation.RemainingPulses = 5;
//        animatingSprite.enabled = true;
//    }


//}

