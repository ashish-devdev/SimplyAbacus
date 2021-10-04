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

public class Addition_operation : MonoBehaviour
{
    [System.Serializable]
    public class AdditionJsonWrapper
    {
        public AdditionJson[] Add;
    }
    public TextMeshProUGUI notificationText;
    public TextMeshProUGUI abacusOutput;
    public TextMeshProUGUI Result_text;
    public TextMeshProUGUI visual_result_text;
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
    public ValueCalculator valueClaculator;

    public Button reset;
    public GameObject sideNote;
    public GameObject CongratulationPannel;
    public string Congratulation_message;
    //public LeanRectTransformSizeDelta loadingBar1;
    public LeanImageFillAmount loadingBar;
    float total_problems;

    public LeanToggle leanCongratulation;
    public LeanToggle leanSideNote;
    public Timer timer;
    public GameObject timer_gamObject;
    AbacusOperations2 abacusOperations2;
    public Button notificationBtn;
    public Button nextOperationBtn;

    public GameObject friendTableBtn;
    public GameObject mulyiplicationTableBtn;

    public PositionRodsOfAbacus positionRodsOfAbacus;

    float orignalFontSize;
    float updatedFontSize;

    public RectTransform numberParentRect;
    public RectTransform resultRect;
    public RectTransform numberParentRectBG;
    public RectTransform resultRectBG;

    Vector2 minAnchorNumberParent;
    Vector2 maxAnchorNumberParent;
    Vector2 minAnchorNumberParentBG;
    Vector2 maxAnchorNumberParentBG;

    Vector2 minAnchorResult;
    Vector2 maxAnchorResult;
    Vector2 minAnchorResultBG;
    Vector2 maxAnchorResultBG;

    bool isLong;
    private void OnEnable()
    {

        Invoke("InvokeOnenableAfterDelay", 0.01f);

        //orignalFontSize = OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize;
        //updatedFontSize= orignalFontSize + 10;

        //Problem_Number = 0;
        //notificationText.text = "perform the operation";
        //for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        //{
        //    if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
        //    {
        //        for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
        //        {
        //            if (activityScriptInstance.classActivityList[i].classData.activityList[j].abacusOperations.active==true && activityScriptInstance.classActivityList[i].classData.activityList[j].activityName == ClassManager.currentActivityName)
        //            {
        //                for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations.Length; m++)
        //                {
        //                    if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations[m] == true)
        //                        Problem_Number++;
        //                }
        //                if (activityScriptInstance.classActivityList[i].classData.activityList[j].abacusOperations.showFriendTable)
        //                    friendTableBtn.SetActive(true);
        //                else
        //                    friendTableBtn.SetActive(false);
        //                if (activityScriptInstance.classActivityList[i].classData.activityList[j].abacusOperations.showMultiplicationTable)
        //                    mulyiplicationTableBtn.SetActive(true);
        //                else
        //                    mulyiplicationTableBtn.SetActive(false);

        //                abacusOperations2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].abacusOperations;
        //                activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
        //                jsonData = JsonUtility.FromJson<AdditionJsonWrapper>(activityScriptInstance.classActivityList[i].classData.activityList[j].abacusOperations.jsonData.text);
        //                ResetBar.OnReset += ResetClicked;
        //                reset.onClick.AddListener(ResetClicked);
        //                nextOperationBtn.onClick.AddListener(ResetClicked);
        //                MoveBeeds_1.dosomthing += compare_abacus_and_operation_value;
        //                MoveBeeds_2.dosomthing += compare_abacus_and_operation_value;
        //                performNext = true;
        //                if (Problem_Number >= jsonData.Add.Length)
        //                {
        //                    for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations.Length; m++)
        //                    {
        //                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations[m] == true)
        //                            Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations[m] = false;
        //                    }
        //                    Problem_Number = 0;
        //                }
        //            }
        //        }
        //    }
        //}


        //loadingBar.Data.FillAmount = (Problem_Number / (jsonData.Add.Length * 1f));
        //loadingBar.BeginAllTransitions();

        ////loadingBar.Data.SizeDelta.x = (Problem_Number / (jsonData.Add.Length * 1f)) * 500;
        ////loadingBar.BeginAllTransitions();


        ////jsonData = JsonUtility.FromJson<AdditionJsonWrapper>(addition_json_data.text);
        ////ResetBar.OnReset += ResetClicked;
        ////reset.onClick.AddListener(ResetClicked);
        ////MoveBeeds_1.dosomthing += compare_abacus_and_operation_value;
        ////MoveBeeds_2.dosomthing += compare_abacus_and_operation_value;
        ////performNext = true;
        ////if (Problem_Number >= jsonData.Add.Length)
        ////{
        ////    Problem_Number = 0;
        ////}


        //notificationBtn.onClick.AddListener(StartTimer);

    }

    public void InvokeOnenableAfterDelay()
    {
        isLong = false;
        orignalFontSize = OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize;
        updatedFontSize = orignalFontSize;

        minAnchorResult = resultRect.anchorMin;
        maxAnchorResult = resultRect.anchorMax;

        minAnchorNumberParent = numberParentRect.anchorMin;
        maxAnchorNumberParent = numberParentRect.anchorMax;



        Problem_Number = 0;
        notificationText.text = "Now let's try to solve the expression on screen using the abacus step by step.";
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].abacusOperations.active == true && activityScriptInstance.classActivityList[i].classData.activityList[j].activityName == ClassManager.currentActivityName && ClassManager.currentActivityIndex == j && ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
                    {
                        for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations.Length; m++)
                        {
                            if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations[m] == true)
                                Problem_Number++;
                        }

                        if (activityScriptInstance.classActivityList[i].classData.activityList[j].abacusOperations.showFriendTable)
                            friendTableBtn.SetActive(true);
                        else
                            friendTableBtn.SetActive(false);
                        if (activityScriptInstance.classActivityList[i].classData.activityList[j].abacusOperations.showMultiplicationTable)
                            mulyiplicationTableBtn.SetActive(true);
                        else
                            mulyiplicationTableBtn.SetActive(false);
                        if (activityScriptInstance.classActivityList[i].classData.activityList[j].abacusOperations.isLong)
                        {
                            Invoke(nameof(ResizeOperationRectAndResultRect), 2f);
                            isLong = true;

                        }



                        abacusOperations2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].abacusOperations;
                        activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
                        jsonData = JsonUtility.FromJson<AdditionJsonWrapper>(activityScriptInstance.classActivityList[i].classData.activityList[j].abacusOperations.jsonData.text);

                        for (int g = 0; g < jsonData.Add.Length; g++)
                        {
                            if (jsonData.Add[g].num_of_oprations != jsonData.Add[g].numbers.Length)
                            {
                                print("false" + g);
                            }
                        }



                        ResetBar.OnReset += ResetClicked;
                        reset.onClick.AddListener(ResetClicked);
                        nextOperationBtn.onClick.AddListener(ResetClicked);
                        MoveBeeds_1.dosomthing += compare_abacus_and_operation_value;
                        MoveBeeds_2.dosomthing += compare_abacus_and_operation_value;
                        performNext = true;
                        if (Problem_Number >= jsonData.Add.Length)
                        {
                            for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations.Length; m++)
                            {
                                if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations[m] == true)
                                    Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations[m] = false;
                            }
                            Problem_Number = 0;
                        }
                    }
                }
            }
        }


        loadingBar.Data.FillAmount = (Problem_Number / (jsonData.Add.Length * 1f));
        loadingBar.BeginAllTransitions();

        //loadingBar.Data.SizeDelta.x = (Problem_Number / (jsonData.Add.Length * 1f)) * 500;
        //loadingBar.BeginAllTransitions();


        //jsonData = JsonUtility.FromJson<AdditionJsonWrapper>(addition_json_data.text);
        //ResetBar.OnReset += ResetClicked;
        //reset.onClick.AddListener(ResetClicked);
        //MoveBeeds_1.dosomthing += compare_abacus_and_operation_value;
        //MoveBeeds_2.dosomthing += compare_abacus_and_operation_value;
        //performNext = true;
        //if (Problem_Number >= jsonData.Add.Length)
        //{
        //    Problem_Number = 0;
        //}


        notificationBtn.onClick.AddListener(StartTimer);
    }

    public void ResizeOperationRectAndResultRect()
    {
        numberParentRect.anchorMin = new Vector2(0.085f, 0.57155f);
        numberParentRect.anchorMax = new Vector2(0.906f, 0.7444f);
        numberParentRect.offsetMin = new Vector2(0, 0);
        numberParentRect.offsetMax = new Vector2(0, 0);


        numberParentRectBG.anchorMin = new Vector2(0.085f, 0.57155f);
        numberParentRectBG.anchorMax = new Vector2(0.906f, 0.7444f);
        numberParentRectBG.offsetMin = new Vector2(0, 0);
        numberParentRectBG.offsetMax = new Vector2(0, 0);

        resultRect.anchorMin = new Vector2(0.57f, 0.75f);
        resultRect.anchorMax = new Vector2(0.906f, 0.9144f);
        resultRect.offsetMin = new Vector2(0, 0);
        resultRect.offsetMax = new Vector2(0, 0);



        resultRectBG.anchorMin = new Vector2(0.57f, 0.75f);
        resultRectBG.anchorMax = new Vector2(0.906f, 0.9144f);
        resultRectBG.offsetMin = new Vector2(0, 0);
        resultRectBG.offsetMax = new Vector2(0, 0);
        orignalFontSize = 68;
        updatedFontSize = orignalFontSize;
    }

    public void StartTimer()
    {
        Timer.savedTime = abacusOperations2.currentSavedTime;
        timer_gamObject.SetActive(true);

    }


    void Start()
    {


    }

    void LateUpdate()
    {
        // print(abacusOperations2.bestTime + " " + abacusOperations2.bestTime_string + " " + abacusOperations2.currentSavedTime);

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
            abacusOperations2.currentSavedTime = Timer.currentTime;
        }


    }


    void CreateAnOperation(int num_of_oprations, double[] numbers, double Result)
    {
        int decimalPlace;
        int maxDigit;
        int p = 0;
        MakeGameObjectsUnintractable.MakeAllGameObjectsAndUiIntractable();

        Result_text.rectTransform.localPosition = new Vector3(Result_text.transform.localPosition.x, 0, Result_text.transform.localPosition.z);// Result_text.transform.localPosition = new Vector3(Result_text.transform.localPosition.x, -200, Result_text.transform.localPosition.z);
        if (num_of_oprations != 2)
        {
            for (int i = 3; i < num_of_oprations + 1; i++)
            {
                Transform textPrefab = Instantiate(Text_prefab, new Vector3(Text_prefab.transform.localPosition.x, Text_prefab.transform.localPosition.y, Text_prefab.transform.localPosition.z), Text_prefab.transform.rotation, OperationNumbers_PARENT);
                textPrefab.localPosition = new Vector3(Text_prefab.transform.localPosition.x, 0 + ((i - 1) * -100), Text_prefab.transform.localPosition.z);
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
                if (isLong)
                    OperationNumbers_PARENT.GetChild(i).GetComponent<TextMeshProUGUI>().fontSize = 68;
            }
            else
            {
                OperationNumbers_PARENT.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";
            }
            p++;
        }

        try
        {
            decimalPlace = FindDecimalPosition(numbers);
            maxDigit = FindMaxDigits(numbers);
            print(decimalPlace);
            print(maxDigit);
            positionRodsOfAbacus.startingRod = 0;
            positionRodsOfAbacus.endingRod = maxDigit;
            valueClaculator.numberOfDecimalPlaces = decimalPlace;
            valueClaculator.decimalPlaceString = GetDecimalPlaceString(decimalPlace);
            positionRodsOfAbacus.EditRod();
        }
        catch
        {; }

        OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
        OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = updatedFontSize;
        if (isLong)
            OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 75;

        sub_operation_output = numbers[0];
        suboperationIndex = 0;
        visual_result_text.text = "Result:";
    }

    public void ResetClicked()
    {
        if (Problem_Number == jsonData.Add.Length)
        {
            CongratulationText.text = Congratulation_message;
            leanCongratulation.TurnOn();// CongratulationPannel.SetActive(true);
            OperationNumbers_PARENT.gameObject.SetActive(false);
            abacusOperations2.currentSavedTime = 0;
            //activityList1.abacusOperations = true;
            if (abacusOperations2.bestTime == -1)
            {
                abacusOperations2.bestTime = Timer.currentTime;
                abacusOperations2.bestTime_string = Timer.timerText.text;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }
            else if (abacusOperations2.bestTime > Timer.currentTime)
            {
                print(5555);
                abacusOperations2.bestTime = Timer.currentTime;
                abacusOperations2.bestTime_string = Timer.timerText.text;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }

            timer_gamObject.SetActive(false);

            SaveManager.Instance.saveDataToDisk(Activity.classParent);

            return;
        }
        if (jsonData.Add[Problem_Number].status == "solved")
        {
            activityList1.abacusOperations[Problem_Number] = true;
            Problem_Number++;
            SaveManager.Instance.saveDataToDisk(Activity.classParent);



            /*if (OperationNumbers_PARENT.childCount > 2)
            {
                for (int i = 2; i < OperationNumbers_PARENT.childCount - 1; i++)
                {
                    DestroyImmediate(OperationNumbers_PARENT.gameObject.transform.GetChild(i).gameObject); ;
                }
            }*/


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

                abacusOperations2.currentSavedTime = 0;
                //activityList1.abacusOperations = true;
                if (abacusOperations2.bestTime == -1)
                {
                    abacusOperations2.bestTime = Timer.currentTime;
                    abacusOperations2.bestTime_string = Timer.timerText.text;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }
                else if (abacusOperations2.bestTime > Timer.currentTime)
                {
                    print(5555);
                    abacusOperations2.bestTime = Timer.currentTime;
                    abacusOperations2.bestTime_string = Timer.timerText.text;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }

                timer_gamObject.SetActive(false);

            }

        }
        else
        {
            OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.white;
            OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().fontSize = orignalFontSize;


            OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
            if (isLong)
                OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 75;
            else
                OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = updatedFontSize;

            sub_operation_output = jsonData.Add[Problem_Number].numbers[0];
            suboperationIndex = 0;
        }

    }



    void compare_abacus_and_operation_value()
    {
        if (RemoveExtraDecimalZeros(sub_operation_output.ToString()) == RemoveExtraDecimalZeros(ValueCalculator.value1.ToString(valueClaculator.decimalPlaceString)))
        {
            if (/*sub_operation_output == jsonData.Add[Problem_Number].Result &&*/ suboperationIndex == jsonData.Add[Problem_Number].num_of_oprations - 1)
            {
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.white;
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().fontSize = orignalFontSize;

                suboperationIndex++;
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().text = jsonData.Add[Problem_Number].Result.ToString();
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.red;
                // OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().fontSize = updatedFontSize;


                jsonData.Add[Problem_Number].status = "solved";
                activityList1.abacusOperations[Problem_Number] = true;
                SaveManager.Instance.saveDataToDisk(Activity.classParent);
                loadingBar.Data.FillAmount = ((Problem_Number + 1) / (jsonData.Add.Length * 1f));
                MakeGameObjectsUnintractable.MakeAllGameObjectsAndUiUnintractable();
                loadingBar.BeginAllTransitions();
                Result_text.text = RemoveExtraDecimalZeros(sub_operation_output.ToString());
                visual_result_text.text = "Result: " + Result_text.text;
                sub_operation_output = -1;
                nextOperationBtn.gameObject.transform.parent.gameObject.SetActive(true);
                reset.interactable = false;
            }
            else if (sub_operation_output == -1)
            {
                ;
            }
            else
            {
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.white;
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().fontSize = orignalFontSize;

                suboperationIndex++;
                sub_operation_output = sub_operation_output + jsonData.Add[Problem_Number].numbers[suboperationIndex];
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.red;
                if (isLong)
                    OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().fontSize = 75;
                else
                    OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().fontSize = updatedFontSize;

            }

        }

    }


    private void OnDisable()
    {
        OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = orignalFontSize;
        OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = orignalFontSize;
        mulyiplicationTableBtn.SetActive(false);
        friendTableBtn.SetActive(false);
        timer_gamObject.SetActive(false);
        notificationBtn.onClick.RemoveListener(StartTimer);
        MoveBeeds_1.dosomthing -= compare_abacus_and_operation_value;
        MoveBeeds_2.dosomthing -= compare_abacus_and_operation_value;
        reset.interactable = true;
        reset.onClick.RemoveListener(ResetClicked);
        nextOperationBtn.onClick.RemoveListener(ResetClicked);
        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
        ResetBar.OnReset -= ResetClicked;


        valueClaculator.decimalPlaceString = "F2";
        valueClaculator.numberOfDecimalPlaces = 2;



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

        resultRect.anchorMin = minAnchorResult;
        resultRect.anchorMax = maxAnchorResult;
        resultRect.offsetMin = new Vector2(0, 0);
        resultRect.offsetMax = new Vector2(0, 0);

        resultRectBG.anchorMin = minAnchorResult;
        resultRectBG.anchorMax = maxAnchorResult;
        resultRectBG.offsetMin = new Vector2(0, 0);
        resultRectBG.offsetMax = new Vector2(0, 0);

        // numberParentRect.anchorMin = minAnchorNumberParent;
        //  numberParentRect.anchorMax = maxAnchorNumberParent;
        //  numberParentRect.offsetMin = new Vector2(0, 0);
        //   numberParentRect.offsetMax = new Vector2(0, 0);   

        numberParentRectBG.anchorMin = new Vector2(0.5187709f, 0.5715555f);
        numberParentRectBG.anchorMax = new Vector2(0.9886103f, 0.9142964f);
        numberParentRectBG.offsetMin = new Vector2(0, 0);
        numberParentRectBG.offsetMax = new Vector2(0, 0);

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

    public int FindDecimalPosition(double[] numbers)
    {
        int decimalPlace = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i].ToString().Contains("."))
            {
                if (decimalPlace < numbers[i].ToString().Length - 1 - numbers[i].ToString().IndexOf("."))
                {
                    decimalPlace = numbers[i].ToString().Length - 1 - numbers[i].ToString().IndexOf(".");
                }

            }
        }

        return decimalPlace;
    }

    public string GetDecimalPlaceString(int decimalPlace)
    {
        string[] decimalPlaceString = new string[] { "F0", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9" };

        return decimalPlaceString[decimalPlace];
    }


    public int FindMaxDigits(double[] numbers)
    {
        int maxDigits = 0;
        bool numberIsNegative = false;
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] < 0)
            {
                numberIsNegative = true;
                numbers[i] = numbers[i] * -1;
            }


            if (numbers[i].ToString().Contains("."))
            {
                if (maxDigits < numbers[i].ToString().Length - 1)
                {
                    maxDigits = numbers[i].ToString().Length - 1;
                }
            }
            else
            {
                if (maxDigits < numbers[i].ToString().Length)
                {
                    maxDigits = numbers[i].ToString().Length;
                }
            }
            if (numberIsNegative)
            {
                numbers[i] = numbers[i] * -1;
            }
            numberIsNegative = false;
        }

        return maxDigits;
    }
}
