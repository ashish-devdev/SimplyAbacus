/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
namespace UnityEngine.UI.Extensions.Examples.FancyScrollViewExample01
{
    class Cell : FancyCell<ItemData>
    {
        [SerializeField] Animator animator = default;
        [SerializeField] Text message = default;
        [SerializeField] int ID = default;
        [SerializeField] Sprite Img = default;
        [SerializeField] bool gameObjectIsClassContent = default;
        [SerializeField] bool gameObjectIsBookContent = default;
        [SerializeField] TextMeshProUGUI completionPercentage = default;
        [SerializeField] TextMeshProUGUI ClassTextString = default;
        [SerializeField] Image numberImage1=default;
        [SerializeField] Image numberImage2=default;
        [SerializeField] Image FG_Image=default;
        [SerializeField] GameObject ClassText = default;
        [SerializeField] Button cardBtn=default;
        [SerializeField] List<Color> colorsOfImageNumbers;
        [SerializeField] ParticleSystem cardGlow;
        [SerializeField] ParticleSystem spark;
        [SerializeField] GameObject Lock;
        [SerializeField] GameObject outLine;
        [SerializeField] Image cardMainImage;
        [SerializeField] Shadow shadow;





        UnityAction btnAction;
        static class AnimatorHash
        {
            public static readonly int Scroll = Animator.StringToHash("scroll");
        }

        void Start()
        {

        }

        public override void UpdateContent(ItemData itemData)
        {

            message.text = itemData.Message;
            ID = itemData.ID;
            gameObjectIsClassContent = itemData.GameObjectIsClassContent;
            gameObjectIsBookContent = itemData.GameObjectIsBookContent;
            completionPercentage.text = itemData.CompletionPercentage.ToString("F0")+"<size=20>%</size>";

            cardBtn.interactable = itemData.CardIsIntractable;

            if (itemData.CardIsIntractable == true)
            {
                if (Lock != null)
                {
                    Lock.SetActive(false);
                    Lock.transform.parent.gameObject.SetActive(false);
                }
                if (cardGlow != null)
                {
                   // cardGlow.GetComponent<Renderer>().enabled=true;
                }
                if (spark != null)
                { 
                   // spark.GetComponent<Renderer>().enabled = true;
                }
            
            }       
            if (itemData.CardIsIntractable == false)
            {
                if (Lock != null)
                {
                    Lock.transform.parent.gameObject.SetActive(true);
                    Lock.SetActive(true);
                }

                if (cardGlow != null)
                {
                   // cardGlow.GetComponent<Renderer>().enabled=false;
                }
                if (spark != null)
                { 
                  //  spark.GetComponent<Renderer>().enabled = false;
                }
            
            }
         
            //Img = itemData.Img;
            // UnityEvent evnt = new UnityEvent(i);
            this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = itemData.Img;
            if (itemData.GameObjectIsClassContent)
            {
                ClassText.SetActive(true);
                ClassTextString.text = itemData.ClassName;


                shadow.enabled = false;
                message.color = new Color(message.color.r, message.color.g, message.color.b, 0);
                if (itemData.NumImage1 != null)
                {
                    numberImage1.color = colorsOfImageNumbers[itemData.ID % colorsOfImageNumbers.Count];
                    numberImage1.gameObject.SetActive(false);// make it true to enable
                    numberImage1.sprite = itemData.NumImage1;
                }
                else
                {
                    numberImage1.gameObject.SetActive(false);
                    print("hi");
                }
                if (itemData.NumImage2 != null)
                {
                    numberImage2.color = colorsOfImageNumbers[itemData.ID % colorsOfImageNumbers.Count];
                    numberImage2.gameObject.SetActive(false);// make it true to enable
                    numberImage2.sprite = itemData.NumImage2;
                }
                else
                {
                    numberImage2.gameObject.SetActive(false);
                }

                if (itemData.FG_Image != null)
                {
                    FG_Image.gameObject.SetActive(true);
                    FG_Image.sprite = itemData.FG_Image;
                    FG_Image.preserveAspect = true;
                }

                

            }

            itemData.onClickedClass = () => {  ClassManager.currentClassName = message.text; };
            itemData.onClickedActivity = () => {  ClassManager.currentActivityName = message.text;  ClassManager.currentActivityIndex = ID; print("ID" + ID); };
            itemData.onClickedBook = () => { BookManager.currentBookName = message.text;  };
            if (itemData.BtnEvents != null)
            {
                if (btnAction != null)
                {
                    this.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.RemoveListener(btnAction);
                }
                btnAction = itemData.BtnEvents;
                this.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(itemData.BtnEvents);
                message.text = itemData.Message;
                completionPercentage.text =  itemData.CompletionPercentage.ToString("F0")+ "<size=20>%</size>";




            }
            this.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { itemData.BtnEvent.Invoke(); });

            if (gameObjectIsBookContent)
            {
                string[] textColor = new string[] { "#00C7FFFF", "#6318F5FF", "#9F0000FF" };

                Color coloR;
                ColorUtility.TryParseHtmlString(textColor[itemData.ID % 3], out coloR);
                this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = Color.white;

               // print("name" + this.gameObject.transform.GetChild(0).gameObject.name + "   " + itemData.ID + " " + textColor[itemData.ID % 3]+ this.gameObject.transform.GetChild(0).GetComponent<Image>().color);

            }



            if (gameObjectIsBookContent)
                this.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => itemData.onClickedBook());
            else if (gameObjectIsClassContent)
            {
                this.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => itemData.onClickedClass());
                outLine.SetActive(false);
                cardMainImage.color = new Color(1, 1, 1, 1);
            }
            else
            {
                this.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => itemData.onClickedActivity());
                outLine.SetActive(false);
                cardMainImage.color = new Color(1, 1, 1, 1f);


            }

            //this.gameObject.transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(itemData.onClicked);

            completionPercentage.text = itemData.CompletionPercentage.ToString("F0") + "<size=20>%</size>";


        }

        public override void UpdatePosition(float position)
        {
            currentPosition = position;

            if (animator.isActiveAndEnabled)
            {
                animator.Play(AnimatorHash.Scroll, -1, position);
            }

            animator.speed = 0;
        }

        // GameObject が非アクティブになると Animator がリセットされてしまうため
        // 現在位置を保持しておいて OnEnable のタイミングで現在位置を再設定します
        float currentPosition = 0;

        void OnEnable() => UpdatePosition(currentPosition);
    }
}
