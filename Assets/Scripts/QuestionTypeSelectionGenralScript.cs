using Lean.Transition;
using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QuestionTypeSelectionGenralScript : MonoBehaviour
{
    public List<string> questionTypes;
    public Transform questionListCardContainer;
    LeanTransformLocalScale leanTransformLocalScaleShrink;
    LeanTransformLocalScale leanTransformLocalScaleGrow;
    LeanJoin leanJoin;
    LeanJoinDelay joinDelay;
    public Button proceedBtn;
    public GameObject glowImage;
    public List<bool> selectedList;



    private void OnEnable()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#264ECB43", out color);
        for (int i = 0; i < questionTypes.Count; i++)
        {
            questionListCardContainer.GetChild(i).gameObject.SetActive(true);
            questionListCardContainer.GetChild(i).GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0f);
            questionListCardContainer.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = questionTypes[i];

        }

        selectedList = new List<bool>(new bool[questionTypes.Count]);
        proceedBtn.interactable = false;
        glowImage.SetActive(false);
    }




    public void ChangeImageColorToGreen(GameObject GO)
    {
        Color color;
        ColorUtility.TryParseHtmlString("#264ECB43", out color);

        if ( GO.gameObject.transform.GetComponent<Image>().color.a!=1)
            GO.gameObject.transform.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1);
        else
            GO.gameObject.transform.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0f);
    }
    public void ClickAnimation(GameObject go)
    {

        go.transform.localScaleTransition(new Vector3(0.9f, 0.9f, 0.9f), 0.1f).JoinDelayTransition(0.001f).localScaleTransition(new Vector3(1f, 1f, 1f), 0.1f);

    }

    public void ChangeSelectedListValue(GameObject go)
    {
        selectedList[go.transform.GetSiblingIndex()] = !(selectedList[go.transform.GetSiblingIndex()]);

        for (int i = 0; i < selectedList.Count; i++)
        {
            if (selectedList[i] == true)
            {
                proceedBtn.interactable = true;
                glowImage.SetActive(true);

                break;
            }
            else
            {
                glowImage.SetActive(false);
                proceedBtn.interactable = false;
            }
        }

    }

}

