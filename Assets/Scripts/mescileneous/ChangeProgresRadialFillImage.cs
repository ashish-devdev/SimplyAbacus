using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeProgresRadialFillImage : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI percentageText;
    string str;
    public Image img;
    int temp = 0;
    private void OnEnable()
    {
        if (percentageText.text.Contains("%"))
            str = percentageText.text.Replace("<size=20>%</size>", "");
        if (str != null)
        {
            img.fillAmount = ((int.Parse(str)) * 1f) / 100;
        }

        temp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (temp < 100)
        {

            if (percentageText.text.Contains("%"))
                str = percentageText.text.Replace("<size=20>%</size>", "");
            if (str != null)
            {
                img.fillAmount = ((int.Parse(str)) * 1f) / 100;
            }
            temp++;
        }
    }
}
