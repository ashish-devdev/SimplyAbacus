using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Michsky.UI.ModernUIPack
{
    public class RangeSlider : MonoBehaviour
    {
        [Header("SETTINGS")]
        [Range(0,2)] public int DecimalPlaces = 0;
        public float minValue = 0;
        public float maxValue = 1;
        public bool useWholeNumbers = false;
        public bool showLabels = true;

        [Header("MIN SLIDER")]
        public RangeMinSlider minSlider;
        public TextMeshProUGUI minSliderLabel;

        [Header("MAX SLIDER")]
        public RangeMaxSlider maxSlider;
        public TextMeshProUGUI maxSliderLabel;
        public Image targetGraphic;

        // Properties
        public float CurrentLowerValue
        {
            get { return minSlider.value; }
        }
        public float CurrentUpperValue
        {
            get { return maxSlider.realValue; }
        }

        public RectTransform FillRect { get; set; }
        public RectTransform HighHandleRect { get; set; }
        public RectTransform LowHandleRect { get; set; }
        public object LowValue { get; set; }
        public object MinValue { get; set; }
        public object MaxValue { get; set; }
        public object HighValue { get; set; }

        void Awake()
        {
            // Define if we use indicators
            if (showLabels)
            {
                minSlider.label = minSliderLabel;
                minSlider.numberFormat = "n" + DecimalPlaces;
                maxSlider.label = maxSliderLabel;
                maxSlider.numberFormat = "n" + DecimalPlaces;
            }

            else
            {
                minSliderLabel.gameObject.SetActive(false);
                maxSliderLabel.gameObject.SetActive(false);
            }

            // Adjust Max/Min values for both sliders
            minSlider.minValue = minValue;
            minSlider.maxValue = maxValue;
            minSlider.wholeNumbers = useWholeNumbers;

            maxSlider.minValue = minValue;
            maxSlider.maxValue = maxValue;
            maxSlider.wholeNumbers = useWholeNumbers;
        }
    }
}