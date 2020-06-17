using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LMZMapGenerator.Core
{
    public class MapPreview : MonoBehaviour
    {
        public MapGeneratorSettings settings;
        public bool autoUpdate;
        public Tilemap tilemap;
        public Renderer textureRender;

        void Start()
        {
            tilemap.gameObject.SetActive(false);
        }
        public void DrawMapInEditor()
        {
            MapData mapData = HeightMapGenerator.GenerateHeightMap(settings, Vector2.zero);
            tilemap.SetTiles(mapData.positionedTiles.positions, mapData.positionedTiles.tileHeights.Select(x => x.tileBase).ToArray());
            textureRender.sharedMaterial.mainTexture = TextureGenerator.TextureFromHeightMap(mapData);
        }
        void OnValuesUpdated()
        {
            if (!Application.isPlaying)
            {
                DrawMapInEditor();
            }
        }

        void ClearTile()
        {
            tilemap.ClearAllTiles();
        }

        void OnValidate()
        {

            if (settings != null)
            {
                settings.OnValuesUpdated -= OnValuesUpdated;
                settings.OnValuesUpdated += OnValuesUpdated;
                settings.OnClearTile -= ClearTile;
                settings.OnClearTile += ClearTile;
            }

        }
    }
}