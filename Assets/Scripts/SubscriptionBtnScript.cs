using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubscriptionBtnScript : MonoBehaviour
{
    public bool _3Months;
    public bool _6Months;
    public bool _1Year;

    public TextMeshProUGUI _3monthText;
    public TextMeshProUGUI _3monthPriceText;
    public GameObject _3monthBorder;
    public GameObject _3MBG;

    public TextMeshProUGUI _6monthText;
    public TextMeshProUGUI _6monthPriceText;
    public GameObject _6monthBorder;
    public GameObject _6MBG;

    public TextMeshProUGUI _1yearText;
    public TextMeshProUGUI _1yearPriceText;
    public GameObject _1YearBorder;
    public GameObject _1YBG;

    public IAPManager iapManager;
    public GameObject allreadySubscribedScreen;
    public GameObject mainSubscriptionScreen;
    public TextMeshProUGUI alreadySubscribedText;

    public GameObject _3monthPlanButton;
    public GameObject _6monthPlanButton;
    public GameObject _12monthPlanButton;
    public GameObject _subscriptionPlanButton;

    public void OnEnable()
    {
        OnClick1YearSubscribtionBtn();

        if (iapManager.CheckIfUserIsSubscribed())
        {
           // mainSubscriptionScreen.SetActive(false);
           // allreadySubscribedScreen.SetActive(true);
            print(iapManager.getSubscriptionEpochTimeAndPlan().Item1);
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(iapManager.getSubscriptionEpochTimeAndPlan().Item1));
            print(dateTimeOffset.DateTime);
            DateTime dateTime = dateTimeOffset.DateTime;
            print(dateTime.AddMonths(iapManager.getSubscriptionEpochTimeAndPlan().Item3).Date.ToString("dd/MM/yyyy"));
            print("purchased on" + dateTime.ToString());
            alreadySubscribedText.text = "You are already a premium member with the subscription plan of " + iapManager.getSubscriptionEpochTimeAndPlan().Item2 + ".\nYour subscription expiers on " + (dateTime.AddMonths(iapManager.getSubscriptionEpochTimeAndPlan().Item3).Date.ToString("dd/MM/yyyy"));
            alreadySubscribedText.gameObject.SetActive(true);

            switch (iapManager.getSubscriptionEpochTimeAndPlan().Item3)
            {
                case 3:
                    _3monthPlanButton.SetActive(false);
                    break;
                case 6:
                    _3monthPlanButton.SetActive(false);
                    _6monthPlanButton.SetActive(false);
                    break;
                case 12:
                    _3monthPlanButton.SetActive(false);
                    _6monthPlanButton.SetActive(false);
                    _12monthPlanButton.SetActive(false);
                    _subscriptionPlanButton.SetActive(false);
                    break;
            }


        }
    


    }



    public void OnClick3MonthsSubscribtionBtn()
    {
        REset();
        _3Months = true;

      //  _3monthText.color = Color.black;
      //  _3monthPriceText.color = Color.black;
        _3monthBorder.SetActive(true);
        _3MBG.SetActive(true);

    }

    public void OnClick6MonthsSubscribtionBtn()
    {
        REset();
        _6Months = true;

      //  _6monthText.color = Color.black;
      //  _6monthPriceText.color = Color.black;
        _6monthBorder.SetActive(true);
        _6MBG.SetActive(true);

    }

    public void OnClick1YearSubscribtionBtn()
    {
        REset();
        _1Year = true;

      //  _1yearText.color = Color.black;
      //  _1yearPriceText.color = Color.black;
        _1YearBorder.SetActive(true);
        _1YBG.SetActive(true);

    }




    public void REset()
    {
        _3Months = false;
        _6Months = false;
        _1Year = false;

        _3monthText.color = Color.white;
        _3monthPriceText.color = Color.white;

        _6monthText.color = Color.white;
        _6monthPriceText.color = Color.white;

        _1yearText.color = Color.white;
        _1yearPriceText.color = Color.white;

        _3monthBorder.SetActive(false);
        _6monthBorder.SetActive(false);
        _1YearBorder.SetActive(false);

        _3MBG.SetActive(false);
        _6MBG.SetActive(false);
        _1YBG.SetActive(false);
    }



    // Start is called before the first frame update

    public void Buy3MonthsSubscription()
    {
        IAPManager.Instance.Buy3MonthsSubscription();
    }


    public void Buy6MonthsSubscription()
    {
        IAPManager.Instance.Buy6MonthsSubscription();

    }

    public void Buy1YearSubscription()
    {
        IAPManager.Instance.Buy1YearSubscription();

    }

    public void BuySubscription()
    {
        if (_3Months)
        {
            Buy3MonthsSubscription();
        }
        else if (_6Months)
        {
            Buy6MonthsSubscription();
        }

        else if (_1Year)
        {
            Buy1YearSubscription();
        }
    }


}
