using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample01;

public class ClassDisplayer : MonoBehaviour
{

    public BookManager bookManager;
    public ProgressLoader progressLoader;
    public Example01 exampleInstance;
    public List<Sprite> classTabImages;
    public int startingClass;
    public int endingClass;
    public List<string> classNames;
    public List<string> classDisplayingName;
    public List<Sprite> FG_Images;
    public List<Sprite> FG_Images_Goal;


    private void OnEnable()
    {
        startingClass = 0;
        endingClass = 0;
        for (int i = 0; i < bookManager.books.Count; i++)
        {

            if (BookManager.currentBookName == bookManager.books[i].bookName)
            {
                startingClass = bookManager.books[i].statingClass;
                endingClass = bookManager.books[i].endingClass;
            }
        }

        exampleInstance.FG_Images = new List<Sprite>();
        exampleInstance.imgs = new List<Sprite>();
        exampleInstance.TabName = new List<string>();
        exampleInstance.className = new List<string>();
        exampleInstance.completionPercentage = new List<float>(new float[endingClass - startingClass + 1]);
       // exampleInstance.cardIsIntractable = new List<bool>(new bool[endingClass - startingClass + 1]);
        for (int i = startingClass; i < endingClass + 1; i++)
        {
            exampleInstance.imgs.Add(classTabImages[(i - startingClass) % classTabImages.Count]);
            exampleInstance.TabName.Add(classNames[i]);

            exampleInstance.FG_Images.Add(FG_Images[i%24]);
     

           /* if ((i-startingClass) == 0)
                exampleInstance.cardIsIntractable[i-startingClass]=(true);
            else
            {
                if (exampleInstance.completionPercentage[i-startingClass - 1] >= 100)
                {
                    exampleInstance.cardIsIntractable[i-startingClass]=(true);
                }
                else
                    exampleInstance.cardIsIntractable[i-startingClass]=(false);
            }*/
         
        }

        for (int i = startingClass; i < endingClass+1; i++)
        {
            exampleInstance.className.Add(classDisplayingName[(i - startingClass) % 12]);
        }
        progressLoader.CalculatePercentage();
        exampleInstance.UpdateData();// can be commented out as its called in CalculatePercentage();
    }

    void Update()
    {

    }


}
