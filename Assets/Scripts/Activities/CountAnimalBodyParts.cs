using Lean.Gui;
using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountAnimalBodyParts : MonoBehaviour
{
    ActivityList1 activityList1;
    ActivityList2 activityList2;
    int problemNumber;
    int temp;
    string question;
    CountBodyParts countBodyParts;
    public ValueCalculator valueCalculator;

    public Image animalImage;
    public Activity activityScriptInstance;
    public Button NotificationBtn;
    public GameObject timer;
    public TextMeshProUGUI sideNoteText;
    public LeanToggle congratulationLean;
    public TextMeshProUGUI congratulationText;
    public LeanTransformLocalPositionX sideNoteMoveSideAnimation;
    public LeanToggle sideNoteLean;
    public Reset reset;
    public PositionRodsOfAbacus positionRodsOfAbacus;

    public LeanImageFillAmount loadingBar;



    private void OnEnable()
    {
        problemNumber = 0;
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].countBodyParts.active == true && ClassManager.currentActivityIndex == j)
                    {
                        activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
                        activityList2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j];

                        countBodyParts = activityScriptInstance.classActivityList[i].classData.activityList[j].countBodyParts;
                        animalImage.sprite = countBodyParts.animalImage;
                        for (int k = 0; k < activityList1.countBodyParts1.completed.Length; k++)
                        {
                            if (activityList1.countBodyParts1.completed[k] == true)
                            {
                                problemNumber++;
                            }
                        }
                        if (problemNumber == activityList1.countBodyParts1.completed.Length)
                        {
                            for (int k = 0; k < activityList1.countBodyParts1.completed.Length; k++)
                            {
                                activityList1.countBodyParts1.completed[k] = false;
                            }
                            problemNumber = 0;
                        }

                    }
                }
            }
        }
        MoveBeeds_1.dosomthing += compareAbacusValueWithAnswer;
        MoveBeeds_2.dosomthing += compareAbacusValueWithAnswer;
        NotificationBtn.onClick.AddListener(StartTimer);

      //  question = "<B><#0080ff><size=60>" + countBodyParts.countOfAnimals[CurrentAnimalCountAndBodyPart().Item1] + "</color></B></size>" + " " + countBodyParts.animalName + " " + "will have how many " + "<B><#0080ff><size=55>" + countBodyParts.bodyPartAndCountOfOne[CurrentAnimalCountAndBodyPart().Item2].bodyPart + "</color></B></size>" + "?";

        question = "How many " + "<B><#0080ff><size=55>" + countBodyParts.bodyPartAndCountOfOne[CurrentAnimalCountAndBodyPart().Item2].bodyPart + "</color></B></size>" + " does " + "<B><#0080ff><size=60>" + countBodyParts.countOfAnimals[CurrentAnimalCountAndBodyPart().Item1] + "</color></B></size> " + countBodyParts.animalName + " have?";



        sideNoteText.text = question;

        loadingBar.Data.FillAmount = (problemNumber / (activityList1.countBodyParts1.completed.Length * 1f));
        loadingBar.BeginAllTransitions();
    }

    private void Update()
    {
        //  print(activityList2.countBodyParts2.bestTime + "-----  " + activityList2.countBodyParts2.bestTime_string + "----" + activityList2.countBodyParts2.currentSavedTime);
        if (timer.gameObject.activeInHierarchy)
        {
            activityList2.countBodyParts2.currentSavedTime = Timer.currentTime;
        }
    }





    private void OnDisable()
    {
        int temp1 = positionRodsOfAbacus.startingRod;
        int temp2 = positionRodsOfAbacus.endingRod;
        positionRodsOfAbacus.startingRod = 0;
        positionRodsOfAbacus.endingRod = 8;
        valueCalculator.decimalPlaceString = "F2";
        valueCalculator.numberOfDecimalPlaces = 2;
        positionRodsOfAbacus.EditRod();
        positionRodsOfAbacus.startingRod = temp1;
        positionRodsOfAbacus.endingRod = temp2;

        MoveBeeds_1.dosomthing -= compareAbacusValueWithAnswer;
        MoveBeeds_2.dosomthing -= compareAbacusValueWithAnswer;
        // activityList2.countBodyParts2.currentSavedTime = Timer.currentTime;
        NotificationBtn.onClick.RemoveListener(StartTimer);
        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);


    }


    public void StartTimer()
    {
        Timer.savedTime = activityList2.countBodyParts2.currentSavedTime;
        timer.SetActive(true);
    }


    public (int, int) CurrentAnimalCountAndBodyPart()
    {
        bool brk = false;
        temp = 0;
        int animalCount, bodyPartCount = 0;



        for (animalCount = 0; animalCount < countBodyParts.countOfAnimals.Count; animalCount++)
        {
            for (bodyPartCount = 0; bodyPartCount < countBodyParts.bodyPartAndCountOfOne.Count; bodyPartCount++)
            {
                if (problemNumber == temp)
                {
                    print(problemNumber + "problemNumber");
                    brk = true;
                    break;
                }
                temp++;
            }

            if (brk == true)
            {
                break;
            }
        }

        print(animalCount + "   " + bodyPartCount);
        return (animalCount, bodyPartCount);
    }




    public void compareAbacusValueWithAnswer()
    {
        if (ValueCalculator.value == (countBodyParts.countOfAnimals[CurrentAnimalCountAndBodyPart().Item1] * countBodyParts.bodyPartAndCountOfOne[CurrentAnimalCountAndBodyPart().Item2].count * 1f))
        {
            activityList1.countBodyParts1.completed[problemNumber] = true;
            SaveManager.Instance.saveDataToDisk(Activity.classParent);



            problemNumber++;
            loadingBar.Data.FillAmount = (problemNumber / (activityList1.countBodyParts1.completed.Length * 1f));
            loadingBar.BeginAllTransitions();
            if ((problemNumber) == (countBodyParts.countOfAnimals.Count * countBodyParts.bodyPartAndCountOfOne.Count))
            {
                if (activityList2.countBodyParts2.bestTime == -1)
                {
                    timer.SetActive(false);

                    activityList2.countBodyParts2.currentSavedTime = 0;
                    activityList2.countBodyParts2.bestTime_string = Timer.timerText.text;
                    activityList2.countBodyParts2.bestTime = Timer.currentTime;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }
                else if (activityList2.countBodyParts2.bestTime > Timer.currentTime)
                {
                    timer.SetActive(false);

                    activityList2.countBodyParts2.currentSavedTime = 0;
                    activityList2.countBodyParts2.bestTime_string = Timer.timerText.text;
                    activityList2.countBodyParts2.bestTime = Timer.currentTime;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }
                else
                {
                    timer.SetActive(false);
                    activityList2.countBodyParts2.currentSavedTime = 0;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);

                }
                Invoke("DelayedShowCongratulation", 0.5f);
            }
            else
            {
                Invoke("DelayedSideNoteAnimation", 0.5f);
                Invoke("DelayedChangeQuestion", 1f);
            }

            Invoke("DelayedResetAbacus", 0.5f);
            Invoke("DelayedNotice", 0.5f);
        }

    }



    public void DelayedResetAbacus()
    {
        reset.RESET();
    }

    public void DelayedChangeQuestion()
    {
      //  question = "<B><#0080ff><size=60>" + countBodyParts.countOfAnimals[CurrentAnimalCountAndBodyPart().Item1] + "</color></B></size>" + " " + countBodyParts.animalName + " " + "will have how many " + "<B><#0080ff><size=50>" + countBodyParts.bodyPartAndCountOfOne[CurrentAnimalCountAndBodyPart().Item2].bodyPart + "</color></B></size>" + "?";
        question = "How many " + "<B><#0080ff><size=55>" + countBodyParts.bodyPartAndCountOfOne[CurrentAnimalCountAndBodyPart().Item2].bodyPart + "</color></B></size>" + " dose " + "<B><#0080ff><size=60>" + countBodyParts.countOfAnimals[CurrentAnimalCountAndBodyPart().Item1] + "</color></B></size> " + countBodyParts.animalName + " have?";

        sideNoteText.text = question;
    }

    public void DelayedNotice()
    {
        print("Thats correct");
    }

    public void DelayedShowCongratulation()
    {
        sideNoteLean.TurnOff();
        congratulationText.text = "Congratulation";
        congratulationLean.TurnOn();
    }

    public void DelayedSideNoteAnimation()
    {
        sideNoteMoveSideAnimation.BeginAllTransitions();
    }





}
