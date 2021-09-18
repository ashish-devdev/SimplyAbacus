using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Lean.Transition.Method;
using Lean.Gui;
using System.Data;
using System;

public class VisualAdditionOperation : MonoBehaviour
{
    [System.Serializable]
    public class AdditionJsonWrapper
    {
        public AdditionJson[] Add;
    }
    public TextMeshProUGUI notificationText;
    public TextMeshProUGUI abacusOutput;
    public TextMeshProUGUI Result_text;
    public TextMeshProUGUI visible_result_text;
    public TextMeshProUGUI CongratulationText;


    public Transform OperationNumbers_PARENT;

    public TextAsset addition_json_data;
    public Activity activityScriptInstance;
    ActivityList1 activityList1;

    public AdditionJsonWrapper jsonData;

    public int Problem_Number = 0;
    public int suboperationIndex = 0;

    public double sub_operation_output;

    public bool performNext = true;

    public Transform Text_prefab;

    public Button reset;
    public GameObject sideNote;
    public GameObject CongratulationPannel;
    public string Congratulation_message;
    public LeanImageFillAmount loadingBar;
    float total_problems;

    public LeanToggle leanCongratulation;
    public LeanToggle leanSideNote;
    public LeanToggle resetHandPositionLean;
    public Timer timer;
    public GameObject timer_gamObject;
    VisualHands2 visualHands2;
    public Button notificationBtn;
    public Button nextOperationBtn;
    bool calledOnenable;

    public ValueCalculator valueCalculator;
    public GameObject leftHand;
    public GameObject rightHand;

    float orignalSize, updatedSize;
    

    private void OnEnable()
    {
        calledOnenable = false;
        Invoke("CalledOnEnableAfterDelay", 0.05f);

    }

    public void StartTimer()
    {
        Timer.savedTime = visualHands2.currentSavedTime;
        timer_gamObject.SetActive(true);

    }


    void Start()
    {


    }

    void LateUpdate()
    {
        if (calledOnenable == false)
            return;



        if ((Problem_Number < jsonData.Add.Length))
        {
            if (performNext == true)
            {
                CreateAnOperation(jsonData.Add[Problem_Number].num_of_oprations, jsonData.Add[Problem_Number].numbers, jsonData.Add[Problem_Number].Result);
            }

            performNext = false;
        }


        if (timer_gamObject.activeInHierarchy)
        {
            visualHands2.currentSavedTime = Timer.currentTime;
        }


    }


    void CreateAnOperation(int num_of_oprations, double[] numbers, double Result)
    {
        int p = 0;
        MakeGameObjectsUnintractable.MakeAllGameObjectsAndUiIntractable();
        Result_text.rectTransform.localPosition = new Vector3(Result_text.transform.localPosition.x, 0, Result_text.transform.localPosition.z);// Result_text.transform.localPosition = new Vector3(Result_text.transform.localPosition.x, -200, Result_text.transform.localPosition.z);
        if (num_of_oprations != 2)
        {
            for (int i = 3; i < num_of_oprations + 1; i++)
            {
                Transform textPrefab = Instantiate(Text_prefab, new Vector3(Text_prefab.transform.localPosition.x, Text_prefab.transform.localPosition.y, Text_prefab.transform.localPosition.z), Text_prefab.transform.rotation, OperationNumbers_PARENT);
                textPrefab.localPosition = new Vector3(Text_prefab.transform.localPosition.x, 0 + ((i - 1) * -100), Text_prefab.transform.localPosition.z);
                textPrefab.GetComponent<TextMeshProUGUI>().fontSize = orignalSize;
            }
            Result_text.rectTransform.localPosition = new Vector3(Result_text.transform.localPosition.x, 0, Result_text.transform.localPosition.z);//Result_text.transform.localPosition = new Vector3(Result_text.transform.localPosition.x, num_of_oprations * -100, Result_text.transform.localPosition.z);
        }

        Result_text.transform.SetAsLastSibling();
        Result_text.rectTransform.localPosition = new Vector3(Result_text.transform.localPosition.x, 0, Result_text.transform.localPosition.z);//Result_text.transform.localPosition = new Vector3(Result_text.transform.localPosition.x, num_of_oprations * -100, Result_text.transform.localPosition.z);

        for (int i = 0; i < num_of_oprations + 1; i++)
        {
            if (i < num_of_oprations)
            {
                OperationNumbers_PARENT.GetChild(i).GetComponent<TextMeshProUGUI>().text = numbers[i].ToString();
            }
            else
            {
                OperationNumbers_PARENT.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";
            }
            p++;
        }

        OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
        OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = updatedSize;
        sub_operation_output = numbers[0];
        suboperationIndex = 0;
        visible_result_text.text = "Result:";
    }

    public void ResetClicked()
    {
        visible_result_text.text = "Result:";
        reset.gameObject.SetActive(true);

        if (Problem_Number == jsonData.Add.Length)
        {
            CongratulationText.text = Congratulation_message;
            leanCongratulation.TurnOn();// CongratulationPannel.SetActive(true);
            OperationNumbers_PARENT.gameObject.SetActive(false);
            visualHands2.currentSavedTime = 0;
            //activityList1.abacusOperations = true;
            if (visualHands2.bestTime == -1)
            {
                visualHands2.bestTime = Timer.currentTime;
                visualHands2.bestTime_string = Timer.timerText.text;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }
            else if (visualHands2.bestTime > Timer.currentTime)
            {
                print(5555);
                visualHands2.bestTime = Timer.currentTime;
                visualHands2.bestTime_string = Timer.timerText.text;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }

            timer_gamObject.SetActive(false);

            SaveManager.Instance.saveDataToDisk(Activity.classParent);

            return;
        }
        if (jsonData.Add[Problem_Number].status == "solved")
        {
            activityList1.visualHands1.completed[Problem_Number] = true;
            Problem_Number++;
            SaveManager.Instance.saveDataToDisk(Activity.classParent);


            for (int i = 0; i < OperationNumbers_PARENT.childCount; i++)
            {
                if (OperationNumbers_PARENT.GetChild(i).CompareTag("NumberClone"))
                {
                    Destroy(OperationNumbers_PARENT.GetChild(i).gameObject);

                }
            }



            performNext = true;
            if (Problem_Number == jsonData.Add.Length)
            {
                CongratulationText.text = Congratulation_message;
                leanCongratulation.TurnOn();// CongratulationPannel.SetActive(true);
                OperationNumbers_PARENT.gameObject.SetActive(false);
                leanSideNote.TurnOff();//sideNote.SetActive(false);
                //activityList1.abacusOperations = true;
                SaveManager.Instance.saveDataToDisk(Activity.classParent);
                //  return;

                visualHands2.currentSavedTime = 0;
                //activityList1.abacusOperations = true;
                if (visualHands2.bestTime == -1)
                {
                    visualHands2.bestTime = Timer.currentTime;
                    visualHands2.bestTime_string = Timer.timerText.text;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }
                else if (visualHands2.bestTime > Timer.currentTime)
                {
                    print(5555);
                    visualHands2.bestTime = Timer.currentTime;
                    visualHands2.bestTime_string = Timer.timerText.text;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }

                timer_gamObject.SetActive(false);

            }

        }
        else
        {
            OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.white;
            OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().fontSize = orignalSize;


            OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
            OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = updatedSize;
            sub_operation_output = jsonData.Add[Problem_Number].numbers[0];
            suboperationIndex = 0;
        }

    }



    void compare_abacus_and_operation_value()
    {
        if (RemoveExtraDecimalZeros(sub_operation_output.ToString()) == RemoveExtraDecimalZeros(ValueCalculator.value.ToString(valueCalculator.decimalPlaceString)))
        {
            if ( suboperationIndex == jsonData.Add[Problem_Number].num_of_oprations - 1)
            {
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.white;
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().fontSize= orignalSize;
                suboperationIndex++;
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().text = jsonData.Add[Problem_Number].Result.ToString();
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.red;
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().fontSize= updatedSize;
                jsonData.Add[Problem_Number].status = "solved";
                activityList1.visualHands1.completed[Problem_Number] = true;
                SaveManager.Instance.saveDataToDisk(Activity.classParent);
                loadingBar.Data.FillAmount = ((Problem_Number + 1) / (jsonData.Add.Length * 1f));
                loadingBar.BeginAllTransitions();
                MakeGameObjectsUnintractable.MakeAllGameObjectsAndUiUnintractable();
                Result_text.text = sub_operation_output.ToString();
                visible_result_text.text = "Result: "+ sub_operation_output.ToString();

                sub_operation_output = -1;
                nextOperationBtn.gameObject.transform.parent.gameObject.SetActive(true);
                reset.interactable = false;
                reset.gameObject.SetActive(false);

            }
            else if (sub_operation_output == -1)
            {
                ;
            }
            else
            {
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.white;
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().fontSize = orignalSize;
                suboperationIndex++;
                sub_operation_output = sub_operation_output + jsonData.Add[Problem_Number].numbers[suboperationIndex];
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.red;
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().fontSize = updatedSize;
            }

        }

    }


    private void OnDisable()
    {
        timer_gamObject.SetActive(false);
        notificationBtn.onClick.RemoveListener(StartTimer);
        MoveBeeds_1.dosomthing -= compare_abacus_and_operation_value;
        MoveBeeds_2.dosomthing -= compare_abacus_and_operation_value;
        VisualHandMovements.dosomthing2 -= compare_abacus_and_operation_value;
        visible_result_text.text = "Result:";

        reset.interactable = true;
        reset.onClick.RemoveListener(ResetClicked);
        nextOperationBtn.onClick.RemoveListener(ResetClicked);
       // resetHandPositionLean.TurnOff();


        ResetBar.OnReset -= ResetClicked;
        try
        {
            for (int i = 0; i < OperationNumbers_PARENT.childCount; i++)
            {
                if (OperationNumbers_PARENT.GetChild(i).CompareTag("NumberClone"))
                {
                    Destroy(OperationNumbers_PARENT.GetChild(i).gameObject);

                }
            }

        }
        catch
        {
            ;
        }


        MakeGameObjectsUnintractable.MakeAllGameObjectsAndUiIntractable();

    }




    public void CalledOnEnableAfterDelay()
    {
        calledOnenable = true;
        orignalSize = 105;
        updatedSize = 130;

        Problem_Number = 0;
        notificationText.text = "Perform the operation";
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    //  print(activityScriptInstance.classActivityList[i].classData.activityList[j].visualHands.active + "-----" + activityScriptInstance.classActivityList[i].classData.activityList[j].activityName + "----- " + ClassManager.currentActivityName);
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].visualHands.active == true && activityScriptInstance.classActivityList[i].classData.activityList[j].activityName == ClassManager.currentActivityName && ClassManager.currentActivityIndex==j)
                    {
                        for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].visualHands1.completed.Length; m++)
                        {
                            if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].visualHands1.completed[m] == true)
                                Problem_Number++;
                        }

                        if (activityScriptInstance.classActivityList[i].classData.activityList[j].visualHands.showHand2 == true)
                        {
                            leftHand.SetActive(true);
                        }
                        else
                        {
                         //   resetHandPositionLean.TurnOn();
                            leftHand.SetActive(false);
                        }

                        visualHands2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].visualHands2;

                        activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];

                        jsonData = JsonUtility.FromJson<AdditionJsonWrapper>(activityScriptInstance.classActivityList[i].classData.activityList[j].visualHands.abacusOperations.text);
                        ResetBar.OnReset += ResetClicked;
                        reset.onClick.AddListener(ResetClicked);
                        nextOperationBtn.onClick.AddListener(ResetClicked);
                        MoveBeeds_1.dosomthing += compare_abacus_and_operation_value;
                        MoveBeeds_2.dosomthing += compare_abacus_and_operation_value;
                        VisualHandMovements.dosomthing2 += compare_abacus_and_operation_value;
                        performNext = true;
                        if (Problem_Number >= jsonData.Add.Length)
                        {
                            for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].visualHands1.completed.Length; m++)
                            {
                                if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].visualHands1.completed[m] == true)
                                    Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].visualHands1.completed[m] = false;
                            }
                            Problem_Number = 0;
                        }
                    }
                }
            }
        }


        loadingBar.Data.FillAmount = (Problem_Number / (jsonData.Add.Length * 1f));
        loadingBar.BeginAllTransitions();

        notificationBtn.onClick.AddListener(StartTimer);
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

}

