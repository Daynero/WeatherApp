using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Weather
{
    public class WeatherManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image icon;
        [SerializeField] private RawImage rawImage;
        public float latitude;
        public float longitude;
        public string apiKey = "b4587d73b4a52466ea94245b87860a07";
        public string apiUrl = "https://api.openweathermap.org/data/2.5/weather";

        private void Start()
        {
            ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
            GetWeatherData(latitude, longitude);
        }

        public void GetWeatherData(float lat, float lon)
        {
            string url = $"{apiUrl}?lat={lat}&lon={lon}&appid={apiKey}";

            StartCoroutine(FetchWeatherData(url));
        }

        private IEnumerator FetchWeatherData(string url)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    WeatherData.WeatherData weatherData = JsonUtility.FromJson<WeatherData.WeatherData>(webRequest.downloadHandler.text);
                    UpdateWeatherCard(weatherData);
                }
                else
                {
                    Debug.LogError($"Error: {webRequest.error}");
                }
            }
        }

        private void UpdateWeatherCard(WeatherData.WeatherData weatherData)
        {
            nameText.text = weatherData.name;
            string iconId = weatherData.weather[0].icon;
            string iconUrl = $"https://openweathermap.org/img/w/{iconId}.png";

            Debug.Log(iconUrl);
            // StartCoroutine(LoadIcon(iconUrl));
            // StartCoroutine(GetTexture(iconUrl));
            // StartCoroutine(LoadIcon(url: iconUrl));
            // LoadImageAsync(iconUrl);
            StartCoroutine(GetWeatherIcon(iconUrl, texture => rawImage.texture = texture, _ => { }));
        }
        
        IEnumerator LoadIcon(string url)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();
        
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                rawImage.texture = myTexture;
            }
        } 
        
        IEnumerator GetTexture(string url) {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();
        
            Texture myTexture = DownloadHandlerTexture.GetContent(www);
            rawImage.texture = myTexture;
        }
        
        private IEnumerator GetWeatherIcon(string url, Action<Texture2D> onSuccess, Action<string> onFailure)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
                onFailure?.Invoke(request.error);
                yield break;
            }
    
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            onSuccess?.Invoke(texture);
        }
        
        // private async void LoadImageAsync(string url)
        // {
        //     using (HttpClient httpClient = new HttpClient())
        //     {
        //         try
        //         {
        //             byte[] imageData = await httpClient.GetByteArrayAsync(url);
        //             Texture2D texture = new Texture2D(2, 2);
        //             bool isLoaded = texture.LoadImage(imageData);
        //     
        //             if (isLoaded)
        //             {
        //                 icon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        //                 icon.preserveAspect = true; // Збереження пропорцій зображення
        //             }
        //             else
        //             {
        //                 Debug.LogError("Failed to load texture");
        //             }
        //         }
        //         catch (HttpRequestException e)
        //         {
        //             Debug.LogError($"Error: {e.Message}");
        //         }
        //     }
        // }
        
        // IEnumerator LoadTextureFromWeb(string url)
        // {
        //     UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        //     yield return www.SendWebRequest();
        //
        //     if (www.isNetworkError || www.isHttpError)
        //     {
        //         Debug.LogError("Error: " + www.error);
        //     }
        //     else
        //     {
        //         Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
        //         icon.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
        //         icon.SetNativeSize();
        //     }
        // }
    }
}
