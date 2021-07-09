using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignRandomCongratulationImage : MonoBehaviour
{

    public Activity activityScriptInstance;
    public List<Sprite> specialCongratulations;
    public List<Sprite> regularCongratulations;
    public List<AudioClip> regularCongratulationSound;
    public List<AudioClip> specialCongratulationSound;
    public Image congratulationImage;

    int r;
    int r2;
    public void AssignSpecialCongratulation()
    {
        r = Random.Range(0, specialCongratulations.Count);
        congratulationImage.sprite = specialCongratulations[r];

        r2 = Random.Range(0, specialCongratulationSound.Count);
        SoundManager.Instance.Play(specialCongratulationSound[r2]);

    }

    public void AssignRegularCongratulation()
    {
        r = Random.Range(0, regularCongratulations.Count);
        congratulationImage.sprite = regularCongratulations[r];
        
        r2 = Random.Range(0, regularCongratulationSound.Count);
        SoundManager.Instance.Play(regularCongratulationSound[r2]);

    }

    public void SelectTypeOfCongratulation()
    {
        bool isLastCard=false;
        for (int i = 0; i < activityScriptInstance.classActivityList.Count; i++)
        {
            if (ClassManager.currentClassName == activityScriptInstance.classActivityList[i].classData.nameOfClass)
            {
                int t = 0;

                t = activityScriptInstance.classActivityList[i].classData.activityList.Count;
                if (activityScriptInstance.classActivityList[i].classData.activityList[t - 1].activityName == ClassManager.currentActivityName)
                {
                    isLastCard = true;
                    AssignSpecialCongratulation();
                }
            }
        }

        if (isLastCard == false)
        {
            AssignRegularCongratulation();
        }
      

    }




}
