using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManageUserProfile : MonoBehaviour
{

    public static int currentUser;
    int userCount;
    public List<Sprite> userProfileImages;
    public GameObject addUserProfileBtn;
    public Transform ParentTransform;
    public Image userProfilePic;
    public TMP_InputField userName;
    public TMP_InputField userID;
    Create_SwitchNewUserProfile create_SwitchNewUser;
    public bool userIsCurrentlyPlaying;
    public SaveManager saveManager;
    public LeanToggle bookScrollView;

    public GameObject currentUserProfilePic;
    public GameObject currentUserName;


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

    }

    private void OnDisable()
    {
        Invoke("DelayedInvokeOnDisable", 0.5f);
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
            FileStream file = File.Open(path, FileMode.Open);
            classUser = (ClassUserInformation)bf.Deserialize(file);
            file.Close();

            userProfile.transform.GetChild(0).GetComponent<Image>().sprite = UserProfilePic(classUser.profilePicName);
            userProfile.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = classUser.userName;
            int k = i;
            userProfile.GetComponent<Button>().onClick.AddListener(delegate { EditUser(userProfile.transform.GetChild(0).GetComponent<Image>().sprite, classUser.userName, classUser.userID, k); });

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


    public void EditUser(Sprite pic, string name, string id, int i)
    {



        userProfilePic.sprite = pic;
        userName.text = name;
        userID.text = id;
        currentUser = i;
    }
    Coroutine saveChangesCoroutine;
    public void SaveChanges()
    {
        string path = Directory.GetDirectories(Application.persistentDataPath + "/User")[currentUser];
        path = path + "/AppUserInformation.dat";

        ClassUserInformation classUser = new ClassUserInformation();
        if (File.Exists(path))
        {


            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            classUser = (ClassUserInformation)bf.Deserialize(file);
            file.Close();
            classUser.userName = userName.text;
            classUser.userID = userID.text;
            classUser.profilePicName = userProfilePic.sprite.name;


            BinaryFormatter bftr = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None); //File.Create(path);
            bftr.Serialize(fileStream, classUser);
            fileStream.Close();
        }
        /*
        if (saveChangesCoroutine==null)
        saveChangesCoroutine = StartCoroutine(saveCoroutine());
        */

    }
    IEnumerator saveCoroutine()
    {
        string path = Directory.GetDirectories(Application.persistentDataPath + "/User")[currentUser];
        path = path + "/AppUserInformation.dat";

        ClassUserInformation classUser = new ClassUserInformation();
        if (File.Exists(path))
        {


            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            classUser = (ClassUserInformation)bf.Deserialize(file);
            file.Close();
            classUser.userName = userName.text;
            classUser.userID = userID.text;
            classUser.profilePicName = userProfilePic.sprite.name;


            BinaryFormatter bftr = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None); //File.Create(path);
            bftr.Serialize(fileStream, classUser);
            fileStream.Close();
        }
        else
        {

        }
        yield return new WaitForSeconds(1);
        saveChangesCoroutine = null;
    }

    public void DeleteUser()
    {
        userIsCurrentlyPlaying = false;
        for (int i = 0; i < 3; i++)
        {
            if (currentUser == i)
            {
                print(i);
                string path = Directory.GetDirectories(Application.persistentDataPath + "/User")[i];
                for (int m = 0; m < SaveManager.Userallocated.Length; m++)
                {
                    if (path.Contains("User1"))
                        SaveManager.Userallocated[0] = false;
                    else if (path.Contains("User2"))
                        SaveManager.Userallocated[1] = false;
                    else if (path.Contains("User3"))
                        SaveManager.Userallocated[2] = false;
                }
                if (Directory.Exists(path))
                {
                    ClassUserInformation classUser = new ClassUserInformation();
                    if (File.Exists(path + "/AppUserInformation.dat"))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        FileStream file;
                        file = File.Open(path + "/AppUserInformation.dat", FileMode.Open);
                        classUser = (ClassUserInformation)bf.Deserialize(file);
                        file.Close();
                        userIsCurrentlyPlaying = classUser.currentUser;

                    }


                }



                Directory.Delete(Directory.GetDirectories(Application.persistentDataPath + "/User")[i], true);

                if (userIsCurrentlyPlaying)
                {
                    path = Directory.GetDirectories(Application.persistentDataPath + "/User")[0];
                    print(path);
                    if (Directory.Exists(path))
                    {
                        ClassUserInformation classUser = new ClassUserInformation();
                        if (File.Exists(path + "/AppUserInformation.dat"))
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            FileStream file;
                            file = File.Open(path + "/AppUserInformation.dat", FileMode.Open);
                            classUser = (ClassUserInformation)bf.Deserialize(file);
                            file.Close();


                            classUser.currentUser = true;

                            for (int l = 0; l < userProfileImages.Count; l++)
                            {
                                if (classUser.profilePicName == userProfileImages[l].name)
                                    currentUserProfilePic.GetComponent<Image>().sprite = userProfileImages[l];

                            }
                            currentUserName.GetComponent<TextMeshProUGUI>().text = classUser.userName;
                            bookScrollView.TurnOn();
                            BinaryFormatter bftr = new BinaryFormatter();
                            FileStream fileStream = File.Create(path + "/AppUserInformation.dat");
                            bftr.Serialize(fileStream, classUser);
                            fileStream.Close();
                            print(Directory.GetDirectories(Application.persistentDataPath + "/User")[0]);
                            if (File.Exists(Directory.GetDirectories(Application.persistentDataPath + "/User")[0] + "/Appdata.dat"))
                            {
                                print("yes");
                            }
                            else
                            { print("No"); }

                            saveManager.savePath = Directory.GetDirectories(Application.persistentDataPath + "/User")[0] + "/Appdata.dat";
                            saveManager.saveStatsPath = Directory.GetDirectories(Application.persistentDataPath + "/User")[0] + "/AppUserstats.dat";
                            saveManager.SaveUserInformationPath = Directory.GetDirectories(Application.persistentDataPath + "/User")[0] + "/AppUserInformation.dat";
                            saveManager.dailyWorkoutStatsPath= Directory.GetDirectories(Application.persistentDataPath + "/User")[0] + "/AppUserDailyworkoutstats.dat";
                            Create_SwitchNewUserProfile.onNewprofileCreation.Invoke();


                            print(classUser.currentUser);
                        }
                    }

                }

                #region commented
                /*
                if (Directory.Exists(Directory.GetDirectories(Application.persistentDataPath)[0]))
                {
                   
                    ClassUserInformation classUser = new ClassUserInformation();
                    if (File.Exists(Directory.GetDirectories(Application.persistentDataPath)[0] + "/AppUserInformation.dat"))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        FileStream file;
                        file = File.Open(Directory.GetDirectories(Application.persistentDataPath)[0] + "/AppUserInformation.dat", FileMode.Open);

                        classUser = (ClassUserInformation)bf.Deserialize(file);
                        file.Close();
                        classUser.currentUser = true;
                        for (int l = 0; l < userProfileImages.Count; l++)
                        {
                            if (classUser.profilePicName == userProfileImages[l].name)
                                currentUserProfilePic.GetComponent<Image>().sprite = userProfileImages[l];

                        }
                        currentUserName.GetComponent<TextMeshProUGUI>().text = classUser.userName;

                        BinaryFormatter bftr = new BinaryFormatter();
                        FileStream fileStream = File.Create(Application.persistentDataPath + SaveManager.userFolderName[i] + "/AppUserInformation.dat");
                        bftr.Serialize(fileStream, classUser);
                        fileStream.Close();
                        saveManager.savePath = Application.persistentDataPath + SaveManager.userFolderName[i] + "/Appdata.dat";
                        saveManager.saveStatsPath = Application.persistentDataPath + SaveManager.userFolderName[i] + "/AppUserstats.dat";
                        saveManager.SaveUserInformationPath = Application.persistentDataPath + SaveManager.userFolderName[i] + "/AppUserInformation.dat";

                        onNewprofileCreation.Invoke();
                    }




                   




                    create_SwitchNewUser.SwitchCurrentUser()
                }
                */
                #endregion
            }
        }

    }

    public void DelayedInvokeOnDisable()
    {
        try
        {
            for (int i = 0; i < 3; i++)
            {
                int k = i;
                ParentTransform.GetChild(k).GetComponent<Button>().onClick.RemoveAllListeners();
                print("removed " + k);
            }
        }
        catch
        {
            ;
        }

    }

}
