/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
namespace UnityEngine.UI.Extensions.Examples.FancyScrollViewExample01
{
   // [System.Serializable]
    class ItemData
    {
        public string Message { get; }
        public string ClassName;
        public int ID;
        public Sprite Img;
        public UnityEvent BtnEvent;
        public UnityEvent[] OnClickedLock;
        public UnityAction BtnEvents;
        public UnityAction onClickedBook;
        public UnityAction onClickedClass;
        public UnityAction onClickedActivity;
        public bool GameObjectIsClassContent;
        public bool GameObjectIsBookContent;
        public float CompletionPercentage;
        public Sprite NumImage1;
        public Sprite NumImage2;
        public Sprite FG_Image;
        public bool CardIsIntractable;
        // public static Action ON_CLICK;



        public ItemData(string message,int id,Sprite img,UnityEvent btnEvent,bool gameObjectIsClassContent,bool gameObjectIsBookContent,bool cardIsIntractable, UnityEvent[] onClickedLock=null ,float completionPercentage=0,Sprite numImage1 = null, Sprite numImage2 = null,Sprite fg_Image=null,string className="")
        {
            this.Message = message;
            this.ClassName = className;
            this.ID = id;
            this.Img = img;
            this.BtnEvent = btnEvent;
            this.OnClickedLock = onClickedLock;
            this.GameObjectIsClassContent = gameObjectIsClassContent;
            this.GameObjectIsBookContent = gameObjectIsBookContent;
            this.CompletionPercentage = completionPercentage;
            this.CardIsIntractable = cardIsIntractable;
            if (numImage1 != null)
                NumImage1 = numImage1;
            if (numImage2 != null)
                NumImage2 = numImage2;
            if (fg_Image != null)
                FG_Image = fg_Image;

            //   ON_CLICK += Print_fn(id);
        }
        public ItemData(string message,int id,Sprite img,UnityEvent btnEvent,bool gameObjectIsClassContent,bool gameObjectIsBookContent,UnityAction btnEvents,bool cardIsIntractable, UnityEvent[] onClickedLock=null,float completionPercentage=0)
        {
            this.Message = message;
            this.ID = id;
            this.Img = img;
            this.BtnEvent = btnEvent;
            this.OnClickedLock = onClickedLock;
            this.BtnEvents = btnEvents;
            this.GameObjectIsClassContent = gameObjectIsClassContent;
            this.GameObjectIsBookContent = gameObjectIsBookContent;
            this.CompletionPercentage = completionPercentage;
            this.CardIsIntractable = cardIsIntractable;


            //   ON_CLICK += Print_fn(id);
        }
    }


    //public void Print_fn(int id)
    //{
    //    print(id);
    //}


    //public void INVOKE_ON_CLICK()
    //{ 
    //    O
    //}

}
