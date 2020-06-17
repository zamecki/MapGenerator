using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LMZMapGenerator.Core
{
    public class TerrainChunk
    {
        public event System.Action<MapData> onTerrainChunkLoaded;
        Tilemap tilemap;
        MapData mapData;
        MapGeneratorSettings settings;
        Vector2 position;
        public TerrainChunk(Vector2 coord, int size, MapGeneratorSettings settings, Tilemap tilemap)
        {
            this.settings = settings;
            this.tilemap = tilemap;
            position = coord * size;
        }
        public void Load()
        {
            ThreadDataRequest.RequestData(() => HeightMapGenerator.GenerateHeightMap(settings, position * settings.mapScale), OnMapDataReceived);
        }
        void OnMapDataReceived(object mapDataObject)
        {
            this.mapData = (MapData)mapDataObject;
            var tiles = this.mapData.positionedTiles.tileHeights.Select(x => x.tileBase).ToArray();
            tilemap.SetTiles(this.mapData.positionedTiles.positions, tiles);
            onTerrainChunkLoaded?.Invoke(mapData);
        }
    }
}