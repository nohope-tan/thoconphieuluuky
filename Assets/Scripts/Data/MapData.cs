using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    public struct MapObjectEntry
    {
        public string objectName;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
    }

    [CreateAssetMenu(fileName = "NewMapData", menuName = "Data/Map Data")]
    public class MapData : ScriptableObject
    {
        public List<MapObjectEntry> objectCoordinates = new List<MapObjectEntry>();

        public void ClearData()
        {
            objectCoordinates.Clear();
        }

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
