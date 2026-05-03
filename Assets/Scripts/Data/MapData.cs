using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    /// <summary>
    /// Cấu trúc dữ liệu để lưu trữ thông tin của một vật thể trên bản đồ.
    /// </summary>
    [System.Serializable]
    public struct MapObjectEntry
    {
        public string objectName;    // Tên hoặc ID của prefab/vật thể
        public Vector3 position;     // Tọa độ vị trí (x, y, z)
        public Vector3 rotation;     // Tọa độ xoay (Euler angles)
        public Vector3 scale;        // Kích thước vật thể
    }

    /// <summary>
    /// ScriptableObject dùng để lưu trữ danh sách các toạ độ của vật thể.
    /// Bạn có thể tạo file này trong Project window bằng cách: Chuột phải -> Create -> Data -> Map Data
    /// </summary>
    [CreateAssetMenu(fileName = "NewMapData", menuName = "Data/Map Data")]
    public class MapData : ScriptableObject
    {
        [Header("Danh sách toạ độ vật thể")]
        public List<MapObjectEntry> objectCoordinates = new List<MapObjectEntry>();

        /// <summary>
        /// Xóa toàn bộ dữ liệu hiện có.
        /// </summary>
        public void ClearData()
        {
            objectCoordinates.Clear();
        }

        /// <summary>
        /// Thêm một vật thể mới vào danh sách.
        /// </summary>
        public void AddObject(string name, Vector3 pos, Vector3 rot, Vector3 scl)
        {
            objectCoordinates.Add(new MapObjectEntry
            {
                objectName = name,
                position = pos,
                rotation = rot,
                scale = scl
            });
        }
    }
}
