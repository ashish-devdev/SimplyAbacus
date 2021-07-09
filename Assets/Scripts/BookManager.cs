using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample01;

public class BookManager : MonoBehaviour
{
    public Example01 exampleInstance;
    public static string currentBookName;
    public List<Book> books;
    public List<Sprite> bookTabImages;
    private void OnEnable()
    {
        exampleInstance.imgs = new List<Sprite>();
        exampleInstance.TabName = new List<string>();
        //exampleInstance.completionPercentage = new List<float>( new float[books.Count]);
        for (int i = 0; i < books.Count; i++)
        {
            exampleInstance.imgs.Add(bookTabImages[i % bookTabImages.Count]);
            exampleInstance.TabName.Add(books[i].bookName);
          //  exampleInstance.completionPercentage.Add(0);
        }


        exampleInstance.UpdateData();
    }
    void Update()
    {
        
    }
    [System.Serializable]
    public class Book
    {
        public string bookName;
       // public int bookID;
        public int statingClass;
        public int endingClass;
    }
}
