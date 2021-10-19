/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace UnityEngine.UI.Extensions.Examples.FancyScrollViewExample01
{
    public class Example01 : MonoBehaviour
    {
        public List<Sprite> imgs;
        public List<UnityAction> btnEvents;
        public List<string> TabName = new List<string> { "a", "b", "c", "d", "e" };
        public List<float> completionPercentage;
        public List<bool> cardIsIntractable;
        [SerializeField] ScrollView scrollView = default;
        public static event Action<ClassActivity> onCurrentClassClicked;
        public UnityEvent btnEvent;
        public UnityEvent onClickedLock;
        public bool gameObjectIsClassContent;
        public bool gameObjectIsBookContent;
        public List<NumberImages> numberImages;
        public List<Sprite> FG_Images;
        public List<string> className= new List<string> { "a", "b", "c", "d", "e" };

        [System.Serializable]
        public class NumberImages
        {
            public String className;
            public Sprite numImg1;
            public Sprite numImg2;
        }

        void OnEnable()
        {

            //var items = Enumerable.Range(0,TabName.Count())
            //    .Select(i => new ItemData(/*$"Cell {i}" +*/ TabName[i], i,imgs[i], btnEvents[i]))
            //    .ToArray();

            //   // .Select(i => new ItemData($"Cell {i}"+TabName[i],i,Sprite imgs[i]))
            //scrollView.UpdateData(items);
            UpdateData();
        }

        public void UpdateData()
        {
            if (gameObject.CompareTag("ClassContent"))
            {
                gameObjectIsClassContent = true;
            }
            else
            {
                gameObjectIsClassContent = false;

            }
            if (gameObject.CompareTag("BookContent"))
            {
                gameObjectIsBookContent = true;
            }
            else
            {
                gameObjectIsBookContent = false;

            }

            //btnEvents = new List<UnityAction>();
            //  btnEvents.Add(new UnityAction(() => { print(123); }));
            //  print(btnEvents[0]);
            if (btnEvents != null)
            {
                //activity

                var items = Enumerable.Range(0, TabName.Count())
                           .Select(i => new ItemData(/*$"Cell {i}" +*/ TabName[i], i, imgs[i], btnEvent, gameObjectIsClassContent, gameObjectIsBookContent, btnEvents[i],/*cardIsIntractable[i]*/true, onClickedLock, completionPercentage[i]))
                           .ToArray();
                scrollView.UpdateData(items);
            }
            else
            {
                if (numberImages.Count != 0)
                {
                    //class
                    var items = Enumerable.Range(0, TabName.Count())
                   .Select(i => new ItemData(/*$"Cell {i}" +*/ TabName[i], i, imgs[i], btnEvent, gameObjectIsClassContent, gameObjectIsBookContent,/*cardIsIntractable[i]*/true, onClickedLock, completionPercentage[i], numberImages[i % numberImages.Count].numImg1, numberImages[i % numberImages.Count].numImg2, FG_Images[i],className[i%12]))
                   .ToArray();
                    scrollView.UpdateData(items);
                }
                else
                {
                    //book
                    var items = Enumerable.Range(0, TabName.Count())
                   .Select(i => new ItemData(/*$"Cell {i}" +*/ TabName[i], i, imgs[i], btnEvent, gameObjectIsClassContent, gameObjectIsBookContent,/* cardIsIntractable[i]*/true, onClickedLock, completionPercentage[i]))
                   .ToArray();
                    scrollView.UpdateData(items);
                }
            }


            // .Select(i => new ItemData($"Cell {i}"+TabName[i],i,Sprite imgs[i]))
        }
    }
}
