namespace Weather.WeatherData
{
    [System.Serializable]
    public class WeatherData
    {
        public Coord coord;
        public Weather[] weather;
        public Main main;
        public Wind wind;
        public Clouds clouds;
        public Sys sys;
        public string name;
    }

    [System.Serializable]
    public class Coord
    {
        public float lon;
        public float lat;
    }

    [System.Serializable]
    public class Weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;
    }

    [System.Serializable]
    public class Main
    {
        public float temp;
        public float feels_like;
        public float temp_min;
        public float temp_max;
        public int humidity;
    }

    [System.Serializable]
    public class Wind
    {
        public float speed;
        public float deg;
    }

    [System.Serializable]
    public class Clouds
    {
        public int all;
    }

    [System.Serializable]
    public class Sys
    {
        public string country;
    }

}