using Lean.Gui;
using Lean.Transition.Method;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchImageAbacusValue : MonoBehaviour
{
    public GameObject congratulationCanvas;
    public TextMeshProUGUI notificationText;
    public TextMeshProUGUI congratulationText;
    public GameObject sideNoteCanvas;
    public Reset reset;
    public Sprite[] img;
    public int[] value;
    public int currentOperation;
    public Activity activityScriptInstance;
    public int totalOperations;
    private bool temp;
    public GameObject canvasSpriteParent;
    public Transform imagePrefab;
    MatchValueWithImage[] matchValueWithImage;
    List<MatchValueWithImage1> matchValueWithImage1;
    Coroutine delay;
    public LeanImageFillAmount loadingBar;
    public LeanToggle leanCongratulation;
    public LeanToggle leanSideNote;
    MatchValueWithImage2 matchValueWithImage2;
    public Button notificatinBtn;
    public GameObject timer;
   // bool activityIsCompletedByTheUser;



    private void OnEnable()
    {
     //   activityIsCompletedByTheUser = false;
        temp = true;
        currentOperation = 0;
        notificationText.text = "Count the items and represent the value on abacus";

        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {

                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].matchValueWithImage.Length > 0 && ClassManager.currentActivityIndex==j)
                    {
                        matchValueWithImage2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchValueWithImage[0];

                        totalOperations = activityScriptInstance.classActivityList[i].classData.activityList[j].matchValueWithImage.Length;
                        img = new Sprite[totalOperations];
                        value = new int[totalOperations];
                        matchValueWithImage = new MatchValueWithImage[totalOperations];

                        matchValueWithImage = activityScriptInstance.classActivityList[i].classData.activityList[j].matchValueWithImage;
                        matchValueWithImage1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage;
                        for (int z = 0; z < totalOperations; z++)
                        {
                            img[z] = activityScriptInstance.classActivityList[i].classData.activityList[j].matchValueWithImage[z].img;
                            value[z] = activityScriptInstance.classActivityList[i].classData.activityList[j].matchValueWithImage[z].val;

                            if (matchValueWithImage1[z].completed)
                            {
                                currentOperation++;
                              //  activityIsCompletedByTheUser = true;
                            }

                            if (currentOperation == activityScriptInstance.classActivityList[i].classData.activityList[j].matchValueWithImage.Length)
                            {
                                currentOperation = 0;
                                for (int r = 0; r < activityScriptInstance.classActivityList[i].classData.activityList[j].matchValueWithImage.Length; r++)
                                {
                                    matchValueWithImage1[r].completed = false;
                                }
                            }
                        }
                    }
                }
            }

        }
        loadingBar.Data.FillAmount = (currentOperation / (totalOperations * 1f));
        loadingBar.BeginAllTransitions();

        notificatinBtn.onClick.AddListener(StartTimer);
        // congratulationBtn.onClick.AddListener

    }

    public void StartTimer()
    {
        Timer.savedTime = matchValueWithImage2.currentSavedTime ;
        timer.SetActive(true);
    }


    private void Update()
    {

        if (timer.gameObject.activeInHierarchy)
        {
            matchValueWithImage2.currentSavedTime = Timer.currentTime;
           
        }


        if (currentOperation != totalOperations)
        {
            if (temp == true)
            {
                CreateNewOperation();
            }

            if (ValueCalculator.value == matchValueWithImage[currentOperation].val)
            {
                if (delay == null)
                {
                    delay = StartCoroutine(Delay());
                    loadingBar.Data.FillAmount = ((currentOperation + 1) / (totalOperations * 1f));
                    loadingBar.BeginAllTransitions();
                }
                #region Commented Code
                //reset.RESET();
                //matchValueWithImage[currentOperation].completed = true;
                //currentOperation++;
                //temp = true;
                //for (int j = 0; j < canvasSpriteParent.transform.childCount; j++)
                //{
                //    Destroy(canvasSpriteParent.transform.GetChild(j).gameObject);
                //}
                #endregion

            }
        }

        if (currentOperation == totalOperations)
        {
            congratulationText.text = "Congratulation ,you have completed this activity";
            leanCongratulation.TurnOn();//congratulationCanvas.SetActive(true);
            leanSideNote.TurnOff();//sideNoteCanvas.SetActive(false);

            // activityIsCompletedByTheUser = true;
            matchValueWithImage2.currentSavedTime = 0;
            if (matchValueWithImage2.bestTime == -1)
            {
                matchValueWithImage2.bestTime = Timer.currentTime;
                matchValueWithImage2.bestTime_string = Timer.timerText.text;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }
            else if (matchValueWithImage2.bestTime > Timer.currentTime)
            {
                print(5555);
                matchValueWithImage2.bestTime = Timer.currentTime;
                matchValueWithImage2.bestTime_string = Timer.timerText.text;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }

            timer.SetActive(false);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.7f);
        reset.RESET();
        // matchValueWithImage[currentOperation].completed = true;
        matchValueWithImage1[currentOperation].completed = true;
        SaveManager.Instance.saveDataToDisk(Activity.classParent);



        currentOperation++;
        temp = true;
        for (int j = 0; j < canvasSpriteParent.transform.childCount; j++)
        {
            Destroy(canvasSpriteParent.transform.GetChild(j).gameObject);
        }
        delay = null;
    }

    private void CreateNewOperation()
    {
        temp = false;
        for (int i = 0; i < matchValueWithImage[currentOperation].val; i++)
        {
            Transform ImagePrefab = Instantiate(imagePrefab, new Vector3(10 * 2.0F, 0, 0), Quaternion.identity);
            ImagePrefab.SetParent(canvasSpriteParent.transform, false);
            var img = ImagePrefab.GetComponent<Image>();
            img.sprite = matchValueWithImage[currentOperation].img;
            img.preserveAspect = true;
        }
    }

    private void OnDisable()
    {
        timer.SetActive(false);
        notificatinBtn.onClick.RemoveListener(StartTimer);
        /*
        if (activityIsCompletedByTheUser)
        {
            for (int i = 0; i < matchValueWithImage1.Count; i++)
            {
                matchValueWithImage1[i].completed = true;
            }
        }
        else
        {
            for (int i = 0; i < matchValueWithImage1.Count; i++)
            {
                matchValueWithImage1[i].completed = false;
            }
        }
        SaveManager.Instance.saveDataToDisk(Activity.classParent);
        */
        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);

        try
        {
            if (canvasSpriteParent.transform.childCount > 0)
            {
                for (int i = 0; i < canvasSpriteParent.transform.childCount; i++)
                {
                    Destroy(canvasSpriteParent.transform.GetChild(i).gameObject);
                }
            }

        }
        catch {; }


    }
}

