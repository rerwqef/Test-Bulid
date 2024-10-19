using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TS.DoubleSlider
{
    public class GameManager : MonoBehaviour
    {
        public Button Leftbtn;
        public Button Rightbtn;
        [SerializeField] private SingleSlider _sliderMininner;
        [SerializeField] private SingleSlider _sliderMaxinner;
        [SerializeField] private SingleSlider _sliderMinOuter;
        [SerializeField] private SingleSlider _sliderMaxouter;
        [SerializeField] private DoubleSlider _sliderInner;
        [SerializeField] private DoubleSlider _sliderOuter;

        // Start is called before the first frame update
        void Start()
        {
            Leftbtn.onClick.AddListener(HandClose);
            Rightbtn.onClick.AddListener(HandOpen);
            Setup();
        }

        void Setup()
        {
            _sliderMininner.Value = 0;
            _sliderMaxinner.Value = 100;
            _sliderMinOuter.Value = 25;
            _sliderMaxouter.Value = 75;
        }

        // Coroutine to smoothly change both slider values
        private IEnumerator SmoothChange(SingleSlider sliderMin, float targetMinValue, SingleSlider sliderMax, float targetMaxValue, float duration)
        {
            float startMinValue = sliderMin.Value;
            float startMaxValue = sliderMax.Value;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                sliderMin.Value = Mathf.Lerp(startMinValue, targetMinValue, (elapsedTime / duration));
                sliderMax.Value = Mathf.Lerp(startMaxValue, targetMaxValue, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }
            sliderMin.Value = targetMinValue; // Ensure the final value is set
            sliderMax.Value = targetMaxValue; // Ensure the final value is set
        }

        void HandOpen()
        {
            Debug.Log("Attempting to open hand");
            StartCoroutine(SmoothChange(_sliderMinOuter, 15, _sliderMaxouter, 90, 1f)); // Change both sliders over 1 second
             // Calculate Arom after opening hand
        }

        void HandClose()
        {
            Debug.Log("Attempting to close hand");
            StartCoroutine(SmoothChange(_sliderMininner, 15, _sliderMaxinner, 90, 1f)); // Change both sliders over 1 second
           // Calculate Arom after closing hand
        }

        // Method to calculate Arom
        void CalculateArom()
        {
            float handOpenedDistance = 15; // Get the max outer slider value
            float handClosedDistance = 15; // Get the max inner slider value

            float arom = (handOpenedDistance - handClosedDistance) / 2; // Calculate Arom
            Debug.Log($"Calculated Arom: {arom}"); // Output the result
        }
    }
}