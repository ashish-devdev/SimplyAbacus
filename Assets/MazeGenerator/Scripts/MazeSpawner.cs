using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour
{
    public enum MazeGenerationAlgorithm
    {
        PureRecursive,
        RecursiveTree,
        RandomTree,
        OldestTree,
        RecursiveDivision,
    }

    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject Pillar = null;
    public int Rows = 5;
    public int Columns = 5;
    public float CellWidth = 5;
    public float CellHeight = 5;
    public bool AddGaps = true;
    public GameObject GoalPrefab = null;
    public GameObject ball;
    Vector3 temp;
    public Button notificationBtn;
    public GameObject timer;

    public int begingSponingCol;
    public int begingSponingRow;
    public int endingSponingCol;
    public int endingSponingRow;
    public List<GoalCell> listOfPossibleCellSponningGoals;
    public Activity activityScriptInstance;

    public TextMeshProUGUI notification;
    public TextMeshProUGUI sideNote;
    public MazeScriptableInputs mazeScriptableInputs;
    public TrailRenderer trailRenderer;
    public GameObject mageBG_Plane;
    int R;
    [System.Serializable]
    public class GoalCell
    {
        public int row;
        public int col;
        public float x;
        public float z;
    }

    public bool sponed;
    int randomVal;

    private BasicMazeGenerator mMazeGenerator = null;



    public void StartTimer()
    {
        Timer.savedTime = 0;
        timer.SetActive(true);

    }

    void OnEnable()
    {

        R = Random.Range(0, mazeScriptableInputs.mazeDatas.Length);




        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].maze.active == true && ClassManager.currentActivityIndex == j)
                    {

                        mageBG_Plane.GetComponent<Renderer>().material.SetTexture("_MainTex", mazeScriptableInputs.mazeDatas[R].background.texture);
                        trailRenderer.material.color = mazeScriptableInputs.mazeDatas[R].trailColor;
                        notification.text = mazeScriptableInputs.mazeDatas[R].noteMessage;//activityScriptInstance.classActivityList[i].classData.activityList[j].maze.sideNoteMessage;
                        sideNote.text = mazeScriptableInputs.mazeDatas[R].noteMessage;//activityScriptInstance.classActivityList[i].classData.activityList[j].maze.sideNoteMessage;
                        Floor.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mazeScriptableInputs.mazeDatas[R].background; //activityScriptInstance.classActivityList[i].classData.activityList[j].maze.floor;
                        GoalPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mazeScriptableInputs.mazeDatas[R].endSprite; //activityScriptInstance.classActivityList[i].classData.activityList[j].maze.goalImage;
                        ball.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mazeScriptableInputs.mazeDatas[R].startSprite;//activityScriptInstance.classActivityList[i].classData.activityList[j].maze.startingImage;
                        Rows = activityScriptInstance.classActivityList[i].classData.activityList[j].maze.row;
                        Columns = activityScriptInstance.classActivityList[i].classData.activityList[j].maze.col;
                        endingSponingCol = activityScriptInstance.classActivityList[i].classData.activityList[j].maze.endingSponingcol;
                        begingSponingCol = activityScriptInstance.classActivityList[i].classData.activityList[j].maze.beginingSponingCol;

                        begingSponingRow = activityScriptInstance.classActivityList[i].classData.activityList[j].maze.beginingSponingrow;
                        endingSponingRow = activityScriptInstance.classActivityList[i].classData.activityList[j].maze.endingSponingRow;

                    }
                }
            }
        }


        if (Rows == 10)
        {
            mageBG_Plane.transform.localPosition = new Vector3(0.74f, 4.22f, 19.89f);
        }
        else if (Rows == 9)
        {
            mageBG_Plane.transform.localPosition = new Vector3(0.58f, 4.22f, 19.89f);

        }
        else if (Rows == 8)
        {
            mageBG_Plane.transform.localPosition = new Vector3(0.51f, 4.15f, 19.89f);

        }
        else if (Rows == 7)
        {
            mageBG_Plane.transform.localPosition = new Vector3(0.28f, 4.08f, 19.89f);

        }
        else if (Rows == 5)
        {
            mageBG_Plane.transform.localPosition = new Vector3(-0.18f, 3.89f, 19.89f);
        }
        else if (Rows == 6)
        {
            mageBG_Plane.transform.localPosition = new Vector3(0.08f, 3.99f, 19.89f);

        }


        notificationBtn.onClick.AddListener(StartTimer);

        listOfPossibleCellSponningGoals = new List<GoalCell>();
        sponed = false;
        if (!FullRandom)
        {
            Random.seed = RandomSeed;
        }
        switch (Algorithm)
        {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveTree:
                mMazeGenerator = new RecursiveTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RandomTree:
                mMazeGenerator = new RandomTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.OldestTree:
                mMazeGenerator = new OldestTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveDivision:
                mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
                break;
        }
        mMazeGenerator.GenerateMaze();
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;
                tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                tmp.transform.parent = transform;
                if (cell.WallRight)
                {
                    tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
                    tmp.transform.parent = transform;
                }
                if (cell.WallFront)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
                    tmp.transform.parent = transform;
                }
                if (cell.WallLeft)
                {
                    tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
                    tmp.transform.parent = transform;
                }
                if (cell.WallBack)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
                    tmp.transform.parent = transform;
                }
                if (cell.IsGoal && GoalPrefab != null)
                {
                    if (row >= begingSponingRow && row <= endingSponingRow)
                        if (column >= begingSponingCol && column <= endingSponingCol)
                        {
                            listOfPossibleCellSponningGoals.Add(new GoalCell() { col = column, row = row, x = x, z = z });  // adding all the  spoing location within the col and row provided.

                            //tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                            //tmp.transform.parent = transform;

                        }

                }
                if (row == Rows - 1 && column == Columns - 1)  //checking if its the last round of the double for loop.
                {
                    randomVal = Random.Range(0, listOfPossibleCellSponningGoals.Count);   // getting the random value between 0 to number of element of the list.
                    try
                    {
                        tmp = Instantiate(GoalPrefab, new Vector3(listOfPossibleCellSponningGoals[randomVal].x, 1, listOfPossibleCellSponningGoals[randomVal].z), Quaternion.Euler(0, 0, 0)) as GameObject;  //spooning only one goal at randomly seclect location.
                        tmp.transform.parent = transform;
                    }
                    catch
                    {
                        // Start();

                        tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                        tmp.transform.parent = transform;
                    }

                }
            }
        }



        if (Pillar != null)
        {
            for (int row = 0; row < Rows + 1; row++)
            {
                for (int column = 0; column < Columns + 1; column++)
                {
                    float x = column * (CellWidth + (AddGaps ? .2f : 0));
                    float z = row * (CellHeight + (AddGaps ? .2f : 0));
                    GameObject tmp = Instantiate(Pillar, new Vector3(x - CellWidth / 2, 0, z - CellHeight / 2), Quaternion.identity) as GameObject;
                    tmp.transform.parent = transform;
                }
            }
        }

        ball.transform.localPosition = new Vector3((-3.922755f - (0.15f) * 10 / Rows) + 3.5f, 0.6393967f, 19.64454f);

        this.gameObject.transform.localEulerAngles = new Vector3(-90, 0, 0);
        this.gameObject.transform.localScale = new Vector3(0.2f * 10 / Rows, 0.2f * 10 / Columns, 0.2f);
        temp = transform.localPosition;
        transform.localPosition = new Vector3(transform.localPosition.x - 12, transform.localPosition.y - 100, 3.1f);

    }

    private void OnDisable()
    {
        notificationBtn.onClick.RemoveListener(StartTimer);
        this.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        this.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        ball.transform.localPosition = new Vector3(-3.922755f, 0.2093967f, 19.64454f);
    }



}
