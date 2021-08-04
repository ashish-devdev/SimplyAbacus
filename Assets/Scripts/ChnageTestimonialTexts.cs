using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChnageTestimonialTexts : MonoBehaviour
{

    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI contextTMP;
    public TextMeshProUGUI supportingTMP;
    public TextMeshProUGUI fiveGiantStridesText;
    
    public Image simplyAbcusImg;

    public string[] name;
    public string[] context;
    public string[] supportingTexts;
    public int index=0;
    public int index2=0;

    public void ResetIndex()
    {
        index = 0;
        index2 = 0;
    }

    public void ChnageNameandContext()
    {
        index++;
        if (index >= name.Length)
        {
            index = 0;
        }
        contextTMP.text ="\""+ context[index]+"\"";
        nameTMP.text = name[index];
    }

    public void ChangeSupportingText()
    {
        index2++;
        if (index2 >= supportingTexts.Length)
        {
            index2 = 0;
        }
        if (index2 == 1)
        {
            fiveGiantStridesText.transform.gameObject.SetActive(true);
        }
        else
        {
            fiveGiantStridesText.transform.gameObject.SetActive(false);

        }


        if (index2 == 0)
        {
            simplyAbcusImg.transform.gameObject.SetActive(true);
            
        }
        else
            simplyAbcusImg.transform.gameObject.SetActive(false);

        supportingTMP.text = supportingTexts[index2];
    }




}
