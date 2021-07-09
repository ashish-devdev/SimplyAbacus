using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Testing2 : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textmain;
    Text selftext;
    void Start()
    {
        selftext = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       // print(textmain.text);
       selftext.text = textmain.text;
    }
}
