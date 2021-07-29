using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDailyWorkoutStats : MonoBehaviour
{
    public List<GameObject> dailyWorkoutStatsDisplayCards;
    public TextMeshProUGUI userName;
    int totalCards;
    public Button share;
    string stringToShare;
    ClassParentDailyWorkoutStats classParentDailyWorkoutStats;
    private void OnEnable()
    {
        stringToShare = "";

        totalCards = dailyWorkoutStatsDisplayCards.Count;
        if (File.Exists(SaveManager.Instance.dailyWorkoutStatsPath))
        {
            classParentDailyWorkoutStats = SaveManager.Instance.loadDailyWorkoutStatsDataFromDisk();
            for (int i = 0; i < totalCards; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            stringToShare += "Name: "+ userName.text;
                            stringToShare += "\n\n.................\n *Easy* \n----------------\n ";
                            break;
                        }
                    case 12:
                        stringToShare += "\n................\n *Medium* \n----------------\n ";
                        break;
                    case 24:
                        stringToShare += "\n................\n *Hard* \n----------------\n ";
                        break;
                    default:
                        break;

                }
                dailyWorkoutStatsDisplayCards[i].transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].QuestionType;
                stringToShare += "Question type: " + classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].QuestionType + "\n";

                dailyWorkoutStatsDisplayCards[i].transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().text = classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].totalQuestions.ToString();
                stringToShare += "Total Question: " + classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].totalQuestions.ToString() + "\n";


                dailyWorkoutStatsDisplayCards[i].transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].totalCorrectAnswers.ToString();
                stringToShare += "Correct Answer: " + classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].totalCorrectAnswers.ToString() + "\n";

                try
                {
                    dailyWorkoutStatsDisplayCards[i].transform.GetChild(3).transform.GetComponent<TextMeshProUGUI>().text = TimeSpan.FromSeconds(classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].totalTime * 1f / classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].totalCorrectAnswers).ToString(@"hh\h\:mm\m\:ss\s");
                    stringToShare += "Avg Time: " + TimeSpan.FromSeconds(classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].totalTime * 1f / classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].totalCorrectAnswers).ToString(@"hh\h\:mm\m\:ss\s")+"\n";
                }
                catch
                {
                    dailyWorkoutStatsDisplayCards[i].transform.GetChild(3).transform.GetComponent<TextMeshProUGUI>().text = "NA";
                    stringToShare += "Avg Time: NA" + "\n";
                }

                stringToShare += "\n";

            }
        }
    }

    public void AttachShareToOnAnsweredCorrecltlyAction()
    {
        QAController.onAnswerdedCorrectly += ShareText;

    }
    public void RemoveShareToOnAnsweredCorrecltlyAction()
    {
        QAController.onAnswerdedCorrectly -= ShareText;

    }


    public void ShareText()
    {

        new NativeShare()
            .SetSubject("Subject goes here").SetText(stringToShare)
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }

}
