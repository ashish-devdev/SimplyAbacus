using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Subtraction_operation : MonoBehaviour
{
    [System.Serializable]
    public class SubtractionJsonWrapper
    {
        public AdditionJson[] Sub;
    }
    public TextMeshProUGUI abacusOutput;
    public TextMeshProUGUI Result_text;
    public TextMeshProUGUI CongratulationText;

    public Transform OperationNumbers_PARENT;

    public TextAsset subtraction_json_data;

    public SubtractionJsonWrapper jsonData;

    public int Problem_Number = 0;
    public int suboperationIndex = 0;

    public double sub_operation_output;

    public bool performNext = true;

    public Transform Text_prefab;

    public Button reset;

    public GameObject CongratulationPannel;
    public string Congratulation_message;
    float total_problems;

    private void OnEnable()
    {
        jsonData = JsonUtility.FromJson<SubtractionJsonWrapper>(subtraction_json_data.text);
        reset.onClick.AddListener(ResetClicked);
        ResetBar.OnReset+=ResetClicked;
        MoveBeeds_1.dosomthing += compare_abacus_and_operation_value;
        MoveBeeds_2.dosomthing += compare_abacus_and_operation_value;
        performNext = true;
        if (Problem_Number >= jsonData.Sub.Length)
        {
            Problem_Number = 0;
        }

    }


    void Start()
    {


    }

    void LateUpdate()
    {
        if ((Problem_Number < jsonData.Sub.Length))
        {
            if (performNext == true)
            {
                CreateAnOperation(jsonData.Sub[Problem_Number].num_of_oprations, jsonData.Sub[Problem_Number].numbers, jsonData.Sub[Problem_Number].Result);
            }

            performNext = false;
        }

    }


    void CreateAnOperation(int num_of_oprations, double[] numbers, double Result)
    {
        int p = 0;
        Result_text.transform.localPosition = new Vector3(Result_text.transform.localPosition.x, -200, Result_text.transform.localPosition.z);
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
        sub_operation_output = numbers[0];
        suboperationIndex = 0;
    }

    public void ResetClicked()
    {
        if (Problem_Number == jsonData.Sub.Length)
        {
            CongratulationText.text = Congratulation_message;
            CongratulationPannel.SetActive(true);
            return;
        }
        if (jsonData.Sub[Problem_Number].status == "solved")
        {
            Problem_Number++;

            if (OperationNumbers_PARENT.childCount > 2)
            {
                for (int i = 2; i < OperationNumbers_PARENT.childCount - 1; i++)
                {
                    DestroyImmediate(OperationNumbers_PARENT.gameObject.transform.GetChild(i).gameObject);
                }
            }
            performNext = true;
            if (Problem_Number == jsonData.Sub.Length)
            {
                CongratulationText.text = Congratulation_message;
                CongratulationPannel.SetActive(true);
                //  return;
            }

        }
        else
        {
            OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.white;
            OperationNumbers_PARENT.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
            sub_operation_output = jsonData.Sub[Problem_Number].numbers[0];
            suboperationIndex = 0;
        }

    }



    void compare_abacus_and_operation_value()
    {
        if (sub_operation_output == ValueCalculator.value)
        {
            if (sub_operation_output == jsonData.Sub[Problem_Number].Result)
            {
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.white;
                suboperationIndex++;
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().text = jsonData.Sub[Problem_Number].Result.ToString();
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.red;
                jsonData.Sub[Problem_Number].status = "solved";
                sub_operation_output = -1;
            }

            else if (sub_operation_output == -1)
            {
                ;
            }

            else
            {
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.white;
                suboperationIndex++;
                sub_operation_output = sub_operation_output + jsonData.Sub[Problem_Number].numbers[suboperationIndex];
                OperationNumbers_PARENT.GetChild(suboperationIndex).GetComponent<TextMeshProUGUI>().color = Color.red;
            }

        }

    }


    private void OnDisable()
    {
        MoveBeeds_1.dosomthing -= compare_abacus_and_operation_value;
        MoveBeeds_2.dosomthing -= compare_abacus_and_operation_value;
        reset.onClick.RemoveListener(ResetClicked);
        ResetBar.OnReset -= ResetClicked;

        try
        {
            for (int i = 0; i < OperationNumbers_PARENT.childCount; i++)
            {
                if (OperationNumbers_PARENT.GetChild(i).CompareTag("NumberClone"))
                {
                    DestroyImmediate(OperationNumbers_PARENT.GetChild(i).gameObject);

                }
            }

        }
        catch
        {
            ;
        }


    }


}
