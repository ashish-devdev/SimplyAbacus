using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayingQuestionAndOptionCards : MonoBehaviour
{

    DailyWorkOutRandomQuestionGeneratorAndAnswerValidator.QuestionAnswerOptionAndTimeDataHolder questionAnswerOptionAndTimeData;

    Image option1Image;
    Image option2Image;
    Image option3Image;
    Image option4Image;

    bool enableDone;

    public int currentQuestionNumber;
    public Button leftArrow;
    public Button rightArrow;
    public DailyWorkOutRandomQuestionGeneratorAndAnswerValidator dailyWorkOutRandomQuestionGeneratorAndAnswerValidator;
    public int totalQuestions;

    public TextMeshProUGUI currentQuestionNumberText;

    public TextMeshProUGUI questionText;

    public TextMeshProUGUI optionText1;
    public TextMeshProUGUI optionText2;
    public TextMeshProUGUI optionText3;
    public TextMeshProUGUI optionText4;

    public Button option1;
    public Button option2;
    public Button option3;
    public Button option4;

    public Button Submit;
    public GameObject glowImage;

    public GameObject[] QuestionListFrom1_20;
    public GameObject[] QuestionListFrom21_40;

    public bool[] QuestionAnswered;



    public Reset reset;
    public List<GameObject> questionList_Questions;


    private void OnEnable()
    {
        Submit.interactable = false;
        glowImage.SetActive(false);
        enableDone = false;
        Invoke("InvokeEnableAfterDelay", 0.1f);



    }
    public void InvokeEnableAfterDelay()
    {
        currentQuestionNumber = 0;
        totalQuestions = dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.numberofQuestion;
        QuestionAnswered = new bool[totalQuestions];
        ChangeButtonIntratbility();
        currentQuestionNumberText.text = (currentQuestionNumber + 1) + " / " + totalQuestions;

        for (int i = 0; i < QuestionListFrom1_20.Length; i++)
        {
            int k = i;
            QuestionListFrom1_20[k].GetComponent<Button>().onClick.AddListener(delegate { ChangeCurrentQuestionInstantly(k); });

        }

        for (int i = 0; i < QuestionListFrom21_40.Length; i++)
        {
            int k = i + 20;
            QuestionListFrom21_40[i].GetComponent<Button>().onClick.AddListener(delegate { ChangeCurrentQuestionInstantly(k); });

        }




        if (totalQuestions > 30)
        {
            for (int i = 10; i < 20; i++)
            {
                QuestionListFrom21_40[i].SetActive(true);
            }


        }
        else
        {
            for (int i = 10; i < 20; i++)
            {
                QuestionListFrom21_40[i].SetActive(false);
            }
        }

        if (totalQuestions > 20)
        {
            for (int i = 0; i < 10; i++)
            {
                QuestionListFrom21_40[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                QuestionListFrom21_40[i].SetActive(false);
            }
        }


        questionAnswerOptionAndTimeData = dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[currentQuestionNumber];
        AssignQuestionAndOptionsToTheCard();
        MakeCurrentQuestionCardBoundaryEnable(currentQuestionNumber);

        option1Image = option1.gameObject.GetComponent<Image>();
        option2Image = option2.gameObject.GetComponent<Image>();
        option3Image = option3.gameObject.GetComponent<Image>();
        option4Image = option4.gameObject.GetComponent<Image>();

        leftArrow.onClick.AddListener(() =>
        {

            currentQuestionNumber--;
            MakeCurrentQuestionCardBoundaryEnable(currentQuestionNumber);

            currentQuestionNumberText.text = (currentQuestionNumber + 1) + " / " + totalQuestions;
            questionAnswerOptionAndTimeData = dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[currentQuestionNumber];
            AssignQuestionAndOptionsToTheCard();
            MakeAllreadyAnsweredCardBlue();






        });

        leftArrow.onClick.AddListener(ChangeButtonIntratbility);

        rightArrow.onClick.AddListener(() =>
        {

            currentQuestionNumber++;
            MakeCurrentQuestionCardBoundaryEnable(currentQuestionNumber);

            currentQuestionNumberText.text = (currentQuestionNumber + 1) + " / " + totalQuestions;
            questionAnswerOptionAndTimeData = dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[currentQuestionNumber];
            AssignQuestionAndOptionsToTheCard();
            MakeAllreadyAnsweredCardBlue();


        });
        rightArrow.onClick.AddListener(ChangeButtonIntratbility);


        option1.onClick.AddListener(delegate { AssignAnswerToQuestion(0); });
        option2.onClick.AddListener(delegate { AssignAnswerToQuestion(1); });
        option3.onClick.AddListener(delegate { AssignAnswerToQuestion(2); });
        option4.onClick.AddListener(delegate { AssignAnswerToQuestion(3); });
        enableDone = true;
    }

    public void Update()
    {
        if (enableDone)
            questionAnswerOptionAndTimeData.currentTime = questionAnswerOptionAndTimeData.currentTime + Time.deltaTime;
    }



    private void OnDisable()
    {
        leftArrow.onClick.RemoveAllListeners();
        rightArrow.onClick.RemoveAllListeners();

        option1.onClick.RemoveListener(delegate { AssignAnswerToQuestion(0); });
        option2.onClick.RemoveListener(delegate { AssignAnswerToQuestion(1); });
        option3.onClick.RemoveListener(delegate { AssignAnswerToQuestion(2); });
        option4.onClick.RemoveListener(delegate { AssignAnswerToQuestion(3); });


        for (int i = 0; i < QuestionListFrom1_20.Length; i++)
        {
            int k = i;
            QuestionListFrom1_20[k].GetComponent<Button>().onClick.RemoveListener(delegate { ChangeCurrentQuestionInstantly(k); });

        }

        for (int i = 0; i < QuestionListFrom21_40.Length; i++)
        {
            int k = i + 20;
            QuestionListFrom21_40[i].GetComponent<Button>().onClick.RemoveListener(delegate { ChangeCurrentQuestionInstantly(k); });

        }

    }


    public void ChangeButtonIntratbility()
    {
        if (currentQuestionNumber == 0)
        {
            leftArrow.interactable = false;
        }
        else
        {
            leftArrow.interactable = true;
        }


        if (currentQuestionNumber == totalQuestions - 1)
        {
            rightArrow.interactable = false;

        }
        else
        {
            rightArrow.interactable = true;

        }


    }




    public void AssignQuestionAndOptionsToTheCard()
    {
        questionText.text = questionAnswerOptionAndTimeData.questionExpression;

        optionText1.text = questionAnswerOptionAndTimeData.option1;
        optionText2.text = questionAnswerOptionAndTimeData.option2;
        optionText3.text = questionAnswerOptionAndTimeData.option3;
        optionText4.text = questionAnswerOptionAndTimeData.option4;

        reset.RESET();
        
    }

    public void AssignAnswerToQuestion(int i)
    {
        questionAnswerOptionAndTimeData.isOption1Picked = false;
        questionAnswerOptionAndTimeData.isOption2Picked = false;
        questionAnswerOptionAndTimeData.isOption3Picked = false;
        questionAnswerOptionAndTimeData.isOption4Picked = false;

        ChangeColorOfQuestionListBlue(currentQuestionNumber);
        MakrkedAsQuestionAnswered(currentQuestionNumber);

        switch (i)
        {
            case 0:
                questionAnswerOptionAndTimeData.isOption1Picked = true;
                break;
            case 1:
                questionAnswerOptionAndTimeData.isOption2Picked = true;
                break;
            case 2:
                questionAnswerOptionAndTimeData.isOption3Picked = true;
                break;
            case 3:
                questionAnswerOptionAndTimeData.isOption4Picked = true;
                break;
        }
    }

    public void MakeAllreadyAnsweredCardBlue()
    {
        option1.image.color = Color.white;
        option2.image.color = Color.white;
        option3.image.color = Color.white;
        option4.image.color = Color.white;
        Color color;
        ColorUtility.TryParseHtmlString("#00C2FF", out color);

        if (questionAnswerOptionAndTimeData.isOption1Picked)
        {
            option1Image.color = color;
        }
        else if (questionAnswerOptionAndTimeData.isOption2Picked)
        {
            option2Image.color = color;
        }
        else if (questionAnswerOptionAndTimeData.isOption3Picked)
        {
            option3Image.color = color;
        }
        else if (questionAnswerOptionAndTimeData.isOption4Picked)
        {
            option4Image.color = color;
        }

    }


    public void ChangeColorOfQuestionListWhite()
    {
        for (int i = 0; i < questionList_Questions.Count; i++)
        {
            questionList_Questions[i].GetComponent<Image>().color = Color.white;
        }
        //00C2FFFF
    }

    public void ChangeColorOfQuestionListBlue(int i)
    {
        Color color;
        ColorUtility.TryParseHtmlString("#00C2FF", out color);
        questionList_Questions[i].GetComponent<Image>().color = color;
    }


    public void ChangeCurrentQuestionInstantly(int i)
    {
        currentQuestionNumber = i;
        MakeCurrentQuestionCardBoundaryEnable(currentQuestionNumber);

        currentQuestionNumberText.text = (currentQuestionNumber + 1) + " / " + totalQuestions;
        questionAnswerOptionAndTimeData = dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[currentQuestionNumber];
        AssignQuestionAndOptionsToTheCard();
        MakeAllreadyAnsweredCardBlue();

        ChangeButtonIntratbility();




    }


    public void MakrkedAsQuestionAnswered(int n)
    {
        QuestionAnswered[n] = true;
        for (int i = 0; i < QuestionAnswered.Length; i++)
        {
            if (QuestionAnswered[i] == false)
            {
                return;
            }
        }
        Submit.interactable = true;
        glowImage.gameObject.SetActive(true);

    }


    public void MakeCurrentQuestionCardBoundaryEnable(int n)
    {
        for (int i = 0; i < questionList_Questions.Count; i++)
        {
            questionList_Questions[i].transform.GetChild(1).gameObject.SetActive(false);
        }
        questionList_Questions[n].transform.GetChild(1).gameObject.SetActive(true);
    }

}
