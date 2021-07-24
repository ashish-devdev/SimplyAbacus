using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager _instance;
    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SaveManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("SaveManager");
                    _instance = container.AddComponent<SaveManager>();
                }
            }

            return _instance;
        }
    }

    bool noUser;

    public GameObject currentUserProfilePic;

    public GameObject currentUserName;

    public GameObject profileHandlingCanvas;
    public GameObject createProfileForFirstTime;
    public List<Sprite> userProfileImages;

    public GameObject intoScreen;
    public GameObject intoScreen2;
    public GameObject AppUI;

    [HideInInspector]
    public string savePath;
    [HideInInspector]
    public string saveStatsPath;
    [HideInInspector]
    public string saveLockData;
    [HideInInspector]
    public string dailyWorkoutStatsPath;
    [HideInInspector]
    public string SaveUserInformationPath;

    int currentActiveUser;
    int currentActiveUserUniqueID;
    public static string[] userFolderName = { "/User1", "/User2", "/User3" };
    public static bool[] Userallocated = { false, false, false };

    int a;

    public Image userProfileImage_forFirstUser;
    public TextMeshProUGUI userName_forFirstUser;
    public TextMeshProUGUI userID_forFirstUser;

    public Image userProfileImage_forSecondAndThirdUser;
    public TextMeshProUGUI userName_forSecondAndThirdtUser;
    public TextMeshProUGUI userID_forSecondAndThirdUser;

    ClassUserInformation classUserInformation;

    private void Awake()
    {



        noUser = true;

        for (int i = 0; i < userFolderName.Length; i++)
        {
            if (File.Exists(Application.persistentDataPath +"/User"+ userFolderName[i] + "/AppUserInformation.dat"))
            {
                noUser = false;
                SaveUserInformationPath = Application.persistentDataPath + "/User" + userFolderName[i] + "/AppUserInformation.dat";
                ClassUserInformation classUser = SaveManager.Instance.LoadUserInformation();
                if (classUser.currentUser)
                {
                    currentActiveUserUniqueID = classUser.uniqueID;
                    currentActiveUser = i;
                }
                Userallocated[i] = true;
            }
        }

        if (noUser == true)
        {
           

            profileHandlingCanvas.SetActive(true);
            createProfileForFirstTime.SetActive(true);
            Userallocated[0] = true;
            savePath = Application.persistentDataPath + "/User" + userFolderName[0] + "/Appdata.dat";
            saveStatsPath = Application.persistentDataPath + "/User" + userFolderName[0] + "/AppUserstats.dat";
            saveLockData = Application.persistentDataPath + "/User" + userFolderName[0] + "/AppLockData.dat";
            dailyWorkoutStatsPath = Application.persistentDataPath + "/User" + userFolderName[0] + "/AppUserDailyworkoutstats.dat";
            SaveUserInformationPath = Application.persistentDataPath + "/User" + userFolderName[0] + "/AppUserInformation.dat";


            if (!Directory.Exists(Application.persistentDataPath + "/User" + userFolderName[0]))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/User" + userFolderName[0]);
            }

        }
        else
        {
            profileHandlingCanvas.SetActive(false);
            createProfileForFirstTime.SetActive(false);
            savePath = Application.persistentDataPath + "/User" + userFolderName[currentActiveUser] + "/Appdata.dat";
            saveStatsPath = Application.persistentDataPath + "/User" + userFolderName[currentActiveUser] + "/AppUserstats.dat";
            saveLockData = Application.persistentDataPath + "/User" + userFolderName[0] + "/AppLockData.dat";
            dailyWorkoutStatsPath = Application.persistentDataPath + "/User" + userFolderName[currentActiveUser] + "/AppUserDailyworkoutstats.dat";
            SaveUserInformationPath = Application.persistentDataPath + "/User" + userFolderName[currentActiveUser] + "/AppUserInformation.dat";
            if (!Directory.Exists(Application.persistentDataPath + "/User" + userFolderName[currentActiveUser]))
            {
                Directory.CreateDirectory(Application.persistentDataPath + userFolderName[currentActiveUser]);
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/User" + SaveManager.userFolderName[currentActiveUser] + "/AppUserInformation.dat", FileMode.Open);
            ClassUserInformation classUser = (ClassUserInformation)bf.Deserialize(file);
            file.Close();
            classUser.currentUser = true;

            for (int l = 0; l < userProfileImages.Count; l++)
            {
                if (classUser.profilePicName == userProfileImages[l].name)
                    currentUserProfilePic.GetComponent<Image>().sprite = userProfileImages[l];

            }
            currentUserName.GetComponent<TextMeshProUGUI>().text = classUser.userName;

        }
    }
    private void OnEnable()
    {
        Invoke("InvokeOnEnableAfterDelay", 0.001f);
    }

    public void InvokeOnEnableAfterDelay()
    {
        if (!noUser)
        {
            intoScreen.SetActive(false);
            intoScreen2.SetActive(true);
            AppUI.SetActive(true);
        }
        else
        {
            intoScreen.SetActive(true);
            intoScreen2.SetActive(false);
            AppUI.SetActive(true);
        }
    }



    public void saveDataToDisk(ClassParent t)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, t);
        file.Close();

    }

    public void SaveStatsToDisk(ClassParentStats t)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveStatsPath);
        bf.Serialize(file, t);

        file.Close();


    }
    public void SaveDailyWorkoutStatsToDisk(ClassParentDailyWorkoutStats t)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dailyWorkoutStatsPath);
        bf.Serialize(file, t);
        file.Close();


    }
    public void SaveLockInformation(AppLockInformation t)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveLockData);
        bf.Serialize(file, t);
        file.Close();


    }



    public void SaveFirstUserInformation()
    {
        ClassUserInformation t = new ClassUserInformation();
        t.currentUser = true;
        t.uniqueID = 0;
        t.userID = userID_forFirstUser.text;
        t.userName = userName_forFirstUser.text;
        t.profilePicName = userProfileImage_forFirstUser.sprite.name;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(SaveUserInformationPath);
        bf.Serialize(file, t);
        file.Close();
    }

    public void SaveSecondAndThirdUserInformation(string path)
    {
        ClassUserInformation t = new ClassUserInformation();
        t.currentUser = false;
        t.uniqueID = 0;
        t.userID = userID_forSecondAndThirdUser.text;
        t.userName = userName_forSecondAndThirdtUser.text;
        t.profilePicName = userProfileImage_forSecondAndThirdUser.sprite.name;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);
        bf.Serialize(file, t);
        file.Close();

    }




    public ClassParent loadDataFromDisk()
    {
        ClassParent classData = new ClassParent();
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            classData = (ClassParent)bf.Deserialize(file);
            file.Close();
        }
        return classData;
    }


    public ClassParentStats loadStatsDataFromDisk()
    {
        ClassParentStats classStats = new ClassParentStats();
        if (File.Exists(saveStatsPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveStatsPath, FileMode.Open);
            classStats = (ClassParentStats)bf.Deserialize(file);
            file.Close();
        }
        return classStats;
    }

    public AppLockInformation LoadLockDataFromDisk()
    {
        AppLockInformation lockData = new AppLockInformation();
        if (File.Exists(saveLockData))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveStatsPath, FileMode.Open);
            lockData = (AppLockInformation)bf.Deserialize(file);
            file.Close();
        }
        return lockData;
    }



    public ClassParentDailyWorkoutStats loadDailyWorkoutStatsDataFromDisk()
    {
        ClassParentDailyWorkoutStats dailyWorkoutStats = new ClassParentDailyWorkoutStats();
        if (File.Exists(dailyWorkoutStatsPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dailyWorkoutStatsPath, FileMode.Open);
            dailyWorkoutStats = (ClassParentDailyWorkoutStats)bf.Deserialize(file);
            file.Close();
        }
        return dailyWorkoutStats;
    }


    public ClassUserInformation LoadUserInformation()
    {
        ClassUserInformation classUser = new ClassUserInformation();
        if (File.Exists(SaveUserInformationPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(SaveUserInformationPath, FileMode.Open);
            classUser = (ClassUserInformation)bf.Deserialize(file);
            file.Close();
        }
        return classUser;
    }







}
