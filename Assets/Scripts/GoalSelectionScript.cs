using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalSelectionScript : MonoBehaviour
{
    public bool[] goals;
    public Button continu;
    public GameObject glow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeImageColorToGreen(GameObject GO)
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeGoalstrueOrFalse(int i)
    {
        goals[i] = !goals[i];
    }

    public void CheckIfSelected()
    {
        for (int i = 0; i < goals.Length; i++)
        {
            if (goals[i] == true)
            {
                continu.interactable = true;
                glow.SetActive(true);
                break;
            }
            else
            {
                glow.SetActive(false);
                continu.interactable = false;
            }
        }

    }

}
