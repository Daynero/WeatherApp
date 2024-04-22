using System;
using System.Collections.Generic;
using UnityEngine;
using Weather.WeatherData;

namespace Weather
{
    public class WeatherController : MonoBehaviour
    {
        [SerializeField] private WeatherCard weatherCardPrefab;
        [SerializeField] private List<Transform> cardParents;
        [SerializeField] private WeatherIconsDatabase weatherIconsDatabase;
        [SerializeField] private int numberOfCards = 9;

        private readonly List<WeatherCard> _weatherCardsPool = new();
        private WeatherAPIController _weatherAPIController;

        private void Awake()
        {
            _weatherAPIController = new();
        }

        public void CreateOrActivateCards()
        {
            if (_weatherCardsPool.Count == 0)
            {
                for (int i = 0; i < numberOfCards; i++)
                {
                    Transform cardParent = i switch
                    {
                        >= 0 and <= 2 => cardParents[0],
                        > 2 and < 6 => cardParents[1],
                        _ => cardParents[2]
                    };
                    
                    WeatherCard card = Instantiate(weatherCardPrefab, cardParent);
                    _weatherCardsPool.Add(card);
                    InitCard(card);
                }
            }
            else
            {
                foreach (var card in _weatherCardsPool)
                {
                    card.gameObject.SetActive(true);
                }
            }

            SetInfoForActiveCards();
        }

        private void SetInfoForActiveCards()
        {
            for (int i = 0; i < _weatherCardsPool.Count; i++)
            {
                var card = _weatherCardsPool[i];

                if (card.gameObject.activeSelf && i < CityCoordinates.Cities.GetLength(0))
                {
                    float lat = CityCoordinates.Cities[i, 0];
                    float lon = CityCoordinates.Cities[i, 1];

                    StartCoroutine(_weatherAPIController.GetWeatherData(lat, lon, (weatherData) =>
                    {
                        if (weatherData != null)
                        {
                            WeatherViewData weatherViewData = new WeatherViewData()
                            {
                                Name = weatherData.name,
                                Temp = ToCelsius(weatherData.main.temp),
                                Icon = weatherIconsDatabase[weatherData.weather[0].icon]
                            };
                            
                            card.UpdateView(weatherViewData);
                        }
                    }));
                }
            }
        }

        private string ToCelsius(float temp)
        {
            float celsiusTemp = temp - 273.15f;
            int roundedTemp = Mathf.RoundToInt(celsiusTemp); 
            return roundedTemp.ToString(); 
        }

        private void InitCard(WeatherCard card)
        {
            card.OnButtonClick += () => DeactivateCard(card.gameObject);
        }

        public void ResetCards()
        {
            foreach (var card in _weatherCardsPool)
            {
                card.gameObject.SetActive(true);
            }

            SetInfoForActiveCards();
        }
        
        private void DeactivateCard(GameObject card)
        {
            card.SetActive(false);
        }
    }
}