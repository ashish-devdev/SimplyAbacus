using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DailyWorkOutRandomQuestionGeneratorAndAnswerValidator;

public class SaveDailyWorkoutStatsInformation : MonoBehaviour
{
    public DailyWorkOutRandomQuestionGeneratorAndAnswerValidator dailyWorkOutRandomQuestionGeneratorAndAnswerValidator;
    public QuestionTypeSelectionGenralScript questionTypeSelectionGenralScript;
    int totalQuestion;
    int temp = 0;
    ClassParentDailyWorkoutStats classParentDailyWorkoutStats;
    

    private void OnEnable()
    {
        if (File.Exists(SaveManager.Instance.dailyWorkoutStatsPath))
        {
            classParentDailyWorkoutStats = SaveManager.Instance.loadDailyWorkoutStatsDataFromDisk();


            switch (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.currentMode)
            {
                case Mode.Easy:
                    temp = 0;
                    break;
                case Mode.Medium:
                    temp = 12;
                    break;
                case Mode.Hard:
                    temp = 24;
                    break;
            }
            totalQuestion = dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.numberofQuestion;

            for (int i = 0; i < totalQuestion; i++)
            {
                for (int j = 0; j < questionTypeSelectionGenralScript.questionTypes.Count; j++)
                {
                    if (ReturnQuestionType(j)== dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].questionType)
                    {

                        classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[j+temp].QuestionType= ReturnQuestionType(j).ToString();

                        classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[j+temp].totalQuestions += 1;


                        if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].isOption1Picked)
                        {
                            if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option1 == dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].correctAnswer)
                            {
                                classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[j + temp].totalCorrectAnswers += 1;
                            }

                        }
                        else if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].isOption2Picked)
                        {
                            if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option2 == dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].correctAnswer)
                            {
                                classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[j + temp].totalCorrectAnswers += 1;
                            }

                        }
                        else if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].isOption3Picked)
                        {
                            if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option3 == dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].correctAnswer)
                            {
                                classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[j + temp].totalCorrectAnswers += 1;
                            }

                        }
                        else if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].isOption4Picked)
                        {
                            if (dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].option4 == dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].correctAnswer)
                            {
                                classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[j + temp].totalCorrectAnswers += 1;
                            }

                        }


                        classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[j + temp].totalTime += dailyWorkOutRandomQuestionGeneratorAndAnswerValidator.questionAnswerOptionAndTimeDataHolder[i].currentTime;

                        break;

                    }

                }

            }

            SaveManager.Instance.SaveDailyWorkoutStatsToDisk(classParentDailyWorkoutStats);
        }
    }




    public QuestionType ReturnQuestionType(int i)
    {

        switch (i)
        {

            case 0:
                return QuestionType.addition;
            case 1:
                return QuestionType.subtraction;
            case 2:
                return QuestionType.multiplication;

            case 3:
                return QuestionType.division;

            case 4:
                return QuestionType.percentage;

            case 5:
                return QuestionType.mixedOperation;

            case 6:
                return QuestionType.additionWithDecimal;

            case 7:
                return QuestionType.subtractionWithDecimal;

            case 8:
                return QuestionType.multiplicationWithDecimal;

            case 9:
                return QuestionType.divisionWithDecimal;

            case 10:
                return QuestionType.percentageWithDecimal;

            case 11:
                return QuestionType.mixedOperationWithDecimal;



            default: return QuestionType.addition; 

        }

    }


}
