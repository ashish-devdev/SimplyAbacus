using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadingScreen : MonoBehaviour
{

    public UnityAction actions;

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void OnUnityActionCalled()
    {
        if(actions!=null)
        actions.Invoke();    
    }

}
