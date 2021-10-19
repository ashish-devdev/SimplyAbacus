using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class QAController : MonoBehaviour
{
    public static event System.Action onAnswerdedCorrectly;
    public UnityEvent OnAnsweredCorrect;
    public UnityEvent OnAnsweredWrong;

    public TextMeshProUGUI question;
    public TextMeshProUGUI[] answers;
    public LeanToggle parentalGateToggle;
    public TextAsset questionsAndAnswers;
    public QA[] qAs;
    string[] optionIndex = new string[4] { "A", "B", "C", "D" };
    QA currentQA;
    bool forShare = true;
    int temp;
    private void Start()
    {
        qAs = MyJsonHelper.FromJson<QA>(questionsAndAnswers.ToString());
        temp = 0;
        // LoadQuestion();
    }

    public void LoadQuestion()
    {
        int k = Random.Range(0, qAs.Length);
        while (true)
        {

            if (k != temp)
            {
                temp = k;
                break;
            }
            k = Random.Range(0, qAs.Length);
        }
        currentQA = qAs[k];

        question.text = currentQA.question;
        for (int i = 0; i < currentQA.answers.Length; i++)
        {
            answers[i].text = /*optionIndex[i] + ". " + */currentQA.answers[i];
        }
        parentalGateToggle.TurnOn();

    }

    public void CheckCorrectAnswer(int i)
    {
        if (answers[i].text.Trim().Equals(currentQA.correctAnswer))
        {

            onAnswerdedCorrectly.Invoke();
            OnAnsweredCorrect.Invoke();

        }
        else
        {
            OnAnsweredWrong.Invoke();
        }
    }
}

[System.Serializable]
public class QA
{
    public string question;
    public string[] answers;
    public string correctAnswer;
}