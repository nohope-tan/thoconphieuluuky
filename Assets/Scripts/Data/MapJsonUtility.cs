using System.IO;
using UnityEngine;

namespace Game.Data
{
    public static class MapJsonUtility
    {
        private static string GetFilePath(string fileName)
        {
            return Path.Combine(Application.persistentDataPath, fileName + ".json");
        }

        public static void SaveToJson(MapData data, string fileName)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetFilePath(fileName), json);
        }

        public static void LoadFromJson(MapData data, string fileName)
        {
            string path = GetFilePath(fileName);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, data);
            }
        }
    }
}
