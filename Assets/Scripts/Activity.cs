using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample01;

public class Activity : MonoBehaviour
{
    public List<GameObject> activityScriptList;
    public QuestionTypeSelectionGenralScript questionTypeSelectionGenralScript;
    public ProgressLoader progressLoader;
    public Example01 activitiesGameObject;
    bool[] isActivityAdded;

    // ClassData classData;
    int totalActivities;
    string className;
    public static ClassParent classParent;
    public static ClassParent tempclassParent;


    public static ClassParent LockParent;
    public static ClassParentStats classParentsStats;
    public static ClassParentStats tempclassParentsStats;
    public static ClassParentDailyWorkoutStats classParentDailyWorkoutStats;
    public MODE mode;
    public List<Book> books;

    public GameObject loadingScreen;
    public List<ClassActivity> classActivityList;
    public Sprite[] activityBoxImages;
    public Sudoku sudoku;
    string currentClass;
    public void OnEnable()
    {
        progressLoader.CalculatePercentage();


        int k = 0;
        // classData = GetComponent<ClassActivity>().classData;
        //print(gameObject.name+ "gameObject.name");
        for (int i = 0; i < classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == classActivityList[i].classData.nameOfClass)
            {
                activitiesGameObject.imgs = new List<Sprite>();
                activitiesGameObject.TabName = new List<string>();
                activitiesGameObject.btnEvents = new List<UnityAction>();
                activitiesGameObject.completionPercentage = new List<float>();
                activitiesGameObject.cardIsIntractable = new List<bool>();

                className = classActivityList[i].classData.nameOfClass;
                totalActivities = classActivityList[i].classData.activityList.Count;
                for (int j = 0; j < totalActivities; j++)
                {
                    activitiesGameObject.imgs.Add(activityBoxImages[j % activityBoxImages.Length]);
                    activitiesGameObject.TabName.Add(classActivityList[i].classData.activityList[j].activityName);
                    activitiesGameObject.completionPercentage.Add(progressLoader.classProgresses[i].allActivityprogress[j].activitiesPercentage);

                    if (j == 0)
                    {
                        activitiesGameObject.cardIsIntractable.Add(true);
                    }
                    else
                    {
                        if (activitiesGameObject.completionPercentage[j - 1] >= 100)
                        {
                            activitiesGameObject.cardIsIntractable.Add(true);
                        }
                        else if (activitiesGameObject.completionPercentage[j] > 0)
                        {
                            activitiesGameObject.cardIsIntractable.Add(true);
                        }
                        else
                            activitiesGameObject.cardIsIntractable.Add(false);
                    }




                    k = j;
                    if (classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length > 0)
                    {
                        UnityAction action = () => { activityScriptList[0].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }
                    else if (classActivityList[i].classData.activityList[j].abacusOperations.active == true)
                    {
                        UnityAction action = () => { activityScriptList[1].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);

                    }
                    else if (classActivityList[i].classData.activityList[j].matchValueWithImage.Length > 0)
                    {
                        UnityAction action = () => { activityScriptList[2].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }
                    else if (classActivityList[i].classData.activityList[j].coloringPageImages.Length > 0)
                    {
                        UnityAction action = () => { activityScriptList[3].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }
                    else if (classActivityList[i].classData.activityList[j].liftBeed01 == true)
                    {
                        UnityAction action = () => { activityScriptList[4].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);

                    }
                    else if (classActivityList[i].classData.activityList[j].liftBeed02 == true)
                    {
                        UnityAction action = () => { activityScriptList[5].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);

                    }
                    else if (classActivityList[i].classData.activityList[j].speedWriting == true)
                    {
                        UnityAction action = () => { activityScriptList[6].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }
                    else if (classActivityList[i].classData.activityList[j].maze.active == true)
                    {
                        UnityAction action = () => { activityScriptList[7].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }
                    else if (classActivityList[i].classData.activityList[j].visualHands.active == true)
                    {
                        UnityAction action = () => { activityScriptList[8].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }
                    else if (classActivityList[i].classData.activityList[j].countBodyParts.active == true)
                    {
                        UnityAction action = () => { activityScriptList[9].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }
                    else if (classActivityList[i].classData.activityList[j].mixedMathematicalOperations.active == true)
                    {
                        UnityAction action = () => { activityScriptList[10].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }
                    else if (classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.active == true)
                    {
                        UnityAction action = () => { activityScriptList[11].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }
                    else if (classActivityList[i].classData.activityList[j].mutliplicationOperation.active == true)
                    {
                        UnityAction action = () => { activityScriptList[12].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);

                    }
                    else if (classActivityList[i].classData.activityList[j].liftBeeds.active == true)
                    {
                        UnityAction action = () => { activityScriptList[13].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }

                    else if (classActivityList[i].classData.activityList[j].divisionOperation.active == true)
                    {
                        UnityAction action = () => { activityScriptList[14].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }
                    else if (classActivityList[i].classData.activityList[j].tutorialVideo.active == true)
                    {
                        UnityAction action = () => { activityScriptList[15].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }
                    else if (classActivityList[i].classData.activityList[j].animatingCountingTutorial.active == true)
                    {
                        UnityAction action = () => { activityScriptList[16].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }
                    else if (classActivityList[i].classData.activityList[j].sudokuGame.active == true)
                    {
                        UnityAction action = () => { activityScriptList[17].SetActive(true); loadingScreen.SetActive(true); };
                        activitiesGameObject.btnEvents.Add(action);
                    }

                }
                break;
            }
        }
        activitiesGameObject.UpdateData();
    }

    private void Update()
    {
        //  classData.

        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveManager.Instance.saveDataToDisk(classParent);

        }

    }

    private void Start()
    {
        #region comented
        //if (File.Exists(SaveManager.Instance.savePath))
        //    classParent = SaveManager.Instance.loadDataFromDisk();



        //if (classParent==null)
        //{
        //    classParent = new ClassParent();
        //    classParent.classActivityCompletionHolderList = new List<ClassActivityCompletionHolder>();
        //    for (int i = 0; i < classActivityList.Count; i++)
        //    {
        //        int z = i;

        //        classParent.classActivityCompletionHolderList.Add(new ClassActivityCompletionHolder() { classData = new ClassData1() });
        //        classParent.classActivityCompletionHolderList[z].classData.activityList = new List<ActivityList1>();
        //        //classParent.classActivityCompletionHolderList[i].classData.activityList = new List<ActivityList1>();
        //        for (int j = 0; j < classActivityList[i].classData.activityList.Count; j++)
        //        {
        //            classParent.classActivityCompletionHolderList[i].classData.activityList.Add(new ActivityList1());
        //            classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers = new List<MatchShapeWithNumbers1>();
        //            classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage = new List<MatchValueWithImage1>();//[classActivityList[i].classData.activityList[j].matchValueWithImage.Length];

        //            if (classActivityList[i].classData.activityList[j].matchValueWithImage.Length > 0)
        //            {
        //                for (int k = 0; k < classActivityList[i].classData.activityList[j].matchValueWithImage.Length; k++)
        //                {
        //                    classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage.Add(new MatchValueWithImage1());
        //                    classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage[k].completed=false;

        //                }

        //            }
        //            if (classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length > 0)
        //            {
        //                //  classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[0] = new MatchShapeWithNumbers1();
        //                for (int k = 0; k < classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length; k++)
        //                {
        //                    classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers.Add(new MatchShapeWithNumbers1());
        //                    classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed = new List<bool>(classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length);

        //                    for (int l = 0; l < classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length; l++)
        //                    {
        //                        classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Add(new bool());
        //                    }

        //                //classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k]=
        //                //    classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed = new bool[/*classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length*/5];
        //                }
        //            }

        //            if (classActivityList[i].classData.activityList[j].abacusOperations != null)
        //            {

        //                classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations = new bool[JsonUtility.FromJson<AdditionJsonWrapper>(classActivityList[i].classData.activityList[j].abacusOperations.text).Add.Length];

        //            }

        //            if (classActivityList[i].classData.activityList[j].coloringPageImages.Length > 0)
        //            {

        //            }
        //            if (classActivityList[i].classData.activityList[j].speedWriting == true)
        //            {

        //            }

        //            if (classActivityList[i].classData.activityList[j].liftBeed01 == true)
        //            {


        //            }

        //            if (classActivityList[i].classData.activityList[j].liftBeed02 == true)
        //            {



        //            }
        //        }

        //    }
        //    SaveManager.Instance.saveDataToDisk(classParent);
        //}
        #endregion

        CreateOrLodeDataFromDisk();

        CreateOrLoadDailyWorkoutStatsDataFromDisk();

    }
    public class AdditionJsonWrapper
    {
        public AdditionJson[] Add;
    }

    public class MixedOpeartionJsonWrapper
    {
        public Operation[] operation;
    }

    public class MultiplicationJsonWrapper
    {
        public MultiplicationJson[] Mul;
    }
    public class MultiplicationAndDivisionPuzzleJsonWrapper
    {
        public MultiplicationAndDivisionQuestions[] questions;
    }

    public class DivisionJsonWrapper
    {
        public DivisionJson[] div;
    }


    public void CreateOrLodeDataFromDisk()
    {
        if (File.Exists(SaveManager.Instance.savePath))
        {


            classParent = SaveManager.Instance.loadDataFromDisk();
            classParentsStats = SaveManager.Instance.loadStatsDataFromDisk();

            tempclassParent = new ClassParent();
            tempclassParentsStats = new ClassParentStats();
            tempclassParent.classActivityCompletionHolderList = new List<ClassActivityCompletionHolder>();
            tempclassParentsStats.classActivityCompletionHolderList2 = new List<ClassActivityCompletionHolder2>();

            for (int i = 0; i < classActivityList.Count; i++)
            {


                #region number of classes less or equal to previous update
                if (classActivityList.Count <= classParent.classActivityCompletionHolderList.Count)
                {
                    isActivityAdded = new bool[classParent.classActivityCompletionHolderList[i].classData.activityList.Count];

                    int z = i;
                    tempclassParent.classActivityCompletionHolderList.Add(new ClassActivityCompletionHolder() { classData = new ClassData1() });
                    tempclassParentsStats.classActivityCompletionHolderList2.Add(new ClassActivityCompletionHolder2() { classData = new ClassData2() });
                    tempclassParent.classActivityCompletionHolderList[z].classData.activityList = new List<ActivityList1>();
                    tempclassParentsStats.classActivityCompletionHolderList2[z].classData.activityList = new List<ActivityList2>();

                    for (int j = 0; j < classActivityList[i].classData.activityList.Count; j++)
                    {
                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList.Add(new ActivityList1());
                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Add(new ActivityList2());

                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers = new List<MatchShapeWithNumbers1>();
                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchShapeWithNumbers = new List<MatchShapeWithNumbers2>();

                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage = new List<MatchValueWithImage1>();//[classActivityList[i].classData.activityList[j].matchValueWithImage.Length];
                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchValueWithImage = new List<MatchValueWithImage2>();

                        for (int w = 0; w < classParent.classActivityCompletionHolderList[i].classData.activityList.Count; w++)
                        {


                            if (classActivityList[i].classData.activityList[j].matchValueWithImage.Length > 0)
                            {


                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].matchValueWithImage.Count > 0)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].matchValueWithImage[0].id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].matchValueWithImage[0].id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].matchValueWithImage[0].id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].matchValueWithImage[0].id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }

                                            isActivityAdded[w] = true;

                                            //replace 
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchValueWithImage.Add(new MatchValueWithImage2());

                                            for (int k = 0; k < classActivityList[i].classData.activityList[j].matchValueWithImage.Length; k++)
                                            {
                                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage.Add(new MatchValueWithImage1());
                                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage[k].completed = false;

                                            }


                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);



                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New

                                            print("IIIIIIIIIIIIIIIIIIIIIIIIDDDDDDDDDDDDDDDDDDDDDDDDDDDDD" + classActivityList[i].classData.activityList[j].iD);
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchValueWithImage.Add(new MatchValueWithImage2() { id = classActivityList[i].classData.activityList[j].iD });

                                            for (int k = 0; k < classActivityList[i].classData.activityList[j].matchValueWithImage.Length; k++)
                                            {
                                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage.Add(new MatchValueWithImage1());
                                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage[k].completed = false;

                                            }


                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New

                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchValueWithImage.Add(new MatchValueWithImage2() { id = classActivityList[i].classData.activityList[j].iD });

                                        for (int k = 0; k < classActivityList[i].classData.activityList[j].matchValueWithImage.Length; k++)
                                        {
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage.Add(new MatchValueWithImage1());
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage[k].completed = false;

                                        }

                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length > 0)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].matchShapeWithNumbers.Count > 0)
                                {
                                    if (isActivityAdded[w] == false)
                                    {

                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].matchShapeWithNumbers[0].id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].matchShapeWithNumbers[0].id == classActivityList[i].classData.activityList[j].iD)
                                        {


                                            isActivityAdded[w] = true;
                                            //replace 
                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].matchShapeWithNumbers[0].id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].matchShapeWithNumbers[0].id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }

                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchShapeWithNumbers.Add(new MatchShapeWithNumbers2());
                                            for (int k = 0; k < classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length; k++)
                                            {
                                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers.Add(new MatchShapeWithNumbers1());
                                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed = new List<bool>(classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length);

                                                for (int l = 0; l < classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length; l++)
                                                {
                                                    tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Add(new bool());
                                                }


                                            }

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New

                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchShapeWithNumbers.Add(new MatchShapeWithNumbers2() { id = classActivityList[i].classData.activityList[j].iD });
                                            for (int k = 0; k < classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length; k++)
                                            {
                                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers.Add(new MatchShapeWithNumbers1());
                                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed = new List<bool>(classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length);

                                                for (int l = 0; l < classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length; l++)
                                                {
                                                    tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Add(new bool());
                                                }


                                            }


                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New
                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchShapeWithNumbers.Add(new MatchShapeWithNumbers2() { id = classActivityList[i].classData.activityList[j].iD });
                                        for (int k = 0; k < classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length; k++)
                                        {
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers.Add(new MatchShapeWithNumbers1());
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed = new List<bool>(classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length);

                                            for (int l = 0; l < classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length; l++)
                                            {
                                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Add(new bool());
                                            }


                                        }


                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].abacusOperations.active == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].abacusOperations != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].abacusOperations.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].abacusOperations.id == classActivityList[i].classData.activityList[j].iD)
                                        {

                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].abacusOperations.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].abacusOperations.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }

                                            isActivityAdded[w] = true;
                                            //replace 
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].abacusOperations = new AbacusOperations2();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations = new bool[JsonUtility.FromJson<AdditionJsonWrapper>(classActivityList[i].classData.activityList[j].abacusOperations.jsonData.text).Add.Length];

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];



                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].abacusOperations = new AbacusOperations2() { id = classActivityList[i].classData.activityList[j].iD };
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations = new bool[JsonUtility.FromJson<AdditionJsonWrapper>(classActivityList[i].classData.activityList[j].abacusOperations.jsonData.text).Add.Length];


                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New
                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].abacusOperations = new AbacusOperations2() { id = classActivityList[i].classData.activityList[j].iD };
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations = new bool[JsonUtility.FromJson<AdditionJsonWrapper>(classActivityList[i].classData.activityList[j].abacusOperations.jsonData.text).Add.Length];


                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].coloringPageImages.Length > 0)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].coloringPageImages != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].coloringPageImages.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].coloringPageImages.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;
                                            //replace 
                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].coloringPageImages.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].coloringPageImages.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }


                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].coloringPageImages = new ColoringPageImages2();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].coloringPageImages = new ColoringPageImages2() { id = classActivityList[i].classData.activityList[j].iD };


                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New

                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].coloringPageImages = new ColoringPageImages2() { id = classActivityList[i].classData.activityList[j].iD };

                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].speedWriting == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].speedWriting != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].speedWriting.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].speedWriting.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;
                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].speedWriting.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].speedWriting.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }


                                            //replace 

                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].speedWriting = new SpeedWriting2();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];


                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].speedWriting = new SpeedWriting2() { id = classActivityList[i].classData.activityList[j].iD };


                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New
                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].speedWriting = new SpeedWriting2() { id = classActivityList[i].classData.activityList[j].iD };


                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].maze.active == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].maze2 != null)
                                {
                                    if (isActivityAdded[w] == false && (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].maze2.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].maze2.id == classActivityList[i].classData.activityList[j].iD))
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].maze2.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].maze2.id == classActivityList[i].classData.activityList[j].iD)
                                        {

                                            isActivityAdded[w] = true;



                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].maze2.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].maze2.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }




                                            //replace 
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].maze1 = new Maze1();
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].maze2 = new Maze2();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];


                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].maze1 = new Maze1();
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].maze2 = new Maze2() { id = classActivityList[i].classData.activityList[j].iD };
                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New

                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].maze1 = new Maze1();
                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].maze2 = new Maze2() { id = classActivityList[i].classData.activityList[j].iD };
                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].visualHands.active == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].visualHands2 != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {

                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].visualHands2.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].visualHands2.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;


                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].visualHands2.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].visualHands2.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }

                                            //replace 
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].visualHands1 = new VisualHands1()
                                            {
                                                completed = new bool[JsonUtility.FromJson<AdditionJsonWrapper>(classActivityList[i].classData.activityList[j].visualHands.abacusOperations.text).Add.Length]
                                            };

                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].visualHands2 = new VisualHands2();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].visualHands1 = new VisualHands1()
                                            {
                                                completed = new bool[JsonUtility.FromJson<AdditionJsonWrapper>(classActivityList[i].classData.activityList[j].visualHands.abacusOperations.text).Add.Length]
                                            };
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].visualHands2 = new VisualHands2() { id = classActivityList[i].classData.activityList[j].iD };

                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New

                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].visualHands1 = new VisualHands1()
                                        {
                                            completed = new bool[JsonUtility.FromJson<AdditionJsonWrapper>(classActivityList[i].classData.activityList[j].visualHands.abacusOperations.text).Add.Length]
                                        };
                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].visualHands2 = new VisualHands2() { id = classActivityList[i].classData.activityList[j].iD };

                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].countBodyParts.active == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].countBodyParts2 != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].countBodyParts2.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].countBodyParts2.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;

                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].countBodyParts2.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].countBodyParts2.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }


                                            //replace 
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].countBodyParts1 = new CountBodyParts1()
                                            {
                                                completed = new bool[classActivityList[i].classData.activityList[j].countBodyParts.bodyPartAndCountOfOne.Count * classActivityList[i].classData.activityList[j].countBodyParts.countOfAnimals.Count]
                                            };
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].countBodyParts2 = new CountBodyParts2();

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].countBodyParts1 = new CountBodyParts1()
                                            {
                                                completed = new bool[classActivityList[i].classData.activityList[j].countBodyParts.bodyPartAndCountOfOne.Count * classActivityList[i].classData.activityList[j].countBodyParts.countOfAnimals.Count]
                                            };
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].countBodyParts2 = new CountBodyParts2() { id = classActivityList[i].classData.activityList[j].iD };

                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New

                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].countBodyParts1 = new CountBodyParts1()
                                        {
                                            completed = new bool[classActivityList[i].classData.activityList[j].countBodyParts.bodyPartAndCountOfOne.Count * classActivityList[i].classData.activityList[j].countBodyParts.countOfAnimals.Count]
                                        };
                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].countBodyParts2 = new CountBodyParts2() { id = classActivityList[i].classData.activityList[j].iD };

                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].mixedMathematicalOperations.active == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].mixedMathematicalOperations2 != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].mixedMathematicalOperations2.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].mixedMathematicalOperations2.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;

                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].mixedMathematicalOperations2.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].mixedMathematicalOperations2.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }

                                            //replace 

                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].mixedMathematicalOperations2 = new MixedMathematicalOperations2();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1 = new MixedMathematicalOperations1();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1.completed = new bool[JsonUtility.FromJson<MixedOpeartionJsonWrapper>(classActivityList[i].classData.activityList[j].mixedMathematicalOperations.jsonData.text).operation.Length];

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New

                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].mixedMathematicalOperations2 = new MixedMathematicalOperations2() { id = classActivityList[i].classData.activityList[j].iD };
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1 = new MixedMathematicalOperations1();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1.completed = new bool[JsonUtility.FromJson<MixedOpeartionJsonWrapper>(classActivityList[i].classData.activityList[j].mixedMathematicalOperations.jsonData.text).operation.Length];


                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New

                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].mixedMathematicalOperations2 = new MixedMathematicalOperations2() { id = classActivityList[i].classData.activityList[j].iD };
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1 = new MixedMathematicalOperations1();
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1.completed = new bool[JsonUtility.FromJson<MixedOpeartionJsonWrapper>(classActivityList[i].classData.activityList[j].mixedMathematicalOperations.jsonData.text).operation.Length];


                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.active == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].multiplicationDivisionPuzzle2 != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].multiplicationDivisionPuzzle2.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].multiplicationDivisionPuzzle2.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;


                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].multiplicationDivisionPuzzle2.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].multiplicationDivisionPuzzle2.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }

                                            //replace 

                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationDivisionPuzzle2 = new MultiplicationDivisionPuzzle2();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1 = new MultiplicationDivisionPuzzle1();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1.completed = new bool[JsonUtility.FromJson<MultiplicationAndDivisionPuzzleJsonWrapper>(classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.jsonData.text).questions.Length];

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationDivisionPuzzle2 = new MultiplicationDivisionPuzzle2() { id = classActivityList[i].classData.activityList[j].iD };
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1 = new MultiplicationDivisionPuzzle1();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1.completed = new bool[JsonUtility.FromJson<MultiplicationAndDivisionPuzzleJsonWrapper>(classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.jsonData.text).questions.Length];


                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New

                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationDivisionPuzzle2 = new MultiplicationDivisionPuzzle2() { id = classActivityList[i].classData.activityList[j].iD };
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1 = new MultiplicationDivisionPuzzle1();
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1.completed = new bool[JsonUtility.FromJson<MultiplicationAndDivisionPuzzleJsonWrapper>(classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.jsonData.text).questions.Length];


                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].mutliplicationOperation.active == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].multiplicationOperation2 != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].multiplicationOperation2.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].multiplicationOperation2.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;

                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].multiplicationOperation2.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].multiplicationOperation2.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }
                                            //replace 

                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationOperation2 = new MultiplicationOperation2();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1 = new MultiplicationOperation1();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed = new bool[JsonUtility.FromJson<MultiplicationJsonWrapper>(classActivityList[i].classData.activityList[j].mutliplicationOperation.jsonData.text).Mul.Length];

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New

                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationOperation2 = new MultiplicationOperation2() { id = classActivityList[i].classData.activityList[j].iD };
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1 = new MultiplicationOperation1();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed = new bool[JsonUtility.FromJson<MultiplicationJsonWrapper>(classActivityList[i].classData.activityList[j].mutliplicationOperation.jsonData.text).Mul.Length];


                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New

                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationOperation2 = new MultiplicationOperation2() { id = classActivityList[i].classData.activityList[j].iD };
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1 = new MultiplicationOperation1();
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed = new bool[JsonUtility.FromJson<MultiplicationJsonWrapper>(classActivityList[i].classData.activityList[j].mutliplicationOperation.jsonData.text).Mul.Length];


                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].liftBeeds.active == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].liftBeeds2 != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].liftBeeds2.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].liftBeeds2.id == classActivityList[i].classData.activityList[j].iD)
                                        {


                                            isActivityAdded[w] = true;


                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].liftBeeds2.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].liftBeeds2.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }


                                            //replace 
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftBeeds2 = new LiftBeeds2();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeeds1 = new LiftBeeds1();

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftBeeds2 = new LiftBeeds2() { id = classActivityList[i].classData.activityList[j].iD };
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeeds1 = new LiftBeeds1();


                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New
                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftBeeds2 = new LiftBeeds2() { id = classActivityList[i].classData.activityList[j].iD };
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeeds1 = new LiftBeeds1();

                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].divisionOperation.active == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].divisionOperation2 != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].divisionOperation2.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].divisionOperation2.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;
                                            //replace 

                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].divisionOperation2.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].divisionOperation2.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }


                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].divisionOperation2 = new DivisionOperation2();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1 = new DivisionOperation1();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1.completed = new bool[JsonUtility.FromJson<DivisionJsonWrapper>(classActivityList[i].classData.activityList[j].divisionOperation.jsonData.text).div.Length];

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].divisionOperation2 = new DivisionOperation2() { id = classActivityList[i].classData.activityList[j].iD };
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1 = new DivisionOperation1();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1.completed = new bool[JsonUtility.FromJson<DivisionJsonWrapper>(classActivityList[i].classData.activityList[j].divisionOperation.jsonData.text).div.Length];

                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New
                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].divisionOperation2 = new DivisionOperation2() { id = classActivityList[i].classData.activityList[j].iD };
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1 = new DivisionOperation1();
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1.completed = new bool[JsonUtility.FromJson<DivisionJsonWrapper>(classActivityList[i].classData.activityList[j].divisionOperation.jsonData.text).div.Length];

                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].liftBeed01 == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].liftingBeed21 != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].liftingBeed21.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].liftingBeed21.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;
                                            //replace 

                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].liftingBeed21.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].liftingBeed21.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }


                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftingBeed21 = new LiftingBeed21();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed01 = new bool[4];


                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                        }
                                        break;
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftingBeed21 = new LiftingBeed21() { id = classActivityList[i].classData.activityList[j].iD };

                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New
                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftingBeed21 = new LiftingBeed21() { id = classActivityList[i].classData.activityList[j].iD };

                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].liftBeed02 == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].LiftingBeed22 != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].LiftingBeed22.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].LiftingBeed22.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;

                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].LiftingBeed22.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].LiftingBeed22.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }

                                            //replace 
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].LiftingBeed22 = new LiftingBeed22();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed02 = new bool[5];

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].LiftingBeed22 = new LiftingBeed22() { id = classActivityList[i].classData.activityList[j].iD };

                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New
                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].LiftingBeed22 = new LiftingBeed22() { id = classActivityList[i].classData.activityList[j].iD };


                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].tutorialVideo.active == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].tutorialVideo2 != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].tutorialVideo2.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].tutorialVideo2.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;
                                            //replace 
                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].tutorialVideo2.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].tutorialVideo2.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].tutorialVideo1 = new TutorialVideo1();
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].tutorialVideo2 = new TutorialVideo2();

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].tutorialVideo1 = new TutorialVideo1();
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].tutorialVideo2 = new TutorialVideo2() { id = classActivityList[i].classData.activityList[j].iD };

                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].tutorialVideo1 = new TutorialVideo1();
                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].tutorialVideo2 = new TutorialVideo2() { id = classActivityList[i].classData.activityList[j].iD };


                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].animatingCountingTutorial.active == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].animatingCountingTutorial2 != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].animatingCountingTutorial2.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].animatingCountingTutorial2.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;
                                            //replace 


                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].animatingCountingTutorial2.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].animatingCountingTutorial2.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }

                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].animatingCountingTutorial2 = new AnimatingCountingTutorial2();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1 = new AnimatingCountingTutorial1();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1.completed = new bool[classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes.Length];

                                            // if(classParent.classActivityCompletionHolderList[i].classData.activityList[w].animatingCountingTutorial1.completed!=null)
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New

                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].animatingCountingTutorial2 = new AnimatingCountingTutorial2() { id = classActivityList[i].classData.activityList[j].iD };
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1 = new AnimatingCountingTutorial1();
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1.completed = new bool[classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes.Length];


                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New

                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].animatingCountingTutorial2 = new AnimatingCountingTutorial2() { id = classActivityList[i].classData.activityList[j].iD };
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1 = new AnimatingCountingTutorial1();
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1.completed = new bool[classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes.Length];
                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }

                            if (classActivityList[i].classData.activityList[j].sudokuGame.active == true)
                            {
                                if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].sudokuGame2 != null)
                                {
                                    if (isActivityAdded[w] == false)
                                    {
                                        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].sudokuGame2.id == null || classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].sudokuGame2.id == classActivityList[i].classData.activityList[j].iD)
                                        {
                                            isActivityAdded[w] = true;
                                            //replace 
                                            if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].sudokuGame2.id == null)
                                            {
                                                classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w].sudokuGame2.id = (((i / 12) + 1).ToString("D2") + "." + ((i % 12) + 1).ToString("D2") + "." + (w + 1).ToString("D2"));
                                            }

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].sudokuGame1 = new SudokuGame1();
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].sudokuGame2 = new SudokuGame2();

                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j] = classParent.classActivityCompletionHolderList[i].classData.activityList[w];
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j] = classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[w];

                                            print("replace" + w + " " + j);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                        {
                                            //create New
                                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].sudokuGame1 = new SudokuGame1();
                                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].sudokuGame2 = new SudokuGame2() { id = classActivityList[i].classData.activityList[j].iD };

                                            print("create " + w + " " + j);

                                        }

                                        //move forward


                                        continue;
                                    }
                                }
                                else
                                {
                                    if (w == classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Count - 1)
                                    {
                                        //create New
                                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].sudokuGame1 = new SudokuGame1();
                                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].sudokuGame2 = new SudokuGame2() { id = classActivityList[i].classData.activityList[j].iD };


                                        print("create " + w + " " + j);

                                        break;
                                    }

                                }
                            }


                        }




                        #region comented

                        ////    for (int j = 0; j < classActivityList[i].classData.activityList.Count; j++)
                        ////{
                        ////    try
                        ////    {

                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchValueWithImage.Count > 0)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].matchValueWithImage.Length > 0)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }
                        ////    }
                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].matchValueWithImage.Length > 0)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }

                        ////    }


                        ////    try
                        ////    {

                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchShapeWithNumbers.Count > 0)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length > 0)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }
                        ////    }

                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length > 0)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }


                        ////    }

                        ////    try
                        ////    {


                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].abacusOperations != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].abacusOperations.active == true)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }
                        ////    }

                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].abacusOperations.active == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }


                        ////    }

                        //    try
                        //    {



                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].coloringPageImages != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].coloringPageImages.Length > 0)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }
                        ////    }

                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].coloringPageImages.Length > 0)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }

                        ////    }


                        ////    try
                        ////    {

                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].coloringPageImages != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].coloringPageImages.Length > 0)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }
                        ////    }
                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].coloringPageImages.Length > 0)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }

                        ////    }


                        ////    try
                        ////    {


                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].speedWriting != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].speedWriting == true)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }
                        ////    }

                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].speedWriting == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }

                        ////    }
                        ////    try {

                        ////    if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].speedWriting !=null)
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].liftBeed01 == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////        //    print("activity Chnaged");

                        ////        }
                        ////    }
                        ////    }

                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].liftBeed01 == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            //    print("activity Chnaged");

                        ////        }
                        ////    }

                        ////    try
                        ////    {


                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].speedWriting != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].liftBeed02 == true)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                //  print("activity Chnaged");

                        ////            }
                        ////        }
                        ////    }
                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].liftBeed02 == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            //  print("activity Chnaged");

                        ////        }

                        ////    }


                        ////    try
                        ////    {


                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].maze2 != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].maze.active == true)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }

                        ////    }
                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].maze.active == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }
                        ////    }

                        ////    try
                        ////    {

                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].visualHands2 != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].visualHands.active == true)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }

                        ////    }
                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].visualHands.active == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }

                        ////    }



                        ////    try
                        ////    {


                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].countBodyParts2 != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].countBodyParts.active == true)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }


                        ////    }
                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].countBodyParts.active == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }

                        ////    }

                        ////    try
                        ////    {

                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].mixedMathematicalOperations2 != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].mixedMathematicalOperations.active == true)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }

                        ////    }
                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].mixedMathematicalOperations.active == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }

                        ////    }

                        ////    try
                        ////    {

                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationDivisionPuzzle2 != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.active == true)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }
                        ////    }

                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.active == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }

                        ////    }


                        ////    try
                        ////    {

                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationOperation2 != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].mutliplicationOperation.active == true)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }

                        ////    }
                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].mutliplicationOperation.active == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }

                        ////    }

                        ////    try
                        ////    {

                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftBeeds2 != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].liftBeeds.active == true)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }

                        ////    }
                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].liftBeeds.active == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }

                        ////    }

                        ////    try
                        ////    {

                        ////        if (classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].divisionOperation2 != null)
                        ////        {
                        ////            if (classActivityList[i].classData.activityList[j].divisionOperation.active == true)
                        ////            {

                        ////            }
                        ////            else
                        ////            {
                        ////                print("activity Chnaged");

                        ////            }
                        ////        }

                        ////    }

                        ////    catch
                        ////    {
                        ////        if (classActivityList[i].classData.activityList[j].divisionOperation.active == true)
                        ////        {

                        ////        }
                        ////        else
                        ////        {
                        ////            print("activity Chnaged");

                        ////        }

                        ////    }

                        #endregion





                    }

                }
                else
                {
                    int z = i;

                    tempclassParent.classActivityCompletionHolderList.Add(new ClassActivityCompletionHolder() { classData = new ClassData1() });
                    tempclassParentsStats.classActivityCompletionHolderList2.Add(new ClassActivityCompletionHolder2() { classData = new ClassData2() });
                    tempclassParent.classActivityCompletionHolderList[z].classData.activityList = new List<ActivityList1>();
                    tempclassParentsStats.classActivityCompletionHolderList2[z].classData.activityList = new List<ActivityList2>();
                    //classParent.classActivityCompletionHolderList[i].classData.activityList = new List<ActivityList1>();
                    for (int j = 0; j < classActivityList[i].classData.activityList.Count; j++)
                    {


                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList.Add(new ActivityList1());
                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Add(new ActivityList2());

                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers = new List<MatchShapeWithNumbers1>();
                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchShapeWithNumbers = new List<MatchShapeWithNumbers2>();

                        tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage = new List<MatchValueWithImage1>();//[classActivityList[i].classData.activityList[j].matchValueWithImage.Length];
                        tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchValueWithImage = new List<MatchValueWithImage2>();

                        if (classActivityList[i].classData.activityList[j].matchValueWithImage.Length > 0)
                        {

                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchValueWithImage.Add(new MatchValueWithImage2() { id = classActivityList[i].classData.activityList[j].iD });


                            for (int k = 0; k < classActivityList[i].classData.activityList[j].matchValueWithImage.Length; k++)
                            {
                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage.Add(new MatchValueWithImage1());


                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage[k].completed = false;

                            }
                            print("Created");

                        }
                        if (classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length > 0)
                        {
                            print("Created");

                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchShapeWithNumbers.Add(new MatchShapeWithNumbers2() { id = classActivityList[i].classData.activityList[j].iD });
                            //  classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[0] = new MatchShapeWithNumbers1();
                            for (int k = 0; k < classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length; k++)
                            {
                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers.Add(new MatchShapeWithNumbers1());
                                tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed = new List<bool>(classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length);

                                for (int l = 0; l < classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length; l++)
                                {
                                    tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Add(new bool());
                                }

                                //classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k]=
                                //    classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed = new bool[/*classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length*/5];
                            }
                        }

                        if (classActivityList[i].classData.activityList[j].abacusOperations.active == true)
                        {
                            print("Created");
                            print(i+""+j);
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].abacusOperations = new AbacusOperations2() { id = classActivityList[i].classData.activityList[j].iD };
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations = new bool[JsonUtility.FromJson<AdditionJsonWrapper>(classActivityList[i].classData.activityList[j].abacusOperations.jsonData.text).Add.Length];

                        }

                        if (classActivityList[i].classData.activityList[j].coloringPageImages.Length > 0)
                        {
                            print("Created");
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].coloringPageImages = new ColoringPageImages2() { id = classActivityList[i].classData.activityList[j].iD };
                        }
                        if (classActivityList[i].classData.activityList[j].speedWriting == true)
                        {
                            print("Created");
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].speedWriting = new SpeedWriting2() { id = classActivityList[i].classData.activityList[j].iD };
                        }

                        if (classActivityList[i].classData.activityList[j].liftBeed01 == true)
                        {
                            print("Created");
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftingBeed21 = new LiftingBeed21() { id = classActivityList[i].classData.activityList[j].iD };
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed01 = new bool[4];

                        }

                        if (classActivityList[i].classData.activityList[j].liftBeed02 == true)
                        {
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].LiftingBeed22 = new LiftingBeed22() { id = classActivityList[i].classData.activityList[j].iD };
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed02 = new bool[5];
                        }

                        if (classActivityList[i].classData.activityList[j].maze.active == true)
                        {
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].maze1 = new Maze1();
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].maze2 = new Maze2() { id = classActivityList[i].classData.activityList[j].iD };
                        }
                        if (classActivityList[i].classData.activityList[j].visualHands.active == true)
                        {
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].visualHands1 = new VisualHands1()
                            {
                                completed = new bool[JsonUtility.FromJson<AdditionJsonWrapper>(classActivityList[i].classData.activityList[j].visualHands.abacusOperations.text).Add.Length]
                            };
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].visualHands2 = new VisualHands2() { id = classActivityList[i].classData.activityList[j].iD };
                        }
                        if (classActivityList[i].classData.activityList[j].countBodyParts.active == true)
                        {
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].countBodyParts1 = new CountBodyParts1()
                            {
                                completed = new bool[classActivityList[i].classData.activityList[j].countBodyParts.bodyPartAndCountOfOne.Count * classActivityList[i].classData.activityList[j].countBodyParts.countOfAnimals.Count]
                            };
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].countBodyParts2 = new CountBodyParts2() { id = classActivityList[i].classData.activityList[j].iD };
                        }

                        if (classActivityList[i].classData.activityList[j].mixedMathematicalOperations.active == true)
                        {
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].mixedMathematicalOperations2 = new MixedMathematicalOperations2() { id = classActivityList[i].classData.activityList[j].iD };
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1 = new MixedMathematicalOperations1();
                            print(i + "" + j);
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1.completed = new bool[JsonUtility.FromJson<MixedOpeartionJsonWrapper>(classActivityList[i].classData.activityList[j].mixedMathematicalOperations.jsonData.text).operation.Length];
                        }

                        if (classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.active == true)
                        {
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationDivisionPuzzle2 = new MultiplicationDivisionPuzzle2() { id = classActivityList[i].classData.activityList[j].iD };
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1 = new MultiplicationDivisionPuzzle1();
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1.completed = new bool[JsonUtility.FromJson<MultiplicationAndDivisionPuzzleJsonWrapper>(classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.jsonData.text).questions.Length];

                        }
                        if (classActivityList[i].classData.activityList[j].mutliplicationOperation.active == true)
                        {
                            print("MULTIPLICATION " + i + "  " + j);
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationOperation2 = new MultiplicationOperation2() { id = classActivityList[i].classData.activityList[j].iD };
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1 = new MultiplicationOperation1();
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed = new bool[JsonUtility.FromJson<MultiplicationJsonWrapper>(classActivityList[i].classData.activityList[j].mutliplicationOperation.jsonData.text).Mul.Length];


                        }
                        if (classActivityList[i].classData.activityList[j].liftBeeds.active == true)
                        {
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftBeeds2 = new LiftBeeds2() { id = classActivityList[i].classData.activityList[j].iD };
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeeds1 = new LiftBeeds1();
                            //  classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed = new bool[JsonUtility.FromJson<MultiplicationJsonWrapper>(classActivityList[i].classData.activityList[j].mutliplicationOperation.jsonData.text).Mul.Length];


                        }

                        if (classActivityList[i].classData.activityList[j].divisionOperation.active == true)
                        {
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].divisionOperation2 = new DivisionOperation2() { id = classActivityList[i].classData.activityList[j].iD };
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1 = new DivisionOperation1();
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1.completed = new bool[JsonUtility.FromJson<DivisionJsonWrapper>(classActivityList[i].classData.activityList[j].divisionOperation.jsonData.text).div.Length];

                        }

                        if (classActivityList[i].classData.activityList[j].tutorialVideo.active == true)
                        {
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].tutorialVideo1 = new TutorialVideo1();
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].tutorialVideo2 = new TutorialVideo2() { id = classActivityList[i].classData.activityList[j].iD };
                        }

                        if (classActivityList[i].classData.activityList[j].animatingCountingTutorial.active == true)
                        {
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1 = new AnimatingCountingTutorial1();
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].animatingCountingTutorial2 = new AnimatingCountingTutorial2() { id = classActivityList[i].classData.activityList[j].iD };
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1.completed = new bool[classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes.Length];

                        }

                        if (classActivityList[i].classData.activityList[j].sudokuGame.active == true)
                        {
                            tempclassParent.classActivityCompletionHolderList[i].classData.activityList[j].sudokuGame1 = new SudokuGame1();
                            tempclassParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].sudokuGame2 = new SudokuGame2() { id = classActivityList[i].classData.activityList[j].iD };
                        }
                    }

                }
            }
            #endregion


            SaveManager.Instance.saveDataToDisk(tempclassParent);
            SaveManager.Instance.SaveStatsToDisk(tempclassParentsStats);

            classParent = SaveManager.Instance.loadDataFromDisk();
            classParentsStats = SaveManager.Instance.loadStatsDataFromDisk();



        }

        else
        {
            classParent = null;
            if (classParent == null)
            {

                classParent = new ClassParent();
                classParentsStats = new ClassParentStats();
                classParent.classActivityCompletionHolderList = new List<ClassActivityCompletionHolder>();
                classParentsStats.classActivityCompletionHolderList2 = new List<ClassActivityCompletionHolder2>();
                for (int i = 0; i < classActivityList.Count; i++)
                {
                    int z = i;

                    classParent.classActivityCompletionHolderList.Add(new ClassActivityCompletionHolder() { classData = new ClassData1() });
                    classParentsStats.classActivityCompletionHolderList2.Add(new ClassActivityCompletionHolder2() { classData = new ClassData2() });
                    classParent.classActivityCompletionHolderList[z].classData.activityList = new List<ActivityList1>();
                    classParentsStats.classActivityCompletionHolderList2[z].classData.activityList = new List<ActivityList2>();
                    //classParent.classActivityCompletionHolderList[i].classData.activityList = new List<ActivityList1>();
                    for (int j = 0; j < classActivityList[i].classData.activityList.Count; j++)
                    {

                        classParent.classActivityCompletionHolderList[i].classData.activityList.Add(new ActivityList1());
                        classParentsStats.classActivityCompletionHolderList2[i].classData.activityList.Add(new ActivityList2());

                        classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers = new List<MatchShapeWithNumbers1>();
                        classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchShapeWithNumbers = new List<MatchShapeWithNumbers2>();

                        classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage = new List<MatchValueWithImage1>();//[classActivityList[i].classData.activityList[j].matchValueWithImage.Length];
                        classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchValueWithImage = new List<MatchValueWithImage2>();

                        if (classActivityList[i].classData.activityList[j].matchValueWithImage.Length > 0)
                        {

                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchValueWithImage.Add(new MatchValueWithImage2() { id = classActivityList[i].classData.activityList[j].iD });

                            for (int k = 0; k < classActivityList[i].classData.activityList[j].matchValueWithImage.Length; k++)
                            {
                                classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage.Add(new MatchValueWithImage1());


                                classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage[k].completed = false;

                            }

                        }
                        if (classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length > 0)
                        {
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchShapeWithNumbers.Add(new MatchShapeWithNumbers2() { id = classActivityList[i].classData.activityList[j].iD });
                            //  classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[0] = new MatchShapeWithNumbers1();
                            for (int k = 0; k < classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length; k++)
                            {
                                classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers.Add(new MatchShapeWithNumbers1());
                                classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed = new List<bool>(classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length);

                                for (int l = 0; l < classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length; l++)
                                {
                                    classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Add(new bool());
                                }

                                //classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k]=
                                //    classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[k].completed = new bool[/*classActivityList[i].classData.activityList[j].matchShapeWithNumbers[k].completed.Length*/5];
                            }
                        }

                        if (classActivityList[i].classData.activityList[j].abacusOperations.active == true)
                        {
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].abacusOperations = new AbacusOperations2() { id = classActivityList[i].classData.activityList[j].iD };
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations = new bool[JsonUtility.FromJson<AdditionJsonWrapper>(classActivityList[i].classData.activityList[j].abacusOperations.jsonData.text).Add.Length];

                        }

                        if (classActivityList[i].classData.activityList[j].coloringPageImages.Length > 0)
                        {
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].coloringPageImages = new ColoringPageImages2() { id = classActivityList[i].classData.activityList[j].iD };
                        }
                        if (classActivityList[i].classData.activityList[j].speedWriting == true)
                        {
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].speedWriting = new SpeedWriting2() { id = classActivityList[i].classData.activityList[j].iD };
                        }

                        if (classActivityList[i].classData.activityList[j].liftBeed01 == true)
                        {
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftingBeed21 = new LiftingBeed21() { id = classActivityList[i].classData.activityList[j].iD };
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed01 = new bool[4];

                        }

                        if (classActivityList[i].classData.activityList[j].liftBeed02 == true)
                        {
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].LiftingBeed22 = new LiftingBeed22() { id = classActivityList[i].classData.activityList[j].iD };
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed02 = new bool[5];
                        }

                        if (classActivityList[i].classData.activityList[j].maze.active == true)
                        {
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].maze1 = new Maze1();
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].maze2 = new Maze2() { id = classActivityList[i].classData.activityList[j].iD };
                        }
                        if (classActivityList[i].classData.activityList[j].visualHands.active == true)
                        {
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].visualHands1 = new VisualHands1()
                            {
                                completed = new bool[JsonUtility.FromJson<AdditionJsonWrapper>(classActivityList[i].classData.activityList[j].visualHands.abacusOperations.text).Add.Length]
                            };
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].visualHands2 = new VisualHands2() { id = classActivityList[i].classData.activityList[j].iD };
                        }
                        if (classActivityList[i].classData.activityList[j].countBodyParts.active == true)
                        {
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].countBodyParts1 = new CountBodyParts1()
                            {
                                completed = new bool[classActivityList[i].classData.activityList[j].countBodyParts.bodyPartAndCountOfOne.Count * classActivityList[i].classData.activityList[j].countBodyParts.countOfAnimals.Count]
                            };
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].countBodyParts2 = new CountBodyParts2() { id = classActivityList[i].classData.activityList[j].iD };
                        }

                        if (classActivityList[i].classData.activityList[j].mixedMathematicalOperations.active == true)
                        {
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].mixedMathematicalOperations2 = new MixedMathematicalOperations2() { id = classActivityList[i].classData.activityList[j].iD };
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1 = new MixedMathematicalOperations1();
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1.completed = new bool[JsonUtility.FromJson<MixedOpeartionJsonWrapper>(classActivityList[i].classData.activityList[j].mixedMathematicalOperations.jsonData.text).operation.Length];
                        }

                        if (classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.active == true)
                        {
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationDivisionPuzzle2 = new MultiplicationDivisionPuzzle2() { id = classActivityList[i].classData.activityList[j].iD };
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1 = new MultiplicationDivisionPuzzle1();
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1.completed = new bool[JsonUtility.FromJson<MultiplicationAndDivisionPuzzleJsonWrapper>(classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.jsonData.text).questions.Length];

                        }
                        if (classActivityList[i].classData.activityList[j].mutliplicationOperation.active == true)
                        {
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationOperation2 = new MultiplicationOperation2() { id = classActivityList[i].classData.activityList[j].iD };
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1 = new MultiplicationOperation1();
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed = new bool[JsonUtility.FromJson<MultiplicationJsonWrapper>(classActivityList[i].classData.activityList[j].mutliplicationOperation.jsonData.text).Mul.Length];


                        }
                        if (classActivityList[i].classData.activityList[j].liftBeeds.active == true)
                        {
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftBeeds2 = new LiftBeeds2() { id = classActivityList[i].classData.activityList[j].iD };
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeeds1 = new LiftBeeds1();
                            //  classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed = new bool[JsonUtility.FromJson<MultiplicationJsonWrapper>(classActivityList[i].classData.activityList[j].mutliplicationOperation.jsonData.text).Mul.Length];


                        }

                        if (classActivityList[i].classData.activityList[j].divisionOperation.active == true)
                        {
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].divisionOperation2 = new DivisionOperation2() { id = classActivityList[i].classData.activityList[j].iD };
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1 = new DivisionOperation1();
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1.completed = new bool[JsonUtility.FromJson<DivisionJsonWrapper>(classActivityList[i].classData.activityList[j].divisionOperation.jsonData.text).div.Length];

                        }

                        if (classActivityList[i].classData.activityList[j].tutorialVideo.active == true)
                        {
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].tutorialVideo1 = new TutorialVideo1();
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].tutorialVideo2 = new TutorialVideo2() { id = classActivityList[i].classData.activityList[j].iD };
                        }

                        if (classActivityList[i].classData.activityList[j].animatingCountingTutorial.active == true)
                        {
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1 = new AnimatingCountingTutorial1();
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].animatingCountingTutorial2 = new AnimatingCountingTutorial2() { id = classActivityList[i].classData.activityList[j].iD };
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1.completed = new bool[classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes.Length];

                        }

                        if (classActivityList[i].classData.activityList[j].sudokuGame.active == true)
                        {
                            classParent.classActivityCompletionHolderList[i].classData.activityList[j].sudokuGame1 = new SudokuGame1();
                            classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].sudokuGame2 = new SudokuGame2() { id = classActivityList[i].classData.activityList[j].iD };
                        }
                    }

                }
                SaveManager.Instance.saveDataToDisk(classParent);
                SaveManager.Instance.SaveStatsToDisk(classParentsStats);
            }
        }
    }

    public void CreateOrLoadDailyWorkoutStatsDataFromDisk()
    {

        if (File.Exists(SaveManager.Instance.dailyWorkoutStatsPath))
        {

            classParentDailyWorkoutStats = SaveManager.Instance.loadDailyWorkoutStatsDataFromDisk();
        }
        else
        {
            classParentDailyWorkoutStats = new ClassParentDailyWorkoutStats();
            classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3 = new List<DailyWorkoutCompletionHolder3>();
            classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3.Add(new DailyWorkoutCompletionHolder3());
            classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation = new List<DailyWorkOutInformation>();

            for (int i = 0; i < 36; i++)
            {

                switch (i / 12)
                {
                    case 0:
                        mode = MODE.easy;
                        break;
                    case 1:
                        mode = MODE.medium;
                        break;
                    case 2:
                        mode = MODE.hard;
                        break;
                }





                classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation.Add(new DailyWorkOutInformation());
                classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].id = i;
                classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].mode = mode;
                classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].QuestionType = questionTypeSelectionGenralScript.questionTypes[i % 12];
                classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].totalCorrectAnswers = 0;
                classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].totalQuestions = 0;
                classParentDailyWorkoutStats.dailyWorkoutCompletionHolder3[0].dailyWorkOutInformation[i].totalTime = 0;









            }


            for (int i = 0; i < 36; i++)
            {


            }






        }

        SaveManager.Instance.SaveDailyWorkoutStatsToDisk(classParentDailyWorkoutStats);
    }


    public void OnSudokuSolved()
    {
        sudoku.OnSudokuSolved();
    }
}



[System.Serializable]
public class ClassParent
{
    public List<ClassActivityCompletionHolder> classActivityCompletionHolderList;
}
[System.Serializable]
public class ClassParentStats
{
    public List<ClassActivityCompletionHolder2> classActivityCompletionHolderList2;
}
[System.Serializable]
public class ClassParentDailyWorkoutStats
{
    public List<DailyWorkoutCompletionHolder3> dailyWorkoutCompletionHolder3;
}

[System.Serializable]
public class AppLockInformation
{
    public List<AppLockInformationHolder3> dailyWorkoutCompletionHolder3;
}

[System.Serializable]
public class ClassUserInformation
{
    public int uniqueID;
    public bool currentUser;
    public string profilePicName;
    public string userName;
    public string userID;
}




[System.Serializable]
public class Book
{
    public string book;
    public List<ClassActivity> classActivities;
}

