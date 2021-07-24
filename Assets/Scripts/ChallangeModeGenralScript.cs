using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallangeModeGenralScript : MonoBehaviour
{

    public bool modeSelected;
    public bool numberOfQuestionSelected;
    public Button TypeOfQuestionBtn;
    public GameObject highlightGameobject;

    public Image[] questionButtons;
    public Image[] modeButtons;

    public Image[] underLineDashs;

    private void OnEnable()
    {
        modeSelected = false;
        numberOfQuestionSelected = false;
        TypeOfQuestionBtn.interactable = false;
        highlightGameobject.SetActive(false);


        for (int i = 0; i < questionButtons.Length; i++)
        {
            questionButtons[i].color = new Color(questionButtons[i].color.r, questionButtons[i].color.g, questionButtons[i].color.b, 0);
        }
        for (int i = 0; i < modeButtons.Length; i++)
        {
            modeButtons[i].color = new Color(modeButtons[i].color.r, modeButtons[i].color.g, modeButtons[i].color.b, 0);
        }

    }


    public void MakeSlectedQuestionButtonHighLight(Image img)
    {
        for (int i = 0; i < questionButtons.Length; i++)
        {
            questionButtons[i].color = new Color(questionButtons[i].color.r, questionButtons[i].color.g, questionButtons[i].color.b, 0);
        }
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1);

    }
    public void MakeSlectedModeButtonHighLight(Image img)
    {
        for (int i = 0; i < modeButtons.Length; i++)
        {
            modeButtons[i].color = new Color(modeButtons[i].color.r, modeButtons[i].color.g, modeButtons[i].color.b, 0);
        }
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0);

    }


    public bool ModeSelected
    {
        get
        { return modeSelected; }

        set
        {
            modeSelected = value;
            if (modeSelected == true && numberOfQuestionSelected == true)
            {
                TypeOfQuestionBtn.interactable = true;
                highlightGameobject.SetActive(true);

            }
            else
            { 
                TypeOfQuestionBtn.interactable = false;
                highlightGameobject.SetActive(false);


            }


        }
    }

    public bool NumberOfQuestionSelected
    {
        get
        { return numberOfQuestionSelected; }

        set
        {
            numberOfQuestionSelected = value;
            if (modeSelected == true && numberOfQuestionSelected == true)
            {
                TypeOfQuestionBtn.interactable = true;
                highlightGameobject.SetActive(true);
            }
            else
            {
                TypeOfQuestionBtn.interactable = false;
                highlightGameobject.SetActive(false);

            }


        }
    }


    public void ChnageImageColorWhite(Image img)
    {
        img.color = Color.white;
    }

    public void ChangeImageColorGreen(Image img)
    {
        img.color = Color.green;

    }

    private void OnDisable()
    {
        modeSelected = false;
        numberOfQuestionSelected = false;

        for (int i = 0; i < underLineDashs.Length; i++)
        {
            underLineDashs[i].color = Color.white;
        }


    }

}
