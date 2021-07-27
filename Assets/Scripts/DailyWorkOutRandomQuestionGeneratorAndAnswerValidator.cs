using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DailyWorkOutRandomQuestionGeneratorAndAnswerValidator : MonoBehaviour
{
    public int numberofQuestion = 20;
    public Mode currentMode = Mode.Easy;
    public List<QuestionAnswerOptionAndTimeDataHolder> questionAnswerOptionAndTimeDataHolder;
    public QuestionTypeSelectionGenralScript questionTypeSelectionGenralScript;
    public AudioClip easy;
    public AudioClip medium;
    public AudioClip defaultMusic;
    public int problemNumber;
    int numberOfQuestionTypeSelectedCard;

    int minRangeForEasy = 1;
    int maxRangeForEasy = 100;

    int minRangeForMedium = 100;
    int maxRangeForMedium = 1000;

    int minRangeForHard = 1000;
    int maxRangeForHard = 10000;

    List<int> selectedQuestionList;

    public void SetNumberOFQuestionValue(int n)
    {
        numberofQuestion = n;
    }

    public void SetModeEasy()
    {
        currentMode = Mode.Easy;
    }

    public void SetModeMedium()
    {
        currentMode = Mode.Medium;
    }

    public void SetModeHard()
    {
        currentMode = Mode.Hard;
    }



    public enum Mode
    {
        Easy,
        Medium,
        Hard
    }

    public void OnEnable()
    {
        problemNumber = 0;
        questionAnswerOptionAndTimeDataHolder = new List<QuestionAnswerOptionAndTimeDataHolder>(new QuestionAnswerOptionAndTimeDataHolder[numberofQuestion]);

        if (currentMode == Mode.Easy)
        {
            //play easy
            SoundManager.Instance.PlayMusic(easy);
        }
        else
        {
            SoundManager.Instance.PlayMusic(medium);
            //play medium
        }

        numberOfQuestionTypeSelectedCard = 0;
        selectedQuestionList = new List<int>();
        for (int i = 0; i < questionTypeSelectionGenralScript.selectedList.Count; i++)
        {
            if (questionTypeSelectionGenralScript.selectedList[i])
            {
                numberOfQuestionTypeSelectedCard++;
            }
        }

        for (int i = 0; i < questionTypeSelectionGenralScript.selectedList.Count; i++)
        {
            if (questionTypeSelectionGenralScript.selectedList[i] == true)
            {
                selectedQuestionList.Add(i);
            }
        }

        for (int i = 0; i < numberofQuestion; i++)
        {


            switch (selectedQuestionList[i % selectedQuestionList.Count])
            {
                case 0:
                    AdditionProblemGenrator();
                    break;
                case 1:
                    SubtractionProblemGenrator();
                    break;
                case 2:
                    AdditionWithDecimalProblemGenrator();
                    break;
                case 3:
                    SubtractionWithDecimalProblemGenrator();
                    break;
                case 4:
                    MultiplicationProblemGenrator();
                    break;
                case 5:
                    DivisionProblemGenrator();
                    break;
                case 6:
                    MultiplicationWithDecimalProblemGenrator();
                    break;
                case 7:
                    DivisionWithDecimalProblemGenrator();

                    break;
                case 8:
                    PercentageProblemGenrator();
                    break;
                case 9:
                    MixedOperationProblemGenrator();
                    break;
                case 10:
                    PercentageWithDecimalProblemGenrator();
                    break;
                case 11:
                    MixedOperationWithDecimalProblemGenrator();
                    break;








            }
        }


    }


    public void AdditionProblemGenrator()
    {
        if (currentMode == Mode.Easy)
        {
            int num1 = 0;
            int num2 = 0;
            int result = 0;
            int optionIndex = 0;
            int[] optionValues;
            bool temp = false;
            int r;
            optionValues = new int[4] { -1, -1, -1, -1 };


            num1 = UnityEngine.Random.Range(minRangeForEasy, maxRangeForEasy);
            num2 = UnityEngine.Random.Range(minRangeForEasy, maxRangeForEasy);
            result = num1 + num2;
            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.addition;
            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = num1.ToString() + " + " + num2.ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1, 51);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {
                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();




        }

        if (currentMode == Mode.Medium)
        {
            //5 number
            // 100-1000


            int[] num;

            int result = 0;
            int optionIndex = 0;
            int[] optionValues;
            bool temp = false;
            int r;
            optionValues = new int[4] { -1, -1, -1, -1 };
            num = new int[5];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.addition;

            for (int i = 0; i < 5; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForMedium, maxRangeForMedium);
                result += num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[i] + " " + "+";
            }

            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression.Trim('+');

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1, 26);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();




        }

        if (currentMode == Mode.Hard)
        {
            //10 number
            // 1000 10000


            int[] num;

            int result = 0;
            int optionIndex = 0;
            int[] optionValues;
            bool temp = false;
            int r;
            optionValues = new int[4] { -1, -1, -1, -1 };
            num = new int[10];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.addition;

            for (int i = 0; i < 10; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForHard, maxRangeForHard);
                result += num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[i] + " " + "+";
            }

            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression.Trim('+');

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1, 8);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();



        }

        problemNumber++;
    }
    public void SubtractionProblemGenrator()
    {
        if (currentMode == Mode.Easy)
        {
            int[] num;

            int result = 0;
            int optionIndex = 0;
            int[] optionValues;
            bool temp = false;
            bool subtractNow = true;
            int r;
            optionValues = new int[4] { -1, -1, -1, -1 };
            num = new int[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.subtraction;

            num[0] = UnityEngine.Random.Range(minRangeForEasy, maxRangeForEasy);
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[0];
            result += num[0];
            for (int i = 1; i < 2; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForEasy, maxRangeForEasy);
                if (subtractNow)
                {
                    if (num[i] < result)
                    {
                        subtractNow = !subtractNow;

                        result -= num[i];
                        questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + " -" + num[i];
                    }
                    else
                    {
                        i--;
                    }

                }
                else
                {
                    subtractNow = !subtractNow;
                    result += num[i];
                    questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + " +" + num[i];

                }



            }

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1, 51);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();



        }

        if (currentMode == Mode.Medium)
        {
            int[] num;

            int result = 0;
            int optionIndex = 0;
            int[] optionValues;
            bool temp = false;
            bool subtractNow = true;
            int r;
            optionValues = new int[4] { -1, -1, -1, -1 };
            num = new int[5];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.subtraction;

            num[0] = UnityEngine.Random.Range(minRangeForMedium, maxRangeForMedium);
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[0];
            result += num[0];
            for (int i = 1; i < 5; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForMedium, maxRangeForMedium);
                if (subtractNow)
                {
                    if (num[i] < result)
                    {
                        subtractNow = !subtractNow;

                        result -= num[i];
                        questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + " -" + num[i];
                    }
                    else
                    {
                        i--;
                    }

                }
                else
                {
                    subtractNow = !subtractNow;

                    result += num[i];
                    questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + " +" + num[i];

                }



            }

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1, 26);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();




        }

        if (currentMode == Mode.Hard)
        {
            int[] num;

            int result = 0;
            int optionIndex = 0;
            int[] optionValues;
            bool temp = false;
            bool subtractNow = true;
            int r;
            optionValues = new int[4] { -1, -1, -1, -1 };
            num = new int[9];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.subtraction;

            num[0] = UnityEngine.Random.Range(minRangeForHard, maxRangeForHard);
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[0];
            result += num[0];
            for (int i = 1; i < 9; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForHard, maxRangeForHard);
                if (subtractNow)
                {
                    if (num[i] < result)
                    {
                        subtractNow = !subtractNow;
                        result -= num[i];
                        questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + " -" + num[i];
                    }
                    else
                    {
                        i--;
                    }

                }
                else
                {
                    subtractNow = !subtractNow;
                    result += num[i];
                    questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + " +" + num[i];

                }



            }

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1, 8);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }
        problemNumber++;
    }
    public void MultiplicationProblemGenrator()
    {
        if (currentMode == Mode.Easy)
        {


            int[] num;

            int result = 1;
            int optionIndex = 0;
            int[] optionValues;
            bool temp = false;
            int r;
            optionValues = new int[4] { -1, -1, -1, -1 };
            num = new int[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.multiplication;

            for (int i = 0; i < 2; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForEasy, maxRangeForEasy);
                result *= num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[i] + " *";
            }

            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression.Trim('*');

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1, 51);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();



        }

        if (currentMode == Mode.Medium)
        {
            int[] num;

            int result = 1;
            int optionIndex = 0;
            int[] optionValues;
            bool temp = false;
            int r;
            optionValues = new int[4] { -1, -1, -1, -1 };
            num = new int[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.multiplication;

            for (int i = 0; i < 2; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForMedium, maxRangeForMedium);
                result *= num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[i] + " *";
            }

            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression.Trim('*');

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1, 26);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }

        if (currentMode == Mode.Hard)
        {
            //2 number
            // 0-100
            //

            int[] num;

            int result = 1;
            int optionIndex = 0;
            int[] optionValues;
            bool temp = false;
            int r;
            optionValues = new int[4] { -1, -1, -1, -1 };
            num = new int[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.multiplication;

            for (int i = 0; i < 2; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForHard, maxRangeForHard);
                result *= num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[i] + " *";
            }

            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression.Trim('*');

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1, 8);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();



        }
        problemNumber++;
    }
    public void DivisionProblemGenrator()
    {
        if (currentMode == Mode.Easy)
        {
            int[] num;

            int result = 1;
            int optionIndex = 0;
            int[] optionValues;
            bool temp = false;
            int r;
            optionValues = new int[4] { -1, -1, -1, -1 };
            num = new int[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.division;

            for (int i = 0; i < 2; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForEasy, maxRangeForEasy);
                result *= num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = result + " /" + num[0];
            }



            optionValues[(int)UnityEngine.Random.Range(0, 4)] = num[1];

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = num[1].ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1, 51);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((num[1] - r > 0 && temp))
                {
                    if ((num[1] - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (num[1] - r)) == -1)
                        {

                            optionValues[optionIndex] = num[1] - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((num[1] + r > 0 && temp))
                {
                    if ((num[1] + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (num[1] + r)) == -1)
                        {
                            optionValues[optionIndex] = num[1] + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();

        }

        if (currentMode == Mode.Medium)
        {

            int[] num;

            int result = 1;
            int optionIndex = 0;
            int[] optionValues;
            bool temp = false;
            int r;
            optionValues = new int[4] { -1, -1, -1, -1 };
            num = new int[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.division;

            for (int i = 0; i < 2; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForMedium, maxRangeForMedium);
                result *= num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = result + " / " + num[0];
            }



            optionValues[(int)UnityEngine.Random.Range(0, 4)] = num[1];

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = num[1].ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1, 26);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((num[1] - r > 0 && temp))
                {
                    if ((num[1] - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (num[1] - r)) == -1)
                        {

                            optionValues[optionIndex] = num[1] - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((num[1] + r > 0 && temp))
                {
                    if ((num[1] + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (num[1] + r)) == -1)
                        {
                            optionValues[optionIndex] = num[1] + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }

        if (currentMode == Mode.Hard)
        {

            int[] num;

            int result = 1;
            int optionIndex = 0;
            int[] optionValues;
            bool temp = false;
            int r;
            optionValues = new int[4] { -1, -1, -1, -1 };
            num = new int[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.division;

            for (int i = 0; i < 2; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForHard, maxRangeForHard);
                result *= num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = result + " / " + num[0];
            }



            optionValues[(int)UnityEngine.Random.Range(0, 4)] = num[1];

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = num[1].ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1, 8);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((num[1] - r > 0 && temp))
                {
                    if ((num[1] - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (num[1] - r)) == -1)
                        {

                            optionValues[optionIndex] = num[1] - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((num[1] + r > 0 && temp))
                {
                    if ((num[1] + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (num[1] + r)) == -1)
                        {
                            optionValues[optionIndex] = num[1] + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }
        problemNumber++;
    }
    public void PercentageProblemGenrator()
    {
        if (currentMode == Mode.Easy)
        {
            int[] num;

            float result = 1;
            int optionIndex = 0;
            float[] optionValues;
            bool temp = false;
            float r;
            optionValues = new float[4] { -1, -1, -1, -1 };
            num = new int[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.percentage;

            for (int i = 0; i < 2; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForEasy, maxRangeForEasy);
            }
            result = (num[0] * num[1] * 1f) / 100;
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = num[0] + " % " + num[1];



            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1f, 51f);
                float f = float.Parse(r.ToString("F2"));

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }


                else if ((result - float.Parse(r.ToString("F2")) > 0 && temp))
                {
                    if ((result - float.Parse(r.ToString("F2"))) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - float.Parse(r.ToString("F2")))) == -1)
                        {

                            optionValues[optionIndex] = result - float.Parse(r.ToString("F2"));
                            optionIndex++;
                        }
                    }
                }
                else if ((result + float.Parse(r.ToString("F2")) > 0 && temp))
                {
                    if ((result + float.Parse(r.ToString("F2"))) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + float.Parse(r.ToString("F2")))) == -1)
                        {
                            optionValues[optionIndex] = result + float.Parse(r.ToString("F2"));
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();



        }

        if (currentMode == Mode.Medium)
        {
            int[] num;

            float result = 1;
            int optionIndex = 0;
            float[] optionValues;
            bool temp = false;
            float r;
            optionValues = new float[4] { -1, -1, -1, -1 };
            num = new int[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.percentage;


            num[0] = UnityEngine.Random.Range(minRangeForEasy, maxRangeForEasy);
            num[1] = UnityEngine.Random.Range(minRangeForMedium, maxRangeForMedium);

            result = (num[0] * num[1] * 1f) / 100;
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = num[0] + " % " + num[1];



            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1f, 26f);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }



                else if ((result - float.Parse(r.ToString("F2")) > 0 && temp))
                {
                    if ((result - float.Parse(r.ToString("F2"))) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - float.Parse(r.ToString("F2")))) == -1)
                        {

                            optionValues[optionIndex] = result - float.Parse(r.ToString("F2"));
                            optionIndex++;
                        }
                    }
                }
                else if ((result + float.Parse(r.ToString("F2")) > 0 && temp))
                {
                    if ((result + float.Parse(r.ToString("F2"))) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + float.Parse(r.ToString("F2")))) == -1)
                        {
                            optionValues[optionIndex] = result + float.Parse(r.ToString("F2"));
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();




        }

        if (currentMode == Mode.Hard)
        {
            int[] num;

            float result = 1;
            int optionIndex = 0;
            float[] optionValues;
            bool temp = false;
            float r;
            optionValues = new float[4] { -1, -1, -1, -1 };
            num = new int[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.percentage;


            num[0] = UnityEngine.Random.Range(minRangeForEasy, maxRangeForEasy);
            num[1] = UnityEngine.Random.Range(minRangeForHard, maxRangeForHard);

            result = (num[0] * num[1] * 1f) / 100;
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = num[0] + " % " + num[1];



            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1f, 8f);

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }



                else if ((result - float.Parse(r.ToString("F2")) > 0 && temp))
                {
                    if ((result - float.Parse(r.ToString("F2"))) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - float.Parse(r.ToString("F2")))) == -1)
                        {

                            optionValues[optionIndex] = result - float.Parse(r.ToString("F2"));
                            optionIndex++;
                        }
                    }
                }
                else if ((result + float.Parse(r.ToString("F2")) > 0 && temp))
                {
                    if ((result + float.Parse(r.ToString("F2"))) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + float.Parse(r.ToString("F2")))) == -1)
                        {
                            optionValues[optionIndex] = result + float.Parse(r.ToString("F2"));
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }
        problemNumber++;
    }
    public void MixedOperationProblemGenrator()
    {
        if (currentMode == Mode.Easy)
        {
            float result = 0;
            bool resultIsGreaterThanZero = false;
            string expression = "";
            int optionIndex = 0;
            float[] optionValues;
            optionValues = new float[4] { -1f, -1f, -1f, -1f };
            float r;
            bool temp = false;
            while (!resultIsGreaterThanZero)
            {
                expression = GenerateMixedExpression(3, false, 1, 50);
                result = (float)Convert.ToDecimal(new DataTable().Compute(expression, ""));
                if (result >= 0)
                {
                    resultIsGreaterThanZero = true;
                }

            }
            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.mixedOperation;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = expression;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            optionValues[UnityEngine.Random.Range(0, 4)] = result;

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1f, 51f);

                if (optionValues[optionIndex] != -1)
                {
                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {
                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (!Array.Exists(optionValues, x => x == (result + r)))
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();



        }

        if (currentMode == Mode.Medium)
        {

            float result = 0;
            bool resultIsGreaterThanZero = false;
            string expression = "";
            int optionIndex = 0;
            float[] optionValues;
            optionValues = new float[4] { -1f, -1f, -1f, -1f };
            float r;
            bool temp = false;
            bool errorOccured = false;


            do
            {
                errorOccured = false;
                resultIsGreaterThanZero = false;
                try
                {

                    while (!resultIsGreaterThanZero)
                    {
                        expression = GenerateMixedExpression(5, false, 51, 150);
                        result = (float)Convert.ToDecimal(new DataTable().Compute(expression, ""));
                        if (result >= 0)
                        {
                            resultIsGreaterThanZero = true;
                        }

                    }
                }
                catch
                {
                    errorOccured = true;
                    print("error ocuured but solved");
                }

            } while (errorOccured);

            if (!errorOccured)
            {
                questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.mixedOperation;


                questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = expression;
                questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

                optionValues[UnityEngine.Random.Range(0, 4)] = result;

                while (optionIndex < 4)
                {
                    r = UnityEngine.Random.Range(100f, 300f);

                    if (optionValues[optionIndex] != -1)
                    {
                        optionIndex++;
                    }

                    else if ((result - r > 0 && temp))
                    {
                        if ((result - r) != result)
                        {
                            if (Array.IndexOf(optionValues, (result - r)) == -1)
                            {
                                optionValues[optionIndex] = result - r;
                                optionIndex++;
                            }
                        }
                    }
                    else if ((result + r > 0 && temp))
                    {
                        if ((result + r) != result)
                        {
                            if (!Array.Exists(optionValues, x => x == (result + r)))
                            {
                                optionValues[optionIndex] = result + r;
                                optionIndex++;
                            }
                        }
                    }

                    temp = !temp;


                }



                questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
                questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
                questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
                questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();

            }




        }

        if (currentMode == Mode.Hard)
        {


            float result = 0;
            bool resultIsGreaterThanZero = false;
            string expression = "";
            int optionIndex = 0;
            float[] optionValues;
            optionValues = new float[4] { -1f, -1f, -1f, -1f };
            float r;
            bool temp = false;
            bool errorOccured = false;

            do
            {
                errorOccured = false;
                resultIsGreaterThanZero = false;
                try
                {
                    while (!resultIsGreaterThanZero)
                    {
                        expression = GenerateMixedExpression(7, false, 150, 750);
                        result = (float)Convert.ToDecimal(new DataTable().Compute(expression, ""));
                        if (result >= 0)
                        {
                            resultIsGreaterThanZero = true;
                        }

                    }
                }
                catch
                {
                    errorOccured = true;
                    print("error ocuured but solved");
                    //problemNumber--;
                }
            }
            while (errorOccured);

            if (!errorOccured)
            {

                questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.mixedOperation;


                questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = expression;
                questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

                optionValues[UnityEngine.Random.Range(0, 4)] = result;

                while (optionIndex < 4)
                {
                    r = UnityEngine.Random.Range(100f, 300f);

                    if (optionValues[optionIndex] != -1)
                    {
                        optionIndex++;
                    }

                    else if ((result - r > 0 && temp))
                    {
                        if ((result - r) != result)
                        {
                            print(Array.IndexOf(optionValues, (result - r)));
                            if (Array.IndexOf(optionValues, (result - r)) == -1)
                            {
                                optionValues[optionIndex] = result - r;
                                optionIndex++;
                            }
                        }
                    }
                    else if ((result + r > 0 && !temp))
                    {
                        if ((result + r) != result)
                        {
                            if (!Array.Exists(optionValues, x => x == (result + r)))
                            {
                                optionValues[optionIndex] = result + r;
                                optionIndex++;
                            }
                        }
                    }

                    temp = !temp;


                }



                questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
                questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
                questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
                questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();




            }



        }

        problemNumber++;
    }


    public void AdditionWithDecimalProblemGenrator()
    {
        if (currentMode == Mode.Easy)
        {

            float[] num;

            float result = 0;
            int optionIndex = 0;
            float[] optionValues;
            bool temp = false;
            float r;
            optionValues = new float[4] { -1, -1, -1, -1 };
            num = new float[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.additionWithDecimal;

            for (int i = 0; i < 2; i++)
            {
                num[i] = UnityEngine.Random.Range(minRangeForEasy * 1f, maxRangeForEasy * 1f);
                num[i] = float.Parse(num[i].ToString("F1"));
                result += num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[i] + "+";
            }

            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression.Trim('+');

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1f, 51f);
                r = float.Parse(r.ToString("F1"));

                if (optionValues[optionIndex] != -1)
                {
                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {
                            print(r + "  " + (result - r));
                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            print(r + "  " + (result + r));

                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();




        }

        if (currentMode == Mode.Medium)
        {
            decimal[] num;
            float n;
            decimal result = 0;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            decimal r;
            float R;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[5];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.additionWithDecimal;

            for (int i = 0; i < 5; i++)
            {
                n = UnityEngine.Random.Range(minRangeForMedium * 1f, maxRangeForMedium * 1f);
                num[i] = decimal.Parse(n.ToString("F4"));
                result += num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[i] + "+";
            }

            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression.Trim('+');

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                R = UnityEngine.Random.Range(1f, 26f);
                r = decimal.Parse(R.ToString("F2"));

                if (optionValues[optionIndex] != -1)
                {
                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {
                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }

        if (currentMode == Mode.Hard)
        {
            decimal[] num;
            float n;
            decimal result = 0;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            decimal r;
            float R;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[8];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.additionWithDecimal;

            for (int i = 0; i < 8; i++)
            {
                n = UnityEngine.Random.Range(minRangeForHard * 1f, maxRangeForHard * 1f);
                num[i] = decimal.Parse(n.ToString("F6"));
                result += num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[i] + "+";
            }

            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression.Trim('+');

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                R = UnityEngine.Random.Range(1f, 8f);
                r = decimal.Parse(R.ToString("F5"));

                if (optionValues[optionIndex] != -1)
                {
                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {
                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }
        problemNumber++;
    }
    public void SubtractionWithDecimalProblemGenrator()
    {
        if (currentMode == Mode.Easy)
        {
            decimal[] num;

            decimal result = 0;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            bool subtractNow = true;
            decimal r;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.subtractionWithDecimal;

            num[0] = decimal.Parse(UnityEngine.Random.Range(minRangeForEasy * 1f, maxRangeForEasy * 1f).ToString("F2"));
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[0];
            result += num[0];
            for (int i = 1; i < 2; i++)
            {
                num[i] = decimal.Parse(UnityEngine.Random.Range(minRangeForEasy * 1f, maxRangeForEasy * 1f).ToString("F2"));
                if (subtractNow)
                {
                    if (num[i] < result)
                    {
                        subtractNow = !subtractNow;

                        result -= num[i];
                        questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + "-" + num[i];
                    }
                    else
                    {
                        i--;
                    }

                }
                else
                {
                    subtractNow = !subtractNow;
                    result += num[i];
                    questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + "+" + num[i];

                }



            }

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = decimal.Parse(UnityEngine.Random.Range(1f, 51f).ToString("F2"));

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();




        }

        if (currentMode == Mode.Medium)
        {
            decimal[] num;

            decimal result = 0;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            bool subtractNow = true;
            decimal r;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[5];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.subtractionWithDecimal;

            num[0] = decimal.Parse(UnityEngine.Random.Range(minRangeForMedium * 1f, maxRangeForMedium * 1f).ToString("F4"));
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[0];
            result += num[0];
            for (int i = 1; i < 5; i++)
            {
                num[i] = decimal.Parse(UnityEngine.Random.Range(minRangeForMedium * 1f, maxRangeForMedium * 1f).ToString("F4"));
                if (subtractNow)
                {
                    if (num[i] < result)
                    {
                        subtractNow = !subtractNow;

                        result -= num[i];
                        questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + "-" + num[i];
                    }
                    else
                    {
                        i--;
                    }

                }
                else
                {
                    subtractNow = !subtractNow;
                    result += num[i];
                    questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + "+" + num[i];

                }



            }

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = decimal.Parse(UnityEngine.Random.Range(1f, 26f).ToString("F4"));

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }

        if (currentMode == Mode.Hard)
        {
            decimal[] num;

            decimal result = 0;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            bool subtractNow = true;
            decimal r;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[9];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.subtractionWithDecimal;

            num[0] = decimal.Parse(UnityEngine.Random.Range(minRangeForHard * 1f, maxRangeForHard * 1f).ToString("F6"));
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[0];
            result += num[0];
            for (int i = 1; i < 9; i++)
            {
                num[i] = decimal.Parse(UnityEngine.Random.Range(minRangeForHard * 1f, maxRangeForHard * 1f).ToString("F6"));
                if (subtractNow)
                {
                    if (num[i] < result)
                    {
                        subtractNow = !subtractNow;

                        result -= num[i];
                        questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + "-" + num[i];
                    }
                    else
                    {
                        i--;
                    }

                }
                else
                {
                    subtractNow = !subtractNow;
                    result += num[i];
                    questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + "+" + num[i];

                }



            }

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = decimal.Parse(UnityEngine.Random.Range(1f, 8f).ToString("F6"));

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();



        }
        problemNumber++;
    }
    public void MultiplicationWithDecimalProblemGenrator()
    {
        if (currentMode == Mode.Easy)
        {
            decimal[] num;

            decimal result = 1;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            decimal r;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.multiplicationWithDecimal;

            for (int i = 0; i < 2; i++)
            {
                num[i] = decimal.Parse(UnityEngine.Random.Range(minRangeForEasy * 1f, maxRangeForEasy * 1f).ToString("F2"));
                result *= num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[i] + "*";
            }

            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression.Trim('*');

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = decimal.Parse(UnityEngine.Random.Range(1f, 51f).ToString("F2"));

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }

        if (currentMode == Mode.Medium)
        {
            //2 number
            // 0-100
            //
            decimal[] num;

            decimal result = 1;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            decimal r;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.multiplicationWithDecimal;

            for (int i = 0; i < 2; i++)
            {
                num[i] = decimal.Parse(UnityEngine.Random.Range(minRangeForMedium * 1f, maxRangeForMedium * 1f).ToString("F4"));
                result *= num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[i] + "*";
            }

            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression.Trim('*');

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = decimal.Parse(UnityEngine.Random.Range(1f, 26f).ToString("F4"));

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();



        }

        if (currentMode == Mode.Hard)
        {
            //2 number
            // 0-100
            //
            decimal[] num;

            decimal result = 1;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            decimal r;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.multiplicationWithDecimal;

            for (int i = 0; i < 2; i++)
            {
                num[i] = decimal.Parse(UnityEngine.Random.Range(minRangeForMedium * 1f, maxRangeForMedium * 1f).ToString("F6"));
                result *= num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression + num[i] + "*";
            }

            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression.Trim('*');

            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = decimal.Parse(UnityEngine.Random.Range(1f, 8f).ToString("F6"));

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {

                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && temp))
                {
                    if ((result + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + r)) == -1)
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();




        }
        problemNumber++;
    }
    public void DivisionWithDecimalProblemGenrator()
    {
        if (currentMode == Mode.Easy)
        {

            decimal[] num;

            decimal result = 1;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            decimal r;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.divisionWithDecimal;

            for (int i = 0; i < 2; i++)
            {
                num[i] = decimal.Parse(UnityEngine.Random.Range(minRangeForEasy * 1f, maxRangeForEasy * 1f).ToString("F2"));
                result *= num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = result + " / " + num[0];
            }



            optionValues[(int)UnityEngine.Random.Range(0, 4)] = num[1];

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = num[1].ToString();

            while (optionIndex < 4)
            {
                r = decimal.Parse(UnityEngine.Random.Range(1f, 51f).ToString("F2"));

                if (optionValues[optionIndex] != -1)
                {
                    optionIndex++;
                }

                else if ((num[1] - r > 0 && temp))
                {
                    if ((num[1] - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (num[1] - r)) == -1)
                        {

                            optionValues[optionIndex] = num[1] - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((num[1] + r > 0 && temp))
                {
                    if ((num[1] + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (num[1] + r)) == -1)
                        {
                            optionValues[optionIndex] = num[1] + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();



        }

        if (currentMode == Mode.Medium)
        {
            //2 number
            // 0-100
            //

            decimal[] num;

            decimal result = 1;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            decimal r;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.divisionWithDecimal;

            for (int i = 0; i < 2; i++)
            {
                num[i] = decimal.Parse(UnityEngine.Random.Range(minRangeForMedium * 1f, maxRangeForMedium * 1f).ToString("F4"));
                result *= num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = result + " / " + num[0];
            }



            optionValues[(int)UnityEngine.Random.Range(0, 4)] = num[1];

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = num[1].ToString();

            while (optionIndex < 4)
            {
                r = decimal.Parse(UnityEngine.Random.Range(1f, 26f).ToString("F4"));

                if (optionValues[optionIndex] != -1)
                {
                    optionIndex++;
                }

                else if ((num[1] - r > 0 && temp))
                {
                    if ((num[1] - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (num[1] - r)) == -1)
                        {

                            optionValues[optionIndex] = num[1] - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((num[1] + r > 0 && temp))
                {
                    if ((num[1] + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (num[1] + r)) == -1)
                        {
                            optionValues[optionIndex] = num[1] + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();




        }

        if (currentMode == Mode.Hard)
        {
            //2 number
            // 0-100
            //
            decimal[] num;

            decimal result = 1;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            decimal r;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.divisionWithDecimal;

            for (int i = 0; i < 2; i++)
            {
                num[i] = decimal.Parse(UnityEngine.Random.Range(minRangeForHard * 1f, maxRangeForHard * 1f).ToString("F6"));
                result *= num[i];
                questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = result + " / " + num[0];
            }



            optionValues[(int)UnityEngine.Random.Range(0, 4)] = num[1];

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = num[1].ToString();

            while (optionIndex < 4)
            {
                r = decimal.Parse(UnityEngine.Random.Range(1f, 8f).ToString("F6"));

                if (optionValues[optionIndex] != -1)
                {
                    optionIndex++;
                }

                else if ((num[1] - r > 0 && temp))
                {
                    if ((num[1] - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (num[1] - r)) == -1)
                        {

                            optionValues[optionIndex] = num[1] - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((num[1] + r > 0 && temp))
                {
                    if ((num[1] + r) != result)
                    {
                        if (Array.IndexOf(optionValues, (num[1] + r)) == -1)
                        {
                            optionValues[optionIndex] = num[1] + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();



        }


        problemNumber++;
    }
    public void PercentageWithDecimalProblemGenrator()
    {
        if (currentMode == Mode.Easy)
        {
            decimal[] num;

            decimal result = 1;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            float r;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.percentageWithDecimal;

            for (int i = 0; i < 2; i++)
            {
                num[i] = decimal.Parse(UnityEngine.Random.Range(minRangeForEasy * 1f, maxRangeForEasy * 1f).ToString("F2"));
            }
            result = (num[0] * num[1] * 1M) / 100;
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = num[0] + " % " + num[1];



            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1f, 51f);
                decimal f = decimal.Parse(r.ToString("F2"));

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }


                else if ((result - decimal.Parse(r.ToString("F2")) > 0 && temp))
                {
                    if ((result - decimal.Parse(r.ToString("F2"))) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - decimal.Parse(r.ToString("F2")))) == -1)
                        {

                            optionValues[optionIndex] = result - decimal.Parse(r.ToString("F2"));
                            optionIndex++;
                        }
                    }
                }
                else if ((result + decimal.Parse(r.ToString("F2")) > 0 && temp))
                {
                    if ((result + decimal.Parse(r.ToString("F2"))) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + decimal.Parse(r.ToString("F2")))) == -1)
                        {
                            optionValues[optionIndex] = result + decimal.Parse(r.ToString("F2"));
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();



        }

        if (currentMode == Mode.Medium)
        {
            decimal[] num;

            decimal result = 1;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            float r;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.percentageWithDecimal;

            for (int i = 0; i < 2; i++)
            {
                num[i] = decimal.Parse(UnityEngine.Random.Range(minRangeForMedium * 1f, maxRangeForMedium * 1f).ToString("F4"));
            }
            result = (num[0] * num[1] * 1M) / 100;
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = num[0] + " % " + num[1];



            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1f, 26f);
                decimal f = decimal.Parse(r.ToString("F4"));

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }


                else if ((result - decimal.Parse(r.ToString("F4")) > 0 && temp))
                {
                    if ((result - decimal.Parse(r.ToString("F4"))) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - decimal.Parse(r.ToString("F4")))) == -1)
                        {

                            optionValues[optionIndex] = result - decimal.Parse(r.ToString("F4"));
                            optionIndex++;
                        }
                    }
                }
                else if ((result + decimal.Parse(r.ToString("F4")) > 0 && temp))
                {
                    if ((result + decimal.Parse(r.ToString("F4"))) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + decimal.Parse(r.ToString("F4")))) == -1)
                        {
                            optionValues[optionIndex] = result + decimal.Parse(r.ToString("F4"));
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }

        if (currentMode == Mode.Hard)
        {
            decimal[] num;

            decimal result = 1;
            int optionIndex = 0;
            decimal[] optionValues;
            bool temp = false;
            float r;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            num = new decimal[2];

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.percentageWithDecimal;

            for (int i = 0; i < 2; i++)
            {
                num[i] = decimal.Parse(UnityEngine.Random.Range(minRangeForHard * 1f, maxRangeForHard * 1f).ToString("F2"));
            }
            result = (num[0] * num[1] * 1M) / 100;
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = num[0] + " % " + num[1];



            optionValues[(int)UnityEngine.Random.Range(0, 4)] = result;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            while (optionIndex < 4)
            {
                r = UnityEngine.Random.Range(1f, 8f);
                decimal f = decimal.Parse(r.ToString("F2"));

                if (optionValues[optionIndex] != -1)
                {

                    optionIndex++;
                }


                else if ((result - decimal.Parse(r.ToString("F2")) > 0 && temp))
                {
                    if ((result - decimal.Parse(r.ToString("F2"))) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - decimal.Parse(r.ToString("F2")))) == -1)
                        {

                            optionValues[optionIndex] = result - decimal.Parse(r.ToString("F2"));
                            optionIndex++;
                        }
                    }
                }
                else if ((result + decimal.Parse(r.ToString("F2")) > 0 && temp))
                {
                    if ((result + decimal.Parse(r.ToString("F2"))) != result)
                    {
                        if (Array.IndexOf(optionValues, (result + decimal.Parse(r.ToString("F2")))) == -1)
                        {
                            optionValues[optionIndex] = result + decimal.Parse(r.ToString("F2"));
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();



        }
        problemNumber++;
    }
    public void MixedOperationWithDecimalProblemGenrator()
    {

        if (currentMode == Mode.Easy)
        {
            decimal result = 0;
            bool resultIsGreaterThanZero = false;
            string expression = "";
            int optionIndex = 0;
            decimal[] optionValues;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            decimal r;
            bool temp = false;
            bool errorOccured = false;

            do
            {
                errorOccured = false;
                resultIsGreaterThanZero = false;
                try
                {

                    while (!resultIsGreaterThanZero)
                    {
                        expression = GenerateMixedExpression(3, true, 1, 50, "F2");
                        result = Convert.ToDecimal(new DataTable().Compute(expression, ""));
                        if (result >= 0)
                        {
                            resultIsGreaterThanZero = true;
                        }

                    }

                }
                catch
                {
                    errorOccured = true;
                    print("error ocuured but solved");
                    //problemNumber--;
                }


            } while (errorOccured);

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.mixedOperationWithDecimal;

            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = expression;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            optionValues[UnityEngine.Random.Range(0, 4)] = result;

            while (optionIndex < 4)
            {
                r = decimal.Parse(UnityEngine.Random.Range(1f, 101f).ToString("F2"));

                if (optionValues[optionIndex] != -1)
                {
                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {
                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && !temp))
                {
                    if ((result + r) != result)
                    {
                        if (!Array.Exists(optionValues, x => x == (result + r)))
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }

        if (currentMode == Mode.Medium)
        {
            //2 number
            // 0-100
            //
            decimal result = 0;
            bool resultIsGreaterThanZero = false;
            string expression = "";
            int optionIndex = 0;
            decimal[] optionValues;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            decimal r;
            bool temp = false;
            bool errorOccured = false;

            do
            {
                errorOccured = false;
                resultIsGreaterThanZero = false;
                try
                {

                    while (!resultIsGreaterThanZero)
                    {
                        expression = GenerateMixedExpression(5, true, 51, 150, "F3");
                        result = (decimal)Convert.ToDecimal(new DataTable().Compute(expression, ""));
                        if (result >= 0)
                        {
                            resultIsGreaterThanZero = true;
                        }

                    }
                }

                catch
                {
                    errorOccured = true;
                    print("error ocuured but solved");
                    //problemNumber--;
                }

            } while (errorOccured);

            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.mixedOperationWithDecimal;


            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = expression;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            optionValues[UnityEngine.Random.Range(0, 4)] = result;

            while (optionIndex < 4)
            {
                r = decimal.Parse(UnityEngine.Random.Range(1f, 100f).ToString("F3"));

                if (optionValues[optionIndex] != -1)
                {
                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {
                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && !temp))
                {
                    if ((result + r) != result)
                    {
                        if (!Array.Exists(optionValues, x => x == (result + r)))
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }

        if (currentMode == Mode.Hard)
        {
            //2 number
            // 0-100
            //
            decimal result = 0;
            bool resultIsGreaterThanZero = false;
            string expression = "";
            int optionIndex = 0;
            decimal[] optionValues;
            optionValues = new decimal[4] { -1, -1, -1, -1 };
            decimal r;
            bool temp = false;
            bool errorOccured = false;

            do
            {

                errorOccured = false;
                resultIsGreaterThanZero = false;
                try
                {
                    while (!resultIsGreaterThanZero)
                    {
                        expression = GenerateMixedExpression(7, true, 150, 570, "F4");
                        result = (decimal)Convert.ToDecimal(new DataTable().Compute(expression, ""));
                        if (result >= 0)
                        {
                            resultIsGreaterThanZero = true;
                        }

                    }
                }

                catch
                {
                    errorOccured = true;
                    print("error ocuured but solved");
                    //problemNumber--;
                }


            } while (errorOccured);
            questionAnswerOptionAndTimeDataHolder[problemNumber] = new QuestionAnswerOptionAndTimeDataHolder();
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionType = QuestionType.mixedOperationWithDecimal;


            questionAnswerOptionAndTimeDataHolder[problemNumber].problemNumber = problemNumber + 1;
            questionAnswerOptionAndTimeDataHolder[problemNumber].questionExpression = expression;
            questionAnswerOptionAndTimeDataHolder[problemNumber].correctAnswer = result.ToString();

            optionValues[UnityEngine.Random.Range(0, 4)] = result;

            while (optionIndex < 4)
            {
                r = decimal.Parse(UnityEngine.Random.Range(1f, 100f).ToString("F4"));

                if (optionValues[optionIndex] != -1)
                {
                    optionIndex++;
                }

                else if ((result - r > 0 && temp))
                {
                    if ((result - r) != result)
                    {
                        if (Array.IndexOf(optionValues, (result - r)) == -1)
                        {
                            optionValues[optionIndex] = result - r;
                            optionIndex++;
                        }
                    }
                }
                else if ((result + r > 0 && !temp))
                {
                    if ((result + r) != result)
                    {
                        if (!Array.Exists(optionValues, x => x == (result + r)))
                        {
                            optionValues[optionIndex] = result + r;
                            optionIndex++;
                        }
                    }
                }

                temp = !temp;


            }



            questionAnswerOptionAndTimeDataHolder[problemNumber].option1 = optionValues[0].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option2 = optionValues[1].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option3 = optionValues[2].ToString();
            questionAnswerOptionAndTimeDataHolder[problemNumber].option4 = optionValues[3].ToString();


        }

        problemNumber++;
    }





    [System.Serializable]
    public class QuestionAnswerOptionAndTimeDataHolder
    {
        public int problemNumber;
        public QuestionType questionType;
        public string questionExpression;
        public string correctAnswer;
        public string option1;
        public string option2;
        public string option3;
        public string option4;
        public bool isOption1Picked;
        public bool isOption2Picked;
        public bool isOption3Picked;
        public bool isOption4Picked;
        public float currentTime = 0;
    }

    public enum QuestionType
    {
        addition, subtraction, multiplication, division, percentage, mixedOperation,
        additionWithDecimal, subtractionWithDecimal, multiplicationWithDecimal, divisionWithDecimal, percentageWithDecimal, mixedOperationWithDecimal


    }

    public string GenerateMixedExpression(int totalNumber, bool isDecimal, int minRange, int maxRange, string s = "")
    {
        float[] num;
        decimal[] Dnum;
        num = new float[totalNumber];
        Dnum = new decimal[totalNumber];
        string expression = "";
        char[] matheOperator = new char[] { '*', '+', '-', '/' };
        if (!isDecimal)
        {
            for (int i = 0; i < totalNumber; i++)
            {
                num[i] = UnityEngine.Random.Range(minRange, maxRange);

            }

        }
        else
        {
            for (int i = 0; i < totalNumber; i++)
            {
                Dnum[i] = decimal.Parse(UnityEngine.Random.Range(minRange * 1f, maxRange * 1f).ToString(s));

            }

        }
        if (!isDecimal)
            expression = expression + num[0];
        else
            expression = expression + Dnum[0];

        for (int i = 1; i < totalNumber; i++)
        {
            if (!isDecimal)
                expression = expression + matheOperator[UnityEngine.Random.Range(0, 4)] + num[i];
            else
                expression = expression + matheOperator[UnityEngine.Random.Range(0, 4)] + Dnum[i];

        }


        return expression;

    }

    public void OnDisable()
    {
        SoundManager.Instance.PlayMusic(defaultMusic);
    }

}
