using System.Collections.Generic;
using UnityEngine;

namespace Weather.WeatherData
{
    [CreateAssetMenu(fileName = "WeatherIconsDatabase", menuName = "SO/WeatherIconsDatabase", order = 0)]
    public class WeatherIconsDatabase : ScriptableObject
    {
        [SerializeField] private List<WeatherIcon> weatherIcons;

        private Dictionary<string, Sprite> _iconsDictionary;
        
        public Sprite this[string id]
        {
            get
            {
                if (_iconsDictionary == null)
                    Init();
                
                return _iconsDictionary?.GetValueOrDefault(id);
            }
        }
        
        private void Init()
        {
            _iconsDictionary = new();
            foreach (var weatherIcon in weatherIcons)
            {
                _iconsDictionary[weatherIcon.ID] =weatherIcon.Sprite;
            }
        }
    }
}