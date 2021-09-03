using UnityEngine;
using System.Collections;
using Lean.Gui;
using UnityEngine.UI;

//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>
public class RollerBall : MonoBehaviour
{

    public GameObject ViewCamera = null;
    public AudioClip JumpSound = null;
    public AudioClip HitSound = null;
    public AudioClip CoinSound = null;

    private Rigidbody mRigidBody = null;
    private AudioSource mAudioSource = null;
    private bool mFloorTouched = false;
    public Activity activityScriptInstance;
    private ActivityList1 activityList1;
    private ActivityList2 activityList2;
    public LeanToggle congratulationLean;
    public LeanToggle sidenoteLean;
    public LeanToggle notificationLean;
    public GameObject timer;
    public GameObject mazeParent;
    public GameObject blurrImageCanvas;
    public Button notificationBtn;
    public BallMovement ballMovement;

   

    public int totalMaze;
    public int completedCount;

    public GameObject joystick;


    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
        mAudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].maze.active == true && ClassManager.currentActivityIndex == j)
                    {
                        activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
                        activityList2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j];
                        completedCount = 0;
                        totalMaze = activityScriptInstance.classActivityList[i].classData.activityList[j].maze.totalMaze;
                    }
                }
            }
        }

        notificationBtn.onClick.AddListener(DisableBlurrImageCanvas);

    }

    public void DisableBlurrImageCanvas()
    {
        blurrImageCanvas.SetActive(false);
        this.gameObject.GetComponent<TrailRenderer>().enabled = true;
        StopTakingInput();
        this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y, 19.08f);

    }

    void FixedUpdate()
    {
        // print(activityList2.maze2.bestTime + " " + activityList2.maze2.bestTime_string + " " + activityList2.maze2.currentSavedTime);



        if (mRigidBody != null)
        {
            if (Input.GetButton("Horizontal"))
            {
                mRigidBody.AddTorque(Vector3.back * Input.GetAxis("Horizontal") * 10);
            }
            if (Input.GetButton("Vertical"))
            {
                mRigidBody.AddTorque(Vector3.right * Input.GetAxis("Vertical") * 10);
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (mAudioSource != null && JumpSound != null)
                {
                    mAudioSource.PlayOneShot(JumpSound);
                }
                mRigidBody.AddForce(Vector3.up * 200);
            }
        }
        if (ViewCamera != null)
        {
            Vector3 direction = (Vector3.up * 2 + Vector3.back) * 2;
            RaycastHit hit;
            Debug.DrawLine(transform.position, transform.position + direction, Color.red);
            if (Physics.Linecast(transform.position, transform.position + direction, out hit))
            {
                ViewCamera.transform.position = hit.point;
            }
            else
            {
                ViewCamera.transform.position = transform.position + direction;
            }
            ViewCamera.transform.LookAt(transform.position);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag.Equals("Floor"))
        {
            mFloorTouched = true;
            if (mAudioSource != null && HitSound != null && coll.relativeVelocity.y > .5f)
            {
                mAudioSource.PlayOneShot(HitSound, coll.relativeVelocity.magnitude);
            }
        }
        else
        {
            if (mAudioSource != null && HitSound != null && coll.relativeVelocity.magnitude > 2f)
            {
                mAudioSource.PlayOneShot(HitSound, coll.relativeVelocity.magnitude);
            }
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag.Equals("Floor"))
        {
            mFloorTouched = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Coin"))
        {
            completedCount++;

            if (completedCount >= totalMaze)
            {

                sidenoteLean.TurnOff();
                congratulationLean.TurnOn();

                activityList1.maze1.completed = true;
                timer.SetActive(false);
                SaveManager.Instance.saveDataToDisk(Activity.classParent);
                activityList2.maze2.currentSavedTime = 0;
                if (activityList2.maze2.bestTime == -1)
                {
                    activityList2.maze2.bestTime = Timer.currentTime;
                    activityList2.maze2.bestTime_string = Timer.timerText.text;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }
                else if (activityList2.maze2.bestTime > Timer.currentTime)
                {
                    print(5555);
                    activityList2.maze2.bestTime = Timer.currentTime;
                    activityList2.maze2.bestTime_string = Timer.timerText.text;
                    SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                }
                if (mAudioSource != null && CoinSound != null)
                {
                    mAudioSource.PlayOneShot(CoinSound);
                }
                Destroy(other.gameObject);
            }
            else
            {
                if (mAudioSource != null && CoinSound != null)
                {
                    mAudioSource.PlayOneShot(CoinSound);
                }
                Destroy(other.gameObject);
                Invoke(nameof(EnableLoadingScreen), 0.7f);
                Invoke(nameof(DisableMazeParent), 1.5f);
                Invoke(nameof(EnableMazeParent), 1.51f);
              //  Invoke(nameof(StopTakingInput), 3.51f);

            }
        }
    }


    public void EnableMazeParent()
    {
        mazeParent.SetActive(true);
      //  joystick.SetActive(true);



    }
    public void EnableLoadingScreen()
    {
        this.gameObject.GetComponent<TrailRenderer>().enabled = false;
        blurrImageCanvas.SetActive(true);   
     

    }

    public void DisableMazeParent()
    {
        ballMovement.stopTakingInput = true;
        mazeParent.SetActive(false);
        notificationLean.TurnOn();
        sidenoteLean.TurnOff();
    //    joystick.SetActive(false);

    }

    public void StopTakingInput()
    { 
        ballMovement.stopTakingInput = false;

    }

    private void OnDisable()
    {
     
        blurrImageCanvas.SetActive(false);
        this.gameObject.GetComponent<TrailRenderer>().enabled = true;
        ballMovement.stopTakingInput = false;
        StopTakingInput();

        notificationBtn.onClick.RemoveListener(DisableBlurrImageCanvas);

        try
        {
            CancelInvoke(nameof(EnableLoadingScreen));
            CancelInvoke(nameof(DisableMazeParent));
            CancelInvoke(nameof(EnableMazeParent));
            CancelInvoke(nameof(StopTakingInput));
        }
        catch 
        {
            ;
        }
    }
}


