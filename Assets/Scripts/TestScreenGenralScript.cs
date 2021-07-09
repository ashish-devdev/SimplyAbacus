using Lean.Transition;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestScreenGenralScript : MonoBehaviour
{
    List<Image> imageComponentOfOptionCards;


    public GameObject numberListFrom_1to20;
    public GameObject numberListFrom_21to40;
    public GameObject[] OptionCards;

    private void OnEnable()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#00BBF900", out color);

        for (int i = 0; i < numberListFrom_1to20.transform.childCount; i++)
        {
            numberListFrom_1to20.transform.GetChild(i).GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0);
            numberListFrom_1to20.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
        }                                                                   
        for (int i = 0; i < numberListFrom_21to40.transform.childCount; i++)
        {
            numberListFrom_21to40.transform.GetChild(i).GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0);
            numberListFrom_21to40.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 21).ToString();

        }

        imageComponentOfOptionCards = new List<Image>();

        for (int i = 0; i < OptionCards.Length; i++)
        {
            int k = i;
            imageComponentOfOptionCards.Add(OptionCards[i].GetComponent<Image>());
            OptionCards[k].GetComponent<Button>().onClick.AddListener(delegate { ChangeCurrentCardColorToBlue(k); });
        }

        ChangeAllCardColorToWhite();


    }

    public void ChangeCurrentCardColorToBlue(int i)
    {
        int k = i;
        Color color;
        ColorUtility.TryParseHtmlString("#00C2FF", out color);
        ChangeAllCardColorToWhite();
        imageComponentOfOptionCards[k].color = color;
    }

    public void ChangeAllCardColorToWhite()
    {
        for (int j = 0; j < OptionCards.Length; j++)
        {
            imageComponentOfOptionCards[j].color = Color.white;
        }
    }


    public void ClickAnimation(GameObject go)
    {

        go.transform.localScaleTransition(new Vector3(0.9f, 0.9f, 0.9f), 0.1f).JoinDelayTransition(0.001f).localScaleTransition(new Vector3(1f, 1f, 1f), 0.1f);

    }



    private void OnDisable()
    {
        for (int i = 0; i < OptionCards.Length; i++)
        {
            int k = i;
            OptionCards[k].GetComponent<Button>().onClick.RemoveListener(delegate {  ChangeCurrentCardColorToBlue(k); });
        }
    }


}
