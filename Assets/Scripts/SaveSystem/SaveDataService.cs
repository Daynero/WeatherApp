using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public class SaveDataService : ISaveDataService
    {
        public bool Save<T>(T data, string fileName)
        {
            var directory = Application.persistentDataPath;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var path = Path.Combine(directory, fileName + ".json");
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
            return true;
        }

        public bool Load<T>(out T data, string fileName)
        {
            var directory = Application.persistentDataPath;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var path = Path.Combine(directory, fileName + ".json");

            if (!File.Exists(path))
            {
                data = default;
                return false;
            }

            string json = File.ReadAllText(path);
            if (json == "null" || string.IsNullOrEmpty(json))
            {
                data = default;
                return false;
            }

            data = JsonConvert.DeserializeObject<T>(json);
            return true;
        }
    }
}