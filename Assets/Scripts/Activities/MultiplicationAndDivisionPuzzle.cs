using Lean.Gui;
using Lean.Transition.Method;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplicationAndDivisionPuzzle : MonoBehaviour
{

    [System.Serializable]
    public class MultiplicationAndDivisionPuzzleJsonWrapper
    {
        public MultiplicationAndDivisionQuestions[] questions;
    }

    double answer;
    int problem_number;
    int NonDecimalDigitsInNumber;
    int decimalDigitsInNumber;
    string floatingDecimalDegitString;
    bool resultContainsdecimals;

    MultiplicationDivisionPuzzle2 multiplicationDivisionPuzzle2;
    ActivityList1 activityList1;
    MultiplicationAndDivisionPuzzleJsonWrapper jsonData;


    public Activity activityScriptInstance;
    public Reset reset;

    public GameObject multplicationTable;
    public GameObject friendTable;
    public GameObject timer_gamObject;

    public LeanImageFillAmount loadingBar;
    public LeanToggle leanCongratulation;
    public LeanToggle leanSideNote;

    public TextMeshProUGUI congratulationText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI questionText;

    public Button nextOperationBtn;
    public Button notificationBtn;

    public PositionRodsOfAbacus positionRodsOfAbacus;
    public ValueCalculator valueCalculator;
    public string[] allNumberInOperationString;



    private void OnEnable()
    {
        problem_number = 0;
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.active == true && ClassManager.currentActivityIndex == j)
                    {
                        if (activityScriptInstance.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.showMultiplicationTable)
                            multplicationTable.SetActive(true);
                        if (activityScriptInstance.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.showFriendTable)
                            friendTable.SetActive(true);


                        for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1.completed.Length; m++)
                        {
                            if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1.completed[m] == true)
                            {
                                problem_number++;
                            }
                        }

                        multiplicationDivisionPuzzle2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationDivisionPuzzle2;
                        activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
                        jsonData = JsonUtility.FromJson<MultiplicationAndDivisionPuzzleJsonWrapper>(activityScriptInstance.classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.jsonData.text);
                        nextOperationBtn.onClick.AddListener(CreateAndDisplayProblem);

                        if (problem_number >= jsonData.questions.Length)
                        {
                            for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1.completed.Length; m++)
                            {
                                if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1.completed[m] == true)
                                    Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1.completed[m] = false;
                            }
                            problem_number = 0;
                        }


                    }
                }
            }
        }

        loadingBar.Data.FillAmount = (problem_number / (jsonData.questions.Length * 1f));
        loadingBar.BeginAllTransitions();


        notificationBtn.onClick.AddListener(StartTimer);
        CreateAndDisplayProblem();

    }

    private void OnDisable()
    {
        try
        {
            notificationBtn.onClick.RemoveListener(StartTimer);
            valueCalculator.decimalPlaceString = "F2";
            valueCalculator.numberOfDecimalPlaces = 2;
            positionRodsOfAbacus.startingRod = 0;
            positionRodsOfAbacus.endingRod = 8;
            positionRodsOfAbacus.EditRod();
            SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            nextOperationBtn.gameObject.transform.parent.gameObject.SetActive(false);
            nextOperationBtn.onClick.RemoveListener(CreateAndDisplayProblem);
            MoveBeeds_1.dosomthing -= compare_abacus_and_operation_value;
            MoveBeeds_2.dosomthing -= compare_abacus_and_operation_value;
            friendTable.SetActive(false);
            multplicationTable.SetActive(false);



        }
        catch
        {
            ;
        }
    }

    void Start()
    {



    }


    void Update()
    {


        if (timer_gamObject.activeInHierarchy)
        {
            multiplicationDivisionPuzzle2.currentSavedTime = Timer.currentTime;
        }
    }

    public void StartTimer()
    {
        Timer.savedTime = multiplicationDivisionPuzzle2.currentSavedTime;
        timer_gamObject.SetActive(true);
    }


    public void CreateAndDisplayProblem()
    {



        reset.RESET();
        MoveBeeds_1.dosomthing += compare_abacus_and_operation_value;
        MoveBeeds_2.dosomthing += compare_abacus_and_operation_value;
        allNumberInOperationString = new string[] { };
        resultText.text = "Result: ";
        if (problem_number >= jsonData.questions.Length)
        {

        }
        else
        {
            questionText.text = jsonData.questions[problem_number].problem;

            answer = Convert.ToDouble(new DataTable().Compute(jsonData.questions[problem_number].result.ToString(), ""));
            resultContainsdecimals = CheckForDecimals(answer);
            if (!resultContainsdecimals)
            {
                NonDecimalDigitsInNumber = numberOfNonDecimalDigits(answer);
                floatingDecimalDegitString = "F0";
            }

            else
            {
                NonDecimalDigitsInNumber = numberOfNonDecimalDigits(answer);
                print("NonDecimalDigitsInNumber" + NonDecimalDigitsInNumber);
                floatingDecimalDegitString = ReturnFloatingDigitString(NonDecimalDigitsInNumber);


            }
            valueCalculator.numberOfDecimalPlaces = GetNumberOfDecimalDigits((decimal)answer);
            valueCalculator.decimalPlaceString = "F" + valueCalculator.numberOfDecimalPlaces.ToString();

            /*  if (jsonData.questions[problem_number].numbers !=null)
              {
                  valueCalculator.numberOfDecimalPlaces = GetMaxDecimalPoints(jsonData.questions[problem_number].numbers, answer.ToString());
                  valueCalculator.decimalPlaceString = "F" + valueCalculator.numberOfDecimalPlaces.ToString();
                  floatingDecimalDegitString = valueCalculator.decimalPlaceString;
                  NonDecimalDigitsInNumber = GetMaxNonDecimalDigit(jsonData.questions[problem_number].numbers, answer);


              }*/
            positionRodsOfAbacus.endingRod = (valueCalculator.numberOfDecimalPlaces + NonDecimalDigitsInNumber - 1) > 8 ? 8 : (valueCalculator.numberOfDecimalPlaces + NonDecimalDigitsInNumber - 1);
            positionRodsOfAbacus.EditRod();


        }
    }




    public void compare_abacus_and_operation_value()
    {
        print(ValueCalculator.value1.ToString(floatingDecimalDegitString));
        print(answer.ToString(floatingDecimalDegitString) + "--------------" + (Math.Truncate(answer * Mathf.Pow(10, (8 - NonDecimalDigitsInNumber))) / (Mathf.Pow(10, (8 - NonDecimalDigitsInNumber)))).ToString());
        if (RemoveExtraDecimalZeros(ValueCalculator.value1.ToString()) == RemoveExtraDecimalZeros(answer.ToString(floatingDecimalDegitString)))
        {

            resultText.text = (ValueCalculator.value1.ToString(floatingDecimalDegitString));
            resultText.text = "Result: " + RemoveExtraDecimalZeros(resultText.text);
            MoveBeeds_1.dosomthing -= compare_abacus_and_operation_value;
            MoveBeeds_2.dosomthing -= compare_abacus_and_operation_value;
            nextOperationBtn.gameObject.transform.parent.gameObject.SetActive(true);
            activityList1.multiplicationDivisionPuzzle1.completed[problem_number] = true;
            SaveManager.Instance.saveDataToDisk(Activity.classParent);

            problem_number++;
            print(123 + jsonData.questions.Length);

            loadingBar.Data.FillAmount = (problem_number / (jsonData.questions.Length * 1f));
            loadingBar.BeginAllTransitions();




        }
        else if (ValueCalculator.value1.ToString() == RemoveExtraDecimalZeros((Math.Truncate(answer * Mathf.Pow(10, (8 - NonDecimalDigitsInNumber))) / (Mathf.Pow(10, (8 - NonDecimalDigitsInNumber)))).ToString()))
        {
            resultText.text = (ValueCalculator.value1.ToString(floatingDecimalDegitString));
            resultText.text = RemoveExtraDecimalZeros(resultText.text);
            MoveBeeds_1.dosomthing -= compare_abacus_and_operation_value;
            MoveBeeds_2.dosomthing -= compare_abacus_and_operation_value;
            nextOperationBtn.gameObject.transform.parent.gameObject.SetActive(true);
            activityList1.multiplicationDivisionPuzzle1.completed[problem_number] = true;
            SaveManager.Instance.saveDataToDisk(Activity.classParent);
            //display next btn
            problem_number++;
            print(23);

            loadingBar.Data.FillAmount = (problem_number / (jsonData.questions.Length * 1f));
            loadingBar.BeginAllTransitions();
        }








        if (problem_number >= jsonData.questions.Length)
        {
            print(000);
            congratulationText.text = "Congratulation.";
            timer_gamObject.SetActive(false);
            nextOperationBtn.gameObject.transform.parent.gameObject.SetActive(false);
            leanCongratulation.TurnOn();
            leanSideNote.TurnOff();


            multiplicationDivisionPuzzle2.currentSavedTime = 0;
            //activityList1.abacusOperations = true;
            if (multiplicationDivisionPuzzle2.bestTime == -1)
            {
                multiplicationDivisionPuzzle2.bestTime = Timer.currentTime;
                multiplicationDivisionPuzzle2.bestTime_string = Timer.timerText.text;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }
            else if (multiplicationDivisionPuzzle2.bestTime > Timer.currentTime)
            {
                print(5555);
                multiplicationDivisionPuzzle2.bestTime = Timer.currentTime;
                multiplicationDivisionPuzzle2.bestTime_string = Timer.timerText.text;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }



            SaveManager.Instance.saveDataToDisk(Activity.classParent);


        }
    }

    public string RemoveExtraDecimalZeros(string str)
    {
        while (Convert.ToDouble(new DataTable().Compute(str, "")) == Convert.ToDouble(new DataTable().Compute(str.Remove(str.Length - 1), "")))
        {
            str = str.Remove(str.Length - 1);
        }
        return str;
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

    public int GetNumberOfDecimalDigits(decimal num)
    {
        if (RemoveExtraDecimalZeros(num.ToString()).Contains("."))
        {
            return RemoveExtraDecimalZeros(num.ToString()).Length - RemoveExtraDecimalZeros(num.ToString()).IndexOf(".") - 1;

        }
        else
            return 0;
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

    public int GetMaxNonDecimalDigit(decimal[] num, double ans)
    {
        int max = -1;
        for (int i = 0; i < num.Length; i++)
        {
            if (numberOfNonDecimalDigits((double)num[i]) > max)
            {
                max = numberOfNonDecimalDigits((double)num[i]);

            }
        }
        if (max < numberOfNonDecimalDigits(ans))
            max = numberOfNonDecimalDigits(ans);

        if (max == -1)
            return 0;

        return max;
    }

    public int GetMaxDecimalPoints(decimal[] num, string ans)
    {
        int max = -1;
        for (int i = 0; i < num.Length; i++)
        {
            if (GetNumberOfDecimalDigits(num[i]) > max)
                max = GetNumberOfDecimalDigits(num[i]);
        }


        if (RemoveExtraDecimalZeros(ans).Contains("."))
        {
            if (max < (RemoveExtraDecimalZeros(ans).Length - RemoveExtraDecimalZeros(ans).IndexOf(".") - 1))
                max = (RemoveExtraDecimalZeros(ans).Length - RemoveExtraDecimalZeros(ans).IndexOf(".") - 1);

        }
        else
            return 0;


        if (max == -1)
            return 0;

        return max;
    }

    public string ReturnFloatingDigitString(int val)
    {
        string[] str = { "F0", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8" };
        return str[8 - val];

    }

}
