using UnityEngine;
using System.Collections.Generic;

namespace Game.Data
{
    public class MapExporter : MonoBehaviour
    {
        [Header("Settings")]
        public MapData mapDataAsset;
        public string targetTag = "MapObject";

        [ContextMenu("Save Scene Objects to MapData")]
        public void SaveToAsset()
        {
            if (mapDataAsset == null) return;

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

            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(mapDataAsset);
            UnityEditor.AssetDatabase.SaveAssets();
            #endif
        }
    }
}
