using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Weather
{
    public class WeatherAPIController
    {
        public string apiKey = "b4587d73b4a52466ea94245b87860a07";
        public string apiUrl = "https://api.openweathermap.org/data/2.5/weather";
        
        public IEnumerator GetWeatherData(float lat, float lon, Action<WeatherData.WeatherData> onWeatherDataReceived)
        {
            string url = $"{apiUrl}?lat={lat}&lon={lon}&appid={apiKey}";

            using UnityWebRequest webRequest = UnityWebRequest.Get(url);
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                WeatherData.WeatherData weatherData = JsonUtility.FromJson<WeatherData.WeatherData>(webRequest.downloadHandler.text);
                onWeatherDataReceived?.Invoke(weatherData);
            }
            else
            {
                Debug.LogError($"Error: {webRequest.error}");
                onWeatherDataReceived?.Invoke(null);
            }
        }
    }
}