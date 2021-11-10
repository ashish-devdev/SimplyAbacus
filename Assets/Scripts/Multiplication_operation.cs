using Lean.Gui;
using Lean.Transition.Method;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Multiplication_operation : MonoBehaviour
{
    public class MultiplicationJsonWrapper
    {
        public MultiplicationJson[] Mul;
    }

    public PositionRodsOfAbacus positionRodsOfAbacus;
    public Activity activityScriptInstance;
    public GameObject multplicationTable;
    public GameObject friendTable;
    public LeanImageFillAmount loadingBar;

    int l3;

    public TextAsset multiplication_json_data;
    public string Congratulation_message;

    public MultiplicationJsonWrapper jsonData;
    public Button nextOperationBtn;

    public GameObject timer_gamObject;
    public GameObject nextOperationBtnGameObject;

    public Transform OperationNumbers_PARENT;

    public Button reset;
    public Button resetGraphicBtn;

    int Problem_Number = 0;

    int numberOfDecimalDigitsInNum1;
    int numberOfDecimalDigitsInNum2;

    bool performNext = true;
    bool decimalCrossedNum1 = false;
    bool decimalCrossedNum2 = false;

    public decimal[] sub_operation_output;

    public int suboperationIndex = 0;

    public int digitsInNum1, tempDigitsNum1;
    public int digitsInNum2, tempDigitsNum2;
    public int totalDigits;
    int power = 0;
    int I, J;
    public double tempNum1, tempNum2;
    public int[] num1;
    public int[] num2;
    public Button notificationBtn;


    public TextMeshProUGUI notificationText;
    public TextMeshProUGUI CongratulationText;
    public TextMeshProUGUI abacusOutput;

    public TextMeshProUGUI Result_text;

    public Transform Text_prefab;

    ActivityList1 activityList1;
    MultiplicationOperation2 multiplicationOperation2;
    public ValueCalculator valueCalculator;

    public LeanToggle leanCongratulation;
    public LeanToggle leanforCompletionMessages;
    public TextMeshProUGUI completionMessageText;
    public LeanToggle leanSideNote;
    float orignalTextSize;
    float updatedTextSize;


    private void OnEnable()
    {

        Invoke("DelayedOnEnable", 0.01f);

    }


    public void DelayedOnEnable()
    {


        orignalTextSize = OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize;
        updatedTextSize = orignalTextSize + 15;

        performNext = true;
        Problem_Number = 0;
        notificationText.text = "perform the operation";
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
            {
                if (activityScriptInstance.classActivityList[i].classData.activityList[j].mutliplicationOperation.active == true && activityScriptInstance.classActivityList[i].classData.activityList[j].activityName == ClassManager.currentActivityName && ClassManager.currentActivityIndex == j && ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
                {

                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].mutliplicationOperation.showMutilplicationTable)
                        multplicationTable.SetActive(true);
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].mutliplicationOperation.showFriendTable)
                        friendTable.SetActive(true);


                    for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed.Length; m++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed[m] == true)
                        {
                            Problem_Number++;

                        }

                    }

                    multiplicationOperation2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationOperation2;
                    activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
                    jsonData = JsonUtility.FromJson<MultiplicationJsonWrapper>(activityScriptInstance.classActivityList[i].classData.activityList[j].mutliplicationOperation.jsonData.text);
                    if (Problem_Number >= jsonData.Mul.Length)
                    {
                        for (int m = 0; m < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed.Length; m++)
                        {
                            if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed[m] == true)
                                Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed[m] = false;
                        }
                        Problem_Number = 0;
                    }
                    //   nextOperationBtn.onClick.AddListener(DisplayProblem);
                    nextOperationBtn.onClick.AddListener(ResetClicked);
                    notificationBtn.onClick.AddListener(StartTimer);
                    DisplayProblem();
                }
            }
        }


        loadingBar.Data.FillAmount = ((Problem_Number) / (jsonData.Mul.Length * 1f));
        loadingBar.BeginAllTransitions();

        ResetBar.OnReset += ResetClicked;
        reset.onClick.AddListener(ResetClicked);
        resetGraphicBtn.onClick.AddListener(ResetClicked);
        MoveBeeds_1.dosomthing += compare_abacus_and_operation_value;
        MoveBeeds_2.dosomthing += compare_abacus_and_operation_value;

    }

    public void StartTimer()
    {
        Timer.savedTime = multiplicationOperation2.currentSavedTime;
        timer_gamObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (timer_gamObject.activeInHierarchy)
        {
            multiplicationOperation2.currentSavedTime = Timer.currentTime;
        }
        DisplayProblem();
    }

    public void DisplayProblem()
    {
        if ((Problem_Number < jsonData.Mul.Length))
        {
            if (performNext == true)
            {
                CreateAnOperation(jsonData.Mul[Problem_Number].num_of_oprations, jsonData.Mul[Problem_Number].numbers, jsonData.Mul[Problem_Number].Result);
            }
            performNext = false;
        }
    }


    void CreateAnOperation(int num_of_oprations, double[] numbers, double Result)
    {
        tempNum1 = numbers[0];
        tempNum2 = numbers[1];
        digitsInNum1 = 0;
        digitsInNum2 = 0;
        int p = 0;
        Result_text.transform.localPosition = new Vector3(Result_text.transform.localPosition.x, -200, Result_text.transform.localPosition.z);

        MakeGameObjectsUnintractable.MakeAllGameObjectsAndUiIntractable();
        if (num_of_oprations != 2)
        {
            for (int i = 3; i < num_of_oprations + 1; i++)
            {
                Transform textPrefab = Instantiate(Text_prefab, new Vector3(Text_prefab.transform.localPosition.x, Text_prefab.transform.localPosition.y, Text_prefab.transform.localPosition.z), Text_prefab.transform.rotation, OperationNumbers_PARENT);
                textPrefab.localPosition = new Vector3(Text_prefab.transform.localPosition.x, 0 + ((i - 1) * -100), Text_prefab.transform.localPosition.z);
            }
            Result_text.transform.localPosition = new Vector3(Result_text.transform.localPosition.x, num_of_oprations * -100, Result_text.transform.localPosition.z);
        }

        Result_text.transform.SetAsLastSibling();
        Result_text.text = "Result: ";
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



        digitsInNum1 = CheckNumberOfDigits(tempNum1);
        digitsInNum2 = CheckNumberOfDigits(tempNum2);

        numberOfDecimalDigitsInNum1 = FindNumberOfdecimalDigits(tempNum1);
        numberOfDecimalDigitsInNum2 = FindNumberOfdecimalDigits(tempNum2);


        tempNum1 = numbers[0];
        tempNum2 = numbers[1];
        num1 = new int[digitsInNum1];
        num2 = new int[digitsInNum2];
        totalDigits = digitsInNum1 + digitsInNum2;
        tempDigitsNum1 = digitsInNum1;
        tempDigitsNum2 = digitsInNum2;
        sub_operation_output = new decimal[digitsInNum1 * digitsInNum2];
        float result = 0;
        while (tempNum1 != 0)
        {
            char c = tempNum1.ToString()[tempNum1.ToString().Length - 1];
            if (c.ToString() != ".")
            {
                var temp = tempNum1.ToString();
                int convertedVal = 0;
                int.TryParse(temp[tempNum1.ToString().Length - 1].ToString(), out convertedVal);
                num1[--tempDigitsNum1] = convertedVal;
            }
            else
            {
                //  num1[--tempDigitsNum1] = -1;
            }

            string s = tempNum1.ToString().Remove(tempNum1.ToString().Length - 1);
            if (s == "")
                break;
            tempNum1 = Convert.ToDouble(new DataTable().Compute(s, ""));

        }

        while (tempNum2 != 0)
        {

            char c = tempNum2.ToString()[tempNum2.ToString().Length - 1];
            if (c.ToString() != ".")
            {
                var temp = tempNum2.ToString();
                int convertedVal = 0;
                int.TryParse(temp[tempNum2.ToString().Length - 1].ToString(), out convertedVal);
                num2[--tempDigitsNum2] = convertedVal;
            }
            else
            {
                //  num2[--tempDigitsNum2] = -1;
            }

            string s = tempNum2.ToString().Remove(tempNum2.ToString().Length - 1);
            if (s == "")
                break;
            tempNum2 = Convert.ToDouble(new DataTable().Compute(s, ""));
        }
        power = totalDigits - (numberOfDecimalDigitsInNum1 + numberOfDecimalDigitsInNum2) - 2;

        for (int i = 0; i < digitsInNum2; i++)
        {
            for (int j = 0; j < digitsInNum1; j++)
            {
                result += (num1[j] * num2[i]) * Mathf.Pow(10, power - j);
                sub_operation_output[i * digitsInNum1 + j] = (decimal)result;
                print("result" + result);
            }
            power = power - 1;
        }

        tempDigitsNum1 = digitsInNum1;
        tempDigitsNum2 = digitsInNum2;
        I = 0;
        J = 0;
        decimalCrossedNum1 = false;
        decimalCrossedNum2 = false;
        OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().text = OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().text.Remove(0, 1).Insert(0, "<color=red><size=" + updatedTextSize + ">" + num1[0] + "</color></size>");
        OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().text = OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().text.Remove(0, 1).Insert(0, "<color=red><size=" + updatedTextSize + ">" + num2[0] + "</color></size>");
        suboperationIndex = 0;
        positionRodsOfAbacus.endingRod = CheckNumberOfDigits((double)sub_operation_output[sub_operation_output.Length - 1]);
        valueCalculator.numberOfDecimalPlaces = FindNumberOfdecimalDigits((double)sub_operation_output[sub_operation_output.Length - 1]);
        valueCalculator.decimalPlaceString = "F" + FindNumberOfdecimalDigits((double)sub_operation_output[sub_operation_output.Length - 1]).ToString();
        positionRodsOfAbacus.EditRod();

    }

    public void ResetClicked()
    {
        if (Problem_Number >= jsonData.Mul.Length)
        {
            return;
        }
        if (jsonData.Mul[Problem_Number].status == "solved")
        {
            Problem_Number++;
            Result_text.text = "Result: ";

            if (OperationNumbers_PARENT.childCount > 2)
            {
                for (int i = 2; i < OperationNumbers_PARENT.childCount - 1; i++)
                {
                    DestroyImmediate(OperationNumbers_PARENT.gameObject.transform.GetChild(i).gameObject); ;
                }
            }
            performNext = true;

        }
        else
        {
            print("Reset");
            OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().text = jsonData.Mul[Problem_Number].numbers[0].ToString();
            OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().text = jsonData.Mul[Problem_Number].numbers[1].ToString();

            OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().text = OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().text.Remove(0, 1).Insert(0, "<color=red><size=" + updatedTextSize + ">" + num1[0] + "</color></size>");
            OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().text = OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().text.Remove(0, 1).Insert(0, "<color=red><size=" + updatedTextSize + ">" + num2[0] + "</color></size>");

            suboperationIndex = 0;
            I = 0;
            J = 0;
            decimalCrossedNum1 = false;
            decimalCrossedNum2 = false;

        }

    }




    void compare_abacus_and_operation_value()
    {
        if (ValueCalculator.value1 == sub_operation_output[sub_operation_output.Length - 1])
        {

            OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().text = jsonData.Mul[Problem_Number].numbers[0].ToString();
            OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().text = jsonData.Mul[Problem_Number].numbers[1].ToString();
            OperationNumbers_PARENT.GetChild(2).GetComponent<TextMeshProUGUI>().text = ValueCalculator.value1.ToString();
            jsonData.Mul[Problem_Number].status = "solved";
            // OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
            // OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.white;
            nextOperationBtnGameObject.SetActive(true);
            reset.interactable = false;
            resetGraphicBtn.interactable = false;
            if (Problem_Number == jsonData.Mul.Length - 1)
            {
                nextOperationBtnGameObject.SetActive(false);

                CongratulationText.text = Congratulation_message;
                leanCongratulation.TurnOn();
                resetGraphicBtn.gameObject.SetActive(false);
                OperationNumbers_PARENT.gameObject.SetActive(false);
                leanSideNote.TurnOff();
                timer_gamObject.SetActive(false);

                multiplicationOperation2.currentSavedTime = 0;

                if (multiplicationOperation2.bestTime == -1)
                {
                    multiplicationOperation2.bestTime = Timer.currentTime;
                    multiplicationOperation2.bestTime_string = Timer.timerText.text;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }
                else if (multiplicationOperation2.bestTime > Timer.currentTime)
                {
                    print(5555);
                    multiplicationOperation2.bestTime = Timer.currentTime;
                    multiplicationOperation2.bestTime_string = Timer.timerText.text;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }
            }
            SaveManager.Instance.saveDataToDisk(Activity.classParent);
            activityList1.multiplicationOperation1.completed[Problem_Number] = true;
            loadingBar.Data.FillAmount = ((Problem_Number + 1) / (jsonData.Mul.Length * 1f));
            loadingBar.BeginAllTransitions();

            if (jsonData.Mul.Length >= 10)
            {
                if ((((Problem_Number + 1f) / jsonData.Mul.Length) >= 0.75f) && (((Problem_Number + 0f) / jsonData.Mul.Length) < 0.75f))
                {
                    leanforCompletionMessages.TurnOn();
                    completionMessageText.text = "Wow, aren’t you a smartie!";
                    print("done 75");
                }
                else if ((((Problem_Number + 1f) / jsonData.Mul.Length) >= 0.5f) && (((Problem_Number + 0f) / jsonData.Mul.Length) < 0.5f))
                {
                    leanforCompletionMessages.TurnOn();
                    completionMessageText.text = "You’re getting very good at this";

                    print("done 50");
                }
                else if ((((Problem_Number + 1f) / jsonData.Mul.Length) >= 0.3f) && (((Problem_Number + 0f) / jsonData.Mul.Length) < 0.3f))
                {
                    leanforCompletionMessages.TurnOn();
                    completionMessageText.text = "Yay, You’re almost done";
                    print("done 30");
                }
            }

            MakeGameObjectsUnintractable.MakeAllGameObjectsAndUiUnintractable();

        }
        else if (sub_operation_output[suboperationIndex] == (decimal)ValueCalculator.value1)
        {

            J++;
            if (J >= digitsInNum1)
            {
                J = 0;
                I++;
            }

            OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().text = jsonData.Mul[Problem_Number].numbers[0].ToString();
            OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().text = jsonData.Mul[Problem_Number].numbers[1].ToString();
            if (OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().text[I].ToString() == ".")
            {
                decimalCrossedNum2 = true;
                I++;
            }
            if (OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().text[J].ToString() == ".")
            {
                decimalCrossedNum1 = true;
                J++;
            }

            if (decimalCrossedNum1 == true)
                OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().text = OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().text.Remove(J, 1).Insert(J, "<color=red><size=" + updatedTextSize + ">" + num1[J - 1] + "</color></size>");
            else
                OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().text = OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().text.Remove(J, 1).Insert(J, "<color=red><size=" + updatedTextSize + ">" + num1[J] + "</color></size>");

            if (decimalCrossedNum2 == true)
                OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().text = OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().text.Remove(I, 1).Insert(I, "<color=red><size=" + updatedTextSize + ">" + num2[I - 1] + "</color></size>");
            else
                OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().text = OperationNumbers_PARENT.GetChild(1).GetComponent<TextMeshProUGUI>().text.Remove(I, 1).Insert(I, "<color=red><size=" + updatedTextSize + ">" + num2[I] + "</color></size>");


            suboperationIndex++;
            //sub_operation_output = -1;
        }

        else
        {
            print("suboperationIndex" + suboperationIndex);
            print(sub_operation_output[suboperationIndex] + " " + ValueCalculator.value);
        }

    }

    private void OnDisable()
    {
        notificationBtn.onClick.RemoveListener(StartTimer);

        MoveBeeds_1.dosomthing -= compare_abacus_and_operation_value;
        MoveBeeds_2.dosomthing -= compare_abacus_and_operation_value;
        reset.onClick.RemoveListener(ResetClicked);
        resetGraphicBtn.onClick.RemoveListener(ResetClicked);
        ResetBar.OnReset -= ResetClicked;
        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
        //  nextOperationBtn.onClick.RemoveListener(DisplayProblem);
        nextOperationBtn.onClick.RemoveListener(ResetClicked);
        friendTable.SetActive(false);
        multplicationTable.SetActive(false);
        timer_gamObject.SetActive(false);
        reset.interactable = true;
        resetGraphicBtn.interactable = true;
        valueCalculator.decimalPlaceString = "F2";
        valueCalculator.numberOfDecimalPlaces = 2;

        MakeGameObjectsUnintractable.MakeAllGameObjectsAndUiIntractable();

    }

    public int CheckNumberOfDigits(double number)
    {
        if (number.ToString().Contains("."))
            return (number.ToString().Length - 1);
        else
            return (number.ToString().Length);
    }

    public int FindNumberOfdecimalDigits(double number)
    {
        if (number.ToString().Contains("."))
        {
            return (number.ToString().Length - number.ToString().IndexOf(".") - 1);

        }
        else
            return 0;
    }


}


/*
 #include <iostream>
#include <math.h>
using namespace std;

int main() {
	// your code goes here
	int a,b,size1=0,size2=0,temp1,temp2,tempsize1,tempsize2,result=0,power=0;
	cin>>a>>b;
	temp1=a;
	temp2=b;
	while(temp1!=0)
	{
	    temp1=temp1/10;
	    size1++;
	    
	    
	}
	while(temp2!=0)
	{
	    temp2=temp2/10;
	    size2++;
	    
	}
	int *num1=new int(size1);
	int *num2=new int(size2);
	
	temp1=a;
	temp2=b;
	tempsize1=size1;
	tempsize2=size2;
	power=size1+size2;
	while(temp1!=0)
	{
	    
	    num1[--tempsize1]=temp1%10;
	    temp1=temp1/10;
	    
	}
	while(temp2!=0)
	{
	    num2[--tempsize2]=temp2%10;
	    temp2=temp2/10;
	    
	}
	power=power-2;
    for(int i=0;i<size2;i++)
    {
        for(int j=0;j<size1;j++)
        {
            result+=(num1[j]*num2[i])*pow(10,power-j);
            cout<<num1[j]<<" "<<num2[i]<<"\n";
            cout<<result<<"\n";
        }
        power=power-1;
    }
        
    
        
	cout<<"\n--------->"<<a*b<<"-------->"<<result;
	return 0;
}

 */
