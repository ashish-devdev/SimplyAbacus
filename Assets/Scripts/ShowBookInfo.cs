using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class ShowBookInfo : MonoBehaviour
{
    public Scroller scroller;
    public TextMeshProUGUI infoText;
    string[] BookInfo = { "<size=50>\u2022</size><indent=30>Familiarization with numbers. Numbers are made to relate with images introducing visualization(activating right brain)</indent><br><br><size=50>\u2022</size><indent=30>Sliding the beads to represent numbers; Initially a little slowly, the child gains speed encountering more & more sums.</indent><br><br><size= 50 >\u2022</size><indent=30>Concentration skills develop through close observation of bead counting.</indent><br><br><size= 50 >\u2022</size><indent=30>As the child plays with numbers, quick progress is made without ever explicitly feeling so.</indent><br><br><size= 50 >\u2022</size><indent=30>Speed Writing introduced to stimulate both the left & right brains.</indent> ",
    "<size=50>\u2022</size><indent=30>Visualization skills stretch further and the range increases.</indent><br><br><size= 50 >\u2022</size><indent= 30 >Increased speed in computations as more digits are introduced.</indent><br><br><size= 50 >\u2022</size><indent= 30 >Concentration skills strengthen appreciably, otherwise not common in an unexposed child.</indent><br><br><size= 50 >\u2022</size><indent= 30 > Speed Writing practice taken to the next level of accuracy.</indent>",
"<size=50>\u2022</size><indent=30>Better Visualization, better memory skills are established.</indent><br><br><size= 50 >\u2022</size><indent= 30 > Students start solving each sum in a few seconds demonstrating their evolving speed.</indent><br><br><size= 50 >\u2022</size><indent= 30 > Meditative quality of concentration can be observed in the child.</indent><br><br><size= 50 >\u2022</size><indent= 30 > The child is more driven to solve challenging problems & achieve targets eventually translating to life time skills.</indent><br><br>"
    };


    public void ShowCurrentBookInfo()
    {
        int i = (int)scroller.GetCurrentPosition();
        if (i >= 0 && i <= 2)
            infoText.text = BookInfo[0];
        else if (i >= 3 && i <= 5)
            infoText.text = BookInfo[1];
        else
            infoText.text = BookInfo[2];

    }


}

