using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames.Sudoku;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Sudoku : MonoBehaviour
{
    public List<TextAsset> _4x4_Easy;
    public List<TextAsset> _4x4_Medium;
    public List<TextAsset> _4x4_Hard;

    public List<TextAsset> _6x6_Easy;
    public List<TextAsset> _6x6_Medium;
    public List<TextAsset> _6x6_Hard;

    public List<TextAsset> _8x8_Easy;
    public List<TextAsset> _8x8_Medium;
    public List<TextAsset> _8x8_Hard;

    public List<TextAsset> _9x9_Easy;
    public List<TextAsset> _9x9_Medium;
    public List<TextAsset> _9x9_Hard;

    public GameManager sudokuGameManager;
    public Activity activityScriptInstance;

    public GameObject timer;
    public Button notificationBtn;
    public BizzyBeeGames.SaveManager saveManager;

    [SerializeField]
    string gameMode;
    string mazeSize;
    string directory;
    bool continueGame;

    public void OnEnable()
    {
        Invoke(nameof(OnEnableAfterDelay), 0.1f);
    }

    public void OnEnableAfterDelay()
    {
        continueGame = false;
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].sudokuGame.active == true && activityScriptInstance.classActivityList[i].classData.activityList[j].activityName == ClassManager.currentActivityName && ClassManager.currentActivityIndex == j)
                    {

                        gameMode = activityScriptInstance.classActivityList[i].classData.activityList[j].sudokuGame.sudokuDifficultiMode.ToString();
                        mazeSize = activityScriptInstance.classActivityList[i].classData.activityList[j].sudokuGame.mazeSize.ToString();

                        int userCount = Directory.GetDirectories(Application.persistentDataPath + "/User").Length;
                        for (int t = 0; t < userCount; t++)
                        {


                            ClassUserInformation classUser = new ClassUserInformation();
                            if (File.Exists(Directory.GetDirectories(Application.persistentDataPath + "/User")[t] + "/AppUserInformation.dat"))
                            {
                                BinaryFormatter bf = new BinaryFormatter();
                                FileStream file;

                                file = File.Open(Directory.GetDirectories(Application.persistentDataPath + "/User")[t] + "/AppUserInformation.dat", FileMode.Open);
                                classUser = (ClassUserInformation)bf.Deserialize(file);
                                file.Close();
                                if (classUser.currentUser == true)
                                {
                                    directory = Directory.GetDirectories(Application.persistentDataPath + "/User")[t] + "/Sudoku" + "/" + activityScriptInstance.classActivityList[i].classData.activityList[j].iD;

                                    if (Directory.Exists((Directory.GetDirectories(Application.persistentDataPath + "/User")[t] + "/Sudoku" + "/" + activityScriptInstance.classActivityList[i].classData.activityList[j].iD)))
                                    {
                                        directory = Directory.GetDirectories(Application.persistentDataPath + "/User")[t] + "/Sudoku" + "/" + activityScriptInstance.classActivityList[i].classData.activityList[j].iD;
                                        string str = saveManager.SaveFolderPath;
                                        saveManager.CallAllInitLoadSave();
                                        continueGame = true;
                                    }
                                }

                            }

                        }

                    }
                }
            }
        }

        notificationBtn.onClick.AddListener(StartTimer);
        AssigneListOfMaze(mazeSize);

        if (continueGame)
            sudokuGameManager.ContinueActiveGame();
        else
            sudokuGameManager.PlayNewGame(gameMode);
    }


    void Update()
    {
       
    }

    private void OnDisable()
    {


        notificationBtn.onClick.RemoveListener(StartTimer);

    }


    public void StartTimer()
    {
        Timer.savedTime = 0;
        timer.SetActive(true);
    }

    public void AssigneListOfMaze(string mazeSize)
    {
        if (string.Compare(mazeSize, "_4x4") == 0)
        {
            sudokuGameManager.puzzleGroups[0].puzzleFiles = new List<TextAsset>();
            for (int i = 0; i < _4x4_Easy.Count; i++)
            {
                sudokuGameManager.puzzleGroups[0].puzzleFiles.Add(_4x4_Easy[i]);
            }

            sudokuGameManager.puzzleGroups[1].puzzleFiles = new List<TextAsset>();
            for (int i = 0; i < _4x4_Medium.Count; i++)
            {
                sudokuGameManager.puzzleGroups[1].puzzleFiles.Add(_4x4_Medium[i]);
            }

            sudokuGameManager.puzzleGroups[2].puzzleFiles = new List<TextAsset>();
            for (int i = 0; i < _4x4_Hard.Count; i++)
            {
                sudokuGameManager.puzzleGroups[2].puzzleFiles.Add(_4x4_Hard[i]);
            }
        }

        else if (string.Compare(mazeSize, "_6x6") == 0)
        {
            sudokuGameManager.puzzleGroups[0].puzzleFiles = new List<TextAsset>();
            for (int i = 0; i < _6x6_Easy.Count; i++)
            {
                sudokuGameManager.puzzleGroups[0].puzzleFiles.Add(_6x6_Easy[i]);
            }

            sudokuGameManager.puzzleGroups[1].puzzleFiles = new List<TextAsset>();
            for (int i = 0; i < _6x6_Medium.Count; i++)
            {
                sudokuGameManager.puzzleGroups[1].puzzleFiles.Add(_6x6_Medium[i]);
            }

            sudokuGameManager.puzzleGroups[2].puzzleFiles = new List<TextAsset>();
            for (int i = 0; i < _6x6_Hard.Count; i++)
            {
                sudokuGameManager.puzzleGroups[2].puzzleFiles.Add(_6x6_Hard[i]);
            }
        }

        else if (string.Compare(mazeSize, "_8x8") == 0)
        {
            sudokuGameManager.puzzleGroups[0].puzzleFiles = new List<TextAsset>();
            for (int i = 0; i < _8x8_Easy.Count; i++)
            {
                sudokuGameManager.puzzleGroups[0].puzzleFiles.Add(_8x8_Easy[i]);
            }

            sudokuGameManager.puzzleGroups[1].puzzleFiles = new List<TextAsset>();
            for (int i = 0; i < _8x8_Medium.Count; i++)
            {
                sudokuGameManager.puzzleGroups[1].puzzleFiles.Add(_8x8_Medium[i]);
            }

            sudokuGameManager.puzzleGroups[2].puzzleFiles = new List<TextAsset>();
            for (int i = 0; i < _8x8_Hard.Count; i++)
            {
                sudokuGameManager.puzzleGroups[2].puzzleFiles.Add(_8x8_Hard[i]);
            }
        }

        else if (string.Compare(mazeSize, "_9x9") == 0)
        {
            sudokuGameManager.puzzleGroups[0].puzzleFiles = new List<TextAsset>();
            for (int i = 0; i < _9x9_Easy.Count; i++)
            {
                sudokuGameManager.puzzleGroups[0].puzzleFiles.Add(_9x9_Easy[i]);
            }

            sudokuGameManager.puzzleGroups[1].puzzleFiles = new List<TextAsset>();
            for (int i = 0; i < _9x9_Medium.Count; i++)
            {
                sudokuGameManager.puzzleGroups[1].puzzleFiles.Add(_9x9_Medium[i]);
            }

            sudokuGameManager.puzzleGroups[2].puzzleFiles = new List<TextAsset>();
            for (int i = 0; i < _9x9_Hard.Count; i++)
            {
                sudokuGameManager.puzzleGroups[2].puzzleFiles.Add(_9x9_Hard[i]);
            }
        }
    }

    public void DeleteSudokuFile()
    {
        if (Directory.Exists(directory))
            Directory.Delete(directory, true);

    }
}
