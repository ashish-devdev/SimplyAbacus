using Lean.Gui;
using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LiftingBeedActivity : MonoBehaviour
{
    public Activity activityScriptInstance;
    public Reset reset;
    public LeanToggle congratulationLean;
    public LeanToggle sideNoteLean;
    public LeanToggle notificationLean;
    public LeanImageFillAmount loadingBar;
    public Button notificationBtn;
    public GameObject timer;
    public ValueCalculator valueCalculator;

    LiftBeeds1 liftBeeds1;
    LiftBeeds2 liftBeeds2;
    LiftBeeds liftBeeds;
    public TextMeshProUGUI sideNote;
    int totalCount ;
    int cuurentCount;
    int currentSubOperation;
    int totalSubOperation;
    bool resetHappened;


    private void OnEnable()
    {
        Invoke(nameof(OnEnableWithDelay), 0.1f);
    }


    public void OnEnableWithDelay()
    {
        currentSubOperation = 0;
        totalCount = 1;
        cuurentCount = 0;
        resetHappened = true;
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].liftBeeds.active == true && ClassManager.currentActivityIndex == j)
                    {
                        liftBeeds = activityScriptInstance.classActivityList[i].classData.activityList[j].liftBeeds;
                        liftBeeds1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeeds1;
                        liftBeeds2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftBeeds2;
                        totalSubOperation = liftBeeds.numbers.Length;
                    }
                }
            }
        }



        MoveBeeds_1.dosomthing += CompareValue;
        MoveBeeds_2.dosomthing += CompareValue;


        loadingBar.Data.FillAmount = ((currentSubOperation) / (totalSubOperation * 1f));
        loadingBar.BeginAllTransitions();
        notificationBtn.onClick.AddListener(StartTimer);
        sideNote.text = "Count to " + liftBeeds.numbers[currentSubOperation];

        valueCalculator.decimalPlaceString = "F0";

    }

    public void StartTimer()
    {
        Timer.savedTime = 0;
        timer.SetActive(true);
    }

    void Update()
    {
        if (timer.activeInHierarchy)
            liftBeeds2.currentSavedTime = Timer.currentTime;

    }


    public void CompareValue()
    {
        if (ValueCalculator.value1 == (decimal)liftBeeds.numbers[currentSubOperation] && resetHappened == true)
        {
            resetHappened = false;
            //cuurentCount++;
            Invoke("DelayedInvokeReset", 0.5f);
            sideNote.text = "<color=green>Well done!! Now, reset the abacus to 0</color>";//cuurentsuboperation will be incremented by 1 at this point.

        }

        if (ValueCalculator.value1 == 0 && resetHappened == false)
        {
            resetHappened = true;
            cuurentCount++;
            sideNote.text = "Count to " + liftBeeds.numbers[currentSubOperation] ;//cuurentsuboperation will be incremented by 1 at this point.

        }

        if (totalCount == cuurentCount)
        {
            cuurentCount = 0;
            currentSubOperation++;

            loadingBar.Data.FillAmount = ((currentSubOperation) / (totalSubOperation * 1f));
            loadingBar.BeginAllTransitions();
            try
            {
                sideNote.text = "Count to " + liftBeeds.numbers[currentSubOperation];//cuurentsuboperation will be incremented by 1 at this point.
            }
            catch
            {; }


            if (currentSubOperation == totalSubOperation)
            {
                timer.SetActive(false);

                reset.RESET();
                congratulationLean.TurnOn();
                sideNoteLean.TurnOff();
                liftBeeds1.completed = true;
                SaveManager.Instance.saveDataToDisk(Activity.classParent);
                liftBeeds2.currentSavedTime = 0;
                if (liftBeeds2.bestTime == -1)
                {
                    liftBeeds2.bestTime = Timer.currentTime;
                    liftBeeds2.bestTime_string = Timer.timerText.text;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }
                else if (liftBeeds2.bestTime > Timer.currentTime)
                {
                    print(5555);
                    liftBeeds2.bestTime = Timer.currentTime;
                    liftBeeds2.bestTime_string = Timer.timerText.text;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }

            }


        }


            
        
    }



    private void OnDisable()
    {
        timer.SetActive(false);
        valueCalculator.decimalPlaceString = "F2";

        notificationBtn.onClick.RemoveListener(StartTimer);
        MoveBeeds_1.dosomthing -= CompareValue;
        MoveBeeds_2.dosomthing -= CompareValue;

    }


    public void DelayedInvokeReset()
    {
      //  resetHappened =true;
      //  reset.RESET();
        try
        {
           // sideNote.text = "Represent " + liftBeeds.numbers[currentSubOperation] + " in abacus.";//cuurentsuboperation will be incremented by 1 at this point.
           // sideNote.text = "Represent Zero(0) in abacus.";//cuurentsuboperation will be incremented by 1 at this point.
        }
        catch
        {
            ;
        }

    }

}
