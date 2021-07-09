using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample01;

public class AddPoemAndInstructionToColorShapes : MonoBehaviour
{
    public TextMeshProUGUI poemNoteText;
    public TextMeshProUGUI sideNoteText;
    public List<string> poems;
    public List<string> notes;
    public Example01 example1Instance;
    public BookManager bookManager;
    int startngClass;
    int index;
    private void OnEnable()
    {
        for (int i = 0; i < example1Instance.TabName.Count; i++)
        {
            if (ClassManager.currentClassName == example1Instance.TabName[i])
            {
                for (int j = 0; j < bookManager.books.Count; j++)
                {
                    if (BookManager.currentBookName == bookManager.books[j].bookName)
                    {
                        poemNoteText.text = poems[i+ bookManager.books[j].statingClass];
                        sideNoteText.text = notes[i+ bookManager.books[j].statingClass];
                    }



                }



            }
        }
    }


    void Update()
    {

    }
}
