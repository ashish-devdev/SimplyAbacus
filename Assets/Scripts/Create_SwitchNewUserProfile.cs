using Lean.Gui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Create_SwitchNewUserProfile : MonoBehaviour
{

    int userCount;
    public List<Sprite> userProfileImages;
    public GameObject addUserProfileBtn;
    public GameObject userProfile_Prefab;
    public Transform ParentTransform;
    public SaveManager saveManager;
    public static UnityAction onNewprofileCreation;
    public static string currentUserPath;

    public LeanToggle bookScrollView;

    public Image profilePicOfFirstTimeUser;
    public TextMeshProUGUI userNameOfFirstTimeUser;
    public GameObject currentUserProfilePic;
    public GameObject currentUserName;
    public Button CreateBtnForFirstTimeUser;



    private void OnEnable()
    {
        userCount = Directory.GetDirectories(Application.persistentDataPath + "/User").Length;// count number of directories , and the count represents the number of users.
        if (userCount >= 3)
        {
            addUserProfileBtn.SetActive(false);
        }
        else
        {
            addUserProfileBtn.SetActive(true);
        }

        for (int i = 0; i < 3; i++)
        {
            if (i < userCount)
                MakeUserProfileActive(i);
            else
            {
                GameObject userProfile = ParentTransform.GetChild(i).gameObject;
                userProfile.SetActive(false);
            }
        }

        CreateBtnForFirstTimeUser.onClick.AddListener(ShowProfilePicAndNameForFirstTimeUser);

    }

    private void OnDisable()
    {
        try
        {
            for (int i = 0; i < 3; i++)
            {
                int k = i;
                ParentTransform.GetChild(k).GetComponent<Button>().onClick.RemoveAllListeners();
                CreateBtnForFirstTimeUser.onClick.RemoveListener(ShowProfilePicAndNameForFirstTimeUser);

            }
        }
        catch
        {
            ;
        }
    }



    public void MakeUserProfileActive(int i)
    {
        string path = Directory.GetDirectories(Application.persistentDataPath + "/User")[i];
        path = path + "/AppUserInformation.dat";

        GameObject userProfile = ParentTransform.GetChild(i).gameObject;
        userProfile.SetActive(true);

        ClassUserInformation classUser = new ClassUserInformation();
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file;
            try
            {
                file = File.Open(path, FileMode.Open);

                classUser = (ClassUserInformation)bf.Deserialize(file);
                file.Close();
                if (classUser.currentUser == true)
                {
                    userProfile.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    userProfile.transform.GetChild(1).gameObject.SetActive(false);
                }
                userProfile.transform.GetChild(0).GetComponent<Image>().sprite = UserProfilePic(classUser.profilePicName);
                userProfile.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = classUser.userName;

                int k = i;
                userProfile.GetComponent<Button>().onClick.AddListener(delegate { SwitchCurrentUser(k); });

            }
            catch (Exception e)
            {
                print("error --->" + e);
            }

        }
    }



    public Sprite UserProfilePic(string picName)
    {
        Sprite profilePic = userProfileImages[0];
        for (int i = 0; i < userProfileImages.Count; i++)
        {
            if (picName == userProfileImages[i].name)
            {
                profilePic = userProfileImages[i];
            }
        }

        return profilePic;
    }


    public void CreateNewUserProfile()
    {
        int unassignedLocation = -1;

        for (int i = 0; i < SaveManager.Userallocated.Length; i++)
        {
            if (SaveManager.Userallocated[i] == false)
            {
                unassignedLocation = i;
                break;
            }

        }
        if (unassignedLocation != -1)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/User" + SaveManager.userFolderName[unassignedLocation]))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/User" + SaveManager.userFolderName[unassignedLocation]);
            }

            saveManager.SaveSecondAndThirdUserInformation(Application.persistentDataPath + "/User" + SaveManager.userFolderName[unassignedLocation] + "/AppUserInformation.dat");
            SaveManager.Userallocated[unassignedLocation] = true;
        }
    }

    public void UpdateCurrentUserProfile()
    {
        userCount = Directory.GetDirectories(Application.persistentDataPath + "/User").Length;
        for (int i = 0; i < userCount; i++)
        {


            ClassUserInformation classUser = new ClassUserInformation();
            if (File.Exists(Directory.GetDirectories(Application.persistentDataPath + "/User")[i] + "/AppUserInformation.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file;

                file = File.Open(Directory.GetDirectories(Application.persistentDataPath + "/User")[i] + "/AppUserInformation.dat", FileMode.Open);
                classUser = (ClassUserInformation)bf.Deserialize(file);
                file.Close();
                if (classUser.currentUser == true)
                {
                    for (int l = 0; l < userProfileImages.Count; l++)
                    {
                        if (classUser.profilePicName == userProfileImages[l].name)
                            currentUserProfilePic.GetComponent<Image>().sprite = userProfileImages[l];

                        currentUserName.GetComponent<TextMeshProUGUI>().text = classUser.userName;

                    }
                }

            }

        }

    }

    public void SwitchCurrentUser(int k)
    {

      
        userCount = Directory.GetDirectories(Application.persistentDataPath + "/User").Length;
        for (int i = 0; i < userCount; i++)
        {
            if (k == i)
            {

                ClassUserInformation classUser = new ClassUserInformation();
                if (File.Exists(Directory.GetDirectories(Application.persistentDataPath + "/User")[i] + "/AppUserInformation.dat"))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file;
                    file = File.Open(Directory.GetDirectories(Application.persistentDataPath + "/User")[i] + "/AppUserInformation.dat", FileMode.Open);

                    classUser = (ClassUserInformation)bf.Deserialize(file);
                    file.Close();
                    if (classUser.currentUser != true)
                    {
                        bookScrollView.TurnOn();
                    }

                    classUser.currentUser = true;
                    for (int l = 0; l < userProfileImages.Count; l++)
                    {
                        if (classUser.profilePicName == userProfileImages[l].name)
                            currentUserProfilePic.GetComponent<Image>().sprite = userProfileImages[l];

                    }
                    currentUserName.GetComponent<TextMeshProUGUI>().text = classUser.userName;

                    BinaryFormatter bftr = new BinaryFormatter();
                    FileStream fileStream = File.Create(Directory.GetDirectories(Application.persistentDataPath + "/User")[i] + "/AppUserInformation.dat");
                    bftr.Serialize(fileStream, classUser);
                    fileStream.Close();
                    saveManager.savePath = Directory.GetDirectories(Application.persistentDataPath + "/User")[i] + "/Appdata.dat";
                    saveManager.saveStatsPath = Directory.GetDirectories(Application.persistentDataPath + "/User")[i] + "/AppUserstats.dat";
                    saveManager.dailyWorkoutStatsPath = Directory.GetDirectories(Application.persistentDataPath + "/User")[i] + "/AppUserDailyworkoutstats.dat";
                    saveManager.SaveUserInformationPath = Directory.GetDirectories(Application.persistentDataPath + "/User")[i] + "/AppUserInformation.dat";
                    onNewprofileCreation.Invoke();
                }
            }
            else
            {

                ClassUserInformation classUser = new ClassUserInformation();
                if (File.Exists(Directory.GetDirectories(Application.persistentDataPath + "/User")[i] + "/AppUserInformation.dat"))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(Directory.GetDirectories(Application.persistentDataPath + "/User")[i] + "/AppUserInformation.dat", FileMode.Open);
                    classUser = (ClassUserInformation)bf.Deserialize(file);
                    file.Close();
                    classUser.currentUser = false;

                    BinaryFormatter bftr = new BinaryFormatter();
                    FileStream fileStream = File.Create(Directory.GetDirectories(Application.persistentDataPath + "/User")[i] + "/AppUserInformation.dat");
                    bftr.Serialize(fileStream, classUser);
                    fileStream.Close();

                  /*  BinaryFormatter bftr2 = new BinaryFormatter();
                    FileStream fileStream2 = File.Create(Directory.GetDirectories(Application.persistentDataPath)[i] + "/AppUserDailyworkoutstats.dat");
                    bftr2.Serialize(fileStream2, classUser);
                    fileStream2.Close();*/

                }

            }
        }


    }

    public void ShowProfilePicAndNameForFirstTimeUser()
    {
        currentUserName.GetComponent<TextMeshProUGUI>().text = userNameOfFirstTimeUser.text;
        currentUserProfilePic.GetComponent<Image>().sprite = profilePicOfFirstTimeUser.sprite;

    }

}
