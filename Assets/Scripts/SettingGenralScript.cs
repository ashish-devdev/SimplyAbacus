using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SettingGenralScript : MonoBehaviour
{
    public SaveManager saveManager;
    public ProgressLoader progressLoader;
    public Activity activity;
    public void ClearDailyWorkout()
    {




        for (int i = 0; i < 3; i++)
        {
            try
            {
                string path = Directory.GetDirectories(Application.persistentDataPath + "/User")[i];
                if (Directory.Exists(path))
                {
                    ClassUserInformation classUser = new ClassUserInformation();
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file;
                    file = File.Open(path + "/AppUserInformation.dat", FileMode.Open);
                    classUser = (ClassUserInformation)bf.Deserialize(file);
                    file.Close();
                    if (classUser.currentUser)
                    {
                        File.Delete(path + "/AppUserDailyworkoutstats.dat");
                        // saveManager.dailyWorkoutStatsPath = Directory.GetDirectories(path) + "/AppUserDailyworkoutstats.dat";
                        activity.CreateOrLoadDailyWorkoutStatsDataFromDisk();
                    }
                }
            }
            catch
            {
                ;
            }

        }
    }

    public void ClearBookProgress()
    {




        for (int i = 0; i < 3; i++)
        {
            try
            {
                string path = Directory.GetDirectories(Application.persistentDataPath + "/User")[i];
                if (Directory.Exists(path))
                {
                    ClassUserInformation classUser = new ClassUserInformation();
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file;
                    file = File.Open(path + "/AppUserInformation.dat", FileMode.Open);
                    classUser = (ClassUserInformation)bf.Deserialize(file);
                    file.Close();
                    if (classUser.currentUser)
                    {
                        File.Delete(path + "/AppUserstats.dat");
                        File.Delete(path + "/Appdata.dat");
                        // saveManager.dailyWorkoutStatsPath = Directory.GetDirectories(path) + "/AppUserDailyworkoutstats.dat";
                        activity.CreateOrLodeDataFromDisk();
                        progressLoader.CalculatePercentage();
                    }
                }
            }
            catch
            {
                ;
            }

        }
    }



    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://www.privacypolicygenerator.info/live.php?token=zA63lsZIzhr16RFWAaxMGVnGX9D4r6pR");
    }



}
