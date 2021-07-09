using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserProfile : MonoBehaviour
{
    public TMP_InputField userName;
    public TMP_InputField userID;
    public Button CreateBtn;
    public Button deleteBtn;

    private void OnEnable()
    {
        userName.onValueChanged.AddListener(delegate { ChangeCreateBtnIntractibility(); });
        userID.onValueChanged.AddListener(delegate { ChangeCreateBtnIntractibility(); });

        if (Directory.GetDirectories(Application.persistentDataPath).Length > 1)
        {
            deleteBtn.interactable = true;
        }
        else
        { deleteBtn.interactable = false; }

    }
    private void OnDisable()
    {
        userName.onValueChanged.RemoveListener(delegate { ChangeCreateBtnIntractibility(); });
        userID.onValueChanged.RemoveListener(delegate { ChangeCreateBtnIntractibility(); });
    }

    public void CreateAndSaveUserProfile()
    { 
        
        
    }


    public void EditAndSaveUserProfile()
    { 
        
    }


    public void ChangeCreateBtnIntractibility()
    {
        if (userName.text == "" || userID.text == "")
        {
            CreateBtn.interactable = false;
        }
        else 
        {
            CreateBtn.interactable = true;
        }
    }






}
