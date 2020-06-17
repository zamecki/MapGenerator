using UnityEngine;
using UnityEditor;
using LMZMapGenerator.Core;

namespace LMZMapGenerator.Utilities
{
    public class MapGeneratorBaseObject
    {
        [MenuItem("GameObject/2D Object/Map Generator")]
        static void Create()
        {
            GameObject go = new GameObject("MapGenerator");
            go.AddComponent<MapGenerator>();
            go.AddComponent<ThreadDataRequest>();
            go.AddComponent<MapPreview>();
            go.transform.position = new Vector3(0, 0, 0);
        }
    }
}