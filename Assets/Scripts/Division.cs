using Lean.Gui;
using Lean.Transition.Method;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Division : MonoBehaviour
{
    int problem_number;
    decimal answer;
    bool firstProblemCreated;
    string decimalStringToCompare;
    bool newQuestion;
    int l3;


    DivisionOperation2 divisionOperation2;
    ActivityList1 activityList1;
    public DivisionJsonWrapper jsonData;

    public Activity activityScriptInstance;

    public Button nextOperationBtn;
    public Button notificationBtn;

    public GameObject multplicationTable;
    public GameObject friendTable;
    public GameObject timer_gamObject;
    public LeanImageFillAmount loadingBar;
    public Reset reset;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI questionText;
    public ValueCalculator valueCalculator;
    public PositionRodsOfAbacus positionRodsOfAbacus;

    public LeanToggle leanCongratulation;
    public LeanToggle leanforCompletionMessages;
    public TextMeshProUGUI completionMessageText;
    public LeanToggle leanSideNote;

    [System.Serializable]
    public class DivisionJsonWrapper
    {
        public DivisionJson[] div;
    }



    private void OnEnable()
    {
        Invoke(nameof(OnEnableDelay),0.1f);

    }

    public void OnEnableDelay()
    {
        firstProblemCreated = false;
        problem_number = 0;
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].divisionOperation.active == true && ClassManager.currentActivityIndex == j && ClassManager.currentClassName==activityScriptInstance.classActivityList[i].classData.nameOfClass)
                    {
                        if (activityScriptInstance.classActivityList[i].classData.activityList[j].divisionOperation.showMutilplicationTable)
                            multplicationTable.SetActive(true);
                        if (activityScriptInstance.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.showFriendTable)
                            friendTable.SetActive(true);


                        for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1.completed.Length; m++)
                        {
                            if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1.completed[m] == true)
                            {
                                problem_number++;
                            }
                        }

                        divisionOperation2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].divisionOperation2;
                        activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
                        jsonData = new DivisionJsonWrapper();
                        jsonData = JsonUtility.FromJson<DivisionJsonWrapper>(activityScriptInstance.classActivityList[i].classData.activityList[j].divisionOperation.jsonData.text);
                        nextOperationBtn.onClick.AddListener(CreateAndDisplayProblem);

                        if (problem_number >= jsonData.div.Length)
                        {
                            for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1.completed.Length; m++)
                            {
                                if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1.completed[m] == true)
                                    Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1.completed[m] = false;
                            }
                            problem_number = 0;
                        }


                    }
                }
            }
        }

        loadingBar.Data.FillAmount = (problem_number / (jsonData.div.Length * 1f));
        loadingBar.BeginAllTransitions();




        notificationBtn.onClick.AddListener(StartTimer);

        Invoke("CreateAndDisplayProblem", 0.5f);


        valueCalculator.numberOfDecimalPlaces = 4;
        valueCalculator.decimalPlaceString = "F4";
        MoveBeeds_1.dosomthing += compare_abacus_and_operation_value;
        MoveBeeds_2.dosomthing += compare_abacus_and_operation_value;

    }

    public void StartTimer()
    {
        Timer.savedTime = divisionOperation2.currentSavedTime;
        timer_gamObject.SetActive(true);
    }


    void Update()
    {
        if (timer_gamObject.activeInHierarchy)
        {
            divisionOperation2.currentSavedTime = Timer.currentTime;
        }

    }



    public void CreateAndDisplayProblem()
    {
        newQuestion = true;
        if (!firstProblemCreated)
        {
            positionRodsOfAbacus.EditRod();
            firstProblemCreated = true;
        }
        reset.RESET();


        resultText.text = "Result: ";
        if (problem_number >= jsonData.div.Length)
        {

        }
        else
        {
            nextOperationBtn.transform.parent.gameObject.SetActive(false);


            questionText.text = jsonData.div[problem_number].Problem;
            questionText.text = questionText.text.Replace("*", " * ");
            questionText.text = questionText.text.Replace("/", " / ");
            questionText.text = questionText.text.Replace("+", " + ");
            questionText.text = questionText.text.Replace("-", " - ");
            questionText.text = questionText.text.Replace("%", " % ");



            answer = Convert.ToDecimal(new DataTable().Compute(jsonData.div[problem_number].answer.ToString(), ""));

            decimalStringToCompare = getDecimalStringForGivenNumber(answer);

        }

    }





    public void compare_abacus_and_operation_value()
    {
        print("ValueCalculator.value1.ToString(decimalStringToCompare)" + ValueCalculator.value1.ToString(decimalStringToCompare) + " =-=---=- " + "answer.ToString(decimalStringToCompare)" + answer.ToString(decimalStringToCompare));
        if (ValueCalculator.value1.ToString(decimalStringToCompare) == answer.ToString(decimalStringToCompare) && newQuestion)
        {
            activityList1.divisionOperation1.completed[problem_number] = true;
            SaveManager.Instance.saveDataToDisk(Activity.classParent);// may be possible to remove from here and can be added to ondisable.
            problem_number++;
            loadingBar.Data.FillAmount = (problem_number / (jsonData.div.Length * 1f));
            loadingBar.BeginAllTransitions();

            if (jsonData.div.Length >= 10)
            {
                if ((((problem_number + 1f) / jsonData.div.Length) >= 0.75f) && (((problem_number + 0f) / jsonData.div.Length) < 0.75f))
                {
                    leanforCompletionMessages.TurnOn();
                    completionMessageText.text = "Wow, aren’t you a smartie!";
                    print("done 75");
                }
                else if ((((problem_number + 1f) / jsonData.div.Length) >= 0.5f) && (((problem_number + 0f) / jsonData.div.Length) < 0.5f))
                {
                    leanforCompletionMessages.TurnOn();
                    completionMessageText.text = "You’re getting very good at this";

                    print("done 50");
                }
                else if ((((problem_number + 1f) / jsonData.div.Length) >= 0.3f) && (((problem_number + 0f) / jsonData.div.Length) < 0.3f))
                {
                    leanforCompletionMessages.TurnOn();
                    completionMessageText.text = "Yay, You’re almost done";
                    print("done 30");
                }
            }

            nextOperationBtn.transform.parent.gameObject.SetActive(true);
            resultText.text = "Result: " + (ValueCalculator.value1.ToString(decimalStringToCompare));

            //  CreateAndDisplayProblem();
            newQuestion = false;
        }

        if (problem_number >= jsonData.div.Length)
        {
            timer_gamObject.SetActive(false);
            nextOperationBtn.transform.parent.gameObject.SetActive(false);

            leanCongratulation.TurnOn();
            leanSideNote.TurnOff();

            divisionOperation2.currentSavedTime = 0;
            //activityList1.abacusOperations = true;
            if (divisionOperation2.bestTime == -1)
            {
                divisionOperation2.bestTime = Timer.currentTime;
                divisionOperation2.bestTime_string = Timer.timerText.text;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }
            else if (divisionOperation2.bestTime > Timer.currentTime)
            {
                print(5555);
                divisionOperation2.bestTime = Timer.currentTime;
                divisionOperation2.bestTime_string = Timer.timerText.text;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }



            SaveManager.Instance.saveDataToDisk(Activity.classParent);
        }




    }

    public string getDecimalStringForGivenNumber(decimal n)
    {
        return "F" + FirstNonZeroDecimalPosition(n);
    }

    public int FirstNonZeroDecimalPosition(decimal n)
    {
        int temp = 0;
        int i = 0;
        if (n.ToString().Contains("."))
        {
            i = n.ToString().IndexOf(".");
            i++;
            while (n.ToString()[i] == '0' && i < n.ToString().Length && temp <= 4)
            {

                temp++;
                i++;
            }


            return temp + 1;
        }
        else
            return 0;
    }



    private void OnDisable()
    {
        try
        {
            friendTable.SetActive(false);
            multplicationTable.SetActive(false);
            notificationBtn.onClick.RemoveListener(StartTimer);
            nextOperationBtn.onClick.RemoveListener(CreateAndDisplayProblem);
            timer_gamObject.SetActive(false);
            nextOperationBtn.transform.parent.gameObject.SetActive(false);
            SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            valueCalculator.numberOfDecimalPlaces = 2;
            valueCalculator.decimalPlaceString = "F2";
            positionRodsOfAbacus.EditRod();
            MoveBeeds_1.dosomthing -= compare_abacus_and_operation_value;
            MoveBeeds_2.dosomthing -= compare_abacus_and_operation_value;
        }
        catch
        {
            ;
        }

    }





}
