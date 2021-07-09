using HighlightPlus;
using Lean.Gui;
using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Coloring : MonoBehaviour
{
    public GameObject notificationPannel;
    public GameObject congrtulationCanvas;
    public TextMeshProUGUI congrtulationText;
    public Activity activityScriptInstance;
    ClassActivityCompletionHolder classActivityCompletionHolder;
    public GameObject colorImagesParent;
    int childIndexValue;
    Ray ray;
    Camera cam;
    GameObject currentSelectedImagePeice;
    int totalImagePeices;
    int subtask;
    ColoringPageImages coloringPageImages;
    ActivityList1 activityList1;
    public LeanImageFillAmount loadingBar;
    public Button notificationBtn;
    public GameObject timer;

    HighlightEffect highlightEffect;

    public HighlightProfile Normalprofiler;
    public HighlightProfile errorProfiler;
    public LeanToggle congratulationLean;
    public LeanToggle sideNoteLean;
    public LeanMaterialFloat coloringShaderEffectLean;
    bool congratulationInvoked;
    ColoringPageImages2 coloringPageImages2;

    public AudioClip correctColorSound;
    public AudioClip wrongColorSound;
    private void OnEnable()
    {
        Invoke("OnEnableDelay", 0.1f);
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
    void Update()
    {
        if (/*mer.currentTime <= 0*/false)
        {
            congratulationLean.TurnOn();//congrtulationCanvas.SetActive(true);
            sideNoteLean.TurnOff();//notificationPannel.SetActive(false);
            congrtulationText.text = "Sorry you ran out of time.";
            return;

        }

      //  print(coloringPageImages2.bestTime + " " + coloringPageImages2.bestTime_string + " " + coloringPageImages2.currentSavedTime);


        if (!Extensions.IsPointerOverUIObject())
        {


            if (Input.GetMouseButtonUp(0) && ChangeColorPaletHighlightPosition.isDraggedFromColorPalet == true)
            {

                print("10");

                ChangeColorPaletHighlightPosition.isDraggedFromColorPalet = false;
                ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    print("20");

                    if (hit.collider.gameObject.CompareTag("Coloring3DImage"))
                    {
                        print(30);
                        currentSelectedImagePeice = hit.collider.gameObject;
                        //print(hit.collider.gameObject.name);


                        if (currentSelectedImagePeice.transform.GetChild(0).GetComponent<TextMeshPro>().text.Trim() == ColorPaletManager.currentColorIndex.ToString())
                        {
                            print(40);

                            if (currentSelectedImagePeice.GetComponent<Renderer>().material.color == Color.white)
                            {
                                print(50);

                                SoundManager.Instance.Play(correctColorSound);
                                subtask++;
                                loadingBar.Data.FillAmount = ((subtask) / (totalImagePeices * 1f));
                                loadingBar.BeginAllTransitions();
                                //loadingBar.Data.FillAmount = ((subtask) / (totalImagePeices * 1f)) * 500;
                                //loadingBar.BeginAllTransitions();

                                currentSelectedImagePeice.GetComponent<HighlightEffect>().ProfileLoad(Normalprofiler);
                                currentSelectedImagePeice.GetComponent<HighlightEffect>().enabled = true;
                                currentSelectedImagePeice.transform.GetChild(0).gameObject.SetActive(false);
                                Invoke("TurnOffOuterHighlight", 0.2f);
                                currentSelectedImagePeice.GetComponent<Renderer>().material.color = ColorPaletManager.currentColor;
                                coloringShaderEffectLean.Data.Target = currentSelectedImagePeice.GetComponent<Renderer>().material;

                                coloringShaderEffectLean.BeginAllTransitions();
                                currentSelectedImagePeice.GetComponent<MeshCollider>().enabled = false;
                            }
                        }
                        else
                        {
                            currentSelectedImagePeice.GetComponent<HighlightEffect>().ProfileLoad(errorProfiler);
                            SoundManager.Instance.Play(wrongColorSound);
                            currentSelectedImagePeice.GetComponent<HighlightEffect>().enabled = true;
                            Invoke("TurnOffOuterHighlight", 0.2f);

                        }
                    }
                    else
                    {
                       print( hit.collider.gameObject.name);
                    }
                }
            }
        }
        if (subtask == totalImagePeices)
        {
            if (congratulationInvoked == false)
            {

                if (coloringPageImages != null)
                {
                    Invoke("InvokeCongratulation", 1.5f);
                    congratulationInvoked = true;
                    coloringPageImages2.currentSavedTime = 0;
                    if (coloringPageImages2.bestTime == -1)
                    {
                        coloringPageImages2.bestTime = Timer.currentTime;
                        coloringPageImages2.bestTime_string = Timer.timerText.text;
                        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                    }
                    else if (coloringPageImages2.bestTime > Timer.currentTime)
                    {
                        coloringPageImages2.bestTime = Timer.currentTime;
                        coloringPageImages2.bestTime_string = Timer.timerText.text;
                        SaveManager.Instance.SaveStatsToDisk(Activity.classParentsStats);
                    }

                    /*congratulationLean.TurnOn();// congrtulationCanvas.SetActive(true);
                    congrtulationText.text = "Wow,That looks good.Let's move to the next acticity.";
                    sideNoteLean.TurnOff();// notificationPannel.SetActive(false);
                    coloringPageImages.completed = true;
                    activityList1.coloringPageImages = true;
                    SaveManager.Instance.saveDataToDisk(Activity.classParent);
                    timer.SetActive(false);*/

                }
            }

        }
        else
        {
            if (timer.activeInHierarchy)
                coloringPageImages2.currentSavedTime = Timer.currentTime;

        }

    }


    public void TurnOffOuterHighlight()
    {
        CancelInvoke("TurnOffOuterHighlight");
        currentSelectedImagePeice.GetComponent<HighlightEffect>().ProfileLoad(Normalprofiler);
        currentSelectedImagePeice.GetComponent<HighlightEffect>().enabled = false;


    }



    public void InvokeCongratulation()
    {
        CancelInvoke("InvokeCongratulation");
        congratulationLean.TurnOn();// congrtulationCanvas.SetActive(true);
        congrtulationText.text = "Wow, That looks good. Let's move to the next acticity.";
        sideNoteLean.TurnOff();// notificationPannel.SetActive(false);
        coloringPageImages.completed = true;
        activityList1.coloringPageImages = true;
        SaveManager.Instance.saveDataToDisk(Activity.classParent);
        timer.SetActive(false);

    }

    public void OnEnableDelay()
    {

        congratulationInvoked = false;
        subtask = 0;

        totalImagePeices = 0;
        cam = Camera.main;
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                for (int j = 0; j < activityScriptInstance.classActivityList[i].classData.activityList.Count; j++)
                {
                    if (activityScriptInstance.classActivityList[i].classData.activityList[j].coloringPageImages.Length > 0 && ClassManager.currentActivityIndex == j)
                    {
                        for (int k = 0; k < activityScriptInstance.classActivityList[i].classData.activityList[j].coloringPageImages.Length; k++)
                        {
                            coloringPageImages = activityScriptInstance.classActivityList[i].classData.activityList[j].coloringPageImages[k];
                            activityList1 = Activity.classParent.classActivityCompletionHolderList[i].classData.activityList[j];
                            coloringPageImages2 = Activity.classParentsStats.classActivityCompletionHolderList2[i].classData.activityList[j].coloringPageImages;
                            childIndexValue = activityScriptInstance.classActivityList[i].classData.activityList[j].coloringPageImages[k].index;
                            for (int z = 0; z < colorImagesParent.transform.childCount; z++)
                            {
                                if (z == childIndexValue)
                                {
                                    colorImagesParent.transform.GetChild(z).gameObject.SetActive(true);

                                    for (int p = 0; p < colorImagesParent.transform.GetChild(childIndexValue).childCount; p++)
                                    {
                                        if (colorImagesParent.transform.GetChild(childIndexValue).GetChild(p).transform.CompareTag("ColoringImageSet"))
                                        {
                                            for (int t = 0; t < colorImagesParent.transform.GetChild(childIndexValue).GetChild(p).childCount; t++)
                                            {

                                                totalImagePeices++;
                                                colorImagesParent.transform.GetChild(childIndexValue).GetChild(p).GetChild(t).GetComponent<Renderer>().material.color = Color.white;
                                                colorImagesParent.transform.GetChild(childIndexValue).GetChild(p).GetChild(t).GetComponent<Renderer>().material.SetFloat("_Cutoff", 1f);
                                                colorImagesParent.transform.GetChild(childIndexValue).GetChild(p).GetChild(t).GetComponent<MeshCollider>().enabled = true;
                                                colorImagesParent.transform.GetChild(childIndexValue).GetChild(p).GetChild(t).GetChild(0).gameObject.SetActive(true);

                                            }

                                        }

                                    }
                                }
                                else
                                {
                                    colorImagesParent.transform.GetChild(z).gameObject.SetActive(false);
                                }
                            }
                        }
                    }
                }
            }
        }

        loadingBar.Data.FillAmount = ((subtask) / (totalImagePeices * 1f));
        loadingBar.BeginAllTransitions();
        notificationBtn.onClick.AddListener(StartTimer);

    }

}
