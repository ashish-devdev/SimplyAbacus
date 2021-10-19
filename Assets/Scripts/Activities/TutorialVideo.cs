using Lean.Gui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
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
    public Back BACK;
    bool videoCompleted;
    float tempTime = 0;
    string currentBook;
    string currentClass;
    string currentActivity;
    string remoteURL;


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
                        currentClass = ClassManager.currentClassName;
                        currentActivity = ClassManager.currentActivityIndex.ToString();

                        remoteURL = activityScriptInstance.classActivityList[i].classData.activityList[j].tutorialVideo.URL;
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
        //videoPlayer.Prepare();

       // Invoke("StartVideo", 4f);
        videoCompleted = false;
        tempTime = 0;
        StartCoroutine(this.loadVideoFromThisURL(remoteURL));



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

        if (videoPlayer.time >= lengthOfTheVedioInSeconds - 1 || videoCompleted)
        {
            videoCompleted = true;
            tempTime++;
            if (tempTime * Time.deltaTime > 2)
            {
                BACK.Escape();
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

        RenderTexture.active = videoPlayer.targetTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = null;
        try
        {
            CancelInvoke("StartVideo");
        }
        catch
        {
            ;
        }


    }


    private IEnumerator loadVideoFromThisURL(string _url)
    {
        StartVideo();
        string _pathToFile = Path.Combine(Application.persistentDataPath, "Video");
        _pathToFile = Path.Combine(_pathToFile, currentClass);
        _pathToFile = Path.Combine(_pathToFile, currentActivity + ".mp4");

        if (File.Exists(_pathToFile))
        {
            StartCoroutine(this.playVideoInThisURL(_pathToFile));
            yield return null;
        }
        else
        {

            UnityWebRequest _videoRequest = UnityWebRequest.Get(_url);
            yield return _videoRequest.SendWebRequest();

            if (_videoRequest.isDone == false || _videoRequest.error != null)
            {
                yield return null;
            }
            else
            {
                Debug.Log("Video Done - " + _videoRequest.isDone);
                byte[] _videoBytes = _videoRequest.downloadHandler.data;
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Video"));
                Directory.CreateDirectory(Path.Combine(Path.Combine(Application.persistentDataPath, "Video"),currentClass));
                File.Create(Path.Combine(Path.Combine(Path.Combine(Application.persistentDataPath, "Video"), currentClass),currentActivity+".mp4"));

                //FileStream fileStream = File.Open(_pathToFile, FileMode.Open, FileAccess.Write);
              

                int numAttempts = 0;
                int maxAttempts = 100;
                bool madeFile = false;
                while (!madeFile)
                {
                    try
                    {
                        File.WriteAllBytes(_pathToFile, _videoBytes);
                        madeFile = true;
                    }
                    catch (Exception)
                    {
                        numAttempts++;
                        if (numAttempts > maxAttempts)
                        {
                            madeFile = true;
                           // Debug.Log("Could not make file " + fName);
                        }
                    }
                    // Add a small delay to allow the file system to no longer be busy
                    yield return new WaitForSeconds(0.1f);
                }
              //  fileStream.Close();
                StartCoroutine(this.playVideoInThisURL(_pathToFile));
                yield return null;
            }
        }
    }

    private IEnumerator playVideoInThisURL(string _url)
    {
        videoPlayer.source = UnityEngine.Video.VideoSource.Url;
        videoPlayer.url = _url;
        videoPlayer.Prepare();

        while (videoPlayer.isPrepared == false)
        {  
            yield return null;
        }
        videoLoadingScreen.SetActive(false);
        videoPlayer.Play();

    }


}