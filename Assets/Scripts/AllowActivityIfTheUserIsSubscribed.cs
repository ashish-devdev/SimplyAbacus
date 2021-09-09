using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AllowActivityIfTheUserIsSubscribed : MonoBehaviour
{
    public LeanToggle buySubscription;
    public IAPManager _IAPManager;
    public LeanToggle book;
    public LeanToggle ShowDoPreviousSectionLean;

    public void OnClickedDailyWorkout(GameObject dailyWorkout)
    {
        if (_IAPManager.CheckIfUserIsSubscribed())
        {
            dailyWorkout.SetActive(true);
        }
        else
        {
            // dailyWorkout.SetActive(true);

            buySubscription.TurnOn();
        }
    }

    public void OnClickedBook()
    {

        Invoke("DelayedOnClickedBook", 0.01f);

    }

    public void DelayedOnClickedBook()
    {
        if (_IAPManager.CheckIfUserIsSubscribed())
        {
            book.TurnOn();
        }
        else if (BookManager.currentBookName == "FOUNDATION LEVEL I")
            book.TurnOn();
        else
        {
            book.TurnOn();
           // buySubscription.TurnOn();
        }
    }

    public void CheckIfUserIsSubscribedOnClickingLock()
    {
        if (_IAPManager.CheckIfUserIsSubscribed())
        {
            ShowDoPreviousSectionLean.TurnOn();
        }
        else
        {
            // dailyWorkout.SetActive(true);

            buySubscription.TurnOn();
        }
    }



}
