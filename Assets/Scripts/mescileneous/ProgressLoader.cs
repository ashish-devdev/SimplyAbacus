using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample01;

public class ProgressLoader : MonoBehaviour
{
    // public BookManager bookManager;
    public Example01 classInstance;
    public Activity activity;
    int totalClass;
    int totalBook;
    public List<float> activityProgress;
    public List<ClassProgress> classProgresses;
    public List<BookProgress> bookProgress;
    public BookManager bookManager;
    public int startClass = 0;
    public int endClass = 0;




    [System.Serializable]
    public class ClassProgress
    {
        public float TotalclassProgress = 0;
        public List<ActivityProgress> allActivityprogress;
    }
    [System.Serializable]
    public class BookProgress
    {
        public float totalBookProgress = 0;
        public List<ClassProgress> allClassProgress;

    }



    [System.Serializable]
    public class ActivityProgress
    {
        public float activitiesPercentage = 0;
    }


    private void OnEnable()
    {
        Invoke("OnEnableAfterSomeDelay", 0.001f);

        //totalBook = bookManager.books.Count;
        //totalClass = activity.classActivityList.Count;
        //classProgresses = new List<ClassProgress>();
        //bookProgress = new List<BookProgress>();
        //for (int i = 0; i < totalClass; i++)
        //{
        //    classProgresses.Add(new ClassProgress());
        //    classProgresses[i].allActivityprogress = new List<ActivityProgress>();
        //    for (int j = 0; j < activity.classActivityList[i].classData.activityList.Count; j++)
        //    {
        //        classProgresses[i].allActivityprogress.Add(new ActivityProgress());
        //    }

        //}
        //activity.CreateOrLodeDataFromDisk();

        //CalculatePercentageAtBegining();

    }
    private void Start()
    {
        Create_SwitchNewUserProfile.onNewprofileCreation += OnEnableAfterSomeDelay;
    }

    private void Update()
    {

        // CalculatePercentage();


    }

    public void CalculatePercentageAtBegining()
    {
        classInstance.completionPercentage = new List<float>();
        classInstance.cardIsIntractable = new List<bool>();

        for (int i = 0; i < totalClass; i++)
        {
            for (int j = 0; j < activity.classActivityList[i].classData.activityList.Count; j++)
            {
                if (activity.classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length > 0)
                {
                    int completed = 0;
                    for (int k = 0; k < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[0].completed.Count; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[0].completed[k] == true)
                            completed++;
                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchShapeWithNumbers[0].bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed / 3f) * 100;
                        else
                        {
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                        }

                    }

                }
                else if (activity.classActivityList[i].classData.activityList[j].matchValueWithImage.Length > 0)
                {

                    int completed = 0;
                    for (int k = 0; k < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage.Count; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage[k].completed == true)
                            completed++;
                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchValueWithImage[0].bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage.Count) * 100;
                        else
                        {
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;

                        }
                    }

                }
                else if (activity.classActivityList[i].classData.activityList[j].abacusOperations.active == true)
                {

                    int completed = 0;
                    for (int k = 0; k < JsonUtility.FromJson<AdditionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].abacusOperations.jsonData.text).Add.Length; k++)
                    {

                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations[k] == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].abacusOperations.bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / JsonUtility.FromJson<AdditionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].abacusOperations.jsonData.text).Add.Length) * 100;
                        else
                        {
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                        }
                    }
                }
                else if (activity.classActivityList[i].classData.activityList[j].coloringPageImages.Length > 0)
                {
                    int completed = 0;

                    if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].coloringPageImages)
                        completed = 1;
                    else completed = 0;

                    classProgresses[i].allActivityprogress[j].activitiesPercentage = completed * 100;
                }
                else if (activity.classActivityList[i].classData.activityList[j].speedWriting == true)
                {
                    int completed = 0;

                    if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].speedWriting)
                        completed = 1;
                    else completed = 0;

                    classProgresses[i].allActivityprogress[j].activitiesPercentage = completed * 100;
                }
                else if (activity.classActivityList[i].classData.activityList[j].liftBeed01 == true)
                {
                    int completed = 0;
                    for (int k = 0; k < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed01.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed01[k] == true)
                            completed++;

                        classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / 4) * 100;
                    }

                }
                else if (activity.classActivityList[i].classData.activityList[j].liftBeed02 == true)
                {
                    int completed = 0;
                    for (int k = 0; k < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed02.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed02[k] == true)
                            completed++;

                        classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / 5) * 100;
                    }

                }
                else if (activity.classActivityList[i].classData.activityList[j].maze.active == true)
                {
                    if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].maze1.completed == true)
                    {
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 1f * 100;
                    }
                    else
                    {
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 0f * 100;
                    }


                }
                else if (activity.classActivityList[i].classData.activityList[j].visualHands.active == true)
                {
                    int completed = 0;
                    for (int k = 0; k < JsonUtility.FromJson<AdditionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].visualHands.abacusOperations.text).Add.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].visualHands1.completed[k] == true)
                            completed++;

                        classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / JsonUtility.FromJson<AdditionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].visualHands.abacusOperations.text).Add.Length) * 100;

                    }


                }

                else if (activity.classActivityList[i].classData.activityList[j].countBodyParts.active == true)
                {
                    int completed = 0;
                    for (int k = 0; k < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].countBodyParts1.completed.Length; k++)
                    {

                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].countBodyParts1.completed[k] == true)
                        {
                            completed++;
                        }
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].countBodyParts1.completed.Length) * 100;

                    }


                }
                else if (activity.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.active == true)
                {

                    int completed = 0;
                    for (int k = 0; k < JsonUtility.FromJson<MixedOpeartionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.jsonData.text).operation.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1.completed[k] == true)
                            completed++;

                        classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / JsonUtility.FromJson<MixedOpeartionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.jsonData.text).operation.Length) * 100;

                    }
                }
                else if (activity.classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.active == true)
                {
                    int completed = 0;
                    for (int k = 0; k < JsonUtility.FromJson<MultiplicationAndDivisionPuzzleJsonWrapper>(activity.classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.jsonData.text).questions.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1.completed[k] == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationDivisionPuzzle2.bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / JsonUtility.FromJson<MultiplicationAndDivisionPuzzleJsonWrapper>(activity.classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.jsonData.text).questions.Length) * 100;
                        else
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                    }



                }
                else if (activity.classActivityList[i].classData.activityList[j].mutliplicationOperation.active == true)
                {
                    int completed = 0;
                    for (int k = 0; k < JsonUtility.FromJson<MultiplicationJsonWrapper>(activity.classActivityList[i].classData.activityList[j].mutliplicationOperation.jsonData.text).Mul.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed[k] == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationOperation2.bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / JsonUtility.FromJson<MultiplicationJsonWrapper>(activity.classActivityList[i].classData.activityList[j].mutliplicationOperation.jsonData.text).Mul.Length) * 100;
                        else
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                    }



                }
                else if (activity.classActivityList[i].classData.activityList[j].liftBeeds.active == true)
                {

                    if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftBeeds2.bestTime == -1)
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 0;
                    else
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                }

                else if (activity.classActivityList[i].classData.activityList[j].divisionOperation.active == true)
                {
                    int completed = 0;
                    for (int k = 0; k < JsonUtility.FromJson<DivisionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].divisionOperation.jsonData.text).div.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1.completed[k] == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].divisionOperation2.bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / JsonUtility.FromJson<DivisionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].divisionOperation.jsonData.text).div.Length) * 100;
                        else
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                    }



                }
                else if (activity.classActivityList[i].classData.activityList[j].tutorialVideo.active == true)
                {
                    if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].tutorialVideo1.completed == true)
                    {
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 1f * 100;
                    }
                    else
                    {
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 0f * 100;
                    }
                    if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].tutorialVideo2.bestTime > 0)
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 1f * 100;

                }
                else if (activity.classActivityList[i].classData.activityList[j].animatingCountingTutorial.active == true)
                {
                    int completed = 0;

                    for (int k = 0; k < activity.classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1.completed[k] == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].animatingCountingTutorial2.bestTime == -1)
                        {
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 100f) / (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1.completed.Length);
                        }
                        else
                        {
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 1f * 100;
                        }

                    }

                }

                else if (activity.classActivityList[i].classData.activityList[j].sudokuGame.active == true)
                {
                    if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].sudokuGame1.completed == true)
                    {
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 1f * 100;
                    }
                    else
                    {
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 0f * 100;
                    }
                    if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].sudokuGame2.bestTime > 0)
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 1f * 100;

                }



                //print(i + "%age  " +j +"  "+ classProgresses[i].allActivityprogress[j].activitiesPercentage);
            }




            float totalProgress = 0;
            for (int j = 0; j < activity.classActivityList[i].classData.activityList.Count; j++)
            {
                totalProgress += classProgresses[i].allActivityprogress[j].activitiesPercentage;
            }

            classProgresses[i].TotalclassProgress = (totalProgress) / activity.classActivityList[i].classData.activityList.Count;

            //print(classProgresses[i].TotalclassProgress);
            classInstance.completionPercentage.Add(classProgresses[i].TotalclassProgress);

            if (i == 0)
                classInstance.cardIsIntractable.Add(true);
            else
            {
                if (classInstance.completionPercentage[i - 1] >= 100)
                {
                    classInstance.cardIsIntractable.Add(true);
                }
                else if (classInstance.completionPercentage[i] > 0)
                {
                    classInstance.cardIsIntractable.Add(true);

                }
                else
                    classInstance.cardIsIntractable.Add(false);
            }




        }
        classInstance.UpdateData();

        bookManager.exampleInstance.completionPercentage = new List<float>();
        bookManager.exampleInstance.cardIsIntractable = new List<bool>();
        for (int i = 0; i < bookManager.books.Count; i++)
        {
            float total = 0;
            for (int j = bookManager.books[i].statingClass; j <= bookManager.books[i].endingClass; j++)
            {
                total += classInstance.completionPercentage[j];
            }
            bookManager.exampleInstance.completionPercentage.Add(total / (bookManager.books[i].endingClass - bookManager.books[i].statingClass + 1));

            if (i == 0)
                bookManager.exampleInstance.cardIsIntractable.Add(true);
            else
            {
                if (bookManager.exampleInstance.completionPercentage[i - 1] >= 100)
                {
                    bookManager.exampleInstance.cardIsIntractable.Add(true);
                }
                else if (bookManager.exampleInstance.completionPercentage[i] > 0)
                {
                    bookManager.exampleInstance.cardIsIntractable.Add(true);
                }
                else
                    bookManager.exampleInstance.cardIsIntractable.Add(false);
            }

        }

    }

    public void CalculatePercentage()
    {
        startClass = 0;
        endClass = 0;
        classInstance.completionPercentage = new List<float>();
        classInstance.cardIsIntractable = new List<bool>();

        for (int t = 0; t < bookManager.books.Count; t++)
        {

            if (BookManager.currentBookName == bookManager.books[t].bookName)
            {
                startClass = bookManager.books[t].statingClass;
                endClass = bookManager.books[t].endingClass;
            }
        }


        for (int i = startClass; i < endClass + 1; i++)
        {
            for (int j = 0; j < activity.classActivityList[i].classData.activityList.Count; j++)
            {
                if (activity.classActivityList[i].classData.activityList[j].matchShapeWithNumbers.Length > 0)
                {
                    int completed = 0;
                    for (int k = 0; k < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[0].completed.Count; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchShapeWithNumbers[0].completed[k] == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchShapeWithNumbers[0].bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed / 3f) * 100;
                        else
                        {
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                        };
                    }

                }
                else if (activity.classActivityList[i].classData.activityList[j].matchValueWithImage.Length > 0)
                {

                    int completed = 0;
                    for (int k = 0; k < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage.Count; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage[k].completed == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].matchValueWithImage[0].bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].matchValueWithImage.Count) * 100;
                        else
                        {
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;

                        }
                    }

                }
                else if (activity.classActivityList[i].classData.activityList[j].abacusOperations.active == true)
                {

                    int completed = 0;
                    for (int k = 0; k < JsonUtility.FromJson<AdditionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].abacusOperations.jsonData.text).Add.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].abacusOperations[k] == true)
                            completed++;
                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].abacusOperations.bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / JsonUtility.FromJson<AdditionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].abacusOperations.jsonData.text).Add.Length) * 100;
                        else
                        {
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                        }
                    }

                }
                else if (activity.classActivityList[i].classData.activityList[j].coloringPageImages.Length > 0)
                {
                    int completed = 0;

                    if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].coloringPageImages)
                        completed = 1;
                    else completed = 0;

                    classProgresses[i].allActivityprogress[j].activitiesPercentage = completed * 100;

                }
                else if (activity.classActivityList[i].classData.activityList[j].speedWriting == true)
                {
                    int completed = 0;

                    if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].speedWriting)
                        completed = 1;
                    else completed = 0;

                    classProgresses[i].allActivityprogress[j].activitiesPercentage = completed * 100;

                }
                else if (activity.classActivityList[i].classData.activityList[j].liftBeed01 == true)
                {
                    int completed = 0;
                    for (int k = 0; k < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed01.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed01[k] == true)
                            completed++;

                        classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / 4) * 100;
                    }

                }
                else if (activity.classActivityList[i].classData.activityList[j].liftBeed02 == true)
                {
                    int completed = 0;
                    for (int k = 0; k < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed02.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].liftBeed02[k] == true)
                            completed++;

                        classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / 5) * 100;
                    }

                }
                else if (activity.classActivityList[i].classData.activityList[j].maze.active == true)
                {
                    if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].maze1.completed == true)
                    {
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 1f * 100;
                    }
                    else
                    {
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 0f * 100;
                    }


                }
                else if (activity.classActivityList[i].classData.activityList[j].visualHands.active == true)
                {
                    int completed = 0;
                    for (int k = 0; k < JsonUtility.FromJson<AdditionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].visualHands.abacusOperations.text).Add.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].visualHands1.completed[k] == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].visualHands2.bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / JsonUtility.FromJson<AdditionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].visualHands.abacusOperations.text).Add.Length) * 100;
                        else
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                    }


                }

                else if (activity.classActivityList[i].classData.activityList[j].countBodyParts.active == true)
                {
                    int completed = 0;
                    for (int k = 0; k < Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].countBodyParts1.completed.Length; k++)
                    {

                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].countBodyParts1.completed[k] == true)
                        {
                            completed++;
                        }
                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].countBodyParts2.bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].countBodyParts1.completed.Length) * 100;
                        else
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;


                    }
                }

                else if (activity.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.active == true)
                {

                    int completed = 0;
                    for (int k = 0; k < JsonUtility.FromJson<MixedOpeartionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.jsonData.text).operation.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].mixedMathematicalOperations1.completed[k] == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].mixedMathematicalOperations2.bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / JsonUtility.FromJson<MixedOpeartionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].mixedMathematicalOperations.jsonData.text).operation.Length) * 100;
                        else
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                    }
                }

                else if (activity.classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.active == true)
                {
                    int completed = 0;
                    for (int k = 0; k < JsonUtility.FromJson<MultiplicationAndDivisionPuzzleJsonWrapper>(activity.classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.jsonData.text).questions.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationDivisionPuzzle1.completed[k] == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationDivisionPuzzle2.bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / JsonUtility.FromJson<MultiplicationAndDivisionPuzzleJsonWrapper>(activity.classActivityList[i].classData.activityList[j].multiplicationDivisionPuzzle.jsonData.text).questions.Length) * 100;
                        else
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                    }



                }
                else if (activity.classActivityList[i].classData.activityList[j].mutliplicationOperation.active == true)
                {
                    int completed = 0;
                    for (int k = 0; k < JsonUtility.FromJson<MultiplicationJsonWrapper>(activity.classActivityList[i].classData.activityList[j].mutliplicationOperation.jsonData.text).Mul.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].multiplicationOperation1.completed[k] == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].multiplicationOperation2.bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / JsonUtility.FromJson<MultiplicationJsonWrapper>(activity.classActivityList[i].classData.activityList[j].mutliplicationOperation.jsonData.text).Mul.Length) * 100;
                        else
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                    }

                }

                else if (activity.classActivityList[i].classData.activityList[j].liftBeeds.active == true)
                {

                    if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].liftBeeds2.bestTime == -1)
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 0;
                    else
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                }


                else if (activity.classActivityList[i].classData.activityList[j].divisionOperation.active == true)
                {
                    int completed = 0;
                    for (int k = 0; k < JsonUtility.FromJson<DivisionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].divisionOperation.jsonData.text).div.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].divisionOperation1.completed[k] == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].divisionOperation2.bestTime == -1)
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 1f / JsonUtility.FromJson<DivisionJsonWrapper>(activity.classActivityList[i].classData.activityList[j].divisionOperation.jsonData.text).div.Length) * 100;
                        else
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 100f;
                    }



                }

                else if (activity.classActivityList[i].classData.activityList[j].tutorialVideo.active == true)
                {
                    if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].tutorialVideo1.completed == true)
                    {
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 1f * 100;
                    }
                    else
                    {
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 0f * 100;
                    }
                    if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].tutorialVideo2.bestTime > 0)
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 1f * 100;



                }
                else if (activity.classActivityList[i].classData.activityList[j].animatingCountingTutorial.active == true)
                {
                    int completed = 0;
                    for (int k = 0; k < activity.classActivityList[i].classData.activityList[j].animatingCountingTutorial.numbes.Length; k++)
                    {
                        if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1.completed[k] == true)
                            completed++;

                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].animatingCountingTutorial2.bestTime == -1)
                        {
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = (completed * 100f) / (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].animatingCountingTutorial1.completed.Length);

                        }
                        else
                        {
                            classProgresses[i].allActivityprogress[j].activitiesPercentage = 1f * 100;
                        }

                    }
                }

                else if (activity.classActivityList[i].classData.activityList[j].sudokuGame.active == true)
                {
                    if (Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].sudokuGame1.completed == true)
                    {
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 1f * 100;
                    }
                    else
                    {
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 0f * 100;
                    }
                    if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].sudokuGame2.bestTime > 0)
                        classProgresses[i].allActivityprogress[j].activitiesPercentage = 1f * 100;
                }
                //print(i + "%age  " +j +"  "+ classProgresses[i].allActivityprogress[j].activitiesPercentage);
            }




            float totalProgress = 0;
            for (int j = 0; j < activity.classActivityList[i].classData.activityList.Count; j++)
            {
                totalProgress += classProgresses[i].allActivityprogress[j].activitiesPercentage;
            }

            classProgresses[i].TotalclassProgress = (totalProgress) / activity.classActivityList[i].classData.activityList.Count;

            //print(classProgresses[i].TotalclassProgress);
            classInstance.completionPercentage.Add(classProgresses[i].TotalclassProgress);

            if (i % 12 == 0)
                classInstance.cardIsIntractable.Add(true);
            else
            {
                if (classInstance.completionPercentage[i % 12 - 1] >= 100)
                {
                    classInstance.cardIsIntractable.Add(true);
                }
                else if (classInstance.completionPercentage[i % 12] > 0)
                {
                    classInstance.cardIsIntractable.Add(true);
                }
                else
                    classInstance.cardIsIntractable.Add(false);
            }
        }
        classInstance.UpdateData();


        // bookManager.exampleInstance.completionPercentage = new List<float>();

        //for (int t = 0; t < bookManager.books.Count; t++)
        //{
        //    float total = 0;
        //    print(BookManager.currentBookName);
        //    if (BookManager.currentBookName == bookManager.books[t].bookName)
        //    {
        //        for (int j = 0; j <= bookManager.books[t].endingClass- bookManager.books[t].endingClass+1; j++)
        //        {
        //            total += classInstance.completionPercentage[j];
        //        }
        //        bookManager.exampleInstance.completionPercentage.Add(total / (bookManager.books[t].endingClass - bookManager.books[t].statingClass + 1));
        //    }
        //}

        // bookManager.exampleInstance.UpdateData();

        for (int i = 0; i < bookManager.books.Count; i++)
        {
            float total = 0;
            if (BookManager.currentBookName == bookManager.books[i].bookName)
            {
                for (int j = 0; j < bookManager.books[i].endingClass - bookManager.books[i].statingClass + 1; j++)
                {
                    total += classInstance.completionPercentage[j];
                }
                bookManager.exampleInstance.completionPercentage[i] = (total / (bookManager.books[i].endingClass - bookManager.books[i].statingClass + 1));

                //if (i == 0)
                //    bookManager.exampleInstance.cardIsIntractable[i] = true;
                //else
                //{
                //    if (bookManager.exampleInstance.completionPercentage[i - 1] >= 100)
                //    {
                //        bookManager.exampleInstance.cardIsIntractable[i - 1] = true;
                //    }
                //    else if (bookManager.exampleInstance.completionPercentage[i] > 0)
                //    {
                //        bookManager.exampleInstance.cardIsIntractable[i - 1] = true;
                //    }
                //    else
                //        bookManager.exampleInstance.cardIsIntractable[i - 1] = false;
                //}

            }
        }

        for (int i = 0; i < bookManager.books.Count; i++)
        {
            if (i == 0)
                bookManager.exampleInstance.cardIsIntractable[i] = true;
            else
            {
                if (bookManager.exampleInstance.completionPercentage[i - 1] >= 100)
                {
                    bookManager.exampleInstance.cardIsIntractable[i] = true;
                }
                else if (bookManager.exampleInstance.completionPercentage[i] > 0)
                {
                    bookManager.exampleInstance.cardIsIntractable[i] = true;
                }
                else
                    bookManager.exampleInstance.cardIsIntractable[i] = false;
            }
        }

        bookManager.exampleInstance.UpdateData();
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

    public void OnEnableAfterSomeDelay()
    {

        totalBook = bookManager.books.Count;
        totalClass = activity.classActivityList.Count;
        classProgresses = new List<ClassProgress>();
        bookProgress = new List<BookProgress>();
        for (int i = 0; i < totalClass; i++)
        {
            classProgresses.Add(new ClassProgress());
            classProgresses[i].allActivityprogress = new List<ActivityProgress>();
            for (int j = 0; j < activity.classActivityList[i].classData.activityList.Count; j++)
            {
                classProgresses[i].allActivityprogress.Add(new ActivityProgress());
            }

        }
        activity.CreateOrLodeDataFromDisk();
        activity.CreateOrLoadDailyWorkoutStatsDataFromDisk();

        CalculatePercentageAtBegining();

    }


}
