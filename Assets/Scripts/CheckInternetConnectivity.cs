using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInternetConnectivity : MonoBehaviour
{
    bool isInternet = true;
    public LeanToggle showNetworkErrorScreen;

    private void Update()
    {

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {

            
            if (isInternet)
            {
                showNetworkErrorScreen.TurnOn();
            }

            isInternet = false;
        }
        else
        {

            if (!isInternet)
            {
                showNetworkErrorScreen.TurnOff();
            }

            isInternet = true;

        }

    }
}
