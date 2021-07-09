using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignAvatarImgs : MonoBehaviour
{
    public List<Sprite> avatarImgs;
    public List<GameObject> tickIMG_Gameobjects;

    public Button selectBtn;
    public List<Image> profilesImage;
    public int currentSelectImageNumber;

    public GameObject Parent;
    void Start()
    {
        tickIMG_Gameobjects = new List<GameObject>();
        for (int i = 0; i < Parent.transform.childCount; i++)
        {
            Parent.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = avatarImgs[i % avatarImgs.Count];
            tickIMG_Gameobjects.Add(Parent.transform.GetChild(i).GetChild(1).gameObject);
            int k = i;
            Parent.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate { removeTick(k);});
        }

        selectBtn.onClick.AddListener(() =>
        {
            for (int i = 0; i < profilesImage.Count; i++)
            {
                profilesImage[i].sprite = avatarImgs[currentSelectImageNumber];
            }

        });
    }



    void Update()
    {

    }

    public void removeTick(int currentBtnNumber)
    {
        for (int i = 0; i < tickIMG_Gameobjects.Count; i++)
        {
            if (currentBtnNumber != i)
                tickIMG_Gameobjects[i].SetActive(false);
            else 
            {
                tickIMG_Gameobjects[i].SetActive(true);
                currentSelectImageNumber = i;
            }


        }
    }





}
