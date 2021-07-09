using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class TutorialVideo : MonoBehaviour
{
    public Activity activityScriptInstance;
    public VideoPlayer videoPlayer;

    public GameObject skipBtnGameObject;
    float lengthOfTheVedioInSeconds;
    public TutorialVideo2 videoStats;
    public TutorialVideo1 videoData;
    public GameObject videoLoadingScreen;
    bool startVideo;
    public GameObject videoPlayerCanvas;

    private void OnEnable()
    {
        startVideo = false;
        skipBtnGameObject.SetActive(false);

        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].tutorialVideo.active == true && ClassManager.currentActivityIndex == j)
                    {
                        videoPlayer.url = activityScriptInstance.classActivityList[i].classData.activityList[j].tutorialVideo.URL;
                        lengthOfTheVedioInSeconds = activityScriptInstance.classActivityList[i].classData.activityList[j].tutorialVideo.time;
                        videoStats = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].tutorialVideo2;
                        videoData = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j].tutorialVideo1;
                        if (Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].tutorialVideo2.bestTime > 0)
                        {
                            skipBtnGameObject.SetActive(true);
                        }
                        break;
                    }
                }
            }
        }

        Invoke("StartVideo", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (startVideo)
        {


            if (videoPlayer.time >= lengthOfTheVedioInSeconds - lengthOfTheVedioInSeconds + 5)
            {
                skipBtnGameObject.SetActive(true);
                videoStats.bestTime = 1;
                videoStats.bestTime_string = "Completed";
                videoData.completed = true;
                print("completed");
            }
            print(videoPlayer.time);

            if (videoPlayer.isPaused)
            {
                print("Pause");

            }
            else
                print("Play");
            if (videoPlayer.isPrepared)
            {
                videoLoadingScreen.SetActive(false);
                print("Dont Display loading screen");

            }
            else
            {
                videoLoadingScreen.SetActive(true);
                print("Display loading screen");
            }

        }

    }

    public void StartVideo()
    {
        startVideo = true;
        videoPlayerCanvas.SetActive(true);
    }

    private void OnDisable()
    {
        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);

        try
        {
            CancelInvoke("StartVideo");
        }
        catch
        {
            ;
        }

    }

}
