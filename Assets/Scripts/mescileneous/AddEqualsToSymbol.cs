using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddEqualsToSymbol : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI textComponent;
    void Start()
    {

    }

    private void OnEnable()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textComponent.text.Length > 0)
        {
            if (!(textComponent.text.Contains("=")))
            {
                textComponent.text = "=" + textComponent.text;
            }
        }
    }
}

