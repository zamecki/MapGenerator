using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LMZMapGenerator.Core
{
    public class MapGenerator : MonoBehaviour
    {
        public MapGeneratorSettings settings;
        public Tilemap tilemap;
        public MapData mapData;

        [HideInInspector]
        public bool terraingLoaded = false;

        void Start()
        {
            TerrainChunk chunk = new TerrainChunk(Vector2.zero, settings.mapSize, settings, tilemap);
            chunk.onTerrainChunkLoaded += OnTerrainChunkLoaded;
            chunk.Load();
        }

        void OnTerrainChunkLoaded(MapData data)
        {
            terraingLoaded = true;
            this.mapData = data;
        }
        public TileHeight GetTileHeightFromCoordinates(Vector3Int coordinates)
        {
            int index = mapData.positionedTiles.positions.Select((coord, i) => new
            {
                Coordinates = coord,
                Position = i
            }).Where(x => x.Coordinates == coordinates).First().Position;
            return mapData.positionedTiles.tileHeights[index];
        }
    }

}