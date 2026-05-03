using UnityEngine;
using System.Collections.Generic;

namespace Game.Data
{
    /// <summary>
    /// Công cụ hỗ trợ lưu toạ độ của các vật thể trong Scene vào file MapData.
    /// </summary>
    public class MapExporter : MonoBehaviour
    {
        [Header("Settings")]
        public MapData mapDataAsset;       // Kéo file MapData đã tạo vào đây
        public string targetTag = "MapObject"; // Chỉ lưu các vật thể có Tag này

        [ContextMenu("Save Scene Objects to MapData")]
        public void SaveToAsset()
        {
            if (mapDataAsset == null)
            {
                Debug.LogError("Chưa gán file MapData vào MapExporter!");
                return;
            }

            mapDataAsset.ClearData();
            GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag);

            foreach (GameObject obj in objects)
            {
                mapDataAsset.AddObject(
                    obj.name,
                    obj.transform.position,
                    obj.transform.eulerAngles,
                    obj.transform.localScale
                );
            }

            Debug.Log($"Đã lưu {objects.Length} vật thể vào {mapDataAsset.name}");
            
            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(mapDataAsset);
            UnityEditor.AssetDatabase.SaveAssets();
            #endif
        }
    }
}
