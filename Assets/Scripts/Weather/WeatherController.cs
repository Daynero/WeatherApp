using System;
using System.Collections.Generic;
using SaveSystem;
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

        private SaveDataService _saveDataService;

        private List<WeatherCard> _weatherCards = new();

        private Dictionary<int, List<(float, float)>> _weatherCardsDictionary;

        private WeatherAPIController _weatherAPIController;

        private void Awake()
        {
            _weatherAPIController = new();
            _saveDataService = new();
        }

        public void CreateCards()
        {
            InitDictionary();
            LoadData();
        }

        private void InitDictionary()
        {
            _weatherCardsDictionary = new()
            {
                {0, new List<(float, float)>()},
                {1, new List<(float, float)>()},
                {2, new List<(float, float)>()},
            };
        }

        private void LoadData()
        {
            if (_saveDataService.Load(out Dictionary<int, List<(float, float)>> savedDictionary,
                    FileNameConstants.CardsData))
            {
                CreateFromSaves(savedDictionary);
            }
            else
            {
                CreateNewCards();
            }
        }

        private void CreateFromSaves(Dictionary<int, List<(float, float)>> savedDictionary)
        {
            _weatherCardsDictionary = savedDictionary;

            foreach (var key in _weatherCardsDictionary.Keys)
            {
                List<(float, float)> coordinatesList = _weatherCardsDictionary[key];

                foreach (var (lat, lon) in coordinatesList)
                {
                    WeatherCard card;

                    switch (key)
                    {
                        case 0:
                            card = Instantiate(weatherCardPrefab, cardParents[0]);
                            // _weatherCards.Add(card);
                            break;
                        case 1:
                            card = Instantiate(weatherCardPrefab, cardParents[1]);
                            // _weatherCards.Add(card);
                            break;
                        case 2:
                            card = Instantiate(weatherCardPrefab, cardParents[2]);
                            // _weatherCards.Add(card);
                            break;
                        default:
                            continue;
                    }

                    InitCard(card);
                    SetWeatherInfo(card, lat, lon);
                }
            }
        }

        private void SetWeatherInfo(WeatherCard card, float lat, float lon)
        {
            StartCoroutine(_weatherAPIController.GetWeatherData(lat, lon, (weatherData) =>
            {
                if (weatherData != null)
                {
                    WeatherViewData weatherViewData = new WeatherViewData()
                    {
                        Name = weatherData.name,
                        Temp = ToCelsius(weatherData.main.temp),
                        Icon = weatherIconsDatabase[weatherData.weather[0].icon],
                        Lat = lat,
                        Lon = lon
                    };

                    card.UpdateView(weatherViewData);
                }
            }));
        }

        public void CreateNewCards()
        {
            RemoveAllCards();

            for (int i = 0; i < numberOfCards; i++)
            {
                WeatherCard card;

                switch (i)
                {
                    case >= 0 and < 3:
                        card = Instantiate(weatherCardPrefab, cardParents[0]);
                        _weatherCards.Add(card);
                        break;
                    case > 2 and < 6:
                        card = Instantiate(weatherCardPrefab, cardParents[1]);
                        _weatherCards.Add(card);
                        break;
                    default:
                        card = Instantiate(weatherCardPrefab, cardParents[2]);
                        _weatherCards.Add(card);
                        break;
                }

                InitCard(card);
            }

            SetInfo();
        }

        private void RemoveAllCards()
        {
            foreach (var card in _weatherCards)
            {
                Destroy(card.gameObject);
            }

            _weatherCards.Clear();
            _weatherCardsDictionary.Clear();
            InitDictionary();
        }

        private void SetInfo()
        {
            for (int i = 0; i < _weatherCards.Count; i++)
            {
                var card = _weatherCards[i];
                float lat = CityCoordinates.Cities[i, 0];
                float lon = CityCoordinates.Cities[i, 1];

                var index = i;
                StartCoroutine(_weatherAPIController.GetWeatherData(lat, lon, (weatherData) =>
                {
                    if (weatherData != null)
                    {
                        WeatherViewData weatherViewData = new WeatherViewData()
                        {
                            Name = weatherData.name,
                            Temp = ToCelsius(weatherData.main.temp),
                            Icon = weatherIconsDatabase[weatherData.weather[0].icon],
                            Lat = lat,
                            Lon = lon
                        };

                        switch (index)
                        {
                            case >= 0 and < 3:
                                _weatherCardsDictionary[0].Add((lat, lon));
                                break;
                            case > 2 and < 6:
                                _weatherCardsDictionary[1].Add((lat, lon));
                                break;
                            default:
                                _weatherCardsDictionary[2].Add((lat, lon));
                                break;
                        }

                        card.UpdateView(weatherViewData);
                        SaveCards();
                    }
                }));
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
            card.OnButtonClick += () => DestroyCard(card);
        }

        private void DestroyCard(WeatherCard card)
        {
            Destroy(card.gameObject);
            RemoveFromDictionary(card.LatAndLon);
            SaveCards();
        }

        private void SaveCards()
        {
            _saveDataService.Save(_weatherCardsDictionary, FileNameConstants.CardsData);
        }

        private void RemoveFromDictionary((float, float) cardLatAndLon)
        {
            foreach (var key in _weatherCardsDictionary.Keys)
            {
                var coordinatesList = _weatherCardsDictionary[key];

                var indexToRemove = coordinatesList.FindIndex(coord =>
                    coord.Item1 == cardLatAndLon.Item1 && coord.Item2 == cardLatAndLon.Item2);

                if (indexToRemove != -1)
                {
                    coordinatesList.RemoveAt(indexToRemove);
                    
                    var cardToRemove = _weatherCards.Find(card => 
                        card.LatAndLon.Item1 == cardLatAndLon.Item1 && card.LatAndLon.Item2 == cardLatAndLon.Item2);
            
                    if (cardToRemove != null)
                    {
                        _weatherCards.Remove(cardToRemove);
                    }
                    
                    break;
                }
            }
        }
    }
}