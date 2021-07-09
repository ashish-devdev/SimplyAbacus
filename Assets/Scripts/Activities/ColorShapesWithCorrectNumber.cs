using Lean.Gui;
using Lean.Transition.Method;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorShapesWithCorrectNumber : MonoBehaviour
{
    public GameObject congratulationCanvas;
    public GameObject sideNoteCanvas;
    public GameObject poemNote;
    public Button notificationBtn;
    public TextMeshProUGUI congratulationText;
    public TextMeshProUGUI notiicationText;
    public Activity activityScriptInstance;
    public GameObject imageHolder;
    Sprite shapeImage;
    int correctValue;
    int numberOfCorrectValueInList;
    List<int> val;
    MatchShapeWithNumbers matchShapeWithNumbersRefrence;
    MatchShapeWithNumbers1 matchShapeWithNumbersRefrence1;
    Ray ray;
    Camera cam;
    string shapeText;
    int completedSubTask = 0;
    public LeanImageFillAmount loadingBar;
    public LeanToggle openPoemNoteLean;
    public LeanToggle CongratulationLean;
    public LeanToggle sideNoteLean;
    LeanShake leanShake;
    MatchShapeWithNumbers2 matchShapeWithNumbers2;
    public GameObject timer;
    bool temp;

    public AudioClip correctColorSound;
    public AudioClip wrongColorSound;


    private void OnEnable()
    {
        temp = true;
        int temp1 = 0;
        completedSubTask = 0;
        cam = Camera.main;
        notiicationText.text = "Click the shape with correct number as instructed.";
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length > 0 && ClassManager.currentActivityIndex == j)
                    {

                        for (int k = 0; k < activityScriptInstance.classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length; k++)
                        {
                            matchShapeWithNumbers2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchShapeWithNumbers[k];
                            shapeImage = activityScriptInstance.classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].img;
                            correctValue = activityScriptInstance.classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].correctValue;
                            numberOfCorrectValueInList = 0;
                            val = new List<int>();
                            matchShapeWithNumbersRefrence = activityScriptInstance.classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k];
                            matchShapeWithNumbersRefrence1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k];

                            for (int l = 0; l < activityScriptInstance.classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].val.Count; l++)
                            {
                                val.Add(activityScriptInstance.classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].val[l]);
                                imageHolder.transform.GetChild(l).GetComponent<Image>().sprite = shapeImage;
                                imageHolder.transform.GetChild(l).GetChild(0).GetComponent<TextMeshProUGUI>().text = activityScriptInstance.classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].val[l].ToString();
                                imageHolder.transform.GetChild(l).GetComponent<Button>().interactable = true;

                                if (correctValue == activityScriptInstance.classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].val[l])
                                {
                                    numberOfCorrectValueInList++;
                                    for (int r = 0; r < activityScriptInstance.classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length; r++)
                                    {
                                        if (matchShapeWithNumbersRefrence.completed[r] == true)
                                        {
                                            //imageHolder.transform.GetChild(l).GetComponent<Image>().color = matchShapeWithNumbersRefrence.color;
                                            matchShapeWithNumbersRefrence.completed[r] = false;

                                        }

                                    }
                                }
                                int z = l;
                                if (imageHolder.transform.GetChild(z).GetChild(0).GetComponent<TextMeshProUGUI>().text == correctValue.ToString())
                                {
                                    int a = temp1;
                                    imageHolder.transform.GetChild(z).GetComponent<Button>().onClick.AddListener(() =>
                                    {
                                        shapeText = imageHolder.transform.GetChild(z).GetChild(0).GetComponent<TextMeshProUGUI>().text;
                                        if (shapeText == correctValue.ToString())
                                        {

                                            matchShapeWithNumbersRefrence1.completed[a] = true;
                                            matchShapeWithNumbersRefrence.completed[a] = true;
                                            SoundManager.Instance.Play(correctColorSound);
                                            completedSubTask++;
                                            loadingBar.Data.FillAmount = (completedSubTask / (3 * 1f));
                                            loadingBar.BeginAllTransitions();
                                            imageHolder.transform.GetChild(z).GetComponent<Button>().interactable = false;
                                            imageHolder.transform.GetChild(z).GetComponent<Image>().color = matchShapeWithNumbersRefrence.color;
                                        }


                                    });
                                    temp1++;

                                }
                                else
                                {
                                    imageHolder.transform.GetChild(z).GetComponent<Button>().onClick.AddListener(() =>
                                    {
                                        SoundManager.Instance.Play(wrongColorSound);
                                        leanShake = imageHolder.transform.GetChild(z).GetComponent<LeanShake>();
                                        leanShake.Strength = 1.5f;
                                    });

                                }

                            }

                        }
                        imageHolder.SetActive(true);
                    }
                }

            }
        }
        loadingBar.Data.FillAmount = (completedSubTask / (3 * 1f));
        loadingBar.BeginAllTransitions();

        notificationBtn.onClick.AddListener(openPoemNote);
        notificationBtn.onClick.AddListener(StartTimer);
    }

    private void OnDisable()
    {

        try
        {
            notificationBtn.onClick.RemoveListener(openPoemNote);
            notificationBtn.onClick.RemoveListener(StartTimer);
            imageHolder.SetActive(false);
            timer.SetActive(false);
            if (matchShapeWithNumbersRefrence != null)
            {

                for (int i = 0; i < imageHolder.transform.childCount; i++)
                {
                    imageHolder.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                    imageHolder.transform.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();


                }


            }
            if (completedSubTask != 3)
            {
                if (matchShapeWithNumbers2.bestTime == -1)
                {
                    for (int i = 0; i < matchShapeWithNumbersRefrence.val.Count; i++)
                    {
                        matchShapeWithNumbersRefrence1.completed[i] = false;
                        matchShapeWithNumbersRefrence.completed[i] = false;
                    }

                }


            }
            SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);

        }
        catch {; }
    }



    private void Update()
    {
        if (completedSubTask == numberOfCorrectValueInList)
        {
            if (temp)
            {
                congratulationText.text = "congratulation";
                CongratulationLean.TurnOn();//congratulationCanvas.SetActive(true);
                sideNoteLean.TurnOff();//sideNoteCanvas.SetActive(false);
                openPoemNoteLean.TurnOff();//poemNote.SetActive(false);
                SaveManager.Instance.saveDataToDisk(Activity.classParent);
                matchShapeWithNumbers2.currentSavedTime = 0;
                if (matchShapeWithNumbers2.bestTime == -1)
                {
                    matchShapeWithNumbers2.bestTime = Timer.currentTime;
                    matchShapeWithNumbers2.bestTime_string = Timer.timerText.text;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }
                else if (matchShapeWithNumbers2.bestTime > Timer.currentTime)
                {
                    matchShapeWithNumbers2.bestTime = Timer.currentTime;
                    matchShapeWithNumbers2.bestTime_string = Timer.timerText.text;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }
                // matchShapeWithNumbersRefrence.completed = true;
                // matchShapeWithNumbersRefrence1.completed = true;
                temp = false;
                timer.SetActive(false);
            }
        }
        else
        {
            if (timer.activeInHierarchy)
                matchShapeWithNumbers2.currentSavedTime = Timer.currentTime;
        }

    }


    public void openPoemNote()
    {
        openPoemNoteLean.TurnOn();// poemNote.SetActive(true);
    }

    public void StartTimer()
    {
        Timer.savedTime = 0;
        timer.SetActive(true);
    }

}
