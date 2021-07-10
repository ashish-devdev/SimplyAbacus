using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreenGenralScript : MonoBehaviour
{

    float totalTime;
    TimeSpan convertedTime;
    int correctAnswerCount;

    public Image circularPercentageImage;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI currentDateText;
    public QuestionTypeSelectionGenralScript questionTypeSelectionGenralScript;
    public DailyWorkOutRandomQuestionGeneratorAndAnswerValidator dailyWorkOutRandomQuestionGeneratorAndAnswerValidator;
    public TextMeshProUGUI UserName;
    public TextMeshProUGUI correctAnswerVsTotalQuestion;
    public TextMeshProUGUI percentage;
    public TextMeshProUGUI mode;
    public TextMeshProUGUI[] questionTypeName;
    public TextMeshProUGUI totalQuestionText;
    public TextMeshProUGUI totalCorrectAnsweredText;
    public TextMeshProUGUI totalWrongAnsweredText;

    public TextMeshProUGUI hourText;
    public TextMeshProUGUI minText;
    public TextMeshProUGUI secText;

    public Button share;


    public GameObject[] resultCard;
    int temp;


    private void OnEnable()
    {
        temp = 0;
        totalTime = 0;
        correctAnswerCount = 0;
        nameText.text = UserName.text;
        currentDateText.text = DateTime.Today.ToString("dd-MM-yyyy");
        share.onClick.AddListener(ShareInformation);

        for (int i = 0; i < dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder.Count; i++)
        {
            resultCard[i].SetActive(true);
        }

        for (int i = 0; i < questionTypeSelectionGenralScript.selectedList.Count; i++)
        {
            if (questionTypeSelectionGenralScript.selectedList[i])
            {
                questionTypeName[temp].transform.gameObject.SetActive(true);
                questionTypeName[temp].text = questionTypeSelectionGenralScript.questionTypes[i];
                temp++;
            }
        }


        for (int i = 0; i < dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder.Count; i++)
        {
            totalTime += dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].currentTime;
        }
        convertedTime = TimeSpan.FromSeconds(totalTime);
        timeText.text = "Test completed in " + convertedTime.ToString(@"hh\h\:mm\m\:ss\s");

        hourText.text = convertedTime.ToString(@"hh\:mm\:ss").Split(':')[0];
        minText.text = convertedTime.ToString(@"hh\:mm\:ss").Split(':')[1];
        secText.text = convertedTime.ToString(@"hh\:mm\:ss").Split(':')[2];

        Color color;
        ColorUtility.TryParseHtmlString("#00FF00", out color);
        for (int i = 0; i < dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder.Count; i++)
        {
            if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].isOption1Picked)
            {

                if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option1 == dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].correctAnswer)
                {
                    correctAnswerCount++;
                    resultCard[i].GetComponent<Image>().color = color;



                }

            }
            else if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].isOption2Picked)
            {
                if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option2 == dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].correctAnswer)
                {
                    correctAnswerCount++;
                    resultCard[i].GetComponent<Image>().color = color;


                }

            }
            else if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].isOption3Picked)
            {
                if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option3 == dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].correctAnswer)
                {
                    correctAnswerCount++;
                    resultCard[i].GetComponent<Image>().color = color;


                }

            }
            else if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].isOption4Picked)
            {
                if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option4 == dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].correctAnswer)
                {
                    correctAnswerCount++;
                    resultCard[i].GetComponent<Image>().color = color;
                }

            }

        }


        correctAnswerVsTotalQuestion.text = correctAnswerCount + " / " + dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder.Count + " answerd correctly";

        if ((correctAnswerCount * 100f / dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder.Count).ToString().Contains("."))
            percentage.text = (correctAnswerCount * 100f / dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder.Count).ToString("F1") + "%";
        else
            percentage.text = (correctAnswerCount * 100f / dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder.Count).ToString("F0") + "%";

        circularPercentageImage.fillAmount = (correctAnswerCount * 1f / dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder.Count);
        mode.text = "Mode : " + dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.currentMode.ToString();
        totalQuestionText.text = dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder.Count.ToString();
        totalCorrectAnsweredText.text = correctAnswerCount.ToString();
        totalWrongAnsweredText.text = (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder.Count - correctAnswerCount).ToString();

        for (int i = 0; i < dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder.Count; i++)
        {
            resultCard[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].questionExpression;
            ColorUtility.TryParseHtmlString("#E92D4BFF", out color);

          


            resultCard[i].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option1;
            resultCard[i].transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option3;
            resultCard[i].transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option2;
            resultCard[i].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option4;


            ColorUtility.TryParseHtmlString("#39B384", out color);
            if (resultCard[i].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text == dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].correctAnswer)
            {
                resultCard[i].transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().color = color;

                resultCard[i].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
                resultCard[i].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<Image>().color=Color.green;

            }
            else if (resultCard[i].transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text == dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].correctAnswer)
            {
                resultCard[i].transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>().color = color;
                resultCard[i].transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(true);
                resultCard[i].transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>().color = Color.green;

            }
            else if (resultCard[i].transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text == dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].correctAnswer)
            {
                resultCard[i].transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().color = color;
                resultCard[i].transform.GetChild(1).GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(true);
                resultCard[i].transform.GetChild(1).GetChild(1).GetChild(0).GetChild(2).GetComponent<Image>().color = Color.green;

            }
            else if (resultCard[i].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text == dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].correctAnswer)
            {
                resultCard[i].transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Image>().color = color;
                resultCard[i].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(2).gameObject.SetActive(true);
                resultCard[i].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(2).GetComponent<Image>().color = Color.green;

            }

            if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].isOption1Picked)
            {
                resultCard[i].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().color = Color.white;
                resultCard[i].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
                resultCard[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Your Answer: " + dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option1;
            }
            else if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].isOption2Picked)
            {
                resultCard[i].transform.GetChild(1).GetChild(1).GetChild(0).GetChild(2).GetComponent<Image>().color = Color.white;
                resultCard[i].transform.GetChild(1).GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(true);


                resultCard[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Your Answer: " + dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option2;
            }
            else if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].isOption3Picked)
            {
                resultCard[i].transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>().color = Color.white;
                resultCard[i].transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(true);


                resultCard[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Your Answer: " + dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option3;
            }
            else if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].isOption4Picked)
            {
                resultCard[i].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(2).GetComponent<Image>().color = Color.white;
                resultCard[i].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(2).gameObject.SetActive(true);

                resultCard[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Your Answer: " + dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option4;
            }



        }

    }

    public void ShareInformation()
    {
        string textToShare = "";
        string typeOfQuestionText = "";
        textToShare += " *Result of daily workout* \n-----------------------\n\n";
        textToShare += " *Name:* " + UserName.text + "\n";
        textToShare += " " + DateTime.Today.ToString("dd-MM-yyyy") + "\n";
        textToShare += " *Mode*: " + dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.currentMode.ToString() + "\n";
        textToShare += " *Percentage:* " + percentage.text + "\n";
        textToShare += " *Time to complete the test:* " + convertedTime.ToString(@"hh\h\:mm\m\:ss\s") + "\n";
        textToShare += " *Total Question:* " + totalQuestionText.text + "\n";
        textToShare += " *Correct Answer:* " + totalCorrectAnsweredText.text + "\n";
        textToShare += " *Wrong Answer:* "+totalWrongAnsweredText.text + "\n";

        for (int i = 0; i < questionTypeSelectionGenralScript.selectedList.Count; i++)
        {
            if (questionTypeSelectionGenralScript.selectedList[i])
            {
                questionTypeName[temp].transform.gameObject.SetActive(true);
                questionTypeName[temp].text = questionTypeSelectionGenralScript.questionTypes[i];
                typeOfQuestionText += "   "+questionTypeName[temp].text + "\n";
                temp++;
            }
        }

        textToShare += "*Type of Question:* \n" + typeOfQuestionText;

        new NativeShare()
            .SetSubject("Subject goes here").SetText(textToShare)
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

    }



    private void OnDisable()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#FF0000FF", out color);

        for (int i = 0; i < resultCard.Length; i++)
        {
            resultCard[i].SetActive(false);
            resultCard[i].GetComponent<Image>().color = color;
            resultCard[i].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(2).GetComponent<Image>().color = Color.white;
            resultCard[i].transform.GetChild(1).GetChild(1).GetChild(0).GetChild(2).GetComponent<Image>().color = Color.white;
            resultCard[i].transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>().color = Color.white;
            resultCard[i].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().color = Color.white;

            resultCard[i].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(2).gameObject.SetActive(false);
            resultCard[i].transform.GetChild(1).GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(false);
            resultCard[i].transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(false);
            resultCard[i].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);

        }
        for (int i = 0; i < questionTypeName.Length; i++)
        {
            questionTypeName[i].transform.gameObject.SetActive(false);
        }
        share.onClick.RemoveListener(ShareInformation);

    }


}
