using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
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
			go.transform.position = Vector3.zero;

			GameObject grid = new GameObject("Grid Preview");
			grid.transform.position = Vector3.zero;
			grid.AddComponent<Grid>();

			GameObject tilemap = new GameObject("TileMap Preview");
			tilemap.transform.position = Vector3.zero;
			tilemap.AddComponent<Tilemap>();
			tilemap.AddComponent<TilemapRenderer>();
			tilemap.transform.parent = grid.transform;
			
			GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
			quad.transform.position = new Vector3(0,0,-1);
			quad.transform.parent = tilemap.transform;
		}
    }
}