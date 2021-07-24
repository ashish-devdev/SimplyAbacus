using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen1 : MonoBehaviour
{

    private void OnEnable()
    {
        Invoke("DisableTheGameObject", 4.2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void DisableTheGameObject()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
