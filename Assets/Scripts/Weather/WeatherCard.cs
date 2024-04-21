using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Weather
{
    public class WeatherCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cityName;
        [SerializeField] private TextMeshProUGUI temperature;
        [SerializeField] private Image icon;
        [SerializeField] private Button selfButton;

        public event Action OnButtonClick;

        private void Awake()
        {
            selfButton.onClick.AddListener(() => OnButtonClick?.Invoke());
        }

        public void UpdateView(WeatherViewData weatherViewData)
        {
            cityName.text = weatherViewData.Name;
            temperature.text = weatherViewData.Temp + "\u00b0";
            icon.sprite = weatherViewData.Icon;
            icon.preserveAspect = true;
        }
    }
}