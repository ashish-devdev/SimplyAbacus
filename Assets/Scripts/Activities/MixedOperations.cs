using Lean.Gui;
using Lean.Transition.Method;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MixedOperations : MonoBehaviour
{
    [System.Serializable]
    public class MixedOpeartionJsonWrapper
    {
        public Operation[] operation;
    }

    MixedMathematicalOperations2 mixedMathematicalOperations2;
    public int Problem_Number;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI congratulationText;

    public Button notificationBtn;
    public Button nextOperationBtn;

    public GameObject multplicationTable;
    public GameObject friendTable;
    public GameObject timer_gamObject;
    public GameObject timer;

    public LeanImageFillAmount loadingBar;
    public LeanToggle leanCongratulation;
    public LeanToggle leanSideNote;
    public MixedOpeartionJsonWrapper jsonData;
    public Activity activityScriptInstance;
    public Reset reset;
    public ValueCalculator valueCalculator;
    public PositionRodsOfAbacus PositionRodsOfAbacus;

    DataTable dt;
    ActivityList1 activityList1;
    bool resultContainsdecimals;
    double answer;
    int NonDecimalDigitsInNumber;
    int decimalDigitsInNumber;
    string floatingDecimalDegitString;
    List<string> setOfNumbersInProblem;
    bool isPercentageActivity;


    private void OnEnable()
    {

        Invoke(nameof(OnEnableAfterDelay), 0.1f);
    }



    public void OnEnableAfterDelay()
    {
        isPercentageActivity = true;
        dt = new DataTable();
        Problem_Number = 0;
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
            {
                if (activityScriptInstance.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.active == true && activityScriptInstance.classActivityList[i].classData.activityList[j].activityName == ClassManager.currentActivityName && ClassManager.currentActivityIndex == j)
                {

                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.showMultiplicationTable)
                        multplicationTable.SetActive(true);
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.showFriendTable)
                        friendTable.SetActive(true);


                    for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1.completed.Length; m++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1.completed[m] == true)
                        {
                            Problem_Number++;
                        }

                    }
                    mixedMathematicalOperations2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].mixedMathematicalOperations2;
                    isPercentageActivity = activityScriptInstance.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.isPercentage;
                    activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
                    jsonData = JsonUtility.FromJson<MixedOpeartionJsonWrapper>(activityScriptInstance.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.jsonData.text);
                    nextOperationBtn.onClick.AddListener(CreateAndDisplayProblem);
                    if (Problem_Number >= jsonData.operation.Length)
                    {
                        for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1.completed.Length; m++)
                        {
                            if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1.completed[m] == true)
                                Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1.completed[m] = false;
                        }
                        Problem_Number = 0;
                    }
                }
            }
        }
        loadingBar.Data.FillAmount = (Problem_Number / (jsonData.operation.Length * 1f));
        loadingBar.BeginAllTransitions();

        notificationBtn.onClick.AddListener(StartTimer);
        CreateAndDisplayProblem();

    }


    public void OnDisable()
    {
        try
        {
            valueCalculator.numberOfDecimalPlaces = 2;
            valueCalculator.decimalPlaceString = "F2";
            nextOperationBtn.gameObject.transform.parent.gameObject.SetActive(false);
            nextOperationBtn.onClick.RemoveListener(CreateAndDisplayProblem);
            MoveBeeds_1.dosomthing -= compare_abacus_and_operation_value;
            MoveBeeds_2.dosomthing -= compare_abacus_and_operation_value;
            SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);

        }
        catch
        {
            ;
        }
    }





    public void StartTimer()
    {
        Timer.savedTime = mixedMathematicalOperations2.currentSavedTime;
        timer_gamObject.SetActive(true);
    }

    void Start()
    {
        DataTable dt = new DataTable();
        double answer = Convert.ToDouble(new DataTable().Compute("2.33+2.337", ""));

    }

    private void Update()
    {
        if (timer.gameObject.activeInHierarchy)
        {
            mixedMathematicalOperations2.currentSavedTime = Timer.currentTime;
        }
    }


    public void compare_abacus_and_operation_value()
    {

        print(answer.ToString(floatingDecimalDegitString) + "--------------" + (Math.Truncate(answer * Mathf.Pow(10, (8 - NonDecimalDigitsInNumber))) / (Mathf.Pow(10, (8 - NonDecimalDigitsInNumber)))).ToString() + "--------------------------" + ValueCalculator.value.ToString(floatingDecimalDegitString));
        if (string.Equals(RemoveExtraDecimalZeros(ValueCalculator.value.ToString(floatingDecimalDegitString)), (RemoveExtraDecimalZeros(answer.ToString(floatingDecimalDegitString)))))
        {
            activityList1.mixedMathematicalOperations1.completed[Problem_Number] = true;
            SaveManager.Instance.saveDataToDisk(Activity.classParent);
            resultText.text = (ValueCalculator.value.ToString(floatingDecimalDegitString));
            resultText.text = "Result: " + RemoveExtraDecimalZeros(resultText.text);
            MoveBeeds_1.dosomthing -= compare_abacus_and_operation_value;
            MoveBeeds_2.dosomthing -= compare_abacus_and_operation_value;
            nextOperationBtn.gameObject.transform.parent.gameObject.SetActive(true);
            Problem_Number++;
            print(123);

            loadingBar.Data.FillAmount = (Problem_Number / (jsonData.operation.Length * 1f));
            loadingBar.BeginAllTransitions();

        }
        else if (RemoveExtraDecimalZeros(ValueCalculator.value.ToString(floatingDecimalDegitString)) == RemoveExtraDecimalZeros((Math.Truncate(answer * Mathf.Pow(10, (8 - NonDecimalDigitsInNumber))) / (Mathf.Pow(10, (8 - NonDecimalDigitsInNumber)))).ToString()))
        {
            activityList1.mixedMathematicalOperations1.completed[Problem_Number] = true;
            SaveManager.Instance.saveDataToDisk(Activity.classParent);
            resultText.text = (ValueCalculator.value.ToString(floatingDecimalDegitString));
            resultText.text = "Result: "+RemoveExtraDecimalZeros(resultText.text);
            MoveBeeds_1.dosomthing -= compare_abacus_and_operation_value;
            MoveBeeds_2.dosomthing -= compare_abacus_and_operation_value;
            nextOperationBtn.gameObject.transform.parent.gameObject.SetActive(true);
            //display next btn
            Problem_Number++;
            print(23);

            loadingBar.Data.FillAmount = (Problem_Number / (jsonData.operation.Length * 1f));
            loadingBar.BeginAllTransitions();

        }






        if (Problem_Number >= jsonData.operation.Length)
        {




            print(000);
            congratulationText.text = "Congratulation.";
            timer_gamObject.SetActive(false);
            nextOperationBtn.gameObject.transform.parent.gameObject.SetActive(false);
            leanCongratulation.TurnOn();
            leanSideNote.TurnOff();

            if (mixedMathematicalOperations2.bestTime == -1)
            {
                timer.SetActive(false);

                mixedMathematicalOperations2.currentSavedTime = 0;
                mixedMathematicalOperations2.bestTime_string = Timer.timerText.text;
                mixedMathematicalOperations2.bestTime = Timer.currentTime;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }
            else if (mixedMathematicalOperations2.bestTime > Timer.currentTime)
            {
                timer.SetActive(false);

                mixedMathematicalOperations2.currentSavedTime = 0;
                mixedMathematicalOperations2.bestTime_string = Timer.timerText.text;
                mixedMathematicalOperations2.bestTime = Timer.currentTime;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }
            else
            {
                timer.SetActive(false);
                mixedMathematicalOperations2.currentSavedTime = 0;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);

            }
        }


    }


    public void CreateAndDisplayProblem()
    {

        reset.RESET();
        MoveBeeds_1.dosomthing += compare_abacus_and_operation_value;
        MoveBeeds_2.dosomthing += compare_abacus_and_operation_value;

        //reset the abacus brfore moving further....
        resultText.text = "Result: ";
        if (Problem_Number >= jsonData.operation.Length)
        {

        }
        else
        {
            questionText.text = jsonData.operation[Problem_Number].problem;
            answer = Convert.ToDouble(new DataTable().Compute(jsonData.operation[Problem_Number].result.ToString(), ""));
            resultContainsdecimals = CheckForDecimals(answer);
            if (!resultContainsdecimals)
            {
                NonDecimalDigitsInNumber = numberOfNonDecimalDigits(answer);
                floatingDecimalDegitString = "F0";

            }
            else
            {
                NonDecimalDigitsInNumber = numberOfNonDecimalDigits(answer);
                floatingDecimalDegitString = ReturnFloatingDigitString(NonDecimalDigitsInNumber);
            }

            //  setOfNumbersInProblem = new List<string>();
            //   SegregateAllNumbersFromTheExpression(jsonData.operation[Problem_Number].result.ToString(), answer.ToString());

        }

        valueCalculator.decimalPlaceString = ReturnFloatingDigitString(NonDecimalDigitsInNumber);

        if (RemoveExtraDecimalZeros(answer.ToString()).Contains("."))
        {
            if (RemoveExtraDecimalZeros(answer.ToString()).Length <= 8)
            {
                valueCalculator.numberOfDecimalPlaces = RemoveExtraDecimalZeros(answer.ToString()).Length - RemoveExtraDecimalZeros(answer.ToString()).IndexOf(".") - 1;
                valueCalculator.decimalPlaceString ="F"+ valueCalculator.numberOfDecimalPlaces;
                PositionRodsOfAbacus.endingRod = RemoveExtraDecimalZeros(answer.ToString()).Length - 1;
            }
            else
            {
                valueCalculator.numberOfDecimalPlaces = 8 - NonDecimalDigitsInNumber;

                PositionRodsOfAbacus.endingRod = 8;
            }
        }
        else
        {
            valueCalculator.numberOfDecimalPlaces = 0;
            valueCalculator.decimalPlaceString = "F0";
            PositionRodsOfAbacus.endingRod = RemoveExtraDecimalZeros(answer.ToString()).Length ;
        }

        if (isPercentageActivity)
        {
            valueCalculator.numberOfDecimalPlaces = 4;
            valueCalculator.decimalPlaceString = "F4";
            PositionRodsOfAbacus.endingRod = 8;
            
        }


        PositionRodsOfAbacus.EditRod();

    }

    public bool CheckForDecimals(double val)
    {
        if (val == Math.Truncate(val))
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    public int numberOfNonDecimalDigits(double val)
    {
        int count = 0;
        while (val >= 1)
        {
            val = val / 10;
            count++;
        }

        print(count);
        return count;

    }

    public string ReturnFloatingDigitString(int val)
    {
        string[] str = { "F0", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8" };
        return str[8 - val];

    }

    public string RemoveExtraDecimalZeros(string str)
    {
        try
        {

            while (Convert.ToDouble(new DataTable().Compute(str, "")) == Convert.ToDouble(new DataTable().Compute(str.Remove(str.Length - 1), "")))
            {

                str = str.Remove(str.Length - 1);
            }
            return str;
        }
        catch
        {
            return str;
        }
    }

    public void SegregateAllNumbersFromTheExpression(string question, string answer)
    {
        int len = question.Length;
        int i = 0;
        int totalnumber = -1;
        bool newNumber = true;
        while (i < len)
        {
            if (Char.IsDigit(question[i]) || question[i].ToString() == ".")
            {
                if (newNumber)
                {
                    setOfNumbersInProblem.Add("");
                    totalnumber++;
                }
                newNumber = false;
                setOfNumbersInProblem[totalnumber] += question[i];
            }
            else
            {
                newNumber = true;
            }
            i++;
        }

        setOfNumbersInProblem.Add("");
        totalnumber++;
        setOfNumbersInProblem[totalnumber] = answer;

        for (int j = 0; j < setOfNumbersInProblem.Count; j++)
        {
            print(setOfNumbersInProblem[j]);
        }

        for (int j = 0; j < setOfNumbersInProblem.Count; j++)
        {
            setOfNumbersInProblem[j] = RemoveExtraDecimalZeros(setOfNumbersInProblem[j]);
            print(setOfNumbersInProblem[j].Length);
        }
    }

    public int MaxDecimalPLaces(List<string> strOfNumbers)
    {
        int max = 0;
        for (int i = 0; i < strOfNumbers.Count; i++)
        {
            if (strOfNumbers[i].Contains("."))
            {
                if (max <= strOfNumbers[i].Substring(strOfNumbers[i].IndexOf(".") + 1).Length)
                {
                    max = strOfNumbers[i].Substring(strOfNumbers[i].IndexOf(".") + 1).Length;
                }

            }

        }

        print(max);
        return max;
    }
}
