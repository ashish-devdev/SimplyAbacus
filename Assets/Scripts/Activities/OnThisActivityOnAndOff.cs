using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OnThisActivityOnAndOff : MonoBehaviour
{

    public List<AudioClip> musics;
    public Image ColorImage;
    public GameObject ParentCell;
    public GameObject cell;
    public UnityEvent invokeOnEnable;
    public UnityEvent invokeOnDisable;
    public UnityEvent invokeAfterDelayOnDisable;
   public  bool canPlaySound = true;
    float timer;
    private void OnEnable()
    {
        
        invokeOnEnable.Invoke();
    }
    public void canplaySoundTrue()
    {
        Invoke("canplaySoundTrueWithDelay", 0.01f);
        canPlaySound = false;
    }

    public void canplaySoundTrueWithDelay()
    { 
        canPlaySound = true;
    
    }

    private void Update()
    {
        /* if (cell && cell.activeInHierarchy&&!canPlaySound)
         {
             timer += Time.deltaTime;
             if (timer > 0.00001f)
             {

                 timer = 0;
                 canPlaySound = true;
             }
         }*/
    }

    public void selectMusicAndPlay()
    {

        int r = Random.Range(0, musics.Count);
        SoundManager.Instance.PlayMusic(musics[r]);

    }


    public void DisableTheImage()
    {
        ColorImage.enabled = false;
    }
    public void EnableImageWithDelay()
    {

        Invoke("EnableImage", 0.07f);
    }

    public void EnableImage()
    {
        ColorImage.enabled = true;
    }



    private void OnDisable()
    {
        if (cell && !cell.activeInHierarchy)
        {
            canPlaySound = false;

        }
        invokeOnDisable.Invoke();
        Invoke("InvokeOnDisableAtfterSomeDelay", 0.001f);
    }
    public void ChangeColor()
    {
        this.gameObject.GetComponent<Image>().color = Color.red;

    }
    public void ChangeColorWhite()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;

    }
    public void ChangerectTransformWidth()
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, this.gameObject.GetComponent<RectTransform>().sizeDelta.y);

    }
    public void ChangePositionOfHighlightPalletAtTop()
    {

        this.gameObject.transform.GetChild(this.gameObject.transform.childCount - 1).transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x, this.gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y);
        //highlight.BeginAllTransitions();

    }

    public void ChangeOverAllLightIntensityforColoring()
    {
        RenderSettings.ambientIntensity = 1.8f;

    }
    public void ResetOverAllLightIntensity()
    {
        RenderSettings.ambientIntensity = 1f;

    }

    public void ResetFillAmount()
    {
        this.GetComponent<Image>().fillAmount = 0;
    }

    public void InvokeOnDisableAtfterSomeDelay()
    {
        invokeAfterDelayOnDisable.Invoke();
    }
    public void enablePlaySound()
    {


    }
    public void delayedInvoke()
    {
        canPlaySound = true;
    }

    public void onCellCenter(AudioClip sound)
    {
        if (canPlaySound)
        {
            SoundManager.Instance.Play(sound);
        }

    }
    
    public void ChangeImageColorToWhite(Image img)
    {
        img.color = Color.white;
    }
}
