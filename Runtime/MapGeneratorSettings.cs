using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LMZMapGenerator.Core
{
    [CreateAssetMenu()]
    public class MapGeneratorSettings : ScriptableObject
    {
        public event System.Action OnValuesUpdated;
        public event System.Action OnClearTile;

        [Header("Noise Settings")]
        public int seed;
        public int noiseScale = 32;
        [Range(1, 10)]
        public int octaves = 5;
        [Range(0, 1)]
        public float persistance = 0.5f;
        public float lacunarity = 2;
        public Vector2 offset;
        public bool showFalloffOnly = false;
        public float fallOffA = 3f;
        public float fallOffB = 2.2f;

        [Header("Map Settings")]
        public float mapScale = 1f;
        public int mapSize = 6;
        public int mapChunkSize = 64;
        public float maxViewDistance = 128;
        public float viewerMoveThresholdForCrunkUpdate = 25;

        [Header("Tile Settings")]
        public Gradient gradient = new Gradient();
        public TileHeight[] tileHeights;

        void OnValidate()
        {

            if (tileHeights == null)
                return;

            for (int i = 0; i < gradient.colorKeys.Length; i++)
            {
                var b = tileHeights.FirstOrDefault(x => x.colorCode == gradient.colorKeys[i].color);
                if (b != null)
                {
                    if (i == 0)
                    {
                        b.minHeight = 0;
                        b.maxHeight = gradient.colorKeys[i].time - Mathf.Epsilon;
                    }
                    else
                    {
                        b.minHeight = gradient.colorKeys[i - 1].time;
                        b.maxHeight = gradient.colorKeys[i].time - Mathf.Epsilon;
                    }
                }
            }
        }

        public TileHeight GetTileHeight(float height)
        {
            foreach (var tileHeight in tileHeights)
            {
                if (height < 0)
                    return tileHeights.OrderBy(x => x.minHeight).First();

                if (height >= 1)
                    return tileHeights.OrderBy(x => x.minHeight).Last();

                if (height < tileHeight.maxHeight && height >= tileHeight.minHeight)
                    return tileHeight;
            }
            return tileHeights.OrderBy(x => x.minHeight).First();
        }

        public void NotifyOfUpdatedValues()
        {
            OnValuesUpdated?.Invoke();
        }

        public void NotifyOfClear()
        {
            OnClearTile?.Invoke();
        }
    }
    [System.Serializable]
    public class TileHeight
    {
        public string name;

        [HideInInspector]
        public float maxHeight;
        [HideInInspector]
        public float minHeight;
        public Color colorCode;
        public TileBase tileBase;
    }
    public class PositionedTile
    {

        public Vector3Int[] positions;
        public TileHeight[] tileHeights;

        public PositionedTile(int size)
        {
            this.positions = new Vector3Int[size];
            this.tileHeights = new TileHeight[size];
        }

        internal void Add(int index, int x, int y, TileHeight tileHeight)
        {
            positions[index] = new Vector3Int(x, y, 0);
            tileHeights[index] = tileHeight;
        }
    }
}