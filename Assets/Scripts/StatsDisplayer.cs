using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplayer : MonoBehaviour
{


    bool classCompleted = false;

    public static string selectedBookName;
    public static string selectedClassName;
    public static string selectedActivityName;
    public TextMeshProUGUI userName;
    private ClassParentStats classParentsStats;
    private ClassParent classParent;


    public BookManager bookManager;
    public Activity activityScriptInstance;

    public List<string> bookNames;
    public List<string> className;

    public List<TextMeshProUGUI> bookBestTime;

    public List<StatsHolder> stats;
    public List<Button> bookBtn;

    public Button nextBtn;
    public GameObject nextBtnGlowImage;
    public Button previousBtn;
    public GameObject previousBtnGlowImage;
    public Button shareBtn;

    public GameObject activityStatsCells;
    public GameObject ParentContainerOfCells;

    public RectTransform scrollRect;

    public TextMeshProUGUI bookNameText;
    public TextMeshProUGUI classNameText;
    public TextMeshProUGUI classBestTimeText;


    float classBestTime;
    TimeSpan ConvertedTime;

    public int temp;
    public List<string> classNameHeading;

    public void OnEnable()
    {


        bookNames = new List<string>();
        className = new List<string>();

        for (int i = 0; i < bookManager.books.Count; i++)
        {
            bookNames.Add(bookManager.books[i].bookName);

        }

        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            className.Add(activityScriptInstance.classActivityList[i].classData.nameOfClass);
        }

        InstantiateActivityStatsCells(15);

        for (int i = 0; i < bookBtn.Count; i++)
        {
            int k = i;
            bookBtn[k].onClick.AddListener(delegate { ChangeSelectedBookName(k); temp = 0; LoadAndDisplayStats(); ChangeClassNameText(); CalculateClassBestTimieng(); });
        }

        nextBtn.onClick.AddListener(() => { ++temp; LoadAndDisplayStats(); ChangeClassNameText(); CalculateClassBestTimieng(); });
        previousBtn.onClick.AddListener(() => { --temp; LoadAndDisplayStats(); ChangeClassNameText(); CalculateClassBestTimieng(); });
        shareBtn.onClick.AddListener(Share);
    }



    public void InstantiateActivityStatsCells(int i)
    {
        for (int k = 0; k < i; k++)
            Instantiate(activityStatsCells, ParentContainerOfCells.transform);
    }

    public void ChangeSelectedBookName(int k)
    {
        selectedBookName = bookNames[k];
    }

    public void ChangeClassNameText()
    {
        classNameText.text = classNameHeading[temp];
    }
    private void OnDisable()
    {
        for (int i = 0; i < bookBtn.Count; i++)
        {
            int k = i;
            bookBtn[k].onClick.RemoveListener(delegate { ChangeSelectedBookName(k); });
        }
        nextBtn.onClick.RemoveAllListeners();
        previousBtn.onClick.RemoveAllListeners();
        shareBtn.onClick.RemoveListener(Share);

    }



    public void CalculateClassBestTimieng()
    {
        ConvertedTime = TimeSpan.FromSeconds(classBestTime);
        if (classBestTime >= 60 * 60 * 24)
            classBestTimeText.text = "Time: " + ConvertedTime.ToString(@"dd\:hh\:mm\:ss");
        else
            classBestTimeText.text = "Time: " + ConvertedTime.ToString(@"hh\:mm\:ss");

    }


    [System.Serializable]
    public class StatsHolder
    {
        public string className;
        public string activityName;
        public float bestTime;
        public string bestTime_inString;
        public float currentTime;
    }

    public void LoadBookStats()
    {
        TimeSpan spanedTime;
        string timeInString;
        string[] s;
        if (File.Exists(SaveManager.Instance.savePath))
        {
            scrollRect.anchoredPosition = new Vector2(scrollRect.anchoredPosition.x, 0);


            classParent = SaveManager.Instance.loadDataFromDisk();
            classParentsStats = SaveManager.Instance.loadStatsDataFromDisk();


            for (int t = 0; t < bookManager.books.Count; t++)
            {
                classBestTime = 0;
                classCompleted = true;
                for (int j = bookManager.books[t].statingClass; j <= bookManager.books[t].endingClass; j++)
                {
                    for (int k = 0; k < activityScriptInstance.classActivityList[j].classData.activityList.Count; k++)
                    {

                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].matchValueWithImage.Length > 0)
                        {
                            //    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].matchValueWithImage[0].bestTime_string;
                           

                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].matchValueWithImage[0].bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].matchValueWithImage[0].bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }

                        }
                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].matchShapeWithNumbers.Length > 0)
                        {
                            //ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].matchShapeWithNumbers[0].bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].matchShapeWithNumbers[0].bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].matchShapeWithNumbers[0].bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }
                            
                        }

                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].abacusOperations.active==true)
                        {
                            //  ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].abacusOperations.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].abacusOperations.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].abacusOperations.bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }
                           
                        }


                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].speedWriting == true)
                        {
                            //   ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].speedWriting.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].speedWriting.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].speedWriting.bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }
                            

                        }

                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].liftBeed01 == true)
                        {
                            //  ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].liftingBeed21.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].liftingBeed21.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].liftingBeed21.bestTime;
                            }
                            else
                            {
                                //classCompleted = false;
                                //break;
                            }
                           

                        }

                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].liftBeed02 == true)
                        {
                            //   ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].LiftingBeed22.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].LiftingBeed22.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].LiftingBeed22.bestTime;
                            }
                            else
                            {
                                //classCompleted = false;
                                //break;
                            }
                          

                        }

                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].maze.active == true)
                        {
                            //  ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].maze2.bestTime_string;

                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].maze2.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].maze2.bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }
                          

                        }
                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].visualHands.active == true)
                        {
                            //   ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].visualHands2.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].visualHands2.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].visualHands2.bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }

                          

                        }
                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].countBodyParts.active == true)
                        {
                            //    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].countBodyParts2.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].countBodyParts2.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].countBodyParts2.bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }
                           

                        }

                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].mixedMathematicalOperations.active == true)
                        {
                            //   ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].mixedMathematicalOperations2.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].mixedMathematicalOperations2.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].mixedMathematicalOperations2.bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }
                           

                        }
                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].coloringPageImages.Length > 0)
                        {
                            //   ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].coloringPageImages.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].coloringPageImages.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].coloringPageImages.bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }
                           

                        }
                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].multiplicationDivisionPuzzle.active == true)
                        {
                            //   ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationDivisionPuzzle2.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationDivisionPuzzle2.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationDivisionPuzzle2.bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }
                        }

                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].mutliplicationOperation.active == true)
                        {
                            //   ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationOperation2.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationOperation2.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationOperation2.bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }
                        }

                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].liftBeeds.active == true)
                        {
                            //   ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationOperation2.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].liftBeeds2.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].liftBeeds2.bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }
                        }

                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].divisionOperation.active == true)
                        {
                            //   ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationOperation2.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].divisionOperation2.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].divisionOperation2.bestTime;
                            }
                            else
                            {
                                classCompleted = false;
                                break;
                            }
                        }

                        if (activityScriptInstance.classActivityList[j].classData.activityList[k].tutorialVideo.active == true)
                        {
                            //  ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].liftingBeed21.bestTime_string;
                            if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].tutorialVideo2.bestTime >= 0)
                            {
                                classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].tutorialVideo2.bestTime;
                            }
                            else
                            {
                                //classCompleted = false;
                                //break;
                            }


                        }

                    }

                    if (classCompleted == false)
                    {
                        break;
                    }
                }
                if (classCompleted == false)
                    bookBestTime[t].text = "Not Completed";
                else
                {

                    spanedTime = TimeSpan.FromSeconds(classBestTime);
                    timeInString = spanedTime.ToString(@"dd\:hh\:mm\:ss");
                    s=timeInString.Split(':');
                    timeInString = "";
                    if (classBestTime >= 60 * 60 * 24)
                        timeInString = s[0] + "d" + ":" + s[1] + "h" + ":" + s[2] + "m";
                    else
                        timeInString = s[1] + "h" + ":" + s[2] + "m" +":"+s[3]+"s";

                    bookBestTime[t].text = timeInString;
                }
            }



        }

    }





    public void LoadAndDisplayStats()
    {

        if (File.Exists(SaveManager.Instance.savePath))
        {
            scrollRect.anchoredPosition = new Vector2(scrollRect.anchoredPosition.x, 0);


            classParent = SaveManager.Instance.loadDataFromDisk();
            classParentsStats = SaveManager.Instance.loadStatsDataFromDisk();

            for (int i = 0; i < bookManager.books.Count; i++)
            {
                if (selectedBookName == bookManager.books[i].bookName)
                {

                    if ((temp + bookManager.books[i].statingClass) == bookManager.books[i].statingClass)
                    {
                        previousBtn.interactable = false;
                        previousBtnGlowImage.SetActive(false);

                    }
                    else
                    {
                        previousBtn.interactable = true;
                        previousBtnGlowImage.SetActive(true);
                    }
                    if ((temp + bookManager.books[i].statingClass) == bookManager.books[i].endingClass)
                    {
                        nextBtn.interactable = false;
                        nextBtnGlowImage.SetActive(false);
                    }
                    else
                    {
                        nextBtnGlowImage.SetActive(true);
                        nextBtn.interactable = true;
                    }




                    for (int j = 0; j <= activityScriptInstance.classActivityList.Count; j++)
                    {
                        if ((temp + bookManager.books[i].statingClass) == j)
                        {
                            for (int t = 0; t < ParentContainerOfCells.transform.childCount; t++)
                            {
                                if (t < activityScriptInstance.classActivityList[j].classData.activityList.Count)
                                {

                                    ParentContainerOfCells.transform.GetChild(t).gameObject.SetActive(true);
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(1).transform.GetComponent<TextMeshProUGUI>().text =  Regex.Replace(activityScriptInstance.classActivityList[j].classData.activityList[t].activityName, @"\s+", " ");  //activity Name                                  

                                }
                                else
                                {
                                    ParentContainerOfCells.transform.GetChild(t).gameObject.SetActive(false);
                                }

                            }

                        }
                    }


                    if (true)//best timeing
                    {
                        classBestTime = 0;
                        int t = 0;
                        if (ParentContainerOfCells.transform.GetChild(t).gameObject.activeInHierarchy)
                        {
                            int j = (temp + bookManager.books[i].statingClass);

                            for (int k = 0; k < activityScriptInstance.classActivityList[j].classData.activityList.Count; k++, t++)
                            {

                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].matchValueWithImage.Length > 0)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].matchValueWithImage[0].bestTime_string;

                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].matchValueWithImage[0].bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].matchValueWithImage[0].bestTime;
                                    }

                                }
                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].matchShapeWithNumbers.Length > 0)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].matchShapeWithNumbers[0].bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].matchShapeWithNumbers[0].bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].matchShapeWithNumbers[0].bestTime;
                                    }
                                }

                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].abacusOperations.active==true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].abacusOperations.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].abacusOperations.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].abacusOperations.bestTime;
                                    }


                                }


                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].speedWriting == true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].speedWriting.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].speedWriting.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].speedWriting.bestTime;
                                    }

                                }

                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].liftBeed01 == true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].liftingBeed21.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].liftingBeed21.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].liftingBeed21.bestTime;
                                    }


                                }

                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].liftBeed02 == true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].LiftingBeed22.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].LiftingBeed22.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].LiftingBeed22.bestTime;
                                    }

                                }

                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].maze.active == true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].maze2.bestTime_string;

                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].maze2.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].maze2.bestTime;
                                    }

                                }
                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].visualHands.active == true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].visualHands2.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].visualHands2.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].visualHands2.bestTime;
                                    }


                                }
                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].countBodyParts.active == true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].countBodyParts2.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].countBodyParts2.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].countBodyParts2.bestTime;
                                    }

                                }

                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].mixedMathematicalOperations.active == true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].mixedMathematicalOperations2.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].mixedMathematicalOperations2.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].mixedMathematicalOperations2.bestTime;
                                    }


                                }
                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].coloringPageImages.Length > 0)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].coloringPageImages.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].coloringPageImages.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].coloringPageImages.bestTime;
                                    }

                                }
                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].multiplicationDivisionPuzzle.active == true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationDivisionPuzzle2.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationDivisionPuzzle2.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationDivisionPuzzle2.bestTime;
                                    }
                                }

                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].mutliplicationOperation.active == true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationOperation2.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationOperation2.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].multiplicationOperation2.bestTime;
                                    }
                                }
                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].liftBeeds.active == true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].liftBeeds2.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].liftBeeds2.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].liftBeeds2.bestTime;
                                    }
                                }

                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].divisionOperation.active == true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].divisionOperation2.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].divisionOperation2.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].divisionOperation2.bestTime;
                                    }


                                }

                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].divisionOperation.active == true)
                                {
                                    ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].divisionOperation2.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].divisionOperation2.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].divisionOperation2.bestTime;
                                    }


                                }

                                if (activityScriptInstance.classActivityList[j].classData.activityList[k].tutorialVideo.active == true)
                                {
                                      ParentContainerOfCells.transform.GetChild(t).GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].tutorialVideo2.bestTime_string;
                                    if (Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].tutorialVideo2.bestTime >= 0)
                                    {
                                        classBestTime += Activity.classParentsStats.classActivityCompletionHolderList2[j].classData.activityList[k].tutorialVideo2.bestTime;
                                    }
                                    else
                                    {
                                        //classCompleted = false;
                                        //break;
                                    }


                                }

                            }

                        }

                    }

                }

            }
        }
    }

    public void Share()
    {
        int i = 0;
        string shareText = "";
        shareText += shareText + "Name: " + userName.text;
        shareText += bookNameText.text;
        shareText += "\n";
        shareText += classNameText.text;
        shareText += "\n";

        while (ParentContainerOfCells.transform.GetChild(i).gameObject.activeInHierarchy)
        {
            shareText += Regex.Replace(ParentContainerOfCells.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text, @"\s+", " "); 
            shareText += "   ";
            shareText += ParentContainerOfCells.transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text;
            shareText += "\n";
            i++;
        }

        new NativeShare()
           .SetSubject("Subject goes here").SetText(shareText)
           .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
           .Share();

    }


}






