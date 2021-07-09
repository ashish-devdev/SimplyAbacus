using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextColorWhite : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
    }

    private void OnDisable()
    {  
        this.GetComponent<TextMeshProUGUI>().color = Color.white;
    }

}
