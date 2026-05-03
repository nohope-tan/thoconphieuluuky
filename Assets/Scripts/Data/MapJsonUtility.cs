using System.IO;
using UnityEngine;

namespace Game.Data
{
    /// <summary>
    /// Công cụ hỗ trợ lưu và đọc toạ độ vật thể dưới dạng file JSON.
    /// </summary>
    public static class MapJsonUtility
    {
        private static string GetFilePath(string fileName)
        {
            return Path.Combine(Application.persistentDataPath, fileName + ".json");
        }

        /// <summary>
        /// Lưu dữ liệu MapData ra file JSON trong thư mục PersistentDataPath.
        /// </summary>
        public static void SaveToJson(MapData data, string fileName)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetFilePath(fileName), json);
            Debug.Log("Đã lưu map vào: " + GetFilePath(fileName));
        }

        /// <summary>
        /// Đọc dữ liệu từ file JSON vào MapData.
        /// </summary>
        public static void LoadFromJson(MapData data, string fileName)
        {
            string path = GetFilePath(fileName);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, data);
                Debug.Log("Đã tải map từ: " + path);
            }
            else
            {
                Debug.LogWarning("Không tìm thấy file: " + path);
            }
        }
    }
}
