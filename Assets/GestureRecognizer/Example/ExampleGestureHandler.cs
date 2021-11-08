using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;
using System.Linq;
using System;
using Lean.Transition.Method;
using TMPro;
using Lean.Gui;

public class ExampleGestureHandler : MonoBehaviour
{

    public List<float> thresholdValue;
    public AudioClip correctRecognisedSound;
    public AudioClip wrongRecognisedSound;
    public Activity activityScriptInstance;
    public DrawDetector drawDetector;
    public Text textResult;
    public Text textInstrction;
    public List<int> Numbers;
    public List<string> instructions;
    public Transform referenceRoot;
    public int currentIndex;
    GesturePatternDraw[] references;
    public GameObject congratulationPannel;
    public LeanImageFillAmount loadingBar;
    public TextMeshProUGUI congratulationText;
    public TextMeshProUGUI notificationText;
    ActivityList1 activityList1;
    public Button notificationBtn;
    public Button notificationBtn2;
    public GameObject time;

    public GameObject sideNote;
    public GameObject handTutorial;
    public LeanToggle sideNoteLean;
    public LeanToggle congratulationLean;
    public LeanToggle notificationLean;
    public LeanToggle notificationLean2;
    public bool writingWithRightIsDone;
    public Recognizer recognizerInstance;
    public List<PatternList> listOf_AllPatterns;
    public Image eraserBtnBGImage;
    SpeedWriting2 speedWriting2;
    public int continuesWrongCount;
    bool isWrong;
    bool handDissapear;
    public GameObject stencileParent;
    public Image stencileImage;
    public GameObject blackScreen;
    public List<Sprite> stencilImages;


    void OnEnable()
    {

        Invoke("OnEnableWithDelay", 0.1f);

    }


    public void OnEnableWithDelay()
    {
        handDissapear = false;
        continuesWrongCount = 0;
        isWrong = false;
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].speedWriting == true && ClassManager.currentActivityIndex == j)
                    {
                        activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
                        speedWriting2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].speedWriting;

                        if (ClassManager.currentClassName == "Class 1")
                        {
                            notificationBtn2.onClick.AddListener(ShowTutorialAnimation);
                        }
                    }
                }
            }
        }
        references = referenceRoot.GetComponentsInChildren<GesturePatternDraw>();
        currentIndex = 0;
        drawDetector.scoreToAccept = thresholdValue[currentIndex];
        loadingBar.Data.FillAmount = (currentIndex / (20 * 1f));
        loadingBar.BeginAllTransitions();
        textResult.text = Numbers[currentIndex].ToString();
        textInstrction.text = "Let's try to write 0";
        notificationBtn.onClick.AddListener(StartTimer);
        notificationBtn.onClick.AddListener(OpenNotification2Lean);
        writingWithRightIsDone = false;
        //creating a new list of patterns in the instance of a recognizer script and loading the new list of patterns(hear it is patterns of 0).
        recognizerInstance.patterns = new List<GesturePattern>();
        for (int i = 0; i < listOf_AllPatterns[currentIndex].patterns.Count; i++)
        {
            recognizerInstance.patterns.Add(listOf_AllPatterns[currentIndex].patterns[i]);
        }


    }

    public void ShowTutorialAnimation()
    {
        notificationBtn2.onClick.RemoveListener(ShowTutorialAnimation);
        handTutorial.SetActive(true);
        blackScreen.SetActive(true);
        
        Invoke(nameof(HandShouldDissapear), 0.1f);

    }

    public void HandShouldDissapear()
    {
        handDissapear = true;
    }

    void StartTimer()
    {
        time.SetActive(true);
        Timer.savedTime = 0;
        notificationBtn.onClick.RemoveListener(StartTimer);

    }


    void ShowAll()
    {
        for (int i = 0; i < references.Length; i++)
        {
            references[i].gameObject.SetActive(true);
        }
    }

    public void OnRecognize(RecognitionResult result)
    {
        print("called");
        StopAllCoroutines();
        ShowAll();
        if (result != RecognitionResult.Empty)
        {
            continuesWrongCount = 0;

            if (Convert.ToInt32(result.gesture.id) == Numbers[currentIndex])
            {

                CancelInvoke("ShowElseMessage");
                textResult.fontSize = 300;
                if (currentIndex + 1 >= (Numbers.Count) && writingWithRightIsDone)
                {
                    textInstrction.text = instructions[currentIndex];
                    eraserBtnBGImage.color = Color.white;
                    //invoke congratulation box;
                    Invoke("invokeNotification", 0.2f);


                }
                else
                {
                    textInstrction.text = instructions[currentIndex];
                    eraserBtnBGImage.color = Color.white;

                }
                continuesWrongCount = 0;
                currentIndex++;
                if (currentIndex < thresholdValue.Count)
                    drawDetector.scoreToAccept = thresholdValue[currentIndex];
                Invoke("ClearLines", 0.6f);
                StartCoroutine(Blink(result.gesture.id));
                if (writingWithRightIsDone)
                {
                    SoundManager.Instance.Play(correctRecognisedSound);
                    loadingBar.Data.FillAmount = ((currentIndex + 10) / (20 * 1f));

                }
                else
                {
                    SoundManager.Instance.Play(correctRecognisedSound);
                    loadingBar.Data.FillAmount = (currentIndex / (20 * 1f));
                }
                loadingBar.BeginAllTransitions();

                // loading new set of patterns to the the list of patterns in  recognizer script instance ,based on the index value.
                if (currentIndex < listOf_AllPatterns.Count)
                {
                    recognizerInstance.patterns = new List<GesturePattern>();
                    for (int i = 0; i < listOf_AllPatterns[currentIndex].patterns.Count; i++)
                    {
                        recognizerInstance.patterns.Add(listOf_AllPatterns[currentIndex].patterns[i]);
                    }
                }





            }
        }
        else
        {

            isWrong = true;
            Invoke("ShowElseMessage", 0.8f);
            //  textInstrction.text = "CAN YOU DRAW IT AGAIN. ";
        }
    }

    void ShowElseMessage()
    {

        textInstrction.text = "Let’s try it once more ";
        eraserBtnBGImage.color = Color.green;
        SoundManager.Instance.Play(wrongRecognisedSound);

    }
    IEnumerator Blink(string id)
    {
        var draw = references.Where(e => e.pattern.id == id).FirstOrDefault();
        if (draw != null)
        {
            var seconds = new WaitForSeconds(0.1f);
            for (int i = 0; i <= 20; i++)
            {
                draw.gameObject.SetActive(i % 2 == 0);
                yield return seconds;
            }
            draw.gameObject.SetActive(true);
        }
    }
    public void ClearLines()
    {
        drawDetector.ClearLines();
        try
        {
            textResult.text = Numbers[currentIndex].ToString();/*result.gesture.id; + "\n" + Mathf.RoundToInt (result.score.score * 100) + "%";*/
        }
        catch { textResult.text = ""; }
    }

    void invokeNotification()
    {
        congratulationText.text = "Congratulations";
        congratulationLean.TurnOn();//congratulationPannel.SetActive(true);
        sideNoteLean.TurnOff();//sideNote.SetActive(false);
        activityList1.speedWriting = true;
        time.SetActive(false);
        if (speedWriting2.bestTime == -1)
        {
            print(223232);

            speedWriting2.bestTime = Timer.currentTime;
            speedWriting2.bestTime_string = Timer.timerText.text;
            SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
        }
        else if (speedWriting2.bestTime > Timer.currentTime)
        {
            print(111);
            speedWriting2.bestTime = Timer.currentTime;
            speedWriting2.bestTime_string = Timer.timerText.text;
            SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
        }
        SaveManager.Instance.saveDataToDisk(Activity.classParent);
    }


    private void Update()
    {
        //  print(speedWriting2.bestTime + " " + speedWriting2.bestTime_string + " " + speedWriting2.currentSavedTime);
        if (handTutorial.activeInHierarchy)
        {
            if (Input.touchCount > 0 && handDissapear)
            {
                handTutorial.SetActive(false);
                blackScreen.SetActive(false);

            }
        }


        if (currentIndex > 20 && writingWithRightIsDone)
        {
            congratulationLean.TurnOn();//congratulationPannel.SetActive(true);
            //congratulationText.text = "sorry you ran out of time";
            sideNoteLean.TurnOff();// sideNote.SetActive(false);
            speedWriting2.currentSavedTime = 0;
            print(999);
            if (speedWriting2.bestTime == -1)
            {
                print(223232);

                speedWriting2.bestTime = Timer.currentTime;
                speedWriting2.bestTime_string = Timer.timerText.text;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }
            else if (speedWriting2.bestTime > Timer.currentTime)
            {
                print(111);
                speedWriting2.bestTime = Timer.currentTime;
                speedWriting2.bestTime_string = Timer.timerText.text;
                SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
            }
        }


        else if (currentIndex == 10 && writingWithRightIsDone == false)
        {
            print("Write withleft hand");
            writingWithRightIsDone = true;
            currentIndex = 0;
            drawDetector.scoreToAccept = thresholdValue[currentIndex];
            recognizerInstance.patterns = new List<GesturePattern>();
            for (int i = 0; i < listOf_AllPatterns[currentIndex].patterns.Count; i++)
            {
                recognizerInstance.patterns.Add(listOf_AllPatterns[currentIndex].patterns[i]);
            }
            notificationLean.TurnOn();
            notificationText.text = "Now, Left hand";
            textInstrction.text = "Let's try to write 0 ";
            sideNoteLean.TurnOff();
            //turn on notification and sAY write with left hand.
        }

        if (continuesWrongCount != 2)
        {
            stencileParent.SetActive(false);
        }

    }

    public void OpenNotification2Lean()
    {
        notificationLean2.TurnOn();
        notificationBtn.onClick.RemoveListener(OpenNotification2Lean);
    }

    private void OnDisable()
    {
        notificationBtn.onClick.RemoveListener(StartTimer);

        try
        {
            notificationBtn.onClick.RemoveListener(OpenNotification2Lean);


        }
        catch
        {
            print("in try catch");
        }

        try
        {
            notificationBtn2.onClick.RemoveListener(ShowTutorialAnimation);
        }
        catch {; }
    }

    [System.Serializable]
    public class PatternList
    {
        public string patternName;
        public List<GesturePattern> patterns;
    }


    public void IncrementContinuesWrongCount()
    {
        if (isWrong)
        {
            isWrong = false; ;
            continuesWrongCount++;
        }


        if (continuesWrongCount == 2)
        {
            stencileImage.sprite = stencilImages[currentIndex % 10];
            stencileParent.SetActive(true);

        }

        if (continuesWrongCount > 2)
        {
            // currentIndex++;
            continuesWrongCount = 0;




            CancelInvoke("ShowElseMessage");
            textResult.fontSize = 300;
            if (currentIndex + 1 >= (Numbers.Count) && writingWithRightIsDone)
            {
                textInstrction.text = instructions[currentIndex].Replace("Congratulations.", "Well tried! You are doing very well!!\n");
                eraserBtnBGImage.color = Color.white;
                //invoke congratulation box;
                Invoke("invokeNotification", 0.2f);


            }
            else
            {
                textInstrction.text = instructions[currentIndex].Replace("Congratulations.", "Well tried! You are doing very well!!\n");
                eraserBtnBGImage.color = Color.white;

            }
            continuesWrongCount = 0;
            currentIndex++;
            if (currentIndex < thresholdValue.Count)
                drawDetector.scoreToAccept = thresholdValue[currentIndex];
            Invoke("ClearLines", 0.6f);
            // StartCoroutine(Blink(result.gesture.id));
            if (writingWithRightIsDone)
            {
                SoundManager.Instance.Play(correctRecognisedSound);
                loadingBar.Data.FillAmount = ((currentIndex + 10) / (20 * 1f));

            }
            else
            {
                SoundManager.Instance.Play(correctRecognisedSound);
                loadingBar.Data.FillAmount = (currentIndex / (20 * 1f));
            }
            loadingBar.BeginAllTransitions();

            // loading new set of patterns to the the list of patterns in  recognizer script instance ,based on the index value.
            if (currentIndex < listOf_AllPatterns.Count)
            {
                recognizerInstance.patterns = new List<GesturePattern>();
                for (int i = 0; i < listOf_AllPatterns[currentIndex].patterns.Count; i++)
                {
                    recognizerInstance.patterns.Add(listOf_AllPatterns[currentIndex].patterns[i]);
                }
            }



        }
    }

}
