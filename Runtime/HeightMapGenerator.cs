using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LMZMapGenerator.Core
{
    public static class HeightMapGenerator
    {
        public static MapData GenerateHeightMap(MapGeneratorSettings settings, Vector2 sampleCenter)
        {
            var size = settings.mapChunkSize * settings.mapSize;
            PositionedTile positionedTiles = new PositionedTile(size * size);
            var falloffResult = FalloffGenerator.GenerateFalloff(size, settings);
            var falloff = falloffResult.Item1;
            var minFalloff = falloffResult.Item2;
            var maxFalloff = falloffResult.Item3;
            var lerpNoise = new float[size, size];
            if (settings.showFalloffOnly)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        var index = y * size + x;

                        lerpNoise[x, y] = Mathf.InverseLerp(maxFalloff, minFalloff, falloff[x, y]);
                        var roundX = Mathf.RoundToInt(sampleCenter.x) + x;
                        var roundY = Mathf.RoundToInt(sampleCenter.y) + y;
                        var tileBase = settings.GetTileHeight(lerpNoise[x, y]);
                        positionedTiles.Add(index, roundX, roundY, tileBase);
                    }
                }
                return new MapData(falloff, lerpNoise, positionedTiles);
            }

            var noiseResult = Noise.GenerateNoise(size, settings, sampleCenter);
            var rawNoise = noiseResult.Item1;
            var min = noiseResult.Item2;
            var max = noiseResult.Item3;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    lerpNoise[x, y] = Mathf.InverseLerp(min, max, rawNoise[x, y]) - falloff[x, y];
                    var tileBase = settings.GetTileHeight(lerpNoise[x, y]);
                    var index = y * size + x;
                    var roundX = Mathf.RoundToInt(sampleCenter.x) + x;
                    var roundY = Mathf.RoundToInt(sampleCenter.y) + y;
                    positionedTiles.Add(index, roundX, roundY, tileBase);
                }
            }
            return new MapData(rawNoise, lerpNoise, positionedTiles);
        }
    }



    public struct MapData
    {
        public readonly float[,] rawNoise;
        public readonly float[,] lerpNoise;
        public readonly PositionedTile positionedTiles;
        public MapData(float[,] rawNoise, float[,] lerpNoise, PositionedTile positionedTiles)
        {
            this.rawNoise = rawNoise;
            this.lerpNoise = lerpNoise;
            this.positionedTiles = positionedTiles;
        }
    }
}