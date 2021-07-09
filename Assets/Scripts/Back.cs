using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample01;

public class Back : MonoBehaviour
{
    public Example01 activityExampleScript;
    public List<LeanToggle> BackLeanToggleObject;
    public GameObject currentLeanToogle_gameObject; 
    public GameObject concepts, scroll;
    private void Start()
    {
        for (int i = 0; i < BackLeanToggleObject.Count; i++)
        {
            int j = i;
            BackLeanToggleObject[j].OnOn.AddListener(() => getCurrentTurnOnObject(j));
        }
    }

    public void getCurrentTurnOnObject(int i)
    {
        currentLeanToogle_gameObject = BackLeanToggleObject[i].transform.gameObject;
    }

    public void  BackListOnTurnON(int i)
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Escape();
        }


    }

    public void Escape()
    {
        if (currentLeanToogle_gameObject.CompareTag("scroll"))
        {
            currentLeanToogle_gameObject = currentLeanToogle_gameObject.transform.parent.transform.parent.GetChild(2).gameObject;
            currentLeanToogle_gameObject.GetComponent<LeanToggle>().TurnOn();
            activityExampleScript.imgs.Clear();
            activityExampleScript.TabName.Clear();

        }
        else if (currentLeanToogle_gameObject.CompareTag("dontgoback"))
        {
            ;
        }
        else
        {
            currentLeanToogle_gameObject = currentLeanToogle_gameObject.transform.parent.GetChild(2).gameObject;
            currentLeanToogle_gameObject.GetComponent<LeanToggle>().TurnOn();

        }
    }



}
