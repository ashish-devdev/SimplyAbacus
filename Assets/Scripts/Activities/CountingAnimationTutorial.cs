using Lean.Gui;
using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountingAnimationTutorial : MonoBehaviour
{
    public Activity activityScriptInstance;

    public Button notificationBtn;

    public LeanToggle sideNoteLean;
    public LeanToggle notificationLean;
    public LeanToggle congratulationLean;

    public LeanImageFillAmount loadingBar;

    public TextMeshProUGUI sidenoteText;
    public TextMeshProUGUI notificationText;

    public GameObject[] beedInUnitsPlace;
    public GameObject[] beedInTensPlace;

    public GameObject notification;

    public GameObject Highlight1;

    public LeanPulse thumbsUpAnimation;
    public LeanPulse fingerDownAnimation;

    public SpriteRenderer thumbsUpSprite;
    public SpriteRenderer fingerDownSprite;

    public ValueCalculator valueCalculator;

    ActivityList1 activityList1;
    ActivityList2 activityList2;
    float[] numbers;


    int currentTask;
    int currentSubTask;
    int totalTask;
    int totalSubTask;

    int numberOfTimeToRepeate;
    int counter;
    bool countingUp;

    List<int> numbersRequiredForAnimation;
    List<int> currentValueToCheck;

    private void OnEnable()
    {
        Invoke(nameof(OnEnableWithDelay), 0.1f);

        currentTask = 0;
        currentSubTask = 0;
        countingUp = true;
        counter = 0;
        Highlight1.SetActive(false);
        fingerDownSprite.enabled = false;

        thumbsUpSprite.enabled = false;
        MakeALlBeedsUnintractable();
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].animatingCountingTutorial.active = true && ClassManager.currentActivityIndex == j)
                    {
                        activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
                        activityList2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j];
                        numbers = new float[activityScriptInstance.classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes.Length];
                        numbers = activityScriptInstance.classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes;
                        numberOfTimeToRepeate = activityScriptInstance.classActivityList[i].classData.activityList[j].animatingCountingTutorial.numberOfTimesToRepeate;
                        for (int k = 0; k < activityScriptInstance.classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes.Length; k++)
                        {
                            if (activityList1.animatingCountingTutorial1.completed[k] == true)
                            {
                                currentTask++;
                            }

                        }
                        totalTask = activityScriptInstance.classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes.Length;
                        if (currentTask == (activityScriptInstance.classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes.Length))
                        {
                            for (int k = 0; k < activityScriptInstance.classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes.Length; k++)
                            {
                                activityList1.animatingCountingTutorial1.completed[k] = false;
                            }

                            currentTask = 0;
                        }

                    }

                }
            }
        }



        MoveBeeds_1.dosomthing += CompareValue;
        MoveBeeds_2.dosomthing += CompareValue;

        notificationBtn.onClick.AddListener(openSideNote);
        notificationBtn.onClick.AddListener(ShowHighlight);
        notificationBtn.onClick.AddListener(DelayedInvokeResetAnimations);

        notificationText.text = "Count to " + numbers[currentTask];
        loadingBar.Data.FillAmount = currentTask * 1f / totalTask;
        loadingBar.BeginAllTransitions();
        PerformTask();
    }

    public void OnEnableWithDelay()
    {
        valueCalculator.decimalPlaceString = "F0";
    }


    private void Update()
    {
        if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary))
        {
            fingerDownSprite.enabled = false;

            thumbsUpSprite.enabled = false;

            //Invoke("DelyedInvoke",0.5f);
        }
        try
        {
            if (!notification.transform.gameObject.activeInHierarchy)
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    thumbsUpAnimation.RemainingPulses = 0;
                    fingerDownAnimation.RemainingPulses = 0;

                    Invoke("DelayedInvokeResetAnimations", 0.5f);

                }
        }
        catch
        {
            ;
        }

    }

    public void DelayedInvokeResetAnimations()
    {
        fingerDownSprite.enabled = true;

        thumbsUpSprite.enabled = true;
        thumbsUpAnimation.RemainingPulses = 5;
        fingerDownAnimation.RemainingPulses = 5;

    }

    public void PerformTask()
    {
        if (currentTask < totalTask)
        {


            numbersRequiredForAnimation = new List<int>();
            currentValueToCheck = new List<int>();
            numbersRequiredForAnimation = GetNumbers(int.Parse(numbers[currentTask].ToString()));
            currentValueToCheck = GetValueToCompare(numbersRequiredForAnimation);


            MakeRequiredBeedIntractable();
        }
        else
        {
            //open celebration


            fingerDownSprite.transform.gameObject.SetActive(false);
            thumbsUpSprite.transform.gameObject.SetActive(false);

            fingerDownSprite.enabled = false;

            thumbsUpSprite.enabled = false;
            activityList2.animatingCountingTutorial2.bestTime = 0;
            activityList2.animatingCountingTutorial2.bestTime_string = "Completed";
            congratulationLean.TurnOn();
        }

    }

    public void MakeRequiredBeedIntractable()
    {
        MakeALlBeedsUnintractable();

        if (currentSubTask < totalSubTask)
        {
            if (numbersRequiredForAnimation[currentSubTask] < 10)
            {
                try
                {
                    if ((numbersRequiredForAnimation[currentSubTask] == 5 && !countingUp) || countingUp)
                    {

                        beedInUnitsPlace[numbersRequiredForAnimation[currentSubTask] - 1].GetComponent<BoxCollider>().enabled = true;
                        Highlight1.transform.SetParent(beedInUnitsPlace[numbersRequiredForAnimation[currentSubTask] - 1].transform);
                        Highlight1.transform.localPosition = new Vector3(0, 0, 0);

                        thumbsUpSprite.transform.SetParent(beedInUnitsPlace[numbersRequiredForAnimation[currentSubTask] - 1].transform);
                        fingerDownSprite.transform.SetParent(beedInUnitsPlace[numbersRequiredForAnimation[currentSubTask] - 1].transform);

                        if (countingUp && numbersRequiredForAnimation[currentSubTask] < 5|| !countingUp && numbersRequiredForAnimation[currentSubTask] == 5)
                        {
                            thumbsUpSprite.transform.localPosition = new Vector3(-1.25f, -0.25f, 2f);
                            thumbsUpSprite.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                            thumbsUpSprite.transform.gameObject.SetActive(true);
                            fingerDownSprite.transform.gameObject.SetActive(false);
                        }
                        else if (!countingUp && numbersRequiredForAnimation[currentSubTask] < 5|| countingUp && numbersRequiredForAnimation[currentSubTask] == 5)
                        {
                            fingerDownSprite.transform.localPosition = new Vector3(-1.25f, -2.3f, 2f);
                            fingerDownSprite.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                            thumbsUpSprite.transform.gameObject.SetActive(false);
                            fingerDownSprite.transform.gameObject.SetActive(true);
                        }



                    }
                    else
                    {
                        beedInUnitsPlace[0].GetComponent<BoxCollider>().enabled = true;
                        Highlight1.transform.SetParent(beedInUnitsPlace[0].transform);
                        Highlight1.transform.localPosition = new Vector3(0, 0, 0);

                        thumbsUpSprite.transform.SetParent(beedInUnitsPlace[0].transform);
                        fingerDownSprite.transform.SetParent(beedInUnitsPlace[0].transform);

                        fingerDownSprite.transform.localPosition = new Vector3(-1.25f, -2.3f, 2f);
                        fingerDownSprite.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                        thumbsUpSprite.transform.gameObject.SetActive(false);
                        fingerDownSprite.transform.gameObject.SetActive(true);


                    }
                }
                catch
                {
                    print("Number is 0");
                }
            }
            else if (numbersRequiredForAnimation[currentSubTask] < 100)
            {
                try
                {
                    if ((numbersRequiredForAnimation[currentSubTask] == 50 && !countingUp) || countingUp)
                    {
                        beedInTensPlace[(numbersRequiredForAnimation[currentSubTask] / 10) - 1].GetComponent<BoxCollider>().enabled = true;
                        Highlight1.transform.SetParent(beedInTensPlace[(numbersRequiredForAnimation[currentSubTask] / 10) - 1].transform);
                        Highlight1.transform.localPosition = new Vector3(0, 0, 0);

                        thumbsUpSprite.transform.SetParent(beedInTensPlace[(numbersRequiredForAnimation[currentSubTask]/10) - 1].transform);
                        fingerDownSprite.transform.SetParent(beedInTensPlace[(numbersRequiredForAnimation[currentSubTask] /10)- 1].transform);

                        if (countingUp && numbersRequiredForAnimation[currentSubTask] < 50 || !countingUp && numbersRequiredForAnimation[currentSubTask] == 50)
                        {
                            thumbsUpSprite.transform.localPosition = new Vector3(-1.25f, -0.25f, 2f);
                            thumbsUpSprite.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                            thumbsUpSprite.transform.gameObject.SetActive(true);
                            fingerDownSprite.transform.gameObject.SetActive(false);
                        }
                        else if (!countingUp && numbersRequiredForAnimation[currentSubTask] < 50 || countingUp && numbersRequiredForAnimation[currentSubTask] == 50)
                        {
                            fingerDownSprite.transform.localPosition = new Vector3(-1.25f, -2.3f, 2f);
                            fingerDownSprite.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                            thumbsUpSprite.transform.gameObject.SetActive(false);
                            fingerDownSprite.transform.gameObject.SetActive(true);
                        }


                    }
                    else
                    {
                        beedInTensPlace[0].GetComponent<BoxCollider>().enabled = true;
                        Highlight1.transform.SetParent(beedInTensPlace[0].transform);
                        Highlight1.transform.localPosition = new Vector3(0, 0, 0);

                        thumbsUpSprite.transform.SetParent(beedInTensPlace[0].transform);
                        fingerDownSprite.transform.SetParent(beedInTensPlace[0].transform);

                        fingerDownSprite.transform.localPosition = new Vector3(-1.25f, -2.3f, 2f);
                        fingerDownSprite.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                        thumbsUpSprite.transform.gameObject.SetActive(false);
                        fingerDownSprite.transform.gameObject.SetActive(true);

                    }
                }
                catch
                {
                    print("Number is 0");
                }
            }
        }
    }

    public void CompareValue()
    {

        if (currentTask < totalTask)
        {
            if (ValueCalculator.value1 == currentValueToCheck[currentSubTask + 1] && countingUp)
            {
                currentSubTask++;
            }
            else if (ValueCalculator.value1 == currentValueToCheck[currentSubTask] && !countingUp)
            {
                currentSubTask--;

            }


        }

        if (currentSubTask > totalSubTask - 1)
        {
            countingUp = false;
            currentSubTask--;
        }
        if (currentSubTask < 0)
        {
            countingUp = true;
            currentSubTask++;
            counter++;

        }

        if (counter == numberOfTimeToRepeate)
        {
            activityList1.animatingCountingTutorial1.completed[currentTask] = true;
            currentTask++;
            loadingBar.Data.FillAmount = currentTask * 1f / totalTask;
            loadingBar.BeginAllTransitions();
            if (currentTask < totalTask)
            {
                sideNoteLean.TurnOff();
                Highlight1.SetActive(false);
                OpenNotificationNote();
            }
            currentSubTask = 0;
            countingUp = true;
            counter = 0;
            PerformTask();
        }

        MakeRequiredBeedIntractable();


    }

    public List<int> GetNumbers(int num)
    {
        List<int> a;
        int digit;
        int multiple = 1;
        a = new List<int>();
        while (num > 0)
        {

            digit = num % 10;
            if (digit == 0)
            {
                ;//do nothing
            }
            else if (digit <= 5)
            {
                a.Add(digit * multiple);

            }
            else
            {
                a.Add(5 * multiple);
                a.Add((digit - 5) * multiple);

            }
            num = num / 10;
            multiple = multiple * 10;
        }
        totalSubTask = a.Count;
        a.Reverse();
        return a;
    }

    public List<int> GetValueToCompare(List<int> nums)
    {
        List<int> returnNums = new List<int>();
        returnNums.Add(0);
        for (int i = 0; i < nums.Count; i++)
        {
            returnNums.Add(nums[i] + returnNums[returnNums.Count - 1]);

        }
        return returnNums;
    }





    public void MakeALlBeedsUnintractable()
    {
        for (int i = 0; i < beedInUnitsPlace.Length; i++)
        {
            beedInUnitsPlace[i].GetComponent<BoxCollider>().enabled = false;

        }
        for (int i = 0; i < beedInTensPlace.Length; i++)
        {
            beedInTensPlace[i].GetComponent<BoxCollider>().enabled = false;

        }

    }


    public void MakeALlBeedsIntractable()
    {
        for (int i = 0; i < beedInUnitsPlace.Length; i++)
        {
            beedInUnitsPlace[i].GetComponent<BoxCollider>().enabled = true;

        }
        for (int i = 0; i < beedInTensPlace.Length; i++)
        {
            beedInTensPlace[i].GetComponent<BoxCollider>().enabled = true;

        }

    }



    private void OnDisable()
    {
        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
        SaveManager.Instance.saveDataToDisk(Activity.classParent);
        notificationBtn.onClick.RemoveListener(openSideNote);
        notificationBtn.onClick.RemoveListener(ShowHighlight);
        notificationBtn.onClick.RemoveListener(DelayedInvokeResetAnimations);
        Highlight1.SetActive(false);
        fingerDownSprite.enabled = true;

        thumbsUpSprite.enabled = true;

        fingerDownSprite.transform.gameObject.SetActive(false);
        thumbsUpSprite.transform.gameObject.SetActive(false);
        valueCalculator.decimalPlaceString = "F2";

        MakeALlBeedsIntractable();
        MoveBeeds_1.dosomthing -= CompareValue;
        MoveBeeds_2.dosomthing -= CompareValue;
    }


    public void openSideNote()
    {
        sideNoteLean.TurnOn();      // sideNote.SetActive(true);
        sidenoteText.text = "Count to " + numbers[currentTask];
    }
    public void OpenNotificationNote()
    {

        notificationText.text = "Count to " + numbers[currentTask];
        notificationLean.TurnOn();      // sideNote.SetActive(true);
    }
    public void ShowHighlight()
    {
        Highlight1.SetActive(true);
    }

}
